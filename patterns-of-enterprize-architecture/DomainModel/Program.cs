using System;
using DomainModel.Services;

namespace DomainModel;

class Program
{
    static void Main(string[] args)
    {
        var checkoutMediaService = new CheckoutMediaService();
        checkoutMediaService.CheckoutMedia(1);
        checkoutMediaService.CheckoutMedia(2);
        checkoutMediaService.CheckoutMedia(3);
        Console.WriteLine("Done");
        Console.ReadLine();
    }
};
