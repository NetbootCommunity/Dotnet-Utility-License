using Netboot.Utility.License.Security.Cryptography;
using NUnit.Framework;
using System;
using System.Text;

namespace Netboot.Utility.License.Test
{
    public class SignerTests
    {
        private const string passphrase = "passphrase";
        private const string privateKey = "MIIBMTAbBgoqhkiG9w0BDAEDMA0ECCKZdIbxFNZ+AgEKBIIBEEhnXR+1mC3+SntH2Uiiyb1B7ivbK1nx1jOCEYvquvoUmVm22MI20sw3YTMPzTalrY8ogR3WBJzJRIn7lRKtQFqJjdlhh3G+5Qf8YBLjiDZbgeLsnL5K4DG+s0cDs8/Wx36a8/OnbhvJdIM3ualRo/az9lkbBt5BPD+Mx8MWHmXpyVVBPoeosZiuYRNmGdB7SY5NaslOpMM7SfV0X961uQp2xCcclhDuExv5XAuOcccONCSf1wfH1MhI1IumQfCMdWOnCdJUhDCxL/IVVjkb7bBLpQiMXLm2rCZoWtGrniQmpL4iQexa4J9SqCupZ+TP4zGpHM5clGuuefY4cKo+E0hH8Ftj55hplRNiNAtBDhG/";
        private const string publicKey = "MIGbMBAGByqGSM49AgEGBSuBBAAjA4GGAAQBMjD7TcXgSMbjDDpkOtNe68prK21mPv3c4q8+CSUZKSz9mO8YB0oXmXKCeKORp2v4bDhx6xqNsXCMX07GmgSm7n0A6q71AkjSGDz7iNrW2TSByFql38c6wdtCKneBu4R29u9z7VE/dfGuwDjmo0Fwpo4zaZSrubwCjqkMoU0fr/j7DtQ=";

        [Theory]
        [TestCase("MIGHAkIB1OypetzT5Dml4axBDyp2wi6kroAEAtR6PbewRMBNJy4CTDyxTdThk++WmvUiB4mT8VbhvRl4+XZZUdlU1rhmrhwCQUb3qclZf9aHr63WGU8ojs9zxZHvwC8+vKjakUBuzLmZ/6N+R38NNu6Uqxdt83DTS5/nAyKScgp08BgRCPHxWJWO")]
        public void VerifySignature_Works(string signature)
        {
            var data = Encoding.UTF8.GetBytes("Hello, World!");

            var signer = new Signer();
            Assert.True(signer.VerifySignature(data, Convert.FromBase64String(signature), publicKey));
        }
    }
}