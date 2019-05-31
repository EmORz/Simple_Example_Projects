using Forum.Services.Interfaces.Pagging;
using Forum.Services.UnitTests.Base;
using Xunit;

namespace Forum.Services.UnitTests.Pagging
{
    public class PaggingServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IPaggingService paggingService;

        public PaggingServiceTests(BaseUnitTest fixture)
        {
            this.paggingService = fixture.Provider.GetService(typeof(IPaggingService)) as IPaggingService;
        }

        [Fact]
        public void GetPagesCount_returns_correct_answer()
        {
            var expectedResult = 5;

            var actualResult = this.paggingService.GetPagesCount(25);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}