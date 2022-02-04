using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.Float
{
    public partial class SnapshotTests
    {
        #region Accept Float Tests

        [Fact]
        public void Match_AcceptFloat_AsFloat_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptFloat_WithRightType_Successful<float>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptFloat_AsFloat_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<float>(
                insertNull: true,
                keepOriginalValue: false,
                "Float");
        }

        [Fact]
        public void Match_AcceptFloat_AsFloat_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptFloat_WithRightType_Successful<float>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptFloat_AsFloat_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptFloat_WithRightType_Successful<float>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptFloat_AsFloat_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<float>(
                insertNull: true,
                keepOriginalValue: true,
                "Float");
        }

        [Fact]
        public void Match_AcceptFloat_AsFloat_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptFloat_WithRightType_Successful<float>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Float Nullable Tests
        
        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptFloat_WithRightType_Successful<float?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptFloat_WithRightType_Successful<float?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptFloat_WithRightType_Successful<float?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptFloat_WithRightType_Successful<float?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptFloat_WithRightType_Successful<float?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptFloat_WithRightType_Successful<float?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptFloat_WithRightType_Successful<float?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptFloat_AsFloatNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptFloat_WithRightType_Successful<float?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Float As Object Tests

        [Fact]
        public void Match_AcceptFloat_AsObject_SuccessfulAccepted()
        {
            Match_AcceptFloat_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptFloat_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptFloat_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Float As Decimal Tests

        [Fact]
        public void Match_AcceptFloat_AsDecimal_SuccessfulAccepted()
        {
            Match_AcceptFloat_WithRightType_Successful<decimal>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptFloat_AsDecimalNullable_SuccessfulAccepted()
        {
            Match_AcceptFloat_WithRightType_Successful<decimal?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Float As Double Tests

        [Fact]
        public void Match_AcceptFloat_AsDouble_SuccessfulAccepted()
        {
            Match_AcceptFloat_WithRightType_Successful<double>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptFloat_AsDoubleNullable_SuccessfulAccepted()
        {
            Match_AcceptFloat_WithRightType_Successful<double?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Float Not As Other Types Tests
        
        [Fact]
        public void Match_AcceptFloat_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptFloat_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptFloat_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptFloat_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptFloat_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptFloat_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptFloat_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptFloat_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptFloat_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptFloat_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptFloat_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptFloat_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptFloat_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptFloat_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptFloat_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptFloat_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptFloat_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptFloat_AsListFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<List<float>>(
                typeName: "List<Float>");
        }

        [Fact]
        public void Match_AcceptFloat_AsListFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<List<float?>>(
                typeName: "List<Float?>");
        }

        [Fact]
        public void Match_AcceptFloat_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptFloat_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptFloat_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptFloatField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptFloat_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {
            // arrange            
            AcceptTypeTestee<float?> testee = CreateFloatAcceptTestee(insertNull);
                
            // act & assert
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptFloatField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {
            // arrange            
            AcceptTypeTestee<float?> testee = CreateFloatAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, float?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<float?> CreateFloatAcceptTestee(bool insertNull)
        {
            float? floatNumber = insertNull ? null : 1.7569876f;

            AcceptTypeTestee<float?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(floatNumber);

            return testee;
        }

        #endregion
    }
}
