using System.Collections.Generic;
using NUnit.Framework;
using Netboot.Utility.License.Security.Cryptography;

namespace Netboot.Utility.License.Test
{
    [TestFixture]
    public class KeyGeneratorTests
    {
        [Test] // See Bug #135
        public void Ensure_To_Not_Generate_Identical_Keys()
        {
            var passPhrase = "test";
            var privKeySet = new HashSet<string>();
            var pubKeySet = new HashSet<string>();

            // add well known key
            privKeySet.Add(
                "MIICITAjBgoqhkiG9w0BDAEDMBUEEF5Fx1gxrWd+0G10a7+UbxQCAQoEggH4SUUim2C3kcHApCKVgIeXpKlZQHcaRgfWt0rVEWr8zRnzO9xT5itU2Sw7j0N3oh6cPer/QGNCmAgnRyiDatruJznDPOmMzK5Yskj6mlCaY6JEjcol+E4SBZJDgvIejy8HVCy+DOIR42JXs9oxgeq8eqB+0RZwvDMBG2hrUjnZ4/hPKRPJY134cqTH68jLv6SXglIPcrL9OxOwdzJBaq0ngSBfqhBWbLRIy/Th2btl9Q/0b+sZxG6r2b80wOxIewlr6EUqXtMaA8Bo5dgVZt1itWYafIAbLWzjZavwdO+EkUMCjZhsfvbXSCmcLRmitdJ6beG7jg7R6m6Q92DpU3qZhEio9akX3MQmOTO63Er4T2t6HHYnTzPaZPjdn8D+8lcTUntp/0vD8SvC3+Cb7tZOHSVGMUDdj7WIW+Bl/5bhdmnChE83HSxR4OsBjLATuZOpYtOefWbXyT8qsUn1IouaCjH+BYejBIPrmFVVl0WZADtbyE0LAOyHCD2quAjCpIwXXONG/gXm+XVGst5clbcuxaG4TxKWA8ifIXaio3aJgLfI+D0Izt2GscKRg6oGTlbC3YFIJg+PAH3A4qufoRSPmtREz0oR1X1ZsS6m/IKezf8vl3S+fSpmR/mUuc6uBx9qI9yJIEW/In90r5vO9fKGusEElP6svlub");
            pubKeySet.Add(
                "MIIBKjCB4wYHKoZIzj0CATCB1wIBATAsBgcqhkjOPQEBAiEA/////wAAAAEAAAAAAAAAAAAAAAD///////////////8wWwQg/////wAAAAEAAAAAAAAAAAAAAAD///////////////wEIFrGNdiqOpPns+u9VXaYhrxlHQawzFOw9jvOPD4n0mBLAxUAxJ02CIbnBJNqZnjhE50mt4GffpAEIQNrF9Hy4SxCR/i85uVjpEDydwN9gS3rM6D0oTlF2JjClgIhAP////8AAAAA//////////+85vqtpxeehPO5ysL8YyVRAgEBA0IABNVLQ1xKY80BFMgGXec++Vw7n8vvNrq32PaHuBiYMm0PEj2JoB7qSSWhfgcjxNVJsxqJ6gDQVWgl0r7LH4dr0KU=");

            for (int i = 0; i < 100; i++)
            {
                var keyGenerator = new KeyGenerator();
                var pair = keyGenerator.GenerateKeyPair();
                var privateKey = pair.ToEncryptedPrivateKeyString(passPhrase);
                var publicKey = pair.ToPublicKeyString();

                Assert.That(privKeySet.Add(privateKey), Is.True);
                Assert.That(pubKeySet.Add(publicKey), Is.True);
            }

            privKeySet.Clear();
            pubKeySet.Clear();
        }
    }
}