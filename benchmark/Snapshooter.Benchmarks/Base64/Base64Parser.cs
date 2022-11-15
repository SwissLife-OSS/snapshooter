namespace Snapshooter.Benchmarks.Base64;

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

    public bool IsBase64TryFromBase64String(string base64String)
    {
        var buffer = new Span<byte>(new byte[base64String.Length]);
        var isBase64 = Convert.TryFromBase64String(base64String, buffer, out var bytesParsed);

        return isBase64;
    }
}

