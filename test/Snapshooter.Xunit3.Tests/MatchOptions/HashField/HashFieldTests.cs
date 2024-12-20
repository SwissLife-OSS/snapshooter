using System;
using System.IO;
using FluentAssertions;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.Helpers;
using Xunit;
using Xunit3.Sdk;

namespace Snapshooter.Xunit3.Tests.MatchOptions.HashField
{
    public class HashFieldTests
    {
        #region Hash ByteArray Tests

        [Fact]
        public void HashField_WithoutHashFieldOption_NoFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .Build();

            // act & assert
            Snapshot.Match(testImage);
        }
        
        [Fact]
        public void HashField_HashBinaryDataField_BinaryFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Data"));
        }
        
        [Fact]
        public void HashField_NewHashBinarySnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("Data"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullBinaryField_NullBinaryFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Data"));
        }

        [Fact]
        public void HashField_HashDifferentBinaryField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisaFake()
                .Build();
            
            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Data"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Data");
        }

        #endregion

        #region Hash Integer Tests

        [Fact]
        public void HashField_HashIntField_IntFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Id"));
        }

        [Fact]
        public void HashField_NewHashIntSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("Id"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullIntField_NullIntFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithId(null)
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Id"));
        }

        [Fact]
        public void HashField_HashDifferentIntField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithId(3450988)
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Id"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Id");
        }

        [Fact]
        public void HashField_CorruptHashIntField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Id"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Id");
        }

        [Fact]
        public void HashField_OtherFieldIncorrect_ThrowsException()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithName("Incorrect Name")
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Id"));

            // assert
            act.Should().Throw<EqualException>()
                .Which.Message.Should().Contain("Name");
        }

        #endregion

        #region Hash Guid Tests

        [Fact]
        public void HashField_HashGuidField_GuidFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("OwnerId"));
        }

        [Fact]
        public void HashField_NewHashGuidSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("OwnerId"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullGuidField_NullGuidFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithOwnerId(null)
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("OwnerId"));
        }

        [Fact]
        public void HashField_HashDifferentGuidField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithOwnerId(Guid.NewGuid())
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("OwnerId"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("OwnerId");
        }

        [Fact]
        public void HashField_CorruptHashGuidField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("OwnerId"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Id");
        }

        #endregion

        #region Hash String Tests

        [Fact]
        public void HashField_HashStringField_StringFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Name"));
        }

        [Fact]
        public void HashField_NewHashStringSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("Name"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullStringField_NullStringFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithName(null)
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Name"));
        }

        [Fact]
        public void HashField_HashDifferentStringField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithName("Different Name")
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Name"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Name");
        }

        [Fact]
        public void HashField_CorruptHashStringField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Name"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Name");
        }

        #endregion

        #region Hash DateTime Tests

        [Fact]
        public void HashField_HashDateTimeField_DateTimeFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("CreationDate"));
        }

        [Fact]
        public void HashField_NewHashDateTimeSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("CreationDate"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullDateTimeField_NullDateTimeFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithCreationDate(null)
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("CreationDate"));
        }

        [Fact]
        public void HashField_HashDifferentDateTimeField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithCreationDate(DateTime.UtcNow)
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("CreationDate"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("CreationDate");
        }

        [Fact]
        public void HashField_CorruptHashDateTimeField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("CreationDate"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("CreationDate");
        }

        #endregion

        #region Hash Decimal Tests

        [Fact]
        public void HashField_HashDecimalField_DecimalFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Price"));
        }

        [Fact]
        public void HashField_NewHashDecimalSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("Price"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullDecimalField_NullDecimalFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithPrice(null)
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("Price"));
        }

        [Fact]
        public void HashField_HashDifferentDecimalField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithPrice(334.1114m)
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Price"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Price");
        }

        [Fact]
        public void HashField_CorruptHashPriceField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("Price"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Price");
        }

        #endregion

        #region Hash Complex Type Tests

        [Fact]
        public void HashField_HashComplexTypeField_ComplexTypeFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()                   
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaFake()
                    .Build())
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("SubImage"));
        }

        [Fact]
        public void HashField_NewHashComplexTypeSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaFake()
                    .Build())
                .WithData(null)
                .Build();

            // act
            Snapshot.Match(testImage, options => options.HashField("SubImage"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullComplexTypeField_NullComplexFieldHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(null)
                .WithData(null)
                .Build();

            // act & assert
            Snapshot.Match(testImage, o => o.HashField("SubImage"));
        }

        [Fact]
        public void HashField_HashDifferentComplexTypeField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaFake()
                    .WithName("Modified Name")
                    .Build())
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("SubImage"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("SubImage");
        }

        [Fact]
        public void HashField_CorruptHashComplexTypeField_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaFake()
                    .Build())
                .WithData(null)
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, o => o.HashField("SubImage"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("SubImage");
        }

        #endregion

        #region Hash Multiple Fields Tests

        [Fact]
        public void HashField_HashMultipleFields_MultipleFieldsHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .Build())                
                .Build();

            // act  & assert
            Snapshot.Match(testImage, options => options
                .HashField("Id")
                .HashField("OwnerId")
                .HashField("CreationDate")
                .HashField("Price")
                .HashField("Data")
                .HashField("SubImage.Id")
                .HashField("SubImage.OwnerId")
                .HashField("SubImage.CreationDate")
                .HashField("SubImage.Price")
                .HashField("SubImage.Data"));
        }

        [Fact]
        public void HashField_NewHashMultipleFieldsSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .Build())
                .Build();

            // act
            Snapshot.Match(testImage, options => options
                .HashField("Id")
                .HashField("OwnerId")
                .HashField("CreationDate")
                .HashField("Price")
                .HashField("Data")
                .HashField("SubImage.Id")
                .HashField("SubImage.OwnerId")
                .HashField("SubImage.CreationDate")
                .HashField("SubImage.Price")
                .HashField("SubImage.Data"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashNullMultipleFields_NullFieldsHashed()
        {
            // arrange
            TestImage testImage = new ()
            {
                SubImage = new ()
            };


            // act & assert
            Snapshot.Match(testImage, options => options
                .HashField("Id")
                .HashField("OwnerId")
                .HashField("CreationDate")
                .HashField("Price")
                .HashField("Data")
                .HashField("SubImage.Id")
                .HashField("SubImage.OwnerId")
                .HashField("SubImage.CreationDate")
                .HashField("SubImage.Price")
                .HashField("SubImage.Data"));
        }

        [Fact]
        public void HashField_HashDifferentMultipleFields_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()                
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .WithPrice(22.33m)
                    .WithId(1)
                    .Build())
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, options => options
                .HashField("Id")
                .HashField("OwnerId")
                .HashField("CreationDate")
                .HashField("Price")
                .HashField("Data")
                .HashField("SubImage.Id")
                .HashField("SubImage.OwnerId")
                .HashField("SubImage.CreationDate")
                .HashField("SubImage.Price")
                .HashField("SubImage.Data"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Id");
        }

        [Fact]
        public void HashField_CorruptHashMultipleFields_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()                    
                    .Build())
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, options => options
               .HashField("Id")
               .HashField("OwnerId")
               .HashField("CreationDate")
               .HashField("Price")
               .HashField("Data")
               .HashField("SubImage.Id")
               .HashField("SubImage.OwnerId")
               .HashField("SubImage.CreationDate")
               .HashField("SubImage.Price")
               .HashField("SubImage.Data"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Price");
        }

        #endregion

        #region Hash Fields By Name Tests

        [Fact]
        public void HashField_HashFieldsByName_MultipleFieldsHashed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .WithSubImage(
                        TestDataBuilder
                        .TestImageMonaLisaFake()
                        .Build())
                    .Build())
                .Build();

            // act  & assert
            Snapshot.Match(testImage, options => options
                .HashField("**.Name")
                .HashField("**.Data"));
        }

        [Fact]
        public void HashField_NewHashFieldsByNameSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .WithSubImage(
                        TestDataBuilder
                        .TestImageMonaLisaFake()
                        .Build())
                    .Build())
                .Build();

            // act
            Snapshot.Match(testImage, options => options
                .HashField("**.Name")
                .HashField("**.Data"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashDifferentFieldsByName_HashCompareFailed()
        {
            // arrange
            TestImage testImage = TestDataBuilder
                .TestImageMonaLisa()
                .WithSubImage(
                    TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .WithSubImage(
                        TestDataBuilder
                        .TestImageMonaLisaFake()
                        .WithName("Wrong Name")
                        .Build())
                    .Build())
                .Build();

            // act
            Action act = () => Snapshot.Match(testImage, options => options
                .HashField("**.Name")
                .HashField("**.Data"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Name");
        }

        #endregion

        #region Hash Objects Array Fields Tests

        [Fact]
        public void HashField_HashObjectArrayFieldsByName_MultipleFieldsHashed()
        {
            // arrange
            TestImage[] testImages = new[]
            {
                TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .Build()
                ,
                TestDataBuilder
                    .TestImageMonaLisa()
                    .WithSubImage(
                        TestDataBuilder
                        .TestImageMonaLisaThumbnail()
                        .WithSubImage(
                            TestDataBuilder
                            .TestImageMonaLisaFake()
                            .Build())
                        .Build())
                    .Build()
                ,
                TestDataBuilder
                    .TestImageMonaLisaFake()
                    .Build()                
            };

            // act  & assert
            Snapshot.Match(testImages, options => options
                .HashField("**.Data")
                .HashField("[*].Name")
                .HashField("[*].SubImage.SubImage.Id"));
        }

        [Fact]
        public void HashField_NewHashObjectArrayFieldsSnapshot_CorrectFormatted()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            File.Delete(snapshotFileName);

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            TestImage[] testImages = new[]
            {
                TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .Build()
                ,
                TestDataBuilder
                    .TestImageMonaLisa()
                    .WithSubImage(
                        TestDataBuilder
                        .TestImageMonaLisaThumbnail()
                        .WithSubImage(
                            TestDataBuilder
                            .TestImageMonaLisaFake()
                            .Build())
                        .Build())
                    .Build()
                ,
                TestDataBuilder
                    .TestImageMonaLisaFake()
                    .Build()
            };

            // act
            Snapshot.Match(testImages, options => options
                .HashField("**.Data")
                .HashField("[*].Name")
                .HashField("[*].SubImage.SubImage.Id"));

            // assert
            Assert.True(File.Exists(snapshotFileName));
            Snapshot.Match(expectedSnapshot);
        }

        [Fact]
        public void HashField_HashDifferentObjectFieldsByName_HashCompareFailed()
        {
            // arrange
            TestImage[] testImages = new[]
            {
                TestDataBuilder
                    .TestImageMonaLisaThumbnail()
                    .Build()
                ,
                TestDataBuilder
                    .TestImageMonaLisa()
                    .WithSubImage(
                        TestDataBuilder
                        .TestImageMonaLisaThumbnail()
                        .WithSubImage(
                            TestDataBuilder
                            .TestImageMonaLisaFake()
                            .Build())
                        .Build())
                    .Build()
                ,
                TestDataBuilder
                    .TestImageMonaLisaFake()
                    .WithName("Different Name")
                    .Build()
            };

            // act
            Action act = () => Snapshot.Match(testImages, options => options
                .HashField("**.Data")
                .HashField("[*].Name")
                .HashField("[*].SubImage.SubImage.Id"));

            // assert
            act.Should().Throw<SnapshotCompareException>()
                .Which.Message.Should().Contain("Name");
        }

        #endregion
    }
}
