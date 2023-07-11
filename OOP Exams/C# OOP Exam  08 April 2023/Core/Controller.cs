using RobotService.Core.Contracts;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Utilities.Messages;
using RobotService.Models;
using System.Reflection;
using System.Diagnostics.Metrics;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements = new();
        private RobotRepository robots = new();
        public string CreateRobot(string model, string typeName)
        {
            if (typeName != "DomesticAssistant" && typeName != "IndustrialAssistant")
            {
                string result = string.Format(OutputMessages.RobotCannotBeCreated, typeName);
                throw new ArgumentException(result);
            }

            IRobot robot;

            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model); ;
            }
            else
            {
                robot = new IndustrialAssistant(model);
            }

            robots.AddNew(robot);
            return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }
         
        public string CreateSupplement(string typeName)
        {
            if (typeName != "SpecializedArm" && typeName != "LaserRadar")
            {
                string result = string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
                throw new ArgumentException(result);
            }

            ISupplement supplement;

            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm(); ;
            }
            else
            {
                supplement = new LaserRadar();
            }

            supplements.AddNew(supplement);
            return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            string result;
            List<IRobot> wantedRobots = robots.Models()
                .Where(x => x.InterfaceStandards.Contains(intefaceStandard))
                .OrderByDescending(x => x.BatteryLevel).ToList();

            if (wantedRobots.Count <= 0)
            {
                result = string.Format(OutputMessages.UnableToPerform, intefaceStandard);
                throw new ArgumentException(result);
            }

            int batteryLevelSum = wantedRobots.Sum(x => x.BatteryLevel);

            if (batteryLevelSum < totalPowerNeeded)
            {
                result = string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - batteryLevelSum);
                throw new ArgumentException(result);
            }

            int robotsCounter = 0;

            foreach (var robot in wantedRobots)
            {
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    robotsCounter++;
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    robotsCounter++;
                    continue;
                }
            }

            result = String.Format(OutputMessages.PerformedSuccessfully, serviceName, robotsCounter);
            return result;
        }

        public string Report()
        {
            List<IRobot> wantedRobots = robots.Models()
                .OrderByDescending(x => x.BatteryLevel)
                .ThenBy(x => x.BatteryCapacity).ToList();

            StringBuilder result = new();

            foreach (var robot in wantedRobots)
            {
                result.AppendLine(robot.ToString());
            }
            return result.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            List<IRobot> wantedRobots = robots.Models()
                    .Where(x => x.Model == model && x.BatteryLevel < 50)
                    .ToList();
            int counter = 0;

            foreach (var robot in wantedRobots)
            {
                robot.Eating(minutes);
                counter++;
            }

            return String.Format(OutputMessages.RobotsFed, counter);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement wantedSupplement = supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            IRobot wantedRobot = robots.Models().FirstOrDefault(x => x.Model == model && !x.InterfaceStandards.Contains(wantedSupplement.InterfaceStandard));

            if (wantedRobot == null)
            {
                return String.Format(OutputMessages.AllModelsUpgraded, model);
            }

            wantedRobot.InstallSupplement(wantedSupplement);
            supplements.RemoveByName(wantedSupplement.GetType().Name);
            return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}
