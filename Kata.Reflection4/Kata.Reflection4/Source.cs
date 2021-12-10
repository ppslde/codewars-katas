using System;
using System.Reflection;
using System.Linq;

namespace Kata.Reflection4
{
    public class TestClass
    {
        public string DoSomeThing()
        {
            return "AA";
        }
    }


    public class Source
    {
        protected Source()
        {
        }

        public static string? InvokeMethod(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                return typeName;

            var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name.ToLower() == typeName.ToLower());
            if (type == null)
                return null;

            var methodInfo = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).SingleOrDefault();
            if (methodInfo == null)
                return null;

            if (methodInfo.ReturnType == typeof(string))
                return methodInfo.Invoke(Activator.CreateInstance(type), null) as string;

            return methodInfo.Invoke(Activator.CreateInstance(type), null)?.ToString();
        }
    }
}