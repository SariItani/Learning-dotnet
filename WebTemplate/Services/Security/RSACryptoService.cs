using System.Security.Cryptography;

public class RSACryptoService : ICryptoService
{
    public async Task<RSAKeyPair> GenerateKeyPairAsync()
    {
        using var rsa = RSA.Create(2048);
        return new RSAKeyPair
        {
            PublicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey()),
            PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey()),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        };
    }

    public async Task<string> EncryptAsync(string data, string publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
        var encryptedData = rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.OaepSHA256);
        return Convert.ToBase64String(encryptedData);
    }

    public async Task<string> DecryptAsync(string encryptedData, string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
        var decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedData), RSAEncryptionPadding.OaepSHA256);
        return System.Text.Encoding.UTF8.GetString(decryptedData);
    }

    public async Task<string> SignAsync(string data, string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
        var signature = rsa.SignData(System.Text.Encoding.UTF8.GetBytes(data), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signature);
    }

    public async Task<bool> VerifySignatureAsync(string data, string signature, string publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
        return rsa.VerifyData(
            System.Text.Encoding.UTF8.GetBytes(data),
            Convert.FromBase64String(signature),
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );
    }
}

