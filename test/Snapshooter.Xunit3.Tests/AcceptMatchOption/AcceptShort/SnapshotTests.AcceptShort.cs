using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.Short
{
    public partial class SnapshotTests
    {
        #region Accept Short Tests

        [Fact]
        public void Match_AcceptShort_AsShort_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptShort_WithRightType_Successful<short>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsShort_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<short>(
                insertNull: true,
                keepOriginalValue: false,
                "Short");
        }

        [Fact]
        public void Match_AcceptShort_AsShort_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptShort_WithRightType_Successful<short>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptShort_AsShort_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptShort_WithRightType_Successful<short>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptShort_AsShort_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<short>(
                insertNull: true,
                keepOriginalValue: true,
                "Short");
        }

        [Fact]
        public void Match_AcceptShort_AsShort_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptShort_WithRightType_Successful<short>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Short Nullable Tests
        
        [Fact]
        public void Match_AcceptShort_AsShortNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptShort_WithRightType_Successful<short?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsShortNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptShort_WithRightType_Successful<short?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsShortNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptShort_WithRightType_Successful<short?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptShort_AsShortNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptShort_WithRightType_Successful<short?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptShort_AsShortNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptShort_WithRightType_Successful<short?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptShort_AsShortNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptShort_WithRightType_Successful<short?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptShort_AsShortNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptShort_WithRightType_Successful<short?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptShort_AsShortNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptShort_WithRightType_Successful<short?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Short As Object Tests

        [Fact]
        public void Match_AcceptShort_AsObject_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Short As Int Tests

        [Fact]
        public void Match_AcceptShort_AsInt_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<int>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsIntNullable_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<int?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Short As Byte Tests

        [Fact]
        public void Match_AcceptShort_AsByte_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<byte>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsByteNullable_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<byte?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Short As Long Tests

        [Fact]
        public void Match_AcceptShort_AsLong_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<long>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptShort_AsLongNullable_SuccessfulAccepted()
        {
            Match_AcceptShort_WithRightType_Successful<long?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Short Not As Other Types Tests
        
        [Fact]
        public void Match_AcceptShort_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptShort_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptShort_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptShort_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptShort_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptShort_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptShort_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptShort_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptShort_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptShort_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptShort_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptShort_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptShort_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptShort_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptShort_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptShort_AsListShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<List<short>>(
                typeName: "List<Short>");
        }

        [Fact]
        public void Match_AcceptShort_AsListShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<List<short?>>(
                typeName: "List<Short?>");
        }

        [Fact]
        public void Match_AcceptShort_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptShort_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptShort_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptShortField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptShort_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<short?> testee = CreateShortAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptShortField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<short?> testee = CreateShortAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, short?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<short?> CreateShortAcceptTestee(bool insertNull)
        {
            short? shortNumber = insertNull ? null : 9;

            AcceptTypeTestee<short?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(shortNumber);

            return testee;
        }

        #endregion
    }
}
