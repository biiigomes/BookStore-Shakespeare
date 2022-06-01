using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Products.Commands;
using CleanArch.Application.Products.Queries;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;

namespace CleanArch.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ??
               throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);
            var result = await _mediator.Send(productByIdQuery);

            if(productByIdQuery == null)
              throw new Exception($"Entity could not be loaded");

            return _mapper.Map<ProductDTO>(result);
        }

        // public async Task<ProductDTO> GetProductCategory(int? id)
        // {
        //     var productByIdQuery = new GetProductByIdQuery(id.Value);
        //     var result = await _mediator.Send(productByIdQuery);

        //     if(productByIdQuery == null)
        //     {
        //         throw new Exception($"Entity could not be loaded");
        //     }

        //     return _mapper.Map<ProductDTO>(result);
        // }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();
            var result = await _mediator.Send(productsQuery);

            if(productsQuery == null)
            {
                throw new Exception($"Entity could not be loaded");
            }

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if(productRemoveCommand == null)
            {
                throw new Exception($"Entity could not be loaded");
            }
            await _mediator.Send(productRemoveCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }
    }
}