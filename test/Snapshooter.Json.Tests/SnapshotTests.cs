using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Json.Tests
{
    public class SnapshotTests
    {
        #region Match Snapshot - Simple Snapshot Tests

        [Fact]
        public void Match_FactMatchSingleSnapshot_GoodCase()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_FactMatchSingleSnapshot_GoodCase);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName);
        }
        
        [Fact]
        public void Match_HashFields()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_HashFields);

            TestPerson testObj = TestPersonBuilder
                .Create()
                .AddChild(TestChildBuilder.Create().WithName("John").Build())
                .AddChild(TestChildBuilder.Create().WithName("Ann").Build())
                .Build();

            // act & assert
            Snapshot.Match(testObj, snapshotName, o => o.HashField("Children[*].Name"));
        }

        [Fact]
        public async Task Match_FactMatchSingleSnapshotAsync_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_FactMatchSingleSnapshotAsync_SuccessfulMatch);

            await Task.Delay(10);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Snapshot.Match(testPerson, snapshotName);

            // assert
            await Task.Delay(10);
        }

        [Fact]
        public void Match_FactMatchSingleSnapshot_OneFieldNotEqual()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_FactMatchSingleSnapshot_OneFieldNotEqual);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithAge(5).Build();

            // act
            Action match = () => Snapshot.Match(testPerson, snapshotName);

            // assert
            Assert.Throws<SnapshotCompareException>(match);
        }

        [Fact]
        public void Match_FactMatchSingleSnapshot_FieldNotExistInSnapshot()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_FactMatchSingleSnapshot_FieldNotExistInSnapshot);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Action match = () => Snapshot.Match(testPerson, snapshotName);

            // assert
            Assert.Throws<SnapshotCompareException>(match);
        }

        [Fact]
        public void Match_FactMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                    nameof(Match_FactMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated);

            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                    new JsonSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName(snapshotName);

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            if (File.Exists(snapshotFileName))
            {
                File.Delete(snapshotFileName);
            }

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Snapshot.Match(testPerson, snapshotName);

            // assert
            Assert.True(File.Exists(snapshotFileName));
        }

        [Theory]
        [InlineData(36, 189.45)]
        [InlineData(42, 173.16)]
        [InlineData(19, 193.02)]
        public void Match_TheoryMatchSingleSnapshot_GoodCase(int age, decimal size)
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_TheoryMatchSingleSnapshot_GoodCase);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Snapshot.Match(
                testPerson, snapshotName, SnapshotNameExtension.Create(age, size));
        }

        [Theory]
        [InlineData(34, 175)]
        [InlineData(36, 177)]
        [InlineData(37, 178)]
        public void Match_TheoryMatchSingleSnapshot_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_TheoryMatchSingleSnapshot_OneFieldNotEqual);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.US;

            // act & assert
            Assert.Throws<SnapshotCompareException>(() => Snapshot.Match(
                testPerson, snapshotName, SnapshotNameExtension.Create(age, size)));
        }

        [Theory]
        [InlineData(22, 160)]
        [InlineData(23, 164)]
        public void Match_TheoryMatchSingleSnapshot_FieldNotExistInSnapshot(int age, decimal size)
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_TheoryMatchSingleSnapshot_FieldNotExistInSnapshot);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Assert.Throws<SnapshotCompareException>(() => Snapshot.Match(
                testPerson, snapshotName, SnapshotNameExtension.Create(age, size)));
        }

        [Theory]
        [InlineData(19, 162.3)]
        [InlineData(17, 112.3)]
        public void Match_TheoryMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated(int age, decimal size)
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                    nameof(Match_TheoryMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated);

            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                    new JsonSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName(snapshotName);

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            if (File.Exists(snapshotFileName))
            {
                File.Delete(snapshotFileName);
            }

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act
            Snapshot.Match(testPerson, snapshotName);

            // assert
            Assert.True(File.Exists(snapshotFileName));
        }

        #endregion

        #region Match Snapshot - Multiple Objects Tests

        [Fact]
        public void Match_MultipleObjectsSnapshot_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_MultipleObjectsSnapshot_SuccessfulMatch);

            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton().Build();
            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider().Build();
            TestChild hanna = TestDataBuilder.TestChildHanna().Build();

            // act & assert
            Snapshot.Match(new { markWalton, sandraSchneider, hanna }, snapshotName);
        }

        [Fact]
        public void Match_ObjectsArraySnapshot_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_ObjectsArraySnapshot_SuccessfulMatch);

            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton().Build();
            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider().Build();
            TestChild hanna = TestDataBuilder.TestChildHanna().Build();

            // act & assert
            Snapshot.Match(new object[] { markWalton, sandraSchneider, hanna }, snapshotName);
        }

        [Fact]
        public void Match_ObjectsListsSnapshot_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_ObjectsListsSnapshot_SuccessfulMatch);

            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton().Build();
            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider().Build();
            TestChild hanna = TestDataBuilder.TestChildHanna().Build();

            // act & assert
            Snapshot.Match(
                new List<object>() { markWalton, sandraSchneider, hanna }, snapshotName);
        }

        #endregion

        #region Match Snapshots - Ignore Fields Tests

        [Fact]
        public void Match_IgnoreScalarField_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreScalarField_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(0.5m).Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreField("Size"));
        }

        [Fact]
        public void Match_IgnoreScalarNullIntField_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreScalarNullIntField_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreField<int?>("Age"));
        }

        [Fact]
        public void Match_IgnoreScalarNullStringField_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreScalarNullStringField_SuccessfulIgnored);

            TestChild testChild = TestDataBuilder.TestChildNull()
                .Build();

            // act & assert
            Snapshot.Match(testChild, snapshotName,
                matchOptions => matchOptions.IgnoreField<string>("Name"));
        }

        [Fact]
        public void Match_IgnoreScalarFieldNullConvertError_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IgnoreScalarFieldNullConvertError_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() => Snapshot.Match(
                testPerson, snapshotName, matchOptions => matchOptions.IgnoreField<int>("Age")));
        }

        [Fact]
        public void Match_IgnoreScalarFieldPathNotExist_SnapshotComparedWithoutIgnoredField()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IgnoreScalarFieldPathNotExist_SnapshotComparedWithoutIgnoredField);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Snapshot.Match(testPerson,
                snapshotName, matchOptions => matchOptions.IgnoreField<decimal>("alt"));
        }

        [Fact]
        public void Match_IgnoreComplexObjectField_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreComplexObjectField_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            testPerson.Address = null;

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreField<object>("Address"));
        }

        [Fact]
        public void Match_IgnoreScalarFieldInAllWays_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreScalarFieldInAllWays_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(1.5m).Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreField("Size"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreField<decimal>("Size"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.Ignore(option => option.Field<decimal>("Size")));
        }

        [Fact]
        public void Match_IgnoreSeveralSingleFields_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreSeveralSingleFields_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Id = Guid.NewGuid();
            testPerson.CreationDate = DateTime.UtcNow;
            testPerson.Address.StreetNumber = -58;
            testPerson.Address.Country = null;
            testPerson.Relatives[0].Address.Plz = null;

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions
                    .IgnoreField<Guid>("Id")
                    .IgnoreField<DateTime>("CreationDate")
                    .IgnoreField<int>("Address.StreetNumber")
                    .IgnoreField<TestChild>("Children[3]")
                    .IgnoreField<TestCountry>("Address.Country")
                    .IgnoreField<TestCountry>("Relatives[0].Address.Plz"));
        }

        [Fact]
        public void Match_IgnoreWildcardScalarFieldsArray_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreWildcardScalarFieldsArray_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).Name = "newName1";
            testPerson.Children.ElementAt(1).Name = "newName2";
            testPerson.Children.ElementAt(2).Name = "newName3";

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreFields("Children[*].Name"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreFields<string>("Children[*].Name"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.Ignore(option => option.Fields<string>("Children[*].Name")));
        }

        [Fact]
        public void Match_IgnoreWildcardComplexFieldsArray_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreWildcardComplexFieldsArray_SuccessfulIgnored);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).Name = "newName1x";
            testPerson.Children.ElementAt(1).Name = "newName2x";
            testPerson.Children.ElementAt(2).Name = "newName3x";

            // act & assert

            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreFields("Children[*]"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreFields<TestChild>("Children[*]"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.Ignore(option => option.Fields<TestChild>("Children[*]")));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IgnoreField("Children"));
        }

        [Fact]
        public void Match_IgnoreArrayFields_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreArrayFields_SuccessfulIgnored);

            object[] testPersons = new object[]
            {
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestPersonSandraSchneider().Build(),
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestChildJames().Build(),
                TestDataBuilder.TestChildHanna().Build(),
                TestDataBuilder.TestCountrySwitzerland().Build()
            };

            // act & assert
            Snapshot.Match(testPersons, snapshotName,
                matchOptions => matchOptions.IgnoreFields<object>("[*]"));
        }

        [Fact]
        public void Match_IgnoreArrayFieldsPersonFirstname_SuccessfulIgnored()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IgnoreArrayFieldsPersonFirstname_SuccessfulIgnored);

            object[] testPersons = new object[]
            {
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestPersonSandraSchneider().Build(),
                TestDataBuilder.TestPersonMarkWalton().Build()
            };

            // act & assert
            Snapshot.Match(testPersons, snapshotName,
                matchOptions => matchOptions.IgnoreFields<object>("[*].Firstname"));
        }

        #endregion

        #region Match Snapshots - Hash Fields Tests

         [Fact]
        public void Match_BinaryData()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_BinaryData);

            TestImage testImage = TestDataBuilder.TestImageMonaLisa().Build();

            // act & assert
            Snapshot.Match(testImage, snapshotName);
        }

        [Fact]
        public void Match_BinaryData_HashField()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_BinaryData_HashField);

            TestImage testImage = TestDataBuilder.TestImageMonaLisa().Build();

            // act & assert
            Snapshot.Match(testImage, snapshotName, o => o.HashField("Data"));
        }

        [Fact]
        public void Match_BinaryDataDoesNotMatch_HashField()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_BinaryData_HashField);

            TestImage testImage = TestDataBuilder.TestImageMonaLisaFake().Build();

            // act & assert
            Action act = () => Snapshot.Match(testImage, snapshotName, o => o.HashField("Data"));

            act.Should().Throw<SnapshotCompareException>();
        }

        [Fact]
        public void Match_BinaryData_AcceptField()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_BinaryData_AcceptField);

            TestImage testImage = TestDataBuilder.TestImageMonaLisa().Build();

            // act & assert
            Snapshot.Match(testImage, snapshotName, o => o.AcceptField<byte[]>("Data"));
        }

        #endregion

        #region Match Snapshots - Complex Tests

        [Fact]
        public void Match_LargeOverallTest_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_LargeOverallTest_SuccessfulMatch);

            TestChild testChild = TestDataBuilder.TestChildJames().Build();
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(testChild)
                .Build();

            testPerson.Id = Guid.NewGuid();
            testPerson.CreationDate = DateTime.UtcNow;
            testPerson.Address.StreetNumber = -58;
            testPerson.Address.Country = null;
            testPerson.Relatives[0].Address.Plz = null;

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOption => matchOption
                    .Assert(option => Assert.NotEqual(Guid.Empty, option.Field<Guid>("Id")))
                    .IgnoreField<DateTime>("CreationDate")
                    .Assert(option => Assert.Equal(-58, option.Field<int>("Address.StreetNumber")))
                    .Assert(option => testChild.Should().BeEquivalentTo(option.Field<TestChild>("Children[3]")))
                    .IgnoreField<TestCountry>("Address.Country")
                    .Assert(option => Assert.Null(option.Field<TestCountry>("Relatives[0].Address.Plz"))));
        }

        [Fact]
        public void Match_CircularReference_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_CircularReference_SuccessfulMatch);

            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton()
                .Build();


            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider()
                .AddRelative(markWalton)
                .Build();

            markWalton.Relatives = new[] { sandraSchneider };

            // act & assert
            Snapshot.Match(markWalton, snapshotName);
        }

        #endregion
    }
}
