using System;
using System.Text;

using BungoNet.Serialization.BnetApi.Attributes;
using BungoNet.Serialization.BnetApi.Interfaces;

namespace BungoNet.Serialization.BnetApi
{
    public class BnetApiSerializer
    {
        private static string ValueToString(object? value, Type type)
        {
            string? valueAsString = value != null ? value.ToString() : null;

            if (type == typeof(bool) && value != null)
            {
                return (bool)value ? "1" : "0";
            }

            if (valueAsString != null)
            {
                return valueAsString;
            }
            else
            {
                if (type == typeof(string))
                {
                    return "null";
                }
                else
                {
                    return "0";
                }
            }
        }

        public static string Serialize(object o, int padding = 0)
        {
            StringBuilder sb = new();

            foreach (var prop in o.GetType().GetProperties())
            {
                var name = BnetApiName.NameForProperty(prop);
                var type = prop.PropertyType;
                object? value = prop.GetValue(o, null);
                var bnetSerializable = value as IBnetSerializable;

                if (bnetSerializable != null)
                    bnetSerializable.Serialize(sb);
                else
                    sb.Append(new string(' ', padding) +  $"{name}: {ValueToString(value, type)}\r\n");
            }

            return sb.ToString();
        }
    }
}
