using System;
using System.Collections.Generic;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.AcceptByte
{
    public partial class SnapshotTests
    {
        #region Accept Byte Tests

        [Fact]
        public void Match_AcceptByte_AsByte_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByte_WithRightType_Successful<byte>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsByte_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<byte>(
                insertNull: true,
                keepOriginalValue: false,
                "Byte");
        }

        [Fact]
        public void Match_AcceptByte_AsByte_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByte_WithRightType_Successful<byte>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptByte_AsByte_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByte_WithRightType_Successful<byte>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByte_AsByte_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<byte>(
                insertNull: true,
                keepOriginalValue: true,
                "Byte");
        }

        [Fact]
        public void Match_AcceptByte_AsByte_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByte_WithRightType_Successful<byte>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Byte Nullable Tests
        
        [Fact]
        public void Match_AcceptByte_AsByteNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByte_WithRightType_Successful<byte?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsByteNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByte_WithRightType_Successful<byte?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsByteNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByte_WithRightType_Successful<byte?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptByte_AsByteNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByte_WithRightType_Successful<byte?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptByte_AsByteNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByte_WithRightType_Successful<byte?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByte_AsByteNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByte_WithRightType_Successful<byte?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptByte_AsByteNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByte_WithRightType_Successful<byte?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptByte_AsByteNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByte_WithRightType_Successful<byte?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Byte As Object Tests

        [Fact]
        public void Match_AcceptByte_AsObject_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsObject_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByte_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsObjectNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion

        #region Accept Int As Long Tests

        [Fact]
        public void Match_AcceptByte_AsLong_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<long>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsLongNullable_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<long?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Int As Short Tests

        [Fact]
        public void Match_AcceptByte_AsShort_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<short>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsShortNullable_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<short?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Long As Int Tests

        [Fact]
        public void Match_AcceptByte_AsInt_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<int>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByte_AsIntNullable_SuccessfulAccepted()
        {
            Match_AcceptByte_WithRightType_Successful<int?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Byte Not As Other Types Tests

        [Fact]
        public void Match_AcceptByte_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptByte_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptByte_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptByte_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptByte_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptByte_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptByte_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptByte_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptByte_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptByte_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptByte_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptByte_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptByte_AsBoolean_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptByte_AsBooleanNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptByte_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptByte_AsListByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<List<byte>>(
                typeName: "List<Byte>");
        }

        [Fact]
        public void Match_AcceptByte_AsListByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<List<byte?>>(
                typeName: "List<Byte?>");
        }

        [Fact]
        public void Match_AcceptByte_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptByte_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptByte_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptByte_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<byte?> testee = CreateByteAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptByteField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<byte?> testee = CreateByteAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, byte?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<byte?> CreateByteAcceptTestee(bool insertNull)
        {
            byte? ByteValue = insertNull ? null : 0x55;

            AcceptTypeTestee<byte?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(ByteValue);

            return testee;
        }

        #endregion
    }
}
