using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Features.Products.Commands.CreateProduct;
using NationsBenefits.Application.Mappings;
using NationsBenefits.Application.UnitTests.Mocks;
using NationsBenefits.Infrastructure.Repositories;
using Shouldly;

namespace NationsBenefits.Application.UnitTests.Features.Products.CreateProduct
{
    public class CreateProductCommandHandlerUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ILogger<CreateProductCommandHandler>> _logger;

        public CreateProductCommandHandlerUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<CreateProductCommandHandler>>();
            _cacheService = new Mock<ICacheService>();

            MockSubCategoryRepository.AddDataSubCategoryRepository(_unitOfWork.Object.NationsBenefitsDbContext);
            MockProductRepository.AddDataProductRepository(_unitOfWork.Object.NationsBenefitsDbContext);
        }

        [Fact]
        public async Task CreateProductCommand_InputProduct_ReturnsId()
        {
            var command = new CreateProductCommand
            {
                SubcategoryId = 8001,
                Name = "Test",
                Ski = "Test",
                Description = "test"
            };

            var handler = new CreateProductCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
