using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string investigatedClass, params string[] fieldNames)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Class under investigation: {investigatedClass}");
            Type classType = Type.GetType(investigatedClass);
            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            foreach (var field in fieldInfo.Where(x => fieldNames.Contains(x.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }
            return sb.ToString().TrimEnd();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();
            Type classType = Type.GetType(className);

            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            foreach (var field in fieldInfo)
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
