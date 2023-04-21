using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Snapshooter.MSTest.Tests
{
    [TestClass]
    public class MSTestSnapshotFullNameReaderTests
    {
        [TestMethod]
        public void ReadSnapshotFullName_ResolveTestSnapshotFullName_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTestSnapshotFullName_ResolvedSuccessfully)}");
        }

        [TestMethod]
        public async Task ReadSnapshotFullName_ResolveTestSnapshotFullNameAsync_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTestSnapshotFullNameAsync_ResolvedSuccessfully)}");
        }

        [DataTestMethod]
        [DataRow("testString1", 5)]
        [DataRow("testString2", 6)]
        [DataRow("testString3", 7)]
        public void ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters(
            string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [DataTestMethod]
        [DataRow("testString1", 5)]
        [DataRow("testString2", 6)]
        [DataRow("testString3", 7)]
        public async Task ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [DataTestMethod]
        [DataSource(nameof(TestCases))]
        public void ReadSnapshotFullName_ResolveTheoryDataSnapshotName_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheoryDataSnapshotName_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [DataTestMethod]
        [DataSource(nameof(TestCases))]
        public async Task ReadSnapshotFullName_ResolveTheoryDataSnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheoryDataSnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [DataTestMethod]
        [DataRow("testString1", 5)]
        [DataRow("testString2", 6)]
        [DataRow("testString3", null)]
        public async Task ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithNullParameters(
           string param1, int? param2)
        {
            // arrange
            var snapshotFullNameResolver = new MSTestSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            Assert.AreEqual(snapshotFullName.Filename,
                $"{nameof(MSTestSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithNullParameters)}" +
                $"_{param1}_{(param2 is null ? "null" : param2)}");
        }

        private static object[] TestCases => new object[]
        {
            new object[] { "testString1", 5 },
            new object[] { "testString2", 6 },
            new object[] { "testString3", 7 }
        };
    }
}
