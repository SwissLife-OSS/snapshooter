using System;
using System.Collections.Generic;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.AcceptMatchOption.TestHelpers;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit.Tests.AcceptMatchOption.Double
{
    public partial class SnapshotTests
    {
        #region Accept Double Tests

        [Fact]
        public void Match_AcceptDouble_AsDouble_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDouble_WithRightType_Successful<double>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDouble_AsDouble_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<double>(
                insertNull: true,
                keepOriginalValue: false,
                "Double");
        }

        [Fact]
        public void Match_AcceptDouble_AsDouble_SnapshotCreated()
        {            
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDouble_WithRightType_Successful<double>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDouble_AsDouble_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDouble_WithRightType_Successful<double>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDouble_AsDouble_KeepOriginal_NullValue_Error()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<double>(
                insertNull: true,
                keepOriginalValue: true,
                "Double");
        }

        [Fact]
        public void Match_AcceptDouble_AsDouble_KeepOriginal_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDouble_WithRightType_Successful<double>(
                    insertNull: false,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Double Nullable Tests
        
        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDouble_WithRightType_Successful<double?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDouble_WithRightType_Successful<double?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDouble_WithRightType_Successful<double?>(
                    insertNull: false,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_NullValue_SnapshotCreated()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDouble_WithRightType_Successful<double?>(
                    insertNull: true,
                    keepOriginalValue: false));
        }

        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_KeepOriginal_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDouble_WithRightType_Successful<double?>(
                insertNull: false,
                keepOriginalValue: true);
        }

        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_KeepOriginal_NullValue_SuccessfulAccepted()
        {
            // arrange & act & assert
            Match_AcceptDouble_WithRightType_Successful<double?>(
                insertNull: true,
                keepOriginalValue: true);
        }
        
        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_KeepOriginal_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDouble_WithRightType_Successful<double?>(
                    insertNull: false,
                    keepOriginalValue: true));
        }
        
        [Fact]
        public void Match_AcceptDouble_AsDoubleNullable_KeepOriginal_NullValue_CreatedSnapshot()
        {
            // arrange & act & assert
            AcceptAssert.AssertVerifiedVsNewCreatedSnapshot(
                () => Match_AcceptDouble_WithRightType_Successful<double?>(
                    insertNull: true,
                    keepOriginalValue: true));
        }

        #endregion

        #region Accept Double As Object Tests

        [Fact]
        public void Match_AcceptDouble_AsObject_SuccessfulAccepted()
        {
            Match_AcceptDouble_WithRightType_Successful<object>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDouble_AsObjectNullable_SuccessfulAccepted()
        {
            Match_AcceptDouble_WithRightType_Successful<object?>(
                insertNull: false,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Double As Decimal Tests

        [Fact]
        public void Match_AcceptDouble_AsDecimal_SuccessfulAccepted()
        {
            Match_AcceptDouble_WithRightType_Successful<decimal>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDouble_AsDecimalNullable_SuccessfulAccepted()
        {
            Match_AcceptDouble_WithRightType_Successful<decimal?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Double As Float Tests

        [Fact]
        public void Match_AcceptDouble_AsFloat_SuccessfulAccepted()
        {
            Match_AcceptDouble_WithRightType_Successful<float>(
                insertNull: false,
                keepOriginalValue: false);
        }

        [Fact]
        public void Match_AcceptDouble_AsFloatNullable_SuccessfulAccepted()
        {
            Match_AcceptDouble_WithRightType_Successful<float?>(
                insertNull: true,
                keepOriginalValue: false);
        }

        #endregion

        #region Accept Double Not As Other Types Tests
        
        [Fact]
        public void Match_AcceptDouble_AsString_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<string>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptDouble_AsStringNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<string?>(
                typeName: "String");
        }

        [Fact]
        public void Match_AcceptDouble_AsGuid_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<Guid>(
                typeName: "Guid");
        }

        [Fact]
        public void Match_AcceptDouble_AsGuidNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<Guid?>(
                typeName: "Guid?");
        }

        [Fact]
        public void Match_AcceptDouble_AsInt_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<int>(
                typeName: "Integer");
        }

        [Fact]
        public void Match_AcceptDouble_AsIntNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<int?>(
                typeName: "Integer?");
        }

        [Fact]
        public void Match_AcceptDouble_AsShort_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<short>(
                typeName: "Short");
        }

        [Fact]
        public void Match_AcceptDouble_AsShortNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<short?>(
                typeName: "Short?");
        }

        [Fact]
        public void Match_AcceptDouble_AsLong_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<long>(
                typeName: "Long");
        }

        [Fact]
        public void Match_AcceptDouble_AsLongNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<long?>(
                typeName: "Long?");
        }

        [Fact]
        public void Match_AcceptDouble_AsDateTime_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<DateTime>(
                typeName: "DateTime");
        }

        [Fact]
        public void Match_AcceptDouble_AsDateTimeNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<DateTime?>(
                typeName: "DateTime?");
        }

        [Fact]
        public void Match_AcceptDouble_AsBool_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<bool>(
                typeName: "Boolean");
        }

        [Fact]
        public void Match_AcceptDouble_AsBoolNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<bool?>(
                typeName: "Boolean?");
        }

        [Fact]
        public void Match_AcceptDouble_AsByte_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<byte>(
                typeName: "Byte");
        }

        [Fact]
        public void Match_AcceptDouble_AsByteNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<byte?>(
                typeName: "Byte?");
        }

        [Fact]
        public void Match_AcceptDouble_AsByteArray_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<byte[]>(
                typeName: "Byte[]");
        }

        [Fact]
        public void Match_AcceptDouble_AsListDouble_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<List<double>>(
                typeName: "List<Double>");
        }

        [Fact]
        public void Match_AcceptDouble_AsListDoubleNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<List<double?>>(
                typeName: "List<Double?>");
        }

        [Fact]
        public void Match_AcceptDouble_AsListDecimalNullable_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<List<decimal?>>(
                typeName: "List<Decimal?>");
        }

        [Fact]
        public void Match_AcceptDouble_AsComplexType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<TestPerson>(
                typeName: "TestPerson");
        }

        [Fact]
        public void Match_AcceptDouble_AsEnumType_ThrowsException()
        {
            // arrange & act & assert
            Match_AcceptDoubleField_WithWrongType_ThrowsException<AcceptEnumTestee>(
                typeName: "AcceptEnumTestee");
        }

        #endregion

        #region Private Test Helpers

        private void Match_AcceptDouble_WithRightType_Successful<T>(
            bool insertNull = false,
            bool keepOriginalValue = false)
        {       
            AcceptTypeTestee<double?> testee = CreateDoubleAcceptTestee(insertNull);
                
            Snapshot.Match(
                testee, matchOptions => matchOptions
                    .AcceptField<T>(nameof(testee.Value), keepOriginalValue));
        }

        private void Match_AcceptDoubleField_WithWrongType_ThrowsException<T>(
            bool insertNull = false,
            bool keepOriginalValue = false,
            string typeName = "NotDefined")
        {           
            AcceptTypeTestee<double?> testee = CreateDoubleAcceptTestee(insertNull);

            AcceptAssert.AssertAcceptWrongTypeExceptionCase<T, double?>(
                insertNull, keepOriginalValue, typeName, testee);
        }
        
        private AcceptTypeTestee<double?> CreateDoubleAcceptTestee(bool insertNull)
        {
            double? doubleNumber = insertNull ? null : 1.7569876543210123d;

            AcceptTypeTestee<double?> testee = AcceptTypeTesteeBuilder
                .CreateAcceptTypeDefaultTestee(doubleNumber);

            return testee;
        }

        #endregion
    }
}
