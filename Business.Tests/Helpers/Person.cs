﻿using System.Collections.Generic;

namespace Business.Tests.Helpers
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Person> Friends { get; set; } = new List<Person>();

        public Person IsFriendsWith(Person person)
        {
            (Friends as List<Person>).Add(person);
            return this;
        }
    }
}
