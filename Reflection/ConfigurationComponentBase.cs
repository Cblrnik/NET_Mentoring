using System;
using System.ComponentModel;
using System.Reflection;

namespace Reflection
{
    internal class ConfigurationComponentBase
    {
        [ConfigurationItem("SecretString", 1)]
        public string SecretString { get; set; }

        [ConfigurationItem("SecretNumber", 0)]
        public int SecretNumber { get; set; }

        [ConfigurationItem("SecretFloat", 1)]
        public float SecretFloat { get; set; }


        [ConfigurationItem("SecretTimeSpan", 0)]
        public TimeSpan SecretTimeSpan { get; set; }

        public void LoadSettings()
        {
            Type type = typeof(ConfigurationComponentBase);
            var props = type.GetProperties();
            foreach (var item in props)
            {
                foreach (var attribute in item.GetCustomAttributes())
                {
                    ConfigurationItemAttribute validation = attribute as ConfigurationItemAttribute;

                    var converter = TypeDescriptor.GetConverter(item.PropertyType);
                    item.SetValue(this, converter.ConvertFromString(validation.Value.ToString()));
                }
            }
        }

        public void SaveSettings()
        {
            Type type = typeof(ConfigurationComponentBase);
            var props = type.GetProperties();
            foreach (var item in props)
            {
                foreach (var attribute in item.GetCustomAttributes())
                {
                    ConfigurationItemAttribute validation = attribute as ConfigurationItemAttribute;

                    validation.Value = item.GetValue(this);
                }
            }
        }
    }
}
