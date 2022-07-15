namespace UdemyCourseExample
{
    public class Person4
    {
        public string Address, Postcode, City;

        public string CompanyName, Position;

        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(Address)}: {Address}, {nameof(Postcode)}: {Postcode}, {nameof(City)}:, {City}\n" + 
                        $"{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}\n" +
                        $"{nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class Person4Builder // The facade.
    {
        protected Person4 Person = new();

        public Person4JobBuilder Works => new(Person);

        public Person4AddressBuilder Lives => new(Person);

        public static implicit operator Person4(Person4Builder builder) => builder.Person;
    }

    public class Person4JobBuilder : Person4Builder
    {
        public Person4JobBuilder(Person4 person)
        {
            Person = person;
        }

        public Person4JobBuilder At(string companyName)
        {
            Person.CompanyName = companyName;
            return this;
        }

        public Person4JobBuilder AsA(string position)
        {
            Person.Position = position;
            return this;
        }

        public Person4JobBuilder Earning(int amount)
        {
            Person.AnnualIncome = amount;
            return this;
        }
    }

    public class Person4AddressBuilder : Person4Builder
    {
        public Person4AddressBuilder(Person4 person)
        {
            Person = person;
        }

        public Person4AddressBuilder At(string address)
        {
            Person.Address = address;
            return this;
        }

        public Person4AddressBuilder WithPostcode(string postcode)
        {
            Person.Postcode = postcode;
            return this;
        }

        public Person4AddressBuilder In(string city)
        {
            Person.City = city;
            return this;
        }
    }

    public class FacetedBuilder
    {
        
    }
}
