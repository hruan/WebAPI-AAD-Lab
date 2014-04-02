using System;
using Microsoft.Owin.Hosting;

namespace AADLab.WebAPI
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.WriteLine("Web API running. Press enter to quit.");

                Console.ReadLine();
            }
        }
    }
}
