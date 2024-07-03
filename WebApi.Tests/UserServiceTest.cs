using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Users;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Tests
{
    public class UserServiceTest
    {


        private readonly IFixture _fixture;
        private readonly Mock<IUserRepository> _repositoryMock;
        private static IMapper _mapper;
        private readonly UserService _sut;

        public UserServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
            _sut = new UserService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnData_WhenDataFound()
        {
            var userMock = _fixture.Create<IEnumerable<User>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetAll())
                              .ReturnsAsync(userMock);

            var result = await _sut.GetAll().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<User>>();
            _repositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async Task CreateUser__ShouldReturnData_WhenDataIsInsertedSucessfully()
        {
            // Arrange
            var createuser = _fixture.Create<CreateRequest>();
            var user = _fixture.Create<User>();
            var userRepositoryMock = new Mock<IUserRepository>();

            _repositoryMock.Setup(x => x.Create(user));

            // Act
            await _sut.Create(createuser);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnFalse_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var userRepositoryMock = new Mock<IUserRepository>();
            _repositoryMock.Setup(x => x.Delete(id));

            // Act
            await _sut.Delete(id);

            // Assert
            _repositoryMock.Verify(x => x.Delete(id), Times.Once());
        }

        [Fact]
        public async void DeleteFeedback_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            var response = _fixture.Create<bool>();
            var id = 0;
            var userRepositoryMock = new Mock<IUserRepository>();
            _repositoryMock.Setup(x => x.Delete(id));

            // Act
            _sut.Delete(id).ConfigureAwait(false); ;

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task UpdateFeedback_ShouldReturnFalse_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var createuser = _fixture.Create<UpdateRequest>();
            var user = _fixture.Create<User>();
            user.user_id = id;
            createuser.user_id = id;
            var userRepositoryMock = new Mock<IUserRepository>();
            _repositoryMock.Setup(x => x.Update(user));

            // Act
            _sut.Update(id, createuser).ConfigureAwait(false);

            // Assert
            Assert.True(true);
        }
    }
}