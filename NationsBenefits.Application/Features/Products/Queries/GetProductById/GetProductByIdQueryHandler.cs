﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NationsBenefits.Application.Cache;
using NationsBenefits.Application.Constants;
using NationsBenefits.Application.Contracts.Persistence;
using NationsBenefits.Application.Exceptions;
using NationsBenefits.Application.Models;
using NationsBenefits.Domain;

namespace NationsBenefits.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetProductByIdQueryHandler(
            ILogger<GetProductByIdQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"{RedisValues.ProductsKey}_{request.Id}";
            var product = _cacheService.GetData<Product>(redisKey);
            if (product == null)
            {
                product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

                if (product == null)
                {
                    _logger.LogError(string.Format(ErrorMessages.EntityNotFound, nameof(Product), request.Id));
                    throw new NotFoundException(nameof(Product), request.Id);
                }

                var expirationTime = DateTime.Now.AddMinutes(RedisValues.CacheTimeInMinutes);
                _cacheService.SetData<Product>(redisKey, product, expirationTime);
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
