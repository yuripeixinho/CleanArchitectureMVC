﻿using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private ApplicationDbContext _productContext;

    public ProductRepository(ApplicationDbContext context)
    {
        _productContext = context;
    }

    public async Task<Product> GetProductByIdAsync(int? id)
    {
        return await _productContext.Products.FindAsync(id);
    }

    public async Task<Product> GetProductCategoryAsync(int productId)
    {
        return await _productContext.Products.Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productContext.Products.ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _productContext.Products.Add(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _productContext.Products.Update(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        _productContext.Products.Remove(product);
        await _productContext.SaveChangesAsync();
        return product;
    }
}
