using Suilder.Reflection.Builder;

namespace Suilder.Test.Reflection
{
    public class BaseTest
    {
        protected ITableBuilder tableBuilder;

        public BaseTest()
        {
            tableBuilder = new TableBuilder();
            InitConfig();
        }

        protected virtual void InitConfig()
        {
        }
    }
}