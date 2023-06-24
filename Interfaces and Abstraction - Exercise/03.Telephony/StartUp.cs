using System;
using Telephony.Models;
using Telephony.Models.Interfaces;

namespace Telephony
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            string[] phonesNumbers = Console.ReadLine().Split();
            string[] urls = Console.ReadLine().Split();

            ICallable callable;
            foreach (var phoneNumber in phonesNumbers)
            {
                if (phoneNumber.Length == 7)
                {
                    callable = new StationaryPhone();
                }
                else
                {
                    callable = new Smartphone();
                }

                try
                {
                    Console.WriteLine(callable.Call(phoneNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            IBrowsable browsable = new Smartphone();

            foreach (var url in urls)
            {
                try
                {
                    Console.WriteLine(browsable.Browse(url));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}