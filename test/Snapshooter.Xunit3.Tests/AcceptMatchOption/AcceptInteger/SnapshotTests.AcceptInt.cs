using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.Int
{
    public partial class SnapshotTests
    {
        #region Accept Int Tests

        [Fact]
        public void Match_AcceptInt_AsInt_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptInt_WithRightType_Successful<int>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsInt_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<int>(
                insertNull: true,
                keepOriginalValue: false,
                "Integer");
        }

        [Fact]
        public void Match_AcceptInt_AsInt_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptInt_WithRightType_Successful<int>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptInt_AsInt_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptInt_WithRightType_Successful<int>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptInt_AsInt_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<int>(
                insertNull: true,
                keepOriginalValue: true,
                "Integer");
        }

        [Fact]
        public void Match_AcceptInt_AsInt_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptInt_WithRightType_Successful<int>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Int Nullable Tests
        
        [Fact]
        public void Match_AcceptInt_AsIntNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptInt_WithRightType_Successful<int?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsIntNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptInt_WithRightType_Successful<int?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsIntNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptInt_WithRightType_Successful<int?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptInt_AsIntNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptInt_WithRightType_Successful<int?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptInt_AsIntNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptInt_WithRightType_Successful<int?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptInt_AsIntNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptInt_WithRightType_Successful<int?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptInt_AsIntNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptInt_WithRightType_Successful<int?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptInt_AsIntNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptInt_WithRightType_Successful<int?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Int As Object Tests

        [Fact]
        public void Match_AcceptInt_AsObject_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Int As Long Tests

        [Fact]
        public void Match_AcceptInt_AsLong_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<long>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsLongNullable_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<long?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Int As Short Tests

        [Fact]
        public void Match_AcceptInt_AsShort_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<short>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsShortNullable_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<short?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Int As Byte Tests

        [Fact]
        public void Match_AcceptInt_AsByte_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<byte>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptInt_AsByteNullable_SuccessfulAccepted()
        {
            Match_AcceptInt_WithRightType_Successful<byte?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Int Not As Other Types Tests

        [Fact]
        public void Match_AcceptInt_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptInt_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptInt_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptInt_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptInt_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptInt_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptInt_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptInt_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptInt_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptInt_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptInt_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptInt_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptInt_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptInt_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }
        
        [Fact]
        public void Match_AcceptInt_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptInt_AsListInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<List<int>>(
                typeName: "List<Integer>");
        }

        [Fact]
        public void Match_AcceptInt_AsListIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<List<int?>>(
                typeName: "List<Integer?>");
        }

        [Fact]
        public void Match_AcceptInt_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptInt_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptInt_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptIntField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptInt_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<int?> testee = CreateIntAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptIntField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<int?> testee = CreateIntAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, int?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<int?> CreateIntAcceptTestee(bool insertNull)
        {
            int? intNumber = insertNull ? null : 21;

            AcceptTypeTestee<int?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(intNumber);

            return testee;
        }

        #endregion
    }
}
