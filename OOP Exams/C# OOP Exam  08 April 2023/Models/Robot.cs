using RobotService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Utilities.Messages;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private List<int> interfaceStandards;


        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            ConvertionCapacityIndex = conversionCapacityIndex;
            BatteryLevel = batteryCapacity;
            interfaceStandards = new List<int>();
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }

                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }

                batteryCapacity = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public int ConvertionCapacityIndex { get; private set; }

        public IReadOnlyCollection<int> InterfaceStandards
        {
            get => interfaceStandards;
        }

        public void Eating(int minutes)
        {
            int energy = ConvertionCapacityIndex * minutes;

            BatteryLevel += energy;

            if (BatteryLevel > BatteryCapacity)
            {
                BatteryLevel = BatteryCapacity;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }
            return false;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"{GetType()} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");

            if (InterfaceStandards.Count <= 0)
            {
                sb.AppendLine($"--Supplements installed: none");
            }
            else
            {
                sb.AppendLine($"--Supplements installed: {string.Join(" ", InterfaceStandards)}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
