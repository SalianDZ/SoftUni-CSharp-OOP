using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
            supplements = new();
            robots = new();
        }

        public string CreateRobot(string model, string typeName)
        {
            IRobot robot;

            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
            }
            else if(typeName == "IndustrialAssistant")
            {
                robot = new IndustrialAssistant(model);
            }
            else
            {
                throw new ArgumentException(String.Format(OutputMessages.RobotCannotBeCreated, typeName));
            }

            robots.AddNew(robot);
            return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        } // done

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;
            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == "LaserRadar")
            {
                supplement = new LaserRadar();
            }
            else
            {
                throw new ArgumentException(String.Format(OutputMessages.SupplementCannotBeCreated, typeName));
            }

            supplements.AddNew(supplement);
            return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        } // done

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            List <IRobot> wantedRobots = robots.Models()
                .Where(x => x.InterfaceStandards.Contains(intefaceStandard)).ToList();

            if (wantedRobots.Count == 0)
            {
                throw new ArgumentException(String.Format(OutputMessages.UnableToPerform, intefaceStandard));
            }

            int batteryLevelSum = wantedRobots.Sum(x => x.BatteryLevel);

            if (batteryLevelSum < totalPowerNeeded)
            {
                throw new ArgumentException(String.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - batteryLevelSum));
            }
            else
            {
                int counter = 0;

                foreach (var robot in wantedRobots.OrderByDescending(x => x.BatteryLevel))
                {
                    if (robot.BatteryLevel >= totalPowerNeeded)
                    {
                        robot.ExecuteService(totalPowerNeeded);
                        counter++;
                        break;
                    }
                    else if (robot.BatteryLevel < totalPowerNeeded)
                    {
                        totalPowerNeeded -= robot.BatteryLevel;
                        robot.ExecuteService(robot.BatteryLevel);
                        counter++;
                    }
                }

                return String.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);
            }
        } // done

        public string Report()
        {
            StringBuilder sb = new();
            foreach (var robot in robots.Models().OrderByDescending(x => x.BatteryLevel).ThenBy(x => x.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            int counter = 0;
            foreach (var robot in robots.Models().Where(x => x.Model == model).Where(x => x.BatteryLevel < x.BatteryCapacity / 2))
            {
                robot.Eating(minutes);
                counter++;
            }
            return String.Format(OutputMessages.RobotsFed, counter);
        } // done

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models()
                .FirstOrDefault(x => x.GetType().Name == supplementTypeName);

            IRobot robot = robots.Models().Where(x => x.Model == model)
                .FirstOrDefault(x => !x.InterfaceStandards.Contains(supplement.InterfaceStandard));

            if (robot == null)
            {
                throw new ArgumentException(String.Format(OutputMessages.AllModelsUpgraded, model));
            }

            robot.InstallSupplement(supplement);
            return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        } // done
    }
}
