public interface ICryptoService
{
    Task<RSAKeyPair> GenerateKeyPairAsync();
    Task<string> EncryptAsync(string data, string publicKey);
    Task<string> DecryptAsync(string encryptedData, string privateKey);
    Task<string> SignAsync(string data, string privateKey);
    Task<bool> VerifySignatureAsync(string data, string signature, string publicKey);
}
