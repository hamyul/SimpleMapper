using System.Collections.Generic;

namespace Business.Tests.Helpers
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Student> Classmates { get; set; } = new List<Student>();
        public IEnumerable<Person> Friends { get; set; } = new List<Person>();

        public Student IsFriendsWith(Person person)
        {
            (Friends as List<Person>).Add(person);
            return this;
        }
    }
}
