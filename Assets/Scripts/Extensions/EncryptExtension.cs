using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class EncryptExtension
{
    private const string Key = "Only_Egg_Encrypt";

    public static async Task<string> EncryptAsync(this string source)
    {
        return await Task.Run(() => 
        {
            return Encrypt(source);
        });
    }

    public static string Encrypt(this string source)
    {
        // RijndaelManaged rijndaelCipher = new RijndaelManaged();
        // rijndaelCipher.Mode = CipherMode.CBC;
        // rijndaelCipher.Padding = PaddingMode.PKCS7;
        // rijndaelCipher.KeySize = 128;
        // rijndaelCipher.BlockSize = 128;

        // byte[] passwordBytes = Encoding.UTF8.GetBytes(Key);
        // byte[] keyBytes = new byte[16];

        // var length = passwordBytes.Length;
        // if (length > keyBytes.Length)
        // {
        //     length = keyBytes.Length;
        // }

        // Array.Copy(passwordBytes, keyBytes, length);

        // rijndaelCipher.Key = keyBytes;
        // rijndaelCipher.IV = keyBytes;

        ICryptoTransform transform = GetBase().CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(source);

        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }

    public static async Task<string> DecryptAsync(this string source)
    {
        return await Task.Run(() => 
        {
            return Decrypt(source);
        });
    }

    public static string Decrypt(this string source)
    {
        // RijndaelManaged rijndaelCipher = new RijndaelManaged();
        // rijndaelCipher.Mode = CipherMode.CBC;
        // rijndaelCipher.Padding = PaddingMode.PKCS7;
        // rijndaelCipher.KeySize = 128;
        // rijndaelCipher.BlockSize = 128;

        // byte[] passwordBytes = Encoding.UTF8.GetBytes(Key);
        // byte[] keyBytes = new byte[16];

        // var length = passwordBytes.Length;
        // if (length > keyBytes.Length)
        // {
        //     length = keyBytes.Length;
        // }
        
        // Array.Copy(passwordBytes, keyBytes, length);

        // rijndaelCipher.Key = keyBytes;
        // rijndaelCipher.IV = keyBytes;

        byte[] encryptedData = Convert.FromBase64String(source);
        byte[] plainText = GetBase().CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

        return Encoding.UTF8.GetString(plainText);
    }

    private static RijndaelManaged GetBase()
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 128;
        rijndaelCipher.BlockSize = 128;

        byte[] passwordBytes = Encoding.UTF8.GetBytes(Key);
        byte[] keyBytes = new byte[16];

        var length = passwordBytes.Length;
        if (length > keyBytes.Length)
        {
            length = keyBytes.Length;
        }
        
        Array.Copy(passwordBytes, keyBytes, length);

        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;

        return rijndaelCipher;
    }
}
