using System;
using System.Collections.Generic;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.AcceptGuid
{
    public partial class SnapshotTests
    {
        #region Accept Guid Tests

        [Fact]
        public void Match_AcceptGuid_AsGuid_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<Guid>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsGuid_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<Guid>(
                insertNull: true,
                keepOriginalValue: false,
                "Guid");
        }

        [Fact]
        public void Match_AcceptGuid_AsGuid_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptGuid_WithRightType_Successful<Guid>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptGuid_AsGuid_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<Guid>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptGuid_AsGuid_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<Guid>(
                insertNull: true,
                keepOriginalValue: true,
                "Guid");
        }

        [Fact]
        public void Match_AcceptGuid_AsGuid_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptGuid_WithRightType_Successful<Guid>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Guid Nullable Tests
        
        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<Guid?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<Guid?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptGuid_WithRightType_Successful<Guid?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptGuid_WithRightType_Successful<Guid?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<Guid?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<Guid?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptGuid_WithRightType_Successful<Guid?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptGuid_AsGuidNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptGuid_WithRightType_Successful<Guid?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Guid As Object Tests

        [Fact]
        public void Match_AcceptGuid_AsObject_SuccessfulAccepted()
        {
            Match_AcceptGuid_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsObject_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptGuid_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptGuid_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptGuid_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsObjectNullable_KeepOriginal_SuccessfulAccepted()
        {
            Match_AcceptGuid_WithRightType_Successful<object?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion

        #region Accept Guid As String Tests

        [Fact]
        public void Match_AcceptGuid_AsString_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<string>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsString_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<string>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptGuid_AsStringNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<string?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptGuid_AsStringNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuid_WithRightType_Successful<string?>(
                insertNull: true,
                keepOriginalValue: true);
        }

        #endregion

        #region Accept Guid Not As Other Types Tests

        [Fact]
        public void Match_AcceptGuid_AsDecimal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<decimal>(
                typeName: "Decimal");
        }

        [Fact]
        public void Match_AcceptGuid_AsDecimalNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<decimal?>(
                typeName: "Decimal?");
        }

        [Fact]
        public void Match_AcceptGuid_AsDouble_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<double>(
                typeName: "Double");
        }

        [Fact]
        public void Match_AcceptGuid_AsDoubleNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<double?>(
                typeName: "Double?");
        }

        [Fact]
        public void Match_AcceptGuid_AsFloat_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<float>(
                typeName: "Float");
        }

        [Fact]
        public void Match_AcceptGuid_AsFloatNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<float?>(
                typeName: "Float?");
        }

        [Fact]
        public void Match_AcceptGuid_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptGuid_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptGuid_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptGuid_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptGuid_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptGuid_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptGuid_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptGuid_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptGuid_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptGuid_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptGuid_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptGuid_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptGuid_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptGuid_AsListGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<List<Guid>>(
                typeName: "List<Guid>");
        }

        [Fact]
        public void Match_AcceptGuid_AsListGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<List<Guid?>>(
                typeName: "List<Guid?>");
        }

        [Fact]
        public void Match_AcceptGuid_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptGuid_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptGuid_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptGuidField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptGuid_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {           
            AcceptTypeTestee<Guid?> testee = CreateGuidAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptGuidField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<Guid?> testee = CreateGuidAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, Guid?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<Guid?> CreateGuidAcceptTestee(bool insertNull)
        {
            Guid? guidValue = insertNull ? null : Guid.Parse("454FDD1E-2EBE-40F2-AAD6-A2872EC57C6F");

            AcceptTypeTestee<Guid?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(guidValue);

            return testee;
        }

        #endregion
    }
}
