using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Constraints;

namespace Snapshooter.NUnit
{
    class NUnitThat
    {
    }

    public class MatchSnapshotConstraint : Constraint
    {
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            SnapResult snapResult;
            if (_snapshotId != null)
            {
                var snapper = NUnitSnapperFactory.GetNUnitSnapper();
                snapResult = snapper.MatchSnapshot(actual, _snapshotId);
            }
            else
            {
                snapResult = MatchSnapshot(actual);
            }

            return new ConstraintResult(this, actual, snapResult);
        }
    }
}
