using System;
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
    }
}
