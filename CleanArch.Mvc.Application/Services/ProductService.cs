using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductService(IMapper mapper, IMediator meditator)
    {
        _mapper = mapper;   
        _mediator = meditator;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery is null)
            throw new Exception("Entity could not be loaded.");

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GeyById(int id)
    {
        var productsByIdQuery = new GetProductByIdQuery(id);

        if (productsByIdQuery is null)
            throw new Exception("Entity could not be loaded.");

        var result = await _mediator.Send(productsByIdQuery);

        return _mapper.Map<ProductDTO>(result);
    }

    //public async Task<ProductDTO> GetProductCategory(int id)
    //{
    //    var productsByIdQuery = new GetProductByIdQuery(id);

    //    if (productsByIdQuery is null)
    //        throw new Exception("Entity could not be loaded.");

    //    var result = await _mediator.Send(productsByIdQuery);

    //    return _mapper.Map<ProductDTO>(result);
    //}

    public async Task Add(ProductDTO productDTO)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
        await _mediator.Send(productUpdateCommand);
    }

    public async Task Remove(int id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id);

        if (productRemoveCommand is null)
            throw new Exception("Entity could not be loaded.");

        await _mediator.Send(productRemoveCommand);
    }
}
