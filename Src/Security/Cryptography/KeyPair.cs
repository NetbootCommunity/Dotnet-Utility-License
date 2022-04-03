using System;
using System.Security.Cryptography;

namespace Netboot.Utility.License.Security.Cryptography;

/// <summary>
/// Represents a private/public encryption key pair.
/// </summary>
public class KeyPair
{
    private readonly AsymmetricAlgorithm _algorithm;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyPair"/> class
    /// with the provided asymmetric key pair.
    /// </summary>
    /// <param name="algorithm"></param>
    internal KeyPair(AsymmetricAlgorithm algorithm)
    {
        _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
    }

    /// <summary>
    /// Gets the encrypted and DER encoded private key.
    /// </summary>
    /// <param name="passPhrase">The pass phrase to encrypt the private key.</param>
    /// <returns>The encrypted private key.</returns>
    public string ToEncryptedPrivateKeyString(string passPhrase)
    {
        var data = _algorithm.ExportEncryptedPkcs8PrivateKey(
            password: passPhrase,
            pbeParameters: new PbeParameters(PbeEncryptionAlgorithm.TripleDes3KeyPkcs12, HashAlgorithmName.SHA1, 10));
        return Convert.ToBase64String(data);
    }

    /// <summary>
    /// Gets the DER encoded public key.
    /// </summary>
    /// <returns>The public key.</returns>
    public string ToPublicKeyString()
    {
        var data = _algorithm.ExportSubjectPublicKeyInfo();
        return Convert.ToBase64String(data);
    }
}