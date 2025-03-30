using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NationsBenefits.Application.Features.SubCategories.Commands.UpdateSubCategory;
using NationsBenefits.Application.Mappings;
using NationsBenefits.Application.UnitTests.Mocks;
using NationsBenefits.Infrastructure.Repositories;
using Shouldly;

namespace NationsBenefits.Application.UnitTests.Features.SubCategories.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandlerUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdateSubCategoryCommandHandler>> _logger;

        public UpdateSubCategoryCommandHandlerUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateSubCategoryCommandHandler>>();

            MockSubCategoryRepository.AddDataSubCategoryRepository(_unitOfWork.Object.NationsBenefitsDbContext);
        }

        [Fact]
        public async Task UpdateSubCategoryCommand_InputSubCategory_ReturnsId()
        {
            var command = new UpdateSubCategoryCommand
            {
                Id = 8001,
                CategoryId = 1,
                Code = "test",
                Description = "test"
            };

            var handler = new UpdateSubCategoryCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
