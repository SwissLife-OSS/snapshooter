using System;
using System.Collections.Generic;
using System.Linq;

namespace Snapshooter.Tests.Data
{
    public class TestPerson
    {
        public TestPerson()
        {
            Children = new List<TestChild>();
        }

        public Guid? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public decimal? Size { get; set; }
        public TestAddress Address { get; set; }
        public IList<TestChild> Children { get; set; }
        public TestPerson[] Relatives { get; set; }
    }

    public class TestPersonBuilder
    {
        private readonly TestPerson _testPerson;

        private TestPersonBuilder()
        {
            _testPerson = new TestPerson();
        }

        public static TestPersonBuilder Create()
        {
            return new TestPersonBuilder();
        }

        public TestPersonBuilder WithId(Guid? id)
        {
            _testPerson.Id = id;
            return this;
        }

        public TestPersonBuilder WithFirstname(string firstname)
        {
            _testPerson.Firstname = firstname;
            return this;
        }

        public TestPersonBuilder WithLastname(string lastname)
        {
            _testPerson.Lastname = lastname;
            return this;
        }

        public TestPersonBuilder WithCreationDate(DateTime? creationDate)
        {
            _testPerson.CreationDate = creationDate;
            return this;
        }

        public TestPersonBuilder WithDateOfBirth(DateTime? dateOfBirth)
        {
            _testPerson.DateOfBirth = dateOfBirth;
            return this;
        }

        public TestPersonBuilder WithAge(int? age)
        {
            _testPerson.Age = age;
            return this;
        }

        public TestPersonBuilder WithSize(decimal? size)
        {
            _testPerson.Size = size;
            return this;
        }

        public TestPersonBuilder AddAddress(TestAddress testAddress)
        {
            _testPerson.Address = testAddress;
            return this;
        }

        public TestPersonBuilder AddChild(TestChild testChild)
        {
            var children = _testPerson.Children.ToList();
            children.Add(testChild);
            _testPerson.Children = children;

            return this;
        }

        public TestPersonBuilder AddRelative(TestPerson relative)
        {
            if (_testPerson.Relatives == null)
            {
                _testPerson.Relatives = new TestPerson[] { relative };
            }
            else
            {
                var relatives = _testPerson.Relatives.ToList();
                relatives.Add(relative);
                _testPerson.Relatives = relatives.ToArray();
            }

            return this;
        }

        public TestPerson Build()
        {
            return _testPerson;
        }
    }
}
