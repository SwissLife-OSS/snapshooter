using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.Decimal
{
    public class SnapshotTests
    {
        #region Accept Decimal Tests

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDecimal_WithRightType_Successful<decimal>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<decimal>(
                insertNull: true,
                keepOriginalValue: false,
                "Decimal");
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_SnapshotCreated()
        {            
            // arrange & act & assert
            Compare_Verified_Vs_NewCreated_Snapshot(
                () => Match_AcceptDecimal_WithRightType_Successful<decimal>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDecimal_WithRightType_Successful<decimal>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<decimal>(
                insertNull: true,
                keepOriginalValue: true,
                "Decimal");
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimal_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            Compare_Verified_Vs_NewCreated_Snapshot(
                () => Match_AcceptDecimal_WithRightType_Successful<decimal>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Decimal Nullable Tests

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Match_AcceptDecimal_AsDecimalNullable_SuccessfulAccepted(bool insertNull)
        {
            // arrange & act & assert
            Match_AcceptDecimal_WithRightType_Successful<decimal?>(
                insertNull: insertNull,
                keepOriginalValue: false);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Match_AcceptDecimal_AsDecimalNullable_SnapshotCreated(bool insertNull)
        {
            // arrange & act & assert
            Compare_Verified_Vs_NewCreated_Snapshot(
                () => Match_AcceptDecimal_WithRightType_Successful<decimal?>(
                    insertNull: insertNull,
                    keepOriginalValue: false));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Match_AcceptDecimal_AsDecimalNullable_KeepOriginal_SuccessfulAccepted(
            bool insertNull)
        {
            // arrange & act & assert
            Match_AcceptDecimal_WithRightType_Successful<decimal?>(
                insertNull: insertNull,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimalNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            Compare_Verified_Vs_NewCreated_Snapshot(
                () => Match_AcceptDecimal_WithRightType_Successful<decimal?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        [Fact]
        public void Match_AcceptDecimal_AsDecimalNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            Compare_Verified_Vs_NewCreated_Snapshot(
                () => Match_AcceptDecimal_WithRightType_Successful<decimal?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Decimal As Double Tests

        [Fact]
        public void Match_AcceptDecimal_AsDouble_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<double>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDecimal_AsDoubleNullable_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<double?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Decimal As Float Tests

        [Fact]
        public void Match_AcceptDecimal_AsFloat_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<float>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDecimal_AsFloatNullable_SuccessfulAccepted()
        {
            Match_AcceptDecimal_WithRightType_Successful<float?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Decimal Not As Other Types Tests
        
        [Fact]
        public void Match_AcceptDecimal_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptDecimal_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptDecimal_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptDecimal_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptDecimal_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptDecimal_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptDecimal_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptDecimal_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptDecimal_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptDecimal_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptDecimal_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptDecimal_AsListDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<List<decimal>>(
                typeName: "List<Decimal>");
        }

        [Fact]
        public void Match_AcceptDecimal_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptDecimal_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDecimalField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        #endregion

        #region Private Test Helpers

        private static void Match_AcceptDecimal_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder
                .TestPersonSandraSchneider()
                .WithSize(insertNull ? null : 1.7569876543210123m)
                .Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions
                    .AcceptField<T>("Size", keepOriginalValue: keepOriginalValue));
        }

        private void Match_AcceptDecimalField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {
            // arrange
            TestPerson testPerson = TestDataBuilder
                .TestPersonSandraSchneider()
                .WithSize(insertNull ? null : 1.756m)
                .Build();

            // act
            Action action = () => Snapshot.Match(
                testPerson, matchOptions => matchOptions
                    .AcceptField<T>("Size", keepOriginalValue: keepOriginalValue));

            // assert
            SnapshotFieldException exception =
                Assert.Throws<SnapshotFieldException>(action);

            if (insertNull)
            {
                Assert.Equal(exception.Message,
                   $"Accept match option failed, " +
                   $"because the field value of 'Size' is 'Null', " +
                   $"but defined accept type '{typeName}' is not nullable.");
            }
            else
            {
                Assert.Equal(exception.Message,
                    $"Accept match option failed, " +
                    $"because the field value of 'Size' is '1.756', " +
                    $"and therefore not of type '{typeName}'.");
            }
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

        #endregion

        // DONE test that the snapshot format is always correct if newly generated.

        // DONE test all with decimal, double, float
        // DONE test all with int, short, long etc.
        // DONE test all with string,
        // DONE test all with Guid
        // DONE test all with DateTime
        // DONE test all with bool,
        // DONE test all with byte 
        // DONE test all with byte[]--->
        // DONE test all with List decimal, class, recors -->
        // test all with List empty -->
        // DONE test all with complex class object --> 1. AcceptClass
        // DONE test with a value and nullable decimal, green
        // DONE test with a null value and a decimal --> error
        // DONE test with a null value and a nullable decimal

        // DONE test with a specific enum (CountryCode = de) --> 2. Accecpt Enum
        // test with a struct --> 3. Accept Struct
        // test with a record --> 4. Accept Record

        // DONE test if the first time the snapshot is wrong, then only write the snapshot to the mismatch folder.
        // test if the first snapshot is with accept double and the secound with accept decimal, then the snapshot has to be overwritten.
        // test if the snapshot is with an ignor and the user changes it to an accept, then the snapshot has to be overwritten.

        // a list with values, replace all values with one json path.
        // a test where multiple pathes are used, but not everyone fits to a field


        //// test acceptField with all/many scalar types (DateTime, int, short, bool etc.) with keepOriginal
        //// test acceptField with a complex type also with keepOriginal.
        //// test acceptField with an array/list/dictionary field also with keepOriginal.
        //// test a existing snapshot that its overwritten if the original flag changes
        //// test is the object to snapshot is a scalar "string, int etc." --> in already exising region for scalar tests.

        // test AcceptAllFields und mit **.
        // -- here create a **. test and a AcceptAllFields test.

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

    }
}
