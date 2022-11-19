﻿using System;
using System.Collections.Generic;

namespace ADCBookApp
{
    public class Person
    {
        public string PersonId { get; set; }
        public FullName FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Person()
        {

        }

        public Person(string fullName, DateTime birthDate, 
            string address, string phoneNumber)
        {
            FullName = new FullName(fullName);
            BirthDate = birthDate;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   PersonId == person.PersonId;
        }

        public override int GetHashCode()
        {
            return -1255590651 + EqualityComparer<string>.Default.GetHashCode(PersonId);
        }

        public int CompareTo(Person other)
        {
            return PersonId.CompareTo(other.PersonId);
        }
    }
}
