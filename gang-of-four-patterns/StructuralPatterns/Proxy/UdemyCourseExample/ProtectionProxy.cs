/// <summary>
/// A protection proxy something that checks whether
/// you have the right to call a particular method,
/// or whether you are allowed to access a specific
/// value/property.
/// <summary>
public class ProtectionProxy
{
}

public interface ICar
{
    void Drive();
}

public class Car : ICar
{
    public void Drive()
    {
        Console.WriteLine("The car is being driven");
    }
}

/*
 * Now we want to prevent that people who are to young
 * are not allowed to drive the car.
 * */

public class Driver
{
    public int Age { get; set; }
}

public class CarProxy : ICar
{
    private Driver _driver;
    private Car _car = new();

    public CarProxy(Driver driver)
    {
        _driver = driver;
    }

    public void Drive()
    {
        if (_driver.Age >= 17)
            _car.Drive();
        else
            Console.WriteLine("too young");
    }
}
