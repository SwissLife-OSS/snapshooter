using System;
using System.Threading.Tasks;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;
using Xunit.Sdk;

namespace Snapshooter.Xunit.Tests
{
    public partial class SnapshotTests
    {
        #region Match Snapshot - Simple Async Tests

        [Fact]
        public async Task Match_FactAsyncSingleSnapshot_SuccessfulMatch()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Snapshot.Match(testPerson);

            // assert
            await Task.Delay(1);
        }

        [Fact]
        public async Task Match_FactAsyncMatchSingleSnapshot_OneFieldNotEqual()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithAge(5).Build();

            await Task.Delay(1);

            // act
            Action match = () => Snapshot.Match(testPerson);

            // assert
            Assert.Throws<EqualException>(match);

            await Task.Delay(1);
        }

        [Theory]
        [InlineData(36, 189.45)]
        [InlineData(42, 173.16)]
        [InlineData(19, 193.02)]
        public async Task Match_TheoryAsyncMatchSingleSnapshot_SuccessfulMatch(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            await Task.Delay(1);

            // act
            Snapshot.Match(testPerson, SnapshotNameExtension.Create(age, size));

            // assert
            await Task.Delay(1);
        }

        [Theory]
        [InlineData(34, 175)]
        [InlineData(36, 177)]
        [InlineData(37, 178)]
        public async Task Match_TheoryAsyncMatchSingleSnapshot_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.US;

            await Task.Delay(1);

            // act
            Assert.Throws<EqualException>(() => Snapshot.Match(
                testPerson, SnapshotNameExtension.Create(age, size)));

            // assert
            await Task.Delay(1);
        }

        #endregion

        #region Match Snapshot - In Async Helper Method Tests

        [Fact]
        public async Task Match_FactMatchSnapshotInAsncMethod_SuccessfulMatch()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            await Task.Delay(1);

            SnapshotFullName snapshotFullName = Snapshot.FullName();

            // act
            await AsyncMatchWithFullName(testPerson, snapshotFullName);

            // assert
            await Task.Delay(1);
        }

        [Fact]
        public async Task Match_FactMatchSnapshotInAsncMethodWithImplcName_SuccessfulMatch()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            await Task.Delay(1);

            Snapshot.FullName();

            // act
            await AsyncMatchWithImplicitFullName(testPerson);

            // assert
            await Task.Delay(1);
        }

        [Fact]
        public async Task Match_FactMatchSnapshotInAsncMethod_OneFieldNotEqual()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                                                   .WithAge(4055).Build();
            
            SnapshotFullName snapshotFullName = Snapshot.FullName();

            // act
            Func<Task> asyncMatch = () => AsyncMatchWithFullName(testPerson, snapshotFullName);

            // assert
            EqualException exception =
                await Assert.ThrowsAsync<EqualException>(asyncMatch);
            Assert.Contains("4055", exception.Message);

            await Task.Delay(1);
        }

        [Fact]
        public async Task Match_FactMatchSnapshotInAsncMethod_ThrowsSnapshotTestException()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Func<Task> asyncMatch = () => AsyncMatch(testPerson);

            // assert
            SnapshotTestException exception = 
                await Assert.ThrowsAsync<SnapshotTestException>(asyncMatch);
            Assert.Contains("async", exception.Message);

            await Task.Delay(1);
        }

        [Theory]
        [InlineData(36, 189.45)]
        public async Task Match_TheoryMatchSnapshotInAsncMethod_SuccessfulMatch(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();
            
            SnapshotFullName snapshotFullName = 
                Snapshot.FullName(SnapshotNameExtension.Create(age, size));

            // act
            await AsyncMatchWithFullName(testPerson, snapshotFullName);

            // assert
            await Task.Delay(1);
        }

        [Theory]
        [InlineData(34, 175)]
        public async Task Match_TheoryMatchSnapshotInAsncMethod_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.US;

            SnapshotFullName snapshotFullName =
                Snapshot.FullName(SnapshotNameExtension.Create(age, size));

            // act
            Func<Task> asyncMatch = () => AsyncMatchWithFullName(testPerson, snapshotFullName);

            // assert
            EqualException exception =
                await Assert.ThrowsAsync<EqualException>(asyncMatch);
            Assert.Contains(CountryCode.US.ToString(), exception.Message);
        }

        [Theory]
        [InlineData(37, 178)]
        public async Task Match_TheoryMatchSnapshotInAsncMethod_ThrowsSnapshotTestException(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();
            
            await Task.Delay(1);

            // act
            Func<Task> asyncMatch = () => AsyncMatchWithNameExtension(
                testPerson, SnapshotNameExtension.Create(age, size));
            
            // assert
            SnapshotTestException exception =
                await Assert.ThrowsAsync<SnapshotTestException>(asyncMatch);
            Assert.Contains("async", exception.Message);
            
            await Task.Delay(1);
        }

        #endregion
        
        #region Match Snapshot - In Asyc Class Helper Method Tests

        [Fact]
        public async Task Match_FactMatchSnapshotInSeperateClassMethodAsync_SuccessfulMatch()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();
            
            SnapshotFullName snapshotFullName = Snapshot.FullName();

            var testClass = new AsyncSnapshotMatchTestClass();

            // act
            await testClass.AsyncMatchMethodWithFullName(testPerson, snapshotFullName);

            // assert
            await Task.Delay(1);
        }

        [Fact]
        public async Task Match_FactMatchSnapshotInSeperateClassMethodAsync_OneFieldNotEqual()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                                                   .WithAge(255).Build();

            SnapshotFullName snapshotFullName = Snapshot.FullName();

            var testClass = new AsyncSnapshotMatchTestClass();

            // act
            Func<Task> asyncMatch = 
                () => testClass.AsyncMatchMethodWithFullName(testPerson, snapshotFullName);

            // assert
            EqualException exception =
                await Assert.ThrowsAsync<EqualException>(asyncMatch);
            Assert.Contains("255", exception.Message);

            await Task.Delay(1);
        }

        [Fact]
        public async Task Match_FactMatchSnapshotInSeperateClassMethodAsync_ThrowsSnapshotTestException()
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            var testClass = new AsyncSnapshotMatchTestClass();

            // act
            Func<Task> asyncMatch = () => testClass.AsyncMatchMethod(testPerson);

            // assert
            SnapshotTestException exception =
                await Assert.ThrowsAsync<SnapshotTestException>(asyncMatch);
            Assert.Contains("async", exception.Message);

            await Task.Delay(1);
        }

        [Theory]
        [InlineData(36, 189.45)]
        public async Task Match_TheoryMatchSnapshotInSeperateClassMethodAsync_SuccessfulMatch(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            SnapshotFullName snapshotFullName =
                Snapshot.FullName(SnapshotNameExtension.Create(age, size));

            var testClass = new AsyncSnapshotMatchTestClass();

            // act
            await testClass.AsyncMatchMethodWithFullName(testPerson, snapshotFullName);

            // assert
            await Task.Delay(1);
        }

        [Theory]
        [InlineData(34, 175)]
        public async Task Match_TheoryMatchSnapshotInSeperateClassMethodAsync_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.EN;

            SnapshotFullName snapshotFullName =
                Snapshot.FullName(SnapshotNameExtension.Create(age, size));

            var testClass = new AsyncSnapshotMatchTestClass();

            // act
            Func<Task> asyncMatch = 
                () => testClass.AsyncMatchMethodWithFullName(testPerson, snapshotFullName);

            // assert
            EqualException exception =
                await Assert.ThrowsAsync<EqualException>(asyncMatch);
            Assert.Contains(CountryCode.EN.ToString(), exception.Message);
        }

        [Theory]
        [InlineData(37, 178)]
        public async Task Match_TheoryMatchSnapshotInSeperateClassMethodAsync_ThrowsSnapshotTestException(int age, decimal size)
        {
            // arrange
            await Task.Delay(1);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            var testClass = new AsyncSnapshotMatchTestClass();

            // act
            Func<Task> asyncMatch = () => testClass.AsyncMatchMethodWithNameExtension(
                testPerson, SnapshotNameExtension.Create(age, size));

            // assert
            SnapshotTestException exception =
                await Assert.ThrowsAsync<SnapshotTestException>(asyncMatch);
            Assert.Contains("async", exception.Message);

            await Task.Delay(1);
        }

        #endregion

        #region Test Helpers

        private async Task AsyncMatch(TestPerson testPerson)
        {
            await Task.Delay(1);

            Snapshot.Match(testPerson);

            await Task.Delay(1);
        }

        private async Task AsyncMatchWithFullName(
            TestPerson testPerson, SnapshotFullName snapshotFullName)
        {
            await Task.Delay(1);

            Snapshot.Match(testPerson, snapshotFullName);

            await Task.Delay(1);
        }

        private async Task AsyncMatchWithImplicitFullName(TestPerson testPerson)
        {
            await Task.Delay(1);

            Snapshot.Match(testPerson);

            await Task.Delay(1);
        }

        private async Task AsyncMatchWithNameExtension(
            TestPerson testPerson, SnapshotNameExtension snapshotNameExtension)
        {
            await Task.Delay(1);

            Snapshot.Match(testPerson, snapshotNameExtension);

            await Task.Delay(1);
        }

        private class AsyncSnapshotMatchTestClass
        {
            public async Task AsyncMatchMethod(TestPerson testPerson)
            {
                await Task.Delay(1);

                Snapshot.Match(testPerson);

                await Task.Delay(1);
            }

            public async Task AsyncMatchMethodWithFullName(
                TestPerson testPerson, SnapshotFullName snapshotFullName)
            {
                await Task.Delay(1);

                Snapshot.Match(testPerson, snapshotFullName);

                await Task.Delay(1);
            }

            public async Task AsyncMatchMethodWithNameExtension(
            TestPerson testPerson, SnapshotNameExtension snapshotNameExtension)
            {
                await Task.Delay(1);

                Snapshot.Match(testPerson, snapshotNameExtension);

                await Task.Delay(1);
            }
        }

        #endregion
    }
}
