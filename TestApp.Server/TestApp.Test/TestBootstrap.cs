using Moq;
using TestApp.Services.Contracts;

namespace TestApp.Tests
{
    public class TestBootstrap
    {
        public Mock<IPersonService> MockPersonService { get; private set; }

        public TestBootstrap()
        {
            // Initialize mocks for dependencies
            MockPersonService = new Mock<IPersonService>();
        }
    }
}