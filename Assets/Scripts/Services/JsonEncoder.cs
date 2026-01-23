using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public class JsonEncoder
{
    // -------------------------
    // JSON → LEVEL CODE
    // -------------------------
    public static string Encode(string json)
    {
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        byte[] compressed = Compress(jsonBytes);
        return Convert.ToBase64String(compressed);
    }

    // -------------------------
    // LEVEL CODE → JSON
    // -------------------------
    public static string Decode(string codedString)
    {
        byte[] compressed = Convert.FromBase64String(codedString);
        byte[] jsonBytes = Decompress(compressed);
        return Encoding.UTF8.GetString(jsonBytes);
    }

    // -------------------------
    // Helpers
    // -------------------------
    static byte[] Compress(byte[] data)
    {
        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionMode.Compress))
        {
            gzip.Write(data, 0, data.Length);
        }
        return output.ToArray();
    }

    static byte[] Decompress(byte[] data)
    {
        using var input = new MemoryStream(data);
        using var gzip = new GZipStream(input, CompressionMode.Decompress);
        using var output = new MemoryStream();
        gzip.CopyTo(output);
        return output.ToArray();
    }
}
