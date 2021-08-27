using System;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Core
{
    public class FieldMatchOperator<T> : FieldMatchOperator
    {
        private Func<FieldOption, T> _fieldOption;
        private Action<T> _fieldAction;

        public FieldMatchOperator(
            Func<FieldOption, T> fieldOption,
            Action<T> fieldAction)
        {
            _fieldOption = fieldOption;
            _fieldAction = fieldAction;
        }

        public override FieldOption ExecuteMatch(JToken snapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);
            T fieldValue = _fieldOption(fieldOption);

            _fieldAction(fieldValue);

            return fieldOption;
        }
    }

    public abstract class FieldMatchOperator
    {
        public abstract FieldOption ExecuteMatch(JToken snapshotData);
    }
}
