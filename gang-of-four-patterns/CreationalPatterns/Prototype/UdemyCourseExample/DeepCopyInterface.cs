namespace UdemyCourseExample
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person2 : IPrototype<Person2>
    {
        public string[] Names { get; set; }

        public Address2 Address { get; set; }

        public Person2(string[] names, Address2 address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public Person2 DeepCopy()
        {
            return new Person2(Names, Address.DeepCopy());
        }
    }

    public class Address2 : IPrototype<Address2>
    {
        public string StreetName { get; set; }

        public int HouseNumber { get; set; }

        public Address2(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public Address2 DeepCopy()
        {
            return new Address2(StreetName, HouseNumber);
        }
    }
}