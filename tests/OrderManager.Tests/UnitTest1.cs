using System.Diagnostics.CodeAnalysis;

namespace OrderManager.Tests
{
    [ExcludeFromCodeCoverage]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test_Fail()
        {
            Assert.True(false);
        }
    }
}