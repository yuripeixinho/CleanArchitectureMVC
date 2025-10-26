﻿using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUi.Controllers;

public class CategoriesController : Controller
{
    private ICategoryService _categoryService;  

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService; 
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategories();
        return View(categories);
    }
}
