using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;
using Xunit.Sdk;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption
{
    public class SnapshotTests
    {
        #region Accept Decimal Field Tests

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDecimal_WithRightType_Successful<decimal>();
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_SnapshotCreated()
        {            
            // arrange & act & assert
            Compare_Verified_Vs_NewCreated_Snapshot(
                () => Match_AcceptDecimal_WithRightType_Successful<decimal>());
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimalNullable_SnapshotCreated()
        {
            // arrange
            SnapshotFullName originalFullName = Snapshot.FullName();
            SnapshotFileHandler snapshotFileHandler = new SnapshotFileHandler();

            snapshotFileHandler.DeleteSnapshot(originalFullName);

            // act
            Match_AcceptDecimal_WithRightType_Successful<decimal?>();

            // assert
            Assert.Equal(
                snapshotFileHandler.ReadSnapshot(Snapshot.FullName(
                    SnapshotNameExtension.Create("Verified"))),
                snapshotFileHandler.ReadSnapshot(originalFullName));
        }

        

        [Fact]
        public void Match_AcceptDecimalField_AsDecimalNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDecimal_WithRightType_Successful<decimal?>();
        }

        [Fact]
        public void Match_AcceptDecimalField_AsDecimal_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptDecimalFieldKeepOriginalValue_WithRightType_Successful<decimal>();
        }
        
        [Fact]
        public void Match_AcceptDecimalField_AsDecimalNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptDecimalFieldKeepOriginalValue_WithRightType_Successful<decimal?>(true);
        }

        [Fact]
        public void Match_AcceptDecimalField_AsDouble_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<double>();
        }

        [Fact]
        public void Match_AcceptDecimalField_AsDoubleNullable_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<double?>();
        }

        [Fact]
        public void Match_AcceptDecimalField_AsFloat_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<float>();
        }

        [Fact]
        public void Match_AcceptDecimalField_AsFloatNullable_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<float?>();
        }

        [Fact]
        public void Match_AcceptDecimalField_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<string>("String");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<Guid>("Guid");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<Guid?>("Guid?");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<int>("Integer");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<short>("Short");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<long>("Long");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<DateTime>("DateTime");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<bool>("Boolean");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<byte>("Byte");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<byte[]>("Byte[]");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsListDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<List<decimal>>("List<Decimal>");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<List<decimal?>>("List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptDecimalField_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<TestPerson>("TestPerson");
        }

        private static void Match_AcceptDecimal_WithRightType_Successful<T>(
            bool insertNull = false)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder
                .TestPersonSandraSchneider()
                .WithSize(insertNull ? null : 1.756m)
                .Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions
                    .AcceptField<T>("Size"));
        }

        private static void Compare_Verified_Vs_NewCreated_Snapshot(Action action)
        {
            // arrange
            SnapshotFullName originalFullName = Snapshot.FullName();
            SnapshotFileHandler snapshotFileHandler = new SnapshotFileHandler();

            snapshotFileHandler.DeleteSnapshot(originalFullName);

            // act
            action();

            // assert
            Assert.Equal(
                snapshotFileHandler.ReadSnapshot(Snapshot.FullName(
                    SnapshotNameExtension.Create("Verified"))),
                snapshotFileHandler.ReadSnapshot(originalFullName));
        }

        private static void Match_AcceptDecimalFieldKeepOriginalValue_WithRightType_Successful<T>(
            bool insertNull = false)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder
                .TestPersonSandraSchneider()
                .WithSize(insertNull ? null : 1.98m)
                .Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions
                    .AcceptField<T>("Size", keepOriginalValue: true));
        }

        private void Match_AcceptDecimalField_WithWrongType_ThrowsException<T>(string typeName)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder
                .TestPersonSandraSchneider()
                .WithSize(1.756m)
                .Build();

            // act
            Action action = () => Snapshot.Match(
                testPerson, matchOptions => matchOptions
                    .AcceptField<T>("Size"));

            // assert
            SnapshotFieldException exception =
                Assert.Throws<SnapshotFieldException>(action);

            Assert.Equal(exception.Message,
                $"Accept match option failed, " +
                $"because the field 'Size' with value " +
                $"'1.756' is not of type {typeName}.");
        }

        // test that the snapshot format is always correct if newly generated.

        // DONE test all with decimal, double, float
        // DONE test all with int, short, long etc.
        // DONE test all with string,
        // DONE test all with Guid
        // DONE test all with DateTime
        // DONE test all with bool,
        // DONE test all with byte 
        // DONE test all with byte[]
        // DONE test all with List decimal
        // test all with List empty
        // DONE test all with complex object
        // DONE test with a value and nullable decimal, green
        // test with a null value and a decimal
        // test with a null value and a nullable decimal

        // test if the first time the snapshot is wrong, then only write the snapshot to the mismatch folder.
        // test if the first snapshot is with accept double and the secound with accept decimal, then the snapshot has to be overwritten.

        

        

        //// test acceptField with all scalar types (DateTime, int, short, bool etc.) with keepOriginal
        //// test acceptField with a complex type also with keepOriginal.
        //// test acceptField with an array/list/dictionary field also with keepOriginal.
        //// test a existing snapshot that its overwritten if the original flag changes
        //// test is the object to snapshot is a scalar "string, int etc." --> in already exising region for scalar tests.

        // test AcceptAllFields und mit **.

        // test ein snapshot existiert schon mit ignore, jetzt wird noch ein accept hinzugefügt, jetzt sollte der snapshot überschrieben werden.
        // test several accepts in one snapshot
        //// unbedingt ein test wo kein snapshot existiert, snapshot ein scalar und snapshot mit einem Pfad der nicht stimmt.

        //[Fact]
        //public void Match_AcceptComplexAddressField_SuccessfulIgnored()
        //{
        //    // arrange
        //    TestPerson testPerson = TestDataBuilder
        //        .TestPersonSandraSchneider()
        //        .WithSize(1.5m)
        //        .Build();

        //    // act & assert
        //    // AcceptAny<bool>("Bar", true)
        //    // AcceptAny<int>("Id", 15)
        //    Snapshot.Match(
        //        testPerson, matchOptions => matchOptions.AcceptField<TestAddress>("Address", true));
        //}

        #endregion
    }
}
