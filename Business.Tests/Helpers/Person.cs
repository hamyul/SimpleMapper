using System.Collections.Generic;

namespace Business.Tests.Helpers
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IEnumerable<Person> Friends { get; set; } = new List<Person>();
    }
}
