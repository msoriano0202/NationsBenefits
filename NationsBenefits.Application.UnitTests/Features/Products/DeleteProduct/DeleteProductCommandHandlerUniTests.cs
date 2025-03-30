using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Features.Products.Commands.DeleteProduct;
using NationsBenefits.Application.Mappings;
using NationsBenefits.Application.UnitTests.Mocks;
using NationsBenefits.Infrastructure.Repositories;

namespace NationsBenefits.Application.UnitTests.Features.Products.DeleteProduct
{
    public class DeleteProductCommandHandlerUniTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ILogger<DeleteProductCommandHandler>> _logger;

        public DeleteProductCommandHandlerUniTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<ILogger<DeleteProductCommandHandler>>();
            _cacheService = new Mock<ICacheService>();

            MockProductRepository.AddDataProductRepository(_unitOfWork.Object.NationsBenefitsDbContext);
        }

        [Fact]
        public async Task DeleteProductCommand_InputProduct_Returns()
        {
            var command = new DeleteProductCommand(8000);

            var handler = new DeleteProductCommandHandler(_logger.Object, _unitOfWork.Object);

            await handler.Handle(command, CancellationToken.None);
        }
    }
}
