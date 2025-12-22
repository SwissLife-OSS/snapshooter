using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Xunit;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers
{
    public static class AcceptAssert
    {
        public static void AssertVerifiedVsNewCreatedSnapshot(Action snapshotAction)
        {
            // arrange
            SnapshotFullName originalFullName = Snapshot.FullName();
            SnapshotFileHandler snapshotFileHandler = new SnapshotFileHandler();

            snapshotFileHandler.DeleteSnapshot(originalFullName);

            // act
            snapshotAction();

            // assert
            Assert.Equal(
                snapshotFileHandler.ReadSnapshot(Snapshot.FullName(
                    SnapshotNameExtension.Create("Verified"))),
                snapshotFileHandler.ReadSnapshot(originalFullName));
        }

        public static void AssertAcceptWrongTypeExceptionCase<TAccept, TTestee>(
            bool insertNull,
            bool keepOriginalValue,
            string typeName,
            AcceptTypeTestee<TTestee> testee,
            string? testeeValue = null)
        {
            // arrange

            // act
            Action action = () => Snapshot.Match(
            testee, matchOptions => matchOptions
                .AcceptField<TAccept>(nameof(testee.Value), keepOriginalValue: keepOriginalValue));

            // assert
            SnapshotFieldException exception =
                Assert.Throws<SnapshotFieldException>(action);

            if (insertNull)
            {
                Assert.Equal(exception.Message,
                   $"Accept match option failed, " +
                   $"because the field value of '{nameof(testee.Value)}' " +
                   $"is 'Null', " +
                   $"but defined accept type '{typeName}' is not nullable.");
            }
            else
            {
                Assert.Equal(exception.Message,
                    $"Accept match option failed, " +
                    $"because the field value of '{nameof(testee.Value)}' " +
                    $"is '{testeeValue ?? testee.Value!.ToString()}', " +
                    $"and therefore not of type '{typeName}'.");
            }
        }
    }
}
