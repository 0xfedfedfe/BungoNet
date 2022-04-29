using System;
using System.Reflection;

namespace BungoNet.Serialization.BnetApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class BnetApiName : Attribute
    {
        public string Name { get; protected set; }

        public BnetApiName(string name)
        {
            Name = name;
        }

        public static string NameForProperty(PropertyInfo propertyInfo)
        {
            string name = propertyInfo.Name;

            foreach (var attribute in propertyInfo.GetCustomAttributes(true))
            {
                BnetApiName? bnetName = attribute as BnetApiName;

                if (bnetName != null)
                    name = bnetName.Name;
            }

            return name;
        }
    }
}
