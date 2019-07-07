using System;
using System.Linq;
using Business.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class InitializerTest
    {
        [TestMethod]
        public void should_create_initializer_instance()
        {
            var sut = new Initializer();
            Assert.IsInstanceOfType(sut, typeof(Initializer));
        }

        [TestMethod]
        public void should_create_instance()
        {
            var sut = new Initializer();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void should_create_destination_object_instance()
        {
            var sut = new Initializer();
            var person = new Person() { FirstName = "John", LastName = "Doe", Age = 18 };

            var expected = sut.Map<Employee>(person);
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void should_create_mapped_instance_of_destination_type()
        {
            var sut = new Initializer();
            var person = new Person() { FirstName = "John", LastName = "Doe", Age = 18 };

            var expected = sut.Map<Employee>(person);
            Assert.IsInstanceOfType(expected, typeof(Employee));
        }

        [TestMethod]
        public void should_map_identical_name_properties()
        {
            var sut = new Initializer();
            var person = new Person() { FirstName = "John", LastName = "Doe", Age = 18 };

            var expected = sut.Map<Employee>(person);
            Assert.IsTrue(expected.FirstName == person.FirstName && expected.LastName == person.LastName);
        }

        [TestMethod]
        public void should_map_lists_when_source_is_subtype_of_destination()
        {
            var sut = new Initializer();

            var john = new Person() { FirstName = "John", LastName = "Lennon", Age = 18 };
            var ringo = new Person() { FirstName = "Ringo", LastName = "Starr", Age = 16 };
            var paul = new Person() { FirstName = "Paul", LastName = "McCartney", Age = 17 };
            var george = new Person() { FirstName = "George", LastName = "Harrison", Age = 15 };

            john.IsFriendsWith(ringo)
                .IsFriendsWith(george)
                .IsFriendsWith(paul);


            var expected = sut.Map<Student>(john);
            Assert.IsTrue(expected.Friends.Any(a => a.FirstName == paul.FirstName && a.LastName == paul.LastName) &&
                          expected.Friends.Any(a => a.FirstName == ringo.FirstName && a.LastName == ringo.LastName) &&
                          expected.Friends.Any(a => a.FirstName == george.FirstName && a.LastName == george.LastName));
        }

        [TestMethod]
        public void should_not_map_lists_when_destination_is_subtype_of_source()
        {
            var sut = new Initializer();

            var john = new Student() { FirstName = "John", LastName = "Lennon", Age = 18 };
            var ringo = new Person() { FirstName = "Ringo", LastName = "Starr", Age = 16 };
            var paul = new Person() { FirstName = "Paul", LastName = "McCartney", Age = 17 };
            var george = new Person() { FirstName = "George", LastName = "Harrison", Age = 15 };

            john.IsFriendsWith(ringo)
                .IsFriendsWith(george)
                .IsFriendsWith(paul);


            var expected = sut.Map<Person>(john);
            Assert.IsTrue(expected.Friends.Count() == 0);
        }
    }
}
