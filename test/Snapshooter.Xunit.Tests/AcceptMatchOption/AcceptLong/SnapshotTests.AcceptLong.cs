using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.Long
{
    public partial class SnapshotTests
    {
        #region Accept Long Tests

        [Fact]
        public void Match_AcceptLong_AsLong_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptLong_WithRightType_Successful<long>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsLong_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<long>(
                insertNull: true,
                keepOriginalValue: false,
                "Long");
        }

        [Fact]
        public void Match_AcceptLong_AsLong_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptLong_WithRightType_Successful<long>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptLong_AsLong_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptLong_WithRightType_Successful<long>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptLong_AsLong_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<long>(
                insertNull: true,
                keepOriginalValue: true,
                "Long");
        }

        [Fact]
        public void Match_AcceptLong_AsLong_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptLong_WithRightType_Successful<long>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Long Nullable Tests
        
        [Fact]
        public void Match_AcceptLong_AsLongNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptLong_WithRightType_Successful<long?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsLongNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptLong_WithRightType_Successful<long?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsLongNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptLong_WithRightType_Successful<long?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptLong_AsLongNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptLong_WithRightType_Successful<long?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptLong_AsLongNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptLong_WithRightType_Successful<long?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptLong_AsLongNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptLong_WithRightType_Successful<long?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptLong_AsLongNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptLong_WithRightType_Successful<long?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptLong_AsLongNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptLong_WithRightType_Successful<long?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Long As Object Tests

        [Fact]
        public void Match_AcceptLong_AsObject_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Long As Int Tests

        [Fact]
        public void Match_AcceptLong_AsInt_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<int>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsIntNullable_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<int?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Long As Short Tests

        [Fact]
        public void Match_AcceptLong_AsShort_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<short>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsShortNullable_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<short?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Long As Byte Tests

        [Fact]
        public void Match_AcceptLong_AsByte_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<byte>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptLong_AsByteNullable_SuccessfulAccepted()
        {
            Match_AcceptLong_WithRightType_Successful<byte?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Long Not As Other Types Tests

        [Fact]
        public void Match_AcceptLong_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptLong_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptLong_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptLong_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptLong_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptLong_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptLong_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptLong_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptLong_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptLong_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptLong_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptLong_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptLong_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptLong_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptLong_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptLong_AsListLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<List<long>>(
                typeName: "List<Long>");
        }

        [Fact]
        public void Match_AcceptLong_AsListLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<List<long?>>(
                typeName: "List<Long?>");
        }

        [Fact]
        public void Match_AcceptLong_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptLong_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptLong_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptLongField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptLong_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<long?> testee = CreateLongAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptLongField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<long?> testee = CreateLongAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, long?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<long?> CreateLongAcceptTestee(bool insertNull)
        {
            long? longNumber = insertNull ? null : 21;

            AcceptTypeTestee<long?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(longNumber);

            return testee;
        }

        #endregion
    }
}
