using UnityEngine;

using System.IO;
using System.Text;
using System.Security.Cryptography;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    private class Wrapper
    {
        public int coins;
        public float highScore;
    }
    private  string filePath => Application.persistentDataPath + "/save.json";

    // Your 16-character AES key and IV (use strong values in production)
    private  readonly string key = "blightedflue2025";
    private  readonly string iv = "lightignios20250";

    public static SaveManager saveManager;

    private void Awake()
    {
        if (saveManager != null)
        {
            Destroy(gameObject);
            return;
        }


        saveManager = this;
        DontDestroyOnLoad(gameObject);
    }




    public void Save(int coins, float highScore)
    {
        string json = $"{{\"coins\":{coins},\"highScore\":{highScore}}}";

        byte[] encrypted = Encrypt(json, key, iv);
        File.WriteAllBytes(filePath, encrypted);

        Debug.Log("Data saved.");

    }

    // Internal wrapper only for parsin
    public void Load(out int coins, out float highScore)
    {
        coins = 0;
        highScore = 0;

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        byte[] encrypted = File.ReadAllBytes(filePath);
        string json = Decrypt(encrypted, key, iv);

        // Simple JSON parsing without a class
        var parsed = JsonUtility.FromJson<Wrapper>(json);
        coins = parsed.coins;
        highScore = parsed.highScore;
    }

   
    private static byte[] Encrypt(string plainText, string key, string iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return ms.ToArray();
            }
        }
    }

    private static string Decrypt(byte[] cipherBytes, string key, string iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            using (var ms = new MemoryStream(cipherBytes))
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}