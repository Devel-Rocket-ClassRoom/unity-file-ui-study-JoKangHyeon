using System.Diagnostics;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class Question2: MonoBehaviour
{
    public TextMeshProUGUI output;

    string data = "Hello Unity World!";
    byte encryptKey = 0b01011100;

    private void Start()
    {
    }

    public void CreateFile()
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "secret.txt"), data);
        output.text = $"원본: {data} 생성 완료";
    }

    public void Encrypt()
    {
        int encryptedByte = 0;

        using (FileStream writeStream = File.OpenWrite(Path.Combine(Application.persistentDataPath, "encrypted.dat")))
        {
            using (FileStream readStream = File.OpenRead(Path.Combine(Application.persistentDataPath, "secret.txt")))
            {
                int read = readStream.ReadByte();
                while (read != -1)
                {
                    byte byteData = (byte)read;
                    writeStream.WriteByte((byte)(byteData ^ encryptKey));

                    encryptedByte++;
                    read = readStream.ReadByte();
                }
            }
        }

        output.text = $"암호화 완료 (파일 크기: {encryptedByte} bytes)";
    }

    public void Decrypt()
    {

        using (FileStream writeStream = File.OpenWrite(Path.Combine(Application.persistentDataPath, "decrypted.txt")))
        {
            using (FileStream readStream = File.OpenRead(Path.Combine(Application.persistentDataPath, "encrypted.dat")))
            {
                int read = readStream.ReadByte();
                while (read != -1)
                {
                    byte byteData = (byte)read;
                    writeStream.WriteByte((byte)(byteData ^ encryptKey));

                    read = readStream.ReadByte();
                }
            }
        }


        string decryptedData = File.ReadAllText(Path.Combine(Application.persistentDataPath, "decrypted.txt"));
        output.text = $"복호화 완료\n복호화 결과: {decryptedData}\n원본과 일치: {data.Equals(decryptedData)}";
    }
}