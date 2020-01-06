using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreDataProtect
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();

            var protector = service.GetDataProtector("Contoso.Example.v2");

            Console.Write("Enter input:");
            string input = Console.ReadLine();

            string protectedPayLoad = protector.Protect(input);
            Console.WriteLine($"Protect returned:{protectedPayLoad}");

            string unProtectedPayLoad = protector.Unprotect(protectedPayLoad);
            Console.WriteLine($"UnProtect returned:{unProtectedPayLoad}");


        }
    }
}
