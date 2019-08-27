using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.Builder.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class ConfigDataTest
    {
        [Fact]
        public void ConfigData_GetConfig()
        {
            ConfigData configData = new ConfigData();

            TableConfig personConfig = configData.GetConfigOrDefault(typeof(Person));

            Assert.Equal(personConfig, configData.GetConfig(typeof(Person)));
        }

        [Fact]
        public void ConfigData_GetParentConfig()
        {
            ConfigData configData = new ConfigData();

            TableConfig personConfig = configData.GetConfigOrDefault(typeof(Person));
            TableConfig employeeConfig = configData.GetConfigOrDefault(typeof(Employee));

            Assert.Equal(personConfig, configData.GetParentConfig(typeof(Employee)));
        }
    }
}