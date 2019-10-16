using System;
using System.Threading.Tasks;
using AspNetCoregRPC;
using Grpc.Net.Client;

namespace AspNetCoregRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.CreateBookAsync(
                new BookRequest() {Name = "123", Price = 2.05});

            Console.WriteLine("Greeting:" + reply.Status);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
