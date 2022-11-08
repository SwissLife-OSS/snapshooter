using System;

namespace Snapshooter.Tests.Data
{
    public class TestImage
    {
        public int? Id { get; set; }
        public Guid? OwnerId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public decimal? Price { get; set; }
        public byte[]? Data { get; set; }

        public TestImage? SubImage { get; set; }
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

        public TestImageBuilder WithId(int? id)
        {
            _testImage.Id = id;
            return this;
        }

        public TestImageBuilder WithOwnerId(Guid? ownerId)
        {
            _testImage.OwnerId = ownerId;
            return this;
        }

        public TestImageBuilder WithName(string? name)
        {
            _testImage.Name = name;
            return this;
        }

        public TestImageBuilder WithCreationDate(DateTime? creationDate)
        {
            _testImage.CreationDate = creationDate;
            return this;
        }

        public TestImageBuilder WithPrice(decimal? price)
        {
            _testImage.Price = price;
            return this;
        }

        public TestImageBuilder WithData(byte[]? data)
        {
            _testImage.Data = data;
            return this;
        }

        public TestImageBuilder WithSubImage(TestImage? subImage)
        {
            _testImage.SubImage = subImage;
            return this;
        }

        public TestImage Build()
        {
            return _testImage;
        }
    }
}
