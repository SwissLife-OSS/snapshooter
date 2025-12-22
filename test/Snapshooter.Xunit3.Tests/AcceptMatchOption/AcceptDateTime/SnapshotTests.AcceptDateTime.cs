using System;
using System.Collections.Generic;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.AcceptDateTime
{
    public partial class SnapshotTests
    {
        #region Accept DateTime Tests

        [Fact]
        public void Match_AcceptDateTime_AsDateTime_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDateTime_WithRightType_Successful<DateTime>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTime_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<DateTime>(
                insertNull: true,
                keepOriginalValue: false,
                "DateTime");
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTime_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDateTime_WithRightType_Successful<DateTime>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTime_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDateTime_WithRightType_Successful<DateTime>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTime_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<DateTime>(
                insertNull: true,
                keepOriginalValue: true,
                "DateTime");
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTime_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDateTime_WithRightType_Successful<DateTime>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept DateTime Nullable Tests
        
        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptDateTime_AsDateTimeNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDateTime_WithRightType_Successful<DateTime?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept DateTime As Object Tests

        [Fact]
        public void Match_AcceptDateTime_AsObject_SuccessfulAccepted()
        {
            Match_AcceptDateTime_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDateTime_AsObject_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptDateTime_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDateTime_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptDateTime_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDateTime_AsObjectNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptDateTime_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion
        
        #region Accept DateTime Not As Other Types Tests

        [Fact]
        public void Match_AcceptDateTime_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptDateTime_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptDateTime_AsDecimal_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptDateTime_AsDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptDateTime_AsDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsFloat_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptDateTime_AsFloatNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptDateTime_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptDateTime_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptDateTime_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptDateTime_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptDateTime_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptDateTime_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptDateTime_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptDateTime_AsListDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<List<DateTime>>(
                typeName: "List<DateTime>");
        }

        [Fact]
        public void Match_AcceptDateTime_AsListDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<List<DateTime?>>(
                typeName: "List<DateTime?>");
        }

        [Fact]
        public void Match_AcceptDateTime_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptDateTime_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptDateTime_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDateTimeField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptDateTime_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<DateTime?> testee = CreateDateTimeAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptDateTimeField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<DateTime?> testee = CreateDateTimeAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, DateTime?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<DateTime?> CreateDateTimeAcceptTestee(bool insertNull)
        {
            DateTime? DateTimeValue = insertNull ? null : DateTime.Parse("2022-05-11T09:52:23");

            AcceptTypeTestee<DateTime?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(DateTimeValue);

            return testee;
        }

        #endregion
    }
}
