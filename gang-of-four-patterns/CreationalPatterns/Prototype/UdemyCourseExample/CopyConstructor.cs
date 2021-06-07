using System;

namespace UdemyCourseExample
{
    public class Person1
    {
        public string[] Names { get; set; }

        public Address1 Address { get; set; }

        public Person1(string[] names, Address1 address)
        {
            Names = names;
            Address = address;
        }

        public Person1(Person1 personToCopy)
        {
            Names = personToCopy.Names;
            Address = new Address1(personToCopy.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address1
    {
        public string StreetName { get; set; }

        public int HouseNumber { get; set; }

        public Address1(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address1(Address1 addressToCopy)
        {
            StreetName = addressToCopy.StreetName;
            HouseNumber = addressToCopy.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }
}