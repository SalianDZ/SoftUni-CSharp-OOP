using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public class Robot : IIdentifiable
    {
        public Robot(string model, string id)
        {
            Model = model;
            Id = id;
        }

        public string Model { get; set; }

        public string Id { get; set; }

        public void BuyFood()
        {
            throw new NotImplementedException();
        }
    }
}
