using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Browse(string url)
        {
            if (!ValidateUrl(url))
            {
                throw new ArgumentException("Invalid URL!");
            }
            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!ValidateNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Calling... {phoneNumber}";
        }

        private bool ValidateNumber(string phoneNumber)
        => phoneNumber.All(c => Char.IsDigit(c));

        private bool ValidateUrl(string url)
        => url.All(c => !Char.IsDigit(c));
    }
}
