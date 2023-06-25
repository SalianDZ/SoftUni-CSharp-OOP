using MilitaryElite.Core;
using MilitaryElite.Core.Interfaces;

namespace MilitaryElite
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}