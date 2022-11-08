using System;
using System.Collections.Generic;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.AcceptBoolean
{
    public partial class SnapshotTests
    {
        #region Accept Boolean Tests

        [Fact]
        public void Match_AcceptBoolean_AsBoolean_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptBoolean_WithRightType_Successful<bool>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptBoolean_AsBoolean_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<bool>(
                insertNull: true,
                keepOriginalValue: false,
                "Boolean");
        }

        [Fact]
        public void Match_AcceptBoolean_AsBoolean_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptBoolean_WithRightType_Successful<bool>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptBoolean_AsBoolean_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptBoolean_WithRightType_Successful<bool>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptBoolean_AsBoolean_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<bool>(
                insertNull: true,
                keepOriginalValue: true,
                "Boolean");
        }

        [Fact]
        public void Match_AcceptBoolean_AsBoolean_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptBoolean_WithRightType_Successful<bool>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Boolean Nullable Tests
        
        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptBoolean_WithRightType_Successful<bool?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptBoolean_WithRightType_Successful<bool?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptBoolean_WithRightType_Successful<bool?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptBoolean_WithRightType_Successful<bool?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptBoolean_WithRightType_Successful<bool?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptBoolean_WithRightType_Successful<bool?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptBoolean_WithRightType_Successful<bool?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptBoolean_AsBooleanNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptBoolean_WithRightType_Successful<bool?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Boolean As Object Tests

        [Fact]
        public void Match_AcceptBoolean_AsObject_SuccessfulAccepted()
        {
            Match_AcceptBoolean_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptBoolean_AsObject_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptBoolean_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptBoolean_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptBoolean_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptBoolean_AsObjectNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptBoolean_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion
        
        #region Accept Boolean Not As Other Types Tests

        [Fact]
        public void Match_AcceptBoolean_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptBoolean_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptBoolean_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptBoolean_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptBoolean_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptBoolean_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptBoolean_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptBoolean_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptBoolean_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptBoolean_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptBoolean_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptBoolean_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptBoolean_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptBoolean_AsListBoolean_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<List<bool>>(
                typeName: "List<Boolean>");
        }

        [Fact]
        public void Match_AcceptBoolean_AsListBooleanNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<List<bool?>>(
                typeName: "List<Boolean?>");
        }

        [Fact]
        public void Match_AcceptBoolean_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptBoolean_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptBoolean_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptBooleanField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptBoolean_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<bool?> testee = CreateBooleanAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptBooleanField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<bool?> testee = CreateBooleanAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, bool?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<bool?> CreateBooleanAcceptTestee(bool insertNull)
        {
            bool? BooleanValue = insertNull ? null : true;

            AcceptTypeTestee<bool?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(BooleanValue);

            return testee;
        }

        #endregion
    }
}
