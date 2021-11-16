using System.Threading.Tasks;
using NUnit.Framework;

namespace Snapshooter.NUnit.Tests
{
    public class NUnitSnapshotFullNameReaderTests
    {
        [Test]
        public void ReadSnapshotFullName_ResolveTestSnapshotFullName_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            Assert.That(snapshotFullName.Filename, Is.EqualTo(
                $"{nameof(NUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTestSnapshotFullName_ResolvedSuccessfully)}"));
        }

        [Test]
        public async Task ReadSnapshotFullName_ResolveTestSnapshotFullNameAsync_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.That(snapshotFullName.Filename, Is.EqualTo(
                $"{nameof(NUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTestSnapshotFullNameAsync_ResolvedSuccessfully)}"));
        }

        #pragma warning disable xUnit1026 // Theory methods should use all of their parameters

        [TestCase("testString1", 5)]
        [TestCase("testString2", 6)]
        [TestCase("testString3", 7)]
        public void ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters(
            string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            Assert.That(snapshotFullName.Filename, Is.EqualTo(
                $"{nameof(NUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}"));
        }

        [TestCase("testString1", 5)]
        [TestCase("testString2", 6)]
        [TestCase("testString3", 7)]
        public async Task ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.That(snapshotFullName.Filename, Is.EqualTo(
                $"{nameof(NUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}"));
        }

        [TestCaseSource(nameof(TestCases))]
        public void ReadSnapshotFullName_ResolveTheoryDataSnapshotName_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            Assert.That(snapshotFullName.Filename, Is.EqualTo(
                $"{nameof(NUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheoryDataSnapshotName_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}"));
        }

        [TestCaseSource(nameof(TestCases))]
        public async Task ReadSnapshotFullName_ResolveTheoryDataSnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.That(snapshotFullName.Filename, Is.EqualTo(
                $"{nameof(NUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheoryDataSnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}"));
        }

        private static object[] TestCases => new object[]
        {
            new object[] { "testString1", 5 },
            new object[] { "testString2", 6 },
            new object[] { "testString3", 7 }
        };

        #pragma warning restore xUnit1026 // Theory methods should use all of their parameters
    }
}
