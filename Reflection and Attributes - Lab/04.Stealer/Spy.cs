using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            Type classType = Type.GetType(className);

            FieldInfo[] fieldInfo = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            MethodInfo[] classPuiblicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] classNonPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();
            foreach (FieldInfo field in fieldInfo)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (MethodInfo method in classPuiblicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            foreach (MethodInfo method in classNonPublicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }
            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            StringBuilder sb = new StringBuilder();
            Type classType = Type.GetType(className);
            sb.AppendLine($"All Private Methods of Class: {classType.FullName}");
            sb.AppendLine($"Base Class: {classType.BaseType}");

            MethodInfo[] privateMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var privateMethod in privateMethods)
            {
                sb.AppendLine(privateMethod.Name);
            }

            return sb.ToString().TrimEnd();
        }

        public string CollectGettersAndSetters(string className)
        {
            StringBuilder sb = new();
            Type classType = Type.GetType(className);

            MethodInfo[] classMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            foreach (var classMethod in classMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{classMethod.Name} will return {classMethod.ReturnType}");
            }

            foreach (var classMethod in classMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{classMethod.Name} will return {classMethod.ReturnType}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
