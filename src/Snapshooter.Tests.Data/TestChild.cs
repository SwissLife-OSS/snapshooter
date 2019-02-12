using System;

namespace Snapshooter.Tests.Data
{
    public class TestChild
    {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class TestChildBuilder
    {
        private TestChild _testChild;

        private TestChildBuilder()
        {
            _testChild = new TestChild();
        }

        public static TestChildBuilder Create()
        {
            return new TestChildBuilder();
        }

        public TestChildBuilder WithName(string name)
        {
            _testChild.Name = name;
            return this;
        }

        public TestChildBuilder WithDateOfBirth(DateTime? dateOfBirth)
        {
            _testChild.DateOfBirth = dateOfBirth;
            return this;
        }

        public TestChild Build()
        {
            return _testChild;
        }
    }
}
