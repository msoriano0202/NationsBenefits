using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Features.Products.Commands.UpdateProduct;
using NationsBenefits.Application.Mappings;
using NationsBenefits.Application.UnitTests.Mocks;
using NationsBenefits.Infrastructure.Repositories;
using Shouldly;

namespace NationsBenefits.Application.UnitTests.Features.Products.UpdateProduct
{
    public class UpdateProductCommandHandlerUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ILogger<UpdateProductCommandHandler>> _logger;

        public UpdateProductCommandHandlerUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<UpdateProductCommandHandler>>();
            _cacheService = new Mock<ICacheService>();

            MockSubCategoryRepository.AddDataSubCategoryRepository(_unitOfWork.Object.NationsBenefitsDbContext);
            MockProductRepository.AddDataProductRepository(_unitOfWork.Object.NationsBenefitsDbContext);
        }

        [Fact]
        public async Task UpdateProductCommand_InputSubCategory_ReturnsId()
        {
            var command = new UpdateProductCommand
            {
                Id = 8000,
                Name = "test",
                Ski = "test",
                SubcategoryId = 8001,
                Description = "test"
            };

            var handler = new UpdateProductCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
