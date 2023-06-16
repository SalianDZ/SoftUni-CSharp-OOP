using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Animal
    {
        protected string name;
        protected int age;
        protected string gender;
        
        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name
        {
            get { return name; }
            set 
            {
                if (value == "" || value == " ")
                {
                    name = "Invalid input!";
                    return;
                }

                name = value;
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
            }
        }
        public string Gender
        {
            get { return gender; }
            set
            {
                if (value == "" || value == " ")
                {
                    gender = "Invalid input!";
                    return;
                }

                gender = value;
            }
        }

        public virtual string ProduceSound()
        {
            return null;
        }

        public override string ToString()
        {
            StringBuilder result = new();
            string sound = ProduceSound();
            if (sound == "Woof!")
            {
                result.AppendLine($"Dog");
            }
            else if (sound == "Meow meow")
            {
                result.AppendLine($"Cat");
            }
            else if (sound == "Ribbit")
            {
                result.AppendLine($"Frog");
            }
            else if(sound == "Meow")
            {
                result.AppendLine($"Kitten");
            }
            else if (sound == "MEOW")
            {
                result.AppendLine($"Tomcat");
            }
            result.AppendLine($"{Name} {Age} {Gender}");
            result.AppendLine($"{ProduceSound()}");
            return result.ToString().TrimEnd();
        }
    }
}
