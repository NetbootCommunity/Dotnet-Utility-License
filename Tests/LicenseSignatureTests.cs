using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using NUnit.Framework;

namespace Netboot.Utility.License.Test
{
    [TestFixture]
    public class LicenseSignatureTests
    {
        private string passPhrase;
        private string privateKey;
        private string publicKey;

        [SetUp]
        public void Init()
        {
            passPhrase = Guid.NewGuid().ToString();
            var keyGenerator = Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            publicKey = keyPair.ToPublicKeyString();
        }

        private static DateTime ConvertToRfc1123(DateTime dateTime)
        {
            return DateTime.ParseExact(
                dateTime.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture)
                , "r", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }

        [Test]
        public void Can_Generate_And_Validate_Signature_With_Empty_License()
        {
            var license = License.New()
                                 .CreateAndSignWithPrivateKey(privateKey, passPhrase);

            Assert.That(license, Is.Not.Null);
            Assert.That(license.Signature, Is.Not.Null);

            // validate xml
            var xmlElement = XElement.Parse(license.ToString(), LoadOptions.None);
            Assert.That(xmlElement.HasElements, Is.True);

            // validate default values when not set
            Assert.That(license.Id, Is.EqualTo(Guid.Empty));
            Assert.That(license.Type, Is.EqualTo(LicenseType.Trial));
            Assert.That(license.ProductFeatures, Is.Null);
            Assert.That(license.Customer, Is.Null);
            Assert.That(license.Expiration, Is.EqualTo(ConvertToRfc1123(DateTime.MaxValue)));

            // verify signature
            Assert.That(license.VerifySignature(publicKey), Is.True);
        }

        [Test]
        public void Can_Generate_And_Validate_Signature_With_Standard_License()
        {
            var licenseId = Guid.NewGuid();
            var customerName = "Max Mustermann";
            var customerEmail = "max@mustermann.tld";
            var expirationDate = DateTime.Now.AddYears(1);
            var productFeatures = new Dictionary<string, string>
            {
                {"Sales Module", "yes"},
                {"Purchase Module", "yes"},
                {"Maximum Transactions", "10000"}
            };

            var license = License.New()
                                 .WithUniqueIdentifier(licenseId)
                                 .As(LicenseType.Standard)
                                 .WithProductFeatures(productFeatures)
                                 .LicensedTo(customerName, customerEmail)
                                 .ExpiresAt(expirationDate)
                                 .CreateAndSignWithPrivateKey(privateKey, passPhrase);

            Assert.That(license, Is.Not.Null);
            Assert.That(license.Signature, Is.Not.Null);

            // validate xml
            var xmlElement = XElement.Parse(license.ToString(), LoadOptions.None);
            Assert.That(xmlElement.HasElements, Is.True);

            // validate default values when not set
            Assert.That(license.Id, Is.EqualTo(licenseId));
            Assert.That(license.Type, Is.EqualTo(LicenseType.Standard));
            Assert.That(license.ProductFeatures, Is.Not.Null);
            Assert.That(license.ProductFeatures.GetAll(), Is.EquivalentTo(productFeatures));
            Assert.That(license.Customer, Is.Not.Null);
            Assert.That(license.Customer.Name, Is.EqualTo(customerName));
            Assert.That(license.Customer.Email, Is.EqualTo(customerEmail));
            Assert.That(license.Expiration, Is.EqualTo(ConvertToRfc1123(expirationDate)));

            // verify signature
            Assert.That(license.VerifySignature(publicKey), Is.True);
        }
    }
}