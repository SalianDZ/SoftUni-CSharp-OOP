namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        private const int BatteryCapacity = 40000;
        private const int ConvertionCapacityIndex = 5000;
        public IndustrialAssistant(string model)
            : base(model, BatteryCapacity, ConvertionCapacityIndex)
        {
        }
    }
}
