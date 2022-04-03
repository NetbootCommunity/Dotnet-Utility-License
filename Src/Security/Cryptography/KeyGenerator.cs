using System.Security.Cryptography;

namespace Netboot.Utility.License.Security.Cryptography;

/// <summary>
/// Represents a generator for signature keys of <see cref="License"/>.
/// </summary>
public class KeyGenerator
{
    /// <summary>
    /// Creates a new instance of the <see cref="KeyGenerator"/> class.
    /// </summary>
    public static KeyGenerator Create()
        => new();

    /// <summary>
    /// Generates a private/public key pair for license signing.
    /// </summary>
    /// <returns>An <see cref="KeyPair"/> containing the keys.</returns>
    public KeyPair GenerateKeyPair()
        => new KeyPair(ECDsa.Create());
}