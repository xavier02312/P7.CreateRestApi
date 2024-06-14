namespace P7CreateRestApiTests
{
    public class RuleNameServiceTests
    {
        // Services
        private readonly RuleNameService _ruleNameService;
        private readonly Mock<IRuleNameRepository> _ruleNameRepositoryMock = new();

        public RuleNameServiceTests()
        {
            _ruleNameService = new RuleNameService(_ruleNameRepositoryMock.Object);
        }

        [Fact]
        // Création d'un RuleName on doit avoir un modèle de sortie RuleName
        public void CreateRuleName_ShouldHaveRuleNameOutputModel()
        {
            // Arrange
            var inputModel = new RuleNameInputModel
            {
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            };
            _ruleNameRepositoryMock.Setup(m => m.Create(It.IsAny<RuleName>()));

            // Act
            var outputModel = _ruleNameService.Create(inputModel);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(inputModel.Name, outputModel.Name);
            Assert.Equal(inputModel.Description, outputModel.Description);
            Assert.Equal(inputModel.Json, outputModel.Json);
            Assert.Equal(inputModel.Template, outputModel.Template);
            Assert.Equal(inputModel.SqlStr, outputModel.SqlStr);
            Assert.Equal(inputModel.SqlPart, outputModel.SqlPart);
            _ruleNameRepositoryMock.Verify(m => m.Create(It.IsAny<RuleName>()), Times.Once);
        }

        [Fact]
        // Supprimer RuleName , Doit avoir le modèle de sortie RuleName 
        public void DeleteRuleName_ShouldHaveRuleNameOutputModel()
        {
            // Arrange
            var ruleNameExcepted = new RuleName()
            {
                Id = 1,
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            };
            _ruleNameRepositoryMock.Setup(m => m.Delete(1)).Returns(ruleNameExcepted);

            // Act
            var outputModel = _ruleNameService.Delete(1);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(ruleNameExcepted.Id, outputModel.Id);
            Assert.Equal(ruleNameExcepted.Name, outputModel.Name);
            Assert.Equal(ruleNameExcepted.Description, outputModel.Description);
            Assert.Equal(ruleNameExcepted.Json, outputModel.Json);
            Assert.Equal(ruleNameExcepted.Template, outputModel.Template);
            Assert.Equal(ruleNameExcepted.SqlStr, outputModel.SqlStr);
            Assert.Equal(ruleNameExcepted.SqlPart, outputModel.SqlPart);
            _ruleNameRepositoryMock.Verify(m => m.Delete(1), Times.Once);
        }

        [Fact]
        // Supprimer RuleName qui n’existe pas, Devrait être nul
        public void DeleteRuleNameDoesntExist_ShouldBeNull()
        {
            // Arrange
            _ruleNameRepositoryMock.Setup(m => m.Delete(1));

            // Act
            var outputModel = _ruleNameService.Delete(1);

            // Assert
            Assert.Null(outputModel);
            _ruleNameRepositoryMock.Verify(m => m.Delete(1), Times.Once);
        }

        [Fact]
        // Avoir un RuleName en entrer, Doit avoir un modèle de sortie RuleName
        public void GetRuleName_ShouldHaveRuleNameOutputModel()
        {
            // Arrange
            var ruleNameExcepted = new RuleName()
            {
                Id = 1,
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            };
            _ruleNameRepositoryMock.Setup(m => m.Get(1)).Returns(ruleNameExcepted);

            // Act
            var outputModel = _ruleNameService.Get(1);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(ruleNameExcepted.Id, outputModel.Id);
            Assert.Equal(ruleNameExcepted.Name, outputModel.Name);
            Assert.Equal(ruleNameExcepted.Description, outputModel.Description);
            Assert.Equal(ruleNameExcepted.Json, outputModel.Json);
            Assert.Equal(ruleNameExcepted.Template, outputModel.Template);
            Assert.Equal(ruleNameExcepted.SqlStr, outputModel.SqlStr);
            Assert.Equal(ruleNameExcepted.SqlPart, outputModel.SqlPart);
            _ruleNameRepositoryMock.Verify(m => m.Get(1), Times.Once);
        }

        [Fact]
        // Avoir un RuleName qui n’existe pas doit être null
        public void GetRuleNameDoesntExist_ShouldBeNull()
        {
            // Arrange
            _ruleNameRepositoryMock.Setup(m => m.Get(1));

            // Act
            var outputModel = _ruleNameService.Get(1);

            // Assert
            Assert.Null(outputModel);
            _ruleNameRepositoryMock.Verify(m => m.Get(1), Times.Once);
        }

        [Fact]
        // Liste RuleName avec un RuleName devrait avoir un RuleName dans la liste
        public void ListRuleNameWithOneRuleName_ShouldHaveOneRuleNameInList()
        {
            // Arrange
            var ruleNameExcepted = new RuleName()
            {
                Id = 1,
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            };
            _ruleNameRepositoryMock.Setup(m => m.List()).Returns(new List<RuleName> { ruleNameExcepted });

            // Act
            var list = _ruleNameService.List();

            // Assert
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Equal(ruleNameExcepted.Id, list[0].Id);
            Assert.Equal(ruleNameExcepted.Name, list[0].Name);
            Assert.Equal(ruleNameExcepted.Description, list[0].Description);
            Assert.Equal(ruleNameExcepted.Json, list[0].Json);
            Assert.Equal(ruleNameExcepted.Template, list[0].Template);
            Assert.Equal(ruleNameExcepted.SqlStr, list[0].SqlStr);
            Assert.Equal(ruleNameExcepted.SqlPart, list[0].SqlPart);
            _ruleNameRepositoryMock.Verify(m => m.List(), Times.Once);
        }

        [Fact]
        // La liste RuleName vide on doit avoir la liste vide
        public void ListRuleNameEmpty_ShouldHaveEmptyList()
        {
            // Arrange
            _ruleNameRepositoryMock.Setup(m => m.List()).Returns(new List<RuleName>());

            // Act
            var outputModel = _ruleNameService.List();

            // Assert
            Assert.NotNull(outputModel);
            Assert.Empty(outputModel);
            _ruleNameRepositoryMock.Verify(m => m.List(), Times.Once);
        }

        [Fact]
        // La mise à jour de RuleName et il n’existe pas, on doit avoir un résulat null
        public void UpdateRuleNameDoesntExist_ShouldBeNull()
        {
            // Arrange
            _ruleNameRepositoryMock.Setup(m => m.Update(It.IsAny<RuleName>()));

            // Act
            var outputModel = _ruleNameService.Update(1, new RuleNameInputModel
            {
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            });

            // Assert
            Assert.Null(outputModel);
            _ruleNameRepositoryMock.Verify(m => m.Update(It.IsAny<RuleName>()), Times.Once);
        }

        [Fact]
        // Mettre à jour RuleName on doit avoir mis à jour RuleName
        public void UpdateRuleName_ShouldHaveUpdateRuleName()
        {
            // Arrange
            var ruleNameExcepted = new RuleName()
            {
                Id = 1,
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            };
            var inputModel = new RuleNameInputModel()
            {
                Name = "RuleName",
                Description = "RuleDescription",
                Json = "Json",
                Template = "Template",
                SqlStr = "SqlStr",
                SqlPart = "SqlPart"
            };
            _ruleNameRepositoryMock.Setup(m => m.Update(It.IsAny<RuleName>())).Returns(ruleNameExcepted);

            // Act
            var outputModel = _ruleNameService.Update(1, inputModel);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(1, outputModel.Id);
            Assert.Equal(inputModel.Name, outputModel.Name);
            Assert.Equal(inputModel.Description, outputModel.Description);
            Assert.Equal(inputModel.Json, outputModel.Json);
            Assert.Equal(inputModel.Template, outputModel.Template);
            Assert.Equal(inputModel.SqlStr, outputModel.SqlStr);
            Assert.Equal(inputModel.SqlPart, outputModel.SqlPart);
            _ruleNameRepositoryMock.Verify(m => m.Update(It.IsAny<RuleName>()), Times.Once);
        }
    }
}
