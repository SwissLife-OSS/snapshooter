using System;
using System.Collections.Generic;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.AcceptByteArray
{
    public partial class SnapshotTests
    {
        #region Accept ByteArray Tests

        [Fact]
        public void Match_AcceptByteArray_AsByteArray_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArray_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArray_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByteArray_WithRightType_Successful<byte[]>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArray_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArray_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]>(
                insertNull: true,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArray_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByteArray_WithRightType_Successful<byte[]>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept ByteArray Nullable Tests
        
        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptByteArray_AsByteArrayNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptByteArray_WithRightType_Successful<byte[]?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept ByteArray As Object Tests

        [Fact]
        public void Match_AcceptByteArray_AsObject_SuccessfulAccepted()
        {
            Match_AcceptByteArray_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsObject_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptByteArray_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByteArray_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptByteArray_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsObjectNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptByteArray_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion


        #region Accept ByteArray As String Tests

        [Fact]
        public void Match_AcceptByteArray_AsString_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<string>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsString_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<string>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptByteArray_AsStringNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<string?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptByteArray_AsStringNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptByteArray_WithRightType_Successful<string?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion

        #region Accept ByteArray Not As Other Types Tests

        [Fact]
        public void Match_AcceptByteArray_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptByteArray_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptByteArray_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptByteArray_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptByteArray_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptByteArray_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptByteArray_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptByteArray_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptByteArray_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptByteArray_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }
        
        [Fact]
        public void Match_AcceptByteArray_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptByteArray_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptByteArray_AsListByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<List<byte[]>>(
                typeName: "List<Byte[]>");
        }

        [Fact]
        public void Match_AcceptByteArray_AsListByteArrayNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<List<byte[]?>>(
                typeName: "List<Byte[]>");
        }

        [Fact]
        public void Match_AcceptByteArray_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptByteArray_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptByteArray_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptByteArrayField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptByteArray_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<byte[]?> testee = CreateByteArrayAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptByteArrayField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {
            AcceptTypeTestee<byte[]?> testee = CreateByteArrayAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, byte[]?>(
                insertNull, keepOriginalValue, typeName, testee, TryToBase64String(testee));
        }
        
        private AcceptTypeTestee<byte[]?> CreateByteArrayAcceptTestee(bool insertNull)
        {
            byte[]? ByteArrayValue = insertNull ? null : new byte[] { 0x01, 0xFF, 0x00, 0x1A};

            AcceptTypeTestee<byte[]?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(ByteArrayValue);

            return testee;
        }

        private string? TryToBase64String(AcceptTypeTestee<byte[]?> testee)
        {
            if (testee.Value is { } testeeValue)
            {
                return Convert.ToBase64String(testeeValue);
            }

            return null;
        }

        #endregion
    }
}
