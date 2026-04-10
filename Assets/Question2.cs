using System.IO;
using System.Text;
using UnityEngine;

public class Question2: MonoBehaviour
{
    private void Start()
    {
        StringBuilder sbDebug = new StringBuilder();

        string data = "Hello Unity World!";
        byte encryptKey = 0b01011100;


        sbDebug.AppendLine($"원본: {data}");
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "secret.txt"), data);
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

        sbDebug.AppendLine($"암호화 완료 (파일 크기: {encryptedByte} bytes)");



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
        sbDebug.AppendLine("복호화 완료");


        string decryptedData = File.ReadAllText(Path.Combine(Application.persistentDataPath, "decrypted.txt"));
        sbDebug.AppendLine($"복호화 결과: {decryptedData}");

        sbDebug.AppendLine($"원본과 일치: {data.Equals(decryptedData)}");

        Debug.Log(sbDebug.ToString());
    }
}