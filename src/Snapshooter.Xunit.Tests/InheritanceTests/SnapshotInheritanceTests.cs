using System;
using Xunit;

namespace Snapshooter.Xunit.Tests
{
    public class SnapshotInheritanceTests
    {
        [Fact]
        public void Match_InheritedObjectSnapshotTest_Successful()
        {
            // arrange
            var developer = new Developer
            {
                Language = "C#",
                Level = "Senior",
                Id = Guid.Parse("7066B471-64C6-4D15-B8F0-19924D200CA9"),
                Number = 234345,
                LoginName = "toddm",
                Name = "Smith",
                Firstname = "Todd",
                Gender = "male"
            };

            // act & assert
            Snapshot.Match<Developer>(developer);
        }

        [Fact]
        public void Match_InheritedObjectsSnapshotTest_Successful()
        {
            // arrange
            var person = new Person
            {
                Gender = "male",
                Firstname = "Mike",
                Name = "Brown"
            };

            var employee = new Employee
            {
                Firstname = "Ester",
                LoginName = "oppa",
                Number = 1989,
                Gender = "female",
                Name = "Miller",
                Id = Guid.Parse("47339B82-CE83-4651-A50A-87340774B17B"),
            };

            // act & assert
            Snapshot.Match(new {person, employee});
        }

        [Fact]
        public void Match_InheritedObjectOverrideVirtualPropertyTest_Successful()
        {
            // arrange
            var overrideVirtualDeveloper = new OverrideVirtualDeveloper
            {
                Language = "Java",
                Level = "Professional",
                Firstname = "Ester",
                LoginName = "oppa",
                Number = 1989,
                Gender = "female",
                Name = "Miller",
                Id = Guid.Parse("47339B82-CE83-4651-A50A-87340774B17B"),
            };

            // act & assert
            Snapshot.Match(overrideVirtualDeveloper);
        }

        [Fact]
        public void Match_InheritedObjectOverrideAbstractPropertyTest_Successful()
        {
            // arrange
            var overrideAbstractDeveloper = new OverrideAbstractDeveloper
            {
                Language = "Java",
                Level = "Professional",
                Firstname = "Ester",
                LoginName = "oppa",
                Number = 1989,
                Gender = "female",
                Name = "Miller",
                Id = Guid.Parse("47339B82-CE83-4651-A50A-87340774B17B"),
            };

            // act & assert
            Snapshot.Match(overrideAbstractDeveloper);
        }

        [Fact]
        public void Match_InheritedObjectNotOverrideVirtualPropertyTest_Successful()
        {
            // arrange
            var notOverrideVirtualDeveloper = new NotOverrideVirtualDeveloper
            {
                Language = "Java",
                Level = "Professional",
                Firstname = "Ester",
                LoginName = "oppa",
                Number = 1989,
                Gender = "female",
                Name = "Miller",
                Id = Guid.Parse("47339B82-CE83-4651-A50A-87340774B17B"),
            };

            // act & assert
            Snapshot.Match(notOverrideVirtualDeveloper);
        }

        [Fact]
        public void Match_InheritedObjectNewVirtualPropertyTest_Successful()
        {
            // arrange
            var newVirtualDeveloper = new NewVirtualDeveloper
            {
                Language = "Java",
                Level = "Professional",
                Firstname = "Ester",
                LoginName = "oppa",
                Number = 1989,
                Gender = "female",
                Name = "Miller",
                Id = Guid.Parse("47339B82-CE83-4651-A50A-87340774B17B"),
            };

            // act & assert
            Snapshot.Match(newVirtualDeveloper);
        }

        [Fact]
        public void Match_InheritedObjectOverrideVirtualPropertyOfGrandParentsTest_Successful()
        {
            // arrange
            var overrideVirtualLoginName = new OverrideVirtualLoginName
            {
                Language = "Java",
                Level = "Professional",
                Firstname = "Ester",
                LoginName = "oppa",
                Number = 1989,
                Gender = "female",
                Name = "Miller",
                Id = Guid.Parse("47339B82-CE83-4651-A50A-87340774B17B"),
            };

            // act & assert
            Snapshot.Match(overrideVirtualLoginName);
        }

        private class OverrideVirtualLoginName : Developer
        {
            public override string LoginName { get; set; }
        }

        private class OverrideAbstractDeveloper : AbstractDeveloper
        {
            public override string Level { get; set; }
        }

        private class NewVirtualDeveloper : VirtualDeveloper
        {
            public new string Level { get; set; }
        }

        private class OverrideVirtualDeveloper : VirtualDeveloper
        {
            public override string Level { get; set; }
        }
        
        private class NotOverrideVirtualDeveloper : VirtualDeveloper
        {
        }

        private abstract class AbstractDeveloper : Employee
        {
            public string Language { get; set; }

            public abstract string Level { get; set; }
        }

        private class VirtualDeveloper : Employee
        {
            public string Language { get; set; }

            public virtual string Level { get; set; }
        }

        private class Developer : Employee
        {
            public string Language { get; set; }
            
            public string Level { get; set; }
        }

        private class Employee : Person
        {
            public Guid Id { get; set; }

            public int Number { get; set; }

            public virtual string LoginName { get; set; }
        }

        private class Person : Human
        {
            public string Name { get; set; }

            public string Firstname { get; set; }
        }

        private abstract class Human
        {
            protected Human() { }

            public string Gender { get; set; }
        }
    }
}
