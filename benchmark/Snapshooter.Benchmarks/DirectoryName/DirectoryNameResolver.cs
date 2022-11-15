namespace Snapshooter.Benchmarks.DirectoryName
{
    public class DirectoryNameResolver
    {
        public string? GetDirectoryName_PathString(string filenameFullPath)
        {
            return Path.GetDirectoryName(filenameFullPath);
        }

        public ReadOnlySpan<char> GetDirectoryName_PathAsSpan(string filenameFullPath)
        {
            return Path.GetDirectoryName(filenameFullPath.AsSpan());            
        }

        public string GetDirectoryName_Split(string filenameFullPath)
        {
            var directorySections = filenameFullPath.Split('\\');
            IEnumerable<string>? minusLastSection = directorySections.Take(directorySections.Length - 1);
            var directoryName = string.Join("\\", minusLastSection);

            return directoryName;
        }

        public string GetDirectoryName_IndexOfString(string filenameFullPath)
        {
            string d = filenameFullPath.Remove(filenameFullPath.LastIndexOf('\\'));

            return d;
        }

        public ReadOnlySpan<char> GetDirectoryName_IndexOfSpan(string filenameFullPath)
        {
            return filenameFullPath.AsSpan(0, filenameFullPath.LastIndexOf('\\'));
        }

        public string GetDirectoryName_IndexOfSpanToString(string filenameFullPath)
        {
            return filenameFullPath.AsSpan(0, filenameFullPath.LastIndexOf('\\')).ToString();
        }

        public string GetDirectoryName_IndexOfString_Slash(string filenameFullPath)
        {
            int index = filenameFullPath.LastIndexOf('\\');

            if(index < 0)
            {
                index = filenameFullPath.LastIndexOf('/');
            }

            return filenameFullPath.Remove(index);
        }
    }
}
