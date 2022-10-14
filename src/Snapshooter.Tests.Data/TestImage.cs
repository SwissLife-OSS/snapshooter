using System;

namespace Snapshooter.Tests.Data
{
    public class TestImage
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }

    public class TestImageBuilder
    {
        private readonly TestImage _testImage;

        private TestImageBuilder()
        {
            _testImage = new TestImage();
        }

        public static TestImageBuilder Create()
        {
            return new TestImageBuilder();
        }

        public TestImageBuilder WithName(string name)
        {
            _testImage.Name = name;
            return this;
        }

        public TestImageBuilder WithData(byte[] data)
        {
            _testImage.Data = data;
            return this;
        }

        public TestImage Build()
        {
            return _testImage;
        }
    }
}
