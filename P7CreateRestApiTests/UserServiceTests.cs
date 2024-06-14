using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace P7CreateRestApiTests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<UserManager<User>> _userManagerMock;

        public UserServiceTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            var optionsMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var userValidators = new List<IUserValidator<User>>();
            var passwordValidators = new List<IPasswordValidator<User>>();
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorsMock = new Mock<IdentityErrorDescriber>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<User>>>();
            _userManagerMock = new(
                userStoreMock.Object,
                optionsMock.Object,
                passwordHasherMock.Object,
                userValidators,
                passwordValidators,
                keyNormalizerMock.Object,
                errorsMock.Object,
                serviceProviderMock.Object,
                loggerMock.Object);
            _userService = new UserService(_userRepositoryMock.Object, _userManagerMock.Object);
        }

        [Fact]
        // Création d'un Utilisateur doit avoir un modèle de sortie Utilisateur
        public void CreateUser_ShouldHaveUserOutputModel()
        {
            // Arrange
            var inputModel = new UserInputModel
            {
                UserName = "UserName",
                FullName = "FullName",
                Role = "Admin",
                Password = "SuperP4ssw0rd!"
            };
            var user = new User
            {
                UserName = inputModel.UserName,
                FullName = inputModel.FullName,
                Role = inputModel.Role
            };
            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var outputModel = _userService.Create(inputModel).Result;

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(inputModel.UserName, outputModel.UserName);
            Assert.Equal(inputModel.FullName, outputModel.FullName);
            Assert.Equal(inputModel.Role, outputModel.Role);
            _userManagerMock.Verify(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        // Supprimer un Utilisateur , Doit avoir le modèle de sortie Utilisateur 
        public void DeleteUser_ShouldHaveUserOutputModel()
        {
            // Arrange
            var userExcepted = new User()
            {
                Id = 1,
                UserName = "UserName",
                FullName = "FullName",
                Role = "Admin"
            };
            _userRepositoryMock.Setup(m => m.FindById(1)).Returns(userExcepted);
            _userManagerMock.Setup(m => m.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var outputModel = _userService.Delete(1).Result;

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(userExcepted.Id, outputModel.Id);
            Assert.Equal(userExcepted.UserName, outputModel.UserName);
            Assert.Equal(userExcepted.FullName, outputModel.FullName);
            Assert.Equal(userExcepted.Role, outputModel.Role);
        }

        [Fact]
        // Supprimer un Utilisateur qui n’existe pas, Devrait être nul
        public void DeleteUserDoesntExist_ShouldBeNull()
        {
            // Arrange
            _userRepositoryMock.Setup(m => m.FindById(1));

            // Act
            var outputModel = _userService.Delete(1).Result;

            // Assert
            Assert.Null(outputModel);
        }

        [Fact]
        // Avoir un Utilisateur en entrer, Doit avoir un modèle de sortie Utilisateur
        public void GetUser_ShouldHaveUserOutputModel()
        {
            // Arrange
            var userExcepted = new User()
            {
                Id = 1,
                UserName = "UserName",
                FullName = "FullName",
                Role = "Admin"
            };
            _userRepositoryMock.Setup(m => m.FindById(1)).Returns(userExcepted);

            // Act
            var outputModel = _userService.Get(1);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(userExcepted.Id, outputModel.Id);
            Assert.Equal(userExcepted.UserName, outputModel.UserName);
            Assert.Equal(userExcepted.FullName, outputModel.FullName);
            Assert.Equal(userExcepted.Role, outputModel.Role);
            _userRepositoryMock.Verify(m => m.FindById(1), Times.Once);
        }

        [Fact]
        // Ne pas avoir d'Utilisateur on doit avoir un résultat null 
        public void GetUserDoesntExist_ShouldBeNull()
        {
            // Arrange
            _userRepositoryMock.Setup(m => m.FindById(1));

            // Act
            var outputModel = _userService.Get(1);

            // Assert
            Assert.Null(outputModel);
            _userRepositoryMock.Verify(m => m.FindById(1), Times.Once);
        }

        [Fact]
        // Liste des Utilisateurs avec 2 Utilisateurs, doit avoir 2 Utilisateurs dans la liste
        public void ListUserWith2User_ShouldHave2UserInList()
        {
            // Arrange
            var usersExcepted = new List<User>
            {
                new User
                {
                    Id = 1,
                    UserName = "UserName1",
                    FullName = "FullName1",
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    UserName = "UserName2",
                    FullName = "FullName2",
                    Role = "User"
                }
            };
            _userRepositoryMock.Setup(m => m.FindAll()).ReturnsAsync(usersExcepted);

            // Act
            var list = _userService.List().Result;

            // Assert
            Assert.NotNull(list);
            Assert.Equal(usersExcepted.Count, list.Count);
            for (var i = 0; i < usersExcepted.Count; i++)
            {
                Assert.Equal(usersExcepted[i].Id, list[i].Id);
                Assert.Equal(usersExcepted[i].UserName, list[i].UserName);
                Assert.Equal(usersExcepted[i].FullName, list[i].FullName);
                Assert.Equal(usersExcepted[i].Role, list[i].Role);
            }
            _userRepositoryMock.Verify(m => m.FindAll(), Times.Once);
        }

        [Fact]
        // La liste Utilisateur et vide, la liste doit être vide
        public void ListUserEmpty_ShouldBeEmptyList()
        {
            // Arrange
            _userRepositoryMock.Setup(m => m.FindAll()).ReturnsAsync(new List<User>());

            // Act
            var list = _userService.List().Result;

            // Assert
            Assert.NotNull(list);
            Assert.Empty(list);
            _userRepositoryMock.Verify(m => m.FindAll(), Times.Once);
        }

        [Fact]
        // Mettre à jour l’Utilisateur, doit avoir un modèle de sortie Utilisateur
        public void UpdateUser_ShouldHaveUserOutputModel()
        {
            // Arrange
            var userExcepted = new User()
            {
                Id = 1,
                UserName = "UserName",
                FullName = "FullName",
                Role = "Admin"
            };
            var inputModel = new UserInputModel
            {
                UserName = "UserName",
                FullName = "FullName",
                Role = "Admin",
                Password = "SuperP4ssw0rd!"
            };
            _userRepositoryMock.Setup(m => m.FindById(1)).Returns(userExcepted);
            _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);
            _userManagerMock.Setup(m => m.GeneratePasswordResetTokenAsync(It.IsAny<User>())).ReturnsAsync("Token");
            _userManagerMock.Setup(m => m.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var outputModel = _userService.Update(1, inputModel).Result;

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(userExcepted.Id, outputModel.Id);
            Assert.Equal(userExcepted.UserName, outputModel.UserName);
            Assert.Equal(userExcepted.FullName, outputModel.FullName);
            Assert.Equal(userExcepted.Role, outputModel.Role);
            _userManagerMock.Verify(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(m => m.GeneratePasswordResetTokenAsync(It.IsAny<User>()), Times.Once);
            _userManagerMock.Verify(m => m.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(m => m.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        // Mettre à jour l’Utilisateur qui n’existe pas doit être null
        public void UpdateUserDoesntExist_ShouldBeNull()
        {
            // Arrange
            _userRepositoryMock.Setup(m => m.FindById(1));

            // Act
            var outputModel = _userService.Update(1, new UserInputModel
            {
                UserName = "UserName",
                FullName = "FullName",
                Role = "Admin",
                Password = "SuperP4ssw0rd!"
            }).Result;

            // Assert
            Assert.Null(outputModel);
        }
    }
}
