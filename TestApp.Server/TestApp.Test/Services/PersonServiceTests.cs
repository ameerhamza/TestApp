using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TestApp.Services.Contracts;

namespace TestApp.Tests
{
    [TestFixture]
    public class PersonServiceTests
    {
        private TestBootstrap _testBootstrap;

        [SetUp]
        public void Setup()
        {
            // Initialize the bootstrap class before each test
            _testBootstrap = new TestBootstrap();
        }

        [Test]
        public async Task AddPersonAsync_ValidPerson_ReturnsAddedPerson()
        {
            // Arrange
            var personService = _testBootstrap.MockPersonService.Object;
            var personToAdd = new Mock<IPerson>();

            // Configure the mock behavior for the specific test scenario
            _testBootstrap.MockPersonService.Setup(service => service.AddPersonAsync(personToAdd.Object))
                .ReturnsAsync(personToAdd.Object);

            // Act
            var addedPerson = await personService.AddPersonAsync(personToAdd.Object);

            // Assert
            ClassicAssert.NotNull(addedPerson);
            ClassicAssert.AreEqual(personToAdd.Object, addedPerson);
        }

    }
}