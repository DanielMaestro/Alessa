using System.Threading.Tasks;
using Xunit;

namespace QueryBuilder.Test.Saving
{
    public class SavingTestBase : SchemaTestBase
    {
        public SavingTestBase() : base(@"C:\Temp\SavingTest.log")
        {

        }

        [Fact]
        public void Save()
        {
            base.SchemaData.ProcessBeforeCreateRecordAsync(new Alessa.QueryBuilder.Entities.BuilderParameters.SaveParameters()
            {
                ItemName = "HideEnableSamplesView",
                SaveType = Alessa.QueryBuilder.Entities.ESaveType.Create
            }).Wait();
        }
    }
}
