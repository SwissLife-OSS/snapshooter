using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snapshooter.TUnit.Tests
{
    public class TUnitSnapshotFullNameReaderTests
    {
        [Test]
        public async Task ReadSnapshotFullName_ResolveTestSnapshotFullName_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFullNameResolver = new TUnitSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Assert.That(snapshotFullName.Filename).IsEqualTo(
                $"{nameof(TUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTestSnapshotFullName_ResolvedSuccessfully)}");
        }

        [Test]
        public async Task ReadSnapshotFullName_ResolveTestSnapshotFullNameAsync_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFullNameResolver = new TUnitSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            await Assert.That(snapshotFullName.Filename).IsEqualTo(
                $"{nameof(TUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTestSnapshotFullNameAsync_ResolvedSuccessfully)}");
        }

        #pragma warning disable xUnit1026 // Theory methods should use all of their parameters

        [Test]
        [Arguments("testString1", 5)]
        [Arguments("testString2", 6)]
        [Arguments("testString3", 7)]
        public async Task ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters(
            string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new TUnitSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Assert.That(snapshotFullName.Filename).IsEqualTo(
                $"{nameof(TUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [Test]
        [Arguments("testString1", 5)]
        [Arguments("testString2", 6)]
        [Arguments("testString3", 7)]
        public async Task ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new TUnitSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            await Assert.That(snapshotFullName.Filename).IsEqualTo(
                $"{nameof(TUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [Test]
        [MethodDataSource(nameof(TestCases))]
        public async Task ReadSnapshotFullName_ResolveTheoryDataSnapshotName_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new TUnitSnapshotFullNameReader();

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Assert.That(snapshotFullName.Filename).IsEqualTo(
                $"{nameof(TUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheoryDataSnapshotName_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        [Test]
        [MethodDataSource(nameof(TestCases))]
        public async Task ReadSnapshotFullName_ResolveTheoryDataSnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFullNameResolver = new TUnitSnapshotFullNameReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            // assert
            await Task.Delay(1);
            await Assert.That(snapshotFullName.Filename).IsEqualTo(
                $"{nameof(TUnitSnapshotFullNameReaderTests)}." +
                $"{nameof(ReadSnapshotFullName_ResolveTheoryDataSnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}" +
                $"_{param1}_{param2}");
        }

        public static IEnumerable<(string, int)> TestCases()
        {
            yield return ("testString1", 5);
            yield return ("testString2", 6);
            yield return ("testString3", 7);
        }
    }
}
