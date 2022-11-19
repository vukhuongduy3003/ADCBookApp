﻿using System;
using System.Collections.Generic;

namespace ADCBookApp
{
    public class Customer : Person
    {
        public string CustomerType { get; set; }
        public int Point { get; set; }
        public DateTime CreateTime { get; set; }
        public string Email { get; set; }

        public Customer() { }

        public Customer(string customerType, int point, DateTime createTime, string email)
        {
            CustomerType = customerType;
            Point = point;
            CreateTime = createTime;
            Email = email;
        }

        public Customer(string fullName, DateTime birthDate,
            string address, string phoneNumber, string customerType,
            int point, DateTime createTime, string email) :
            base(fullName, birthDate, address, phoneNumber)
        {
            CustomerType = customerType;
            Point = point;
            CreateTime = createTime;
            Email = email;
        }

        public override bool Equals(object obj)
        {
            return obj is Customer customer &&
                   base.Equals(obj) &&
                   PersonId == customer.PersonId;
        }

        public override int GetHashCode()
        {
            int hashCode = 2079290131;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PersonId);
            return hashCode;
        }

        public int CompareTo(Customer other)
        {
            return PersonId.CompareTo(other.PersonId);
        }
    }
}
