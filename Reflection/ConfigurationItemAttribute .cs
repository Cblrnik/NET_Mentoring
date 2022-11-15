using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        private readonly IConfigurationRoot configuration;
        private readonly JsonConfigurationProvider jsonConfigurationProvider;
        private readonly string settingName;
        private readonly int providerType;

        private string FileName
        {
            get
            {
                if (this.providerType == 0)
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
                if (this.providerType == 0)
                {
                    return this.configuration[this.settingName];
                }

                if (this.jsonConfigurationProvider.TryGet(this.settingName, out string value))
                {
                    return value;
                };

                return default;
            }

            set
            {
                if (value != null && !value.ToString().Equals(this.Value))
                {
                    if (this.providerType == 0)
                    {
                        this.configuration[this.settingName] = value.ToString();
                    }
                    else
                    {
                        this.jsonConfigurationProvider.Set(settingName, value.ToString());
                    }

                    this.SaveSettings();
                }
            }
        }

        public ConfigurationItemAttribute(string settingName, int providerType)
        {
            this.settingName = settingName;
            this.providerType = providerType;
            switch (providerType)
            {
                case 0:
                    this.configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .Build();
                    break;
                default:
                    var source = new JsonConfigurationSource
                    {
                        Optional = false,
                        ReloadOnChange = true
                    };

                    this.jsonConfigurationProvider = new JsonConfigurationProvider(source);
                    using (var sr = new StreamReader("settings.json"))
                    {
                        this.jsonConfigurationProvider.Load(sr.BaseStream);
                    }
                        
                    break;
            }
        }

        private void SaveSettings()
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, this.FileName);
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                jsonObj[this.settingName] = JToken.Parse($"\"{this.Value}\"");


                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
