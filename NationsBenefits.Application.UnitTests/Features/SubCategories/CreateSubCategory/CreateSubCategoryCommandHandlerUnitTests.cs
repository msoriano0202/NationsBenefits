using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Features.SubCategories.Commands.CreateSubCategory;
using NationsBenefits.Application.Mappings;
using NationsBenefits.Application.UnitTests.Mocks;
using NationsBenefits.Infrastructure.Repositories;
using Shouldly;

namespace NationsBenefits.Application.UnitTests.Features.SubCategories.CreateSubCategory
{
    public class CreateSubCategoryCommandHandlerUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ILogger<CreateSubCategoryCommandHandler>> _logger;

        public CreateSubCategoryCommandHandlerUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateSubCategoryCommandHandler>>();
            _cacheService = new Mock<ICacheService>();

            MockSubCategoryRepository.AddDataSubCategoryRepository(_unitOfWork.Object.NationsBenefitsDbContext);
        }

        [Fact]
        public async Task CreateSubCategoryCommand_InputSubCategory_ReturnsId()
        {
            var command = new CreateSubCategoryCommand
            {
                CategoryId = 1,
                Code = "test",
                Description = "test"
            };

            var handler = new CreateSubCategoryCommandHandler(_logger.Object, _mapper, _unitOfWork.Object, _cacheService.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
