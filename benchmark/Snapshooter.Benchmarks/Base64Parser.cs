namespace Snapshooter.Benchmarks;

public class Base64Parser
{
    public bool IsBase64FromBase64String(string base64String)
    {
        try
        {
            Convert.FromBase64String(base64String);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsBase64TryFromBase64String(string base64String)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
        bool isBase64 = Convert.TryFromBase64String(base64String, buffer, out int bytesParsed);

        return isBase64;
    }
}

