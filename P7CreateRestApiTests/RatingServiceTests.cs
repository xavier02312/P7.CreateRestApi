namespace P7CreateRestApiTests
{
    public class RatingServiceTests
    {
        // Services
        private readonly RatingService _ratingService;
        private readonly Mock<IRatingRepository> _ratingRepositoryMock = new();
        public RatingServiceTests()
        {
            _ratingService = new RatingService(_ratingRepositoryMock.Object);
        }

        [Fact]
        // Création de Rating on doit avoir un modèle de sortie Rating
        public void CreateRating_ShouldHaveRatingOutputModel()
        {
            // Arrange
            var inputModel = new RatingInputModel
            {
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            };
            _ratingRepositoryMock.Setup(m => m.Create(It.IsAny<Rating>()));

            // Act
            var outputModel = _ratingService.Create(inputModel);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(inputModel.MoodysRating, outputModel.MoodysRating);
            Assert.Equal(inputModel.SandPRating, outputModel.SandPRating);
            Assert.Equal(inputModel.FitchRating, outputModel.FitchRating);
            Assert.Equal(inputModel.OrderNumber, outputModel.OrderNumber);
            _ratingRepositoryMock.Verify(m => m.Create(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        // Supprimer Rating, Doit avoir un modèle de sortie Rating
        public void DeleteRating_ShouldHaveRatingOutputModel()
        {
            // Arrange
            var ratingExcepted = new Rating()
            {
                Id = 1,
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            };
            _ratingRepositoryMock.Setup(m => m.Delete(1)).Returns(ratingExcepted);

            // Act
            var outputModel = _ratingService.Delete(1);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(ratingExcepted.Id, outputModel.Id);
            Assert.Equal(ratingExcepted.MoodysRating, outputModel.MoodysRating);
            Assert.Equal(ratingExcepted.SandPRating, outputModel.SandPRating);
            Assert.Equal(ratingExcepted.FitchRating, outputModel.FitchRating);
            Assert.Equal(ratingExcepted.OrderNumber, outputModel.OrderNumber);
            _ratingRepositoryMock.Verify(m => m.Delete(1), Times.Once);
        }

        [Fact]
        // Supprimer Rating et n’existe pas, Devrait être nul
        public void DeleteRatingDoesntExist_ShouldBeNull()
        {
            // Arrange
            _ratingRepositoryMock.Setup(m => m.Delete(1));

            // Act
            var outputModel = _ratingService.Delete(1);

            // Assert
            Assert.Null(outputModel);
            _ratingRepositoryMock.Verify(m => m.Delete(1), Times.Once);
        }

        [Fact]
        // Avoir un Rating en entrer, Doit avoir un modèle de sortie Rating
        public void GetRating_ShouldHaveRatingOutputModel()
        {
            // Arrange
            var ratingExcepted = new Rating()
            {
                Id = 1,
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            };
            _ratingRepositoryMock.Setup(m => m.Get(1)).Returns(ratingExcepted);

            // Act
            var outputModel = _ratingService.Get(1);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(ratingExcepted.MoodysRating, outputModel.MoodysRating);
            Assert.Equal(ratingExcepted.SandPRating, outputModel.SandPRating);
            Assert.Equal(ratingExcepted.FitchRating, outputModel.FitchRating);
            Assert.Equal(ratingExcepted.OrderNumber, outputModel.OrderNumber);
        }

        [Fact]
        // Avoir un Rating et qui n’existe pas doit être null
        public void GetRatingDoesntExist_ShouldBeNull()
        {
            // Arrange
            _ratingRepositoryMock.Setup(m => m.Get(1));

            // Act
            var outputModel = _ratingService.Get(1);

            // Assert
            Assert.Null(outputModel);
            _ratingRepositoryMock.Verify(m => m.Get(1), Times.Once);
        }

        [Fact]
        // Mettre à jour Rating, on doit avoir mis à jour Rating
        public void UpdateRating_ShouldHaveUpdateRating()
        {
            // Arrange
            var ratingExcepted = new Rating()
            {
                Id = 1,
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            };
            var inputModel = new RatingInputModel()
            {
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            };
            _ratingRepositoryMock.Setup(m => m.Update(It.IsAny<Rating>())).Returns(ratingExcepted);

            // Act
            var outputModel = _ratingService.Update(1, inputModel);

            // Assert
            Assert.NotNull(outputModel);
            Assert.Equal(ratingExcepted.Id, outputModel.Id);
            Assert.Equal(ratingExcepted.MoodysRating, outputModel.MoodysRating);
            Assert.Equal(ratingExcepted.SandPRating, outputModel.SandPRating);
            Assert.Equal(ratingExcepted.FitchRating, outputModel.FitchRating);
            Assert.Equal(ratingExcepted.OrderNumber, outputModel.OrderNumber);
            _ratingRepositoryMock.Verify(m => m.Update(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        // La mise à jour de Rating et il n’existe pas, on doit avoir un résulat null
        public void UpdateRatingDoesntExist_ShouldBeNull()
        {
            // Arrange
            _ratingRepositoryMock.Setup(m => m.Update(It.IsAny<Rating>()));

            // Act
            var outputModel = _ratingService.Update(1, new RatingInputModel
            {
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            });

            // Assert
            Assert.Null(outputModel);
            _ratingRepositoryMock.Verify(m => m.Update(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        // Liste Rating avec un Rating devrait avoir un Rating dans la liste
        public void ListRatingWithOneRating_ShouldHaveOneRatingInList()
        {
            // Arrange
            var ratingExcepted = new Rating()
            {
                Id = 1,
                MoodysRating = "A1",
                SandPRating = "A+",
                FitchRating = "A+",
                OrderNumber = 1
            };
            _ratingRepositoryMock.Setup(m => m.List()).Returns(new List<Rating> { ratingExcepted });

            // Act
            var list = _ratingService.List();

            // Assert
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Equal(ratingExcepted.Id, list[0].Id);
            Assert.Equal(ratingExcepted.MoodysRating, list[0].MoodysRating);
            Assert.Equal(ratingExcepted.SandPRating, list[0].SandPRating);
            Assert.Equal(ratingExcepted.FitchRating, list[0].FitchRating);
            Assert.Equal(ratingExcepted.OrderNumber, list[0].OrderNumber);
            _ratingRepositoryMock.Verify(m => m.List(), Times.Once);
        }

        [Fact]
        // La liste Rating vide on doit avoir la liste vide
        public void ListRatingEmpty_ShouldHaveEmptyRating()
        {
            // Arrange
            _ratingRepositoryMock.Setup(m => m.List()).Returns(new List<Rating>());

            // Act
            var list = _ratingService.List();

            // Assert
            Assert.NotNull(list);
            Assert.Empty(list);
            _ratingRepositoryMock.Verify(repo => repo.List(), Times.Once);
        }
    }
}
