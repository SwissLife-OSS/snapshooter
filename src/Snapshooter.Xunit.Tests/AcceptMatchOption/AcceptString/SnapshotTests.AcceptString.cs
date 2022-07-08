using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.String
{
    public partial class SnapshotTests
    {
        #region Accept String Tests

        [Fact]
        public void Match_AcceptString_AsString_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptString_AsString_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptString_AsString_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptString_WithRightType_Successful<string>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptString_AsString_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptString_AsString_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string>(
                insertNull: true,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptString_AsString_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptString_WithRightType_Successful<string>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept String Nullable Tests
        
        [Fact]
        public void Match_AcceptString_AsStringNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptString_AsStringNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptString_AsStringNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptString_WithRightType_Successful<string?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptString_AsStringNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptString_WithRightType_Successful<string?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptString_AsStringNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptString_AsStringNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptString_WithRightType_Successful<string?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptString_AsStringNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptString_WithRightType_Successful<string?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptString_AsStringNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptString_WithRightType_Successful<string?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept String As Object Tests

        [Fact]
        public void Match_AcceptString_AsObject_SuccessfulAccepted()
        {
            Match_AcceptString_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptString_AsObject_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptString_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptString_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptString_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptString_AsObjectNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptString_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion
        
        #region Accept String Not As Other Types Tests
        
        [Fact]
        public void Match_AcceptString_AsInt_ThrowsException()
        {
            Match_AcceptStringField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptString_AsIntNullable_ThrowsException()
        {
            Match_AcceptStringField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptString_AsLong_ThrowsException()
        {
            Match_AcceptStringField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptString_AsLongNullable_ThrowsException()
        {
            Match_AcceptStringField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptString_AsShort_ThrowsException()
        {
            Match_AcceptStringField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptString_AsShortNullable_ThrowsException()
        {
            Match_AcceptStringField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptString_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptString_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptString_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptString_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptString_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptString_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptString_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptString_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptString_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptString_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptString_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptString_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptString_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptString_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptString_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptString_AsListString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<List<string>>(
                typeName: "List<String>");
        }

        [Fact]
        public void Match_AcceptString_AsListStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<List<string?>>(
                typeName: "List<String>");
        }

        [Fact]
        public void Match_AcceptString_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptString_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptString_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptStringField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptString_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<string?> testee = CreateStringAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptStringField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<string?> testee = CreateStringAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, string?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<string?> CreateStringAcceptTestee(bool insertNull)
        {
            string? textValue = insertNull ? null : "foobar";

            AcceptTypeTestee<string?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(textValue);

            return testee;
        }

        #endregion
    }
}
