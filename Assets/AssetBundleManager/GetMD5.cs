using System.IO;
using System.Security.Cryptography;

public static class GetMD5
{
    public static string GetAtPath(string path)
    {
        FileInfo file = new FileInfo(path);
        return GetAtFile(file);
    }
    public static string GetAtFile(FileInfo file)
    {
        FileStream stream = file.Open(FileMode.Open);
        string md5String = GetAtStream(stream);
        stream.Close();
        return md5String;
    }
    public static string GetAtStream(FileStream stream)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] md5Bytes = md5.ComputeHash(stream);
        string md5String = System.BitConverter.ToString(md5Bytes);
        return md5String;
    }
    public static string GetAtByte(byte[] bytes)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] md5Bytes = md5.ComputeHash(bytes);
        string md5String = System.BitConverter.ToString(md5Bytes);
        return md5String;
    }
}
