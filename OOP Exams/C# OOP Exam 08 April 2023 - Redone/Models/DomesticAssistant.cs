namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        private const int BatteryCapacity = 20000;
        private const int ConvertionCapacityIndex = 2000;
        public DomesticAssistant(string model)
            : base(model, BatteryCapacity, ConvertionCapacityIndex)
        {
        }
    }
}
