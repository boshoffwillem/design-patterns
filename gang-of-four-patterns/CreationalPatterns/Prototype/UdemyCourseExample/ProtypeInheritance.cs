namespace UdemyCourseExample
{
    public interface IDeepCopyable<T>
        where T : new()
    {
        /// <summary>
        /// Every class implementing this method has to be able to copy its
        /// internals to some target.
        /// </summary>
        /// <param name="target"></param>
        void CopyTo(T target);

        T DeepCopy()
        {
            T t = new();
            CopyTo(t);
            return t;
        }
    }

    public class Person3 : IDeepCopyable<Person3>
    {
        public string[] Names { get; set; }

        public Address3 Address { get; set; }

        public Person3()
        {

        }

        public Person3(string[] names, Address3 address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public void CopyTo(Person3 target)
        {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy();
        }
    }

    public class Employee : Person3, IDeepCopyable<Employee>
    {
        public int Salary { get; set; }

        public Employee()
        {

        }

        public Employee(string[] names, Address3 address, int salary) :
            base(names, address)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }
    }

    public class Address3 : IDeepCopyable<Address3>
    {
        public string StreetName { get; set; }

        public int HouseNumber { get; set; }

        public Address3()
        {

        }

        public Address3(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public void CopyTo(Address3 target)
        {
            target.StreetName = StreetName;
            target.HouseNumber = HouseNumber;
        }
    }

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item)
            where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T>(this T person)
            where T : Person3, new()
        {
            return ((IDeepCopyable<T>)person).DeepCopy();
        }
    }
}