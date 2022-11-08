using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Snapshooter.Tests.Data
{
    public class TestFileLoader
    {
        public static byte[] LoadBinaryFile(string fileName)
        {
            Func<Stream, byte[]> streamToByteArray = (stream) =>
            {
                try
                {
                    byte[] ba = new byte[stream.Length];
                    stream.Read(ba, 0, ba.Length);
                    return ba;
                }
                finally
                {
                    stream?.Dispose();
                }
            };

            var binaryFile = LoadResourceFile(fileName, streamToByteArray);

            return binaryFile;
        }

        public static Stream LoadFileStream(string fileName)
        {
            Func<Stream, Stream> streamToStream = (stream) =>
            {
                return stream;
            };

            return LoadResourceFile(fileName, streamToStream);
        }

        public static string LoadTextFile(string fileName)
        {
            Func<Stream, string> streamToByteArray = (stream) =>
            {
                using var reader = new StreamReader(stream, Encoding.UTF8);

                return reader.ReadToEnd();
            };

            var textFile = LoadResourceFile(fileName, streamToByteArray);

            return textFile;
        }

        private static T LoadResourceFile<T>(string fileName, Func<Stream, T> streamConverter)
        {
            Assembly assembly = typeof(TestFileLoader).GetTypeInfo().Assembly;

            var resourceNames = assembly.GetManifestResourceNames().ToList();

            string? resourceName = resourceNames
                .SingleOrDefault(name => name.EndsWith($"__testsources__.{fileName}"));

            if (string.IsNullOrEmpty(resourceName))
            {
                throw new FileNotFoundException(
                    $"The test resource file with name {fileName} " +
                    $"could not be found in the __testsources__ " +
                    $"folder of assembly {assembly.FullName}");
            }

            Stream? resourceStream = assembly
                .GetManifestResourceStream(resourceName);

            if (resourceStream == null)
            {
                throw new Exception($"No manifest resource stream " +
                    $"for file {resourceName} could be found.");
            }

            return streamConverter(resourceStream);

        }
    }
}
