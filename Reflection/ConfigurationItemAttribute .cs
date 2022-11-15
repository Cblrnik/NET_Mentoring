using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        private readonly IConfigurationRoot _configuration;
        private readonly JsonConfigurationProvider _jsonConfigurationProvider;
        private readonly string _settingName;
        private readonly int _providerType;

        private string FileName
        {
            get
            {
                if (_providerType == 0)
                {
                    return "appsettings.json";
                }
                else
                {
                    return "settings.json";
                }
            }
        }

        public object Value {
            get
            {
                if (_providerType == 0)
                {
                    return _configuration[_settingName];
                }

                if (_jsonConfigurationProvider.TryGet(_settingName, out string value))
                {
                    return value;
                };

                return default;
            }

            set
            {
                if (value != null && !value.ToString().Equals(Value))
                {
                    if (_providerType == 0)
                    {
                        _configuration[_settingName] = value.ToString();
                    }
                    else
                    {
                        _jsonConfigurationProvider.Set(_settingName, value.ToString());
                    }

                    SaveSettings();
                }
            }
        }

        public ConfigurationItemAttribute(string settingName, int providerType)
        {
            _settingName = settingName;
            _providerType = providerType;
            switch (providerType)
            {
                case 0:
                    _configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .Build();
                    break;
                default:
                    var source = new JsonConfigurationSource
                    {
                        Optional = false,
                        ReloadOnChange = true
                    };

                    _jsonConfigurationProvider = new JsonConfigurationProvider(source);
                    using (var sr = new StreamReader("settings.json"))
                    {
                        _jsonConfigurationProvider.Load(sr.BaseStream);
                    }
                        
                    break;
            }
        }

        private void SaveSettings()
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, FileName);
                var json = File.ReadAllText(filePath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                jsonObj[_settingName] = JToken.Parse($"\"{Value}\"");


                var output = JsonConvert.SerializeObject(jsonObj,Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
