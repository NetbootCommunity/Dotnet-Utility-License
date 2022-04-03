using System;
using System.Security.Cryptography;

namespace Netboot.Utility.License.Security.Cryptography
{
    public class Signer
    {
        /// <summary>
        /// Signs the specified document to sign.
        /// </summary>
        /// <param name="documentToSign">The document to sign.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <returns></returns>
        public byte[] Sign(byte[] documentToSign, string privateKey, string passPhrase)
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportEncryptedPkcs8PrivateKey(passPhrase, Convert.FromBase64String(privateKey), out int _);
            return ecdsa.SignData(documentToSign, HashAlgorithmName.SHA512, DSASignatureFormat.Rfc3279DerSequence);
        }

        /// <summary>
        /// Verifies the signature.
        /// </summary>
        /// <param name="documentToSign">The document to sign.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns></returns>
        public bool VerifySignature(byte[] documentToSign, byte[] signature, string publicKey)
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out int read);
            return ecdsa.VerifyData(documentToSign, signature, HashAlgorithmName.SHA512, DSASignatureFormat.Rfc3279DerSequence);
        }
    }
}