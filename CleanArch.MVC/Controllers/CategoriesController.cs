using System;
using System.Threading.Tasks;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.MVC.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

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

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.Add(categoryDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            var categoryDTO = await _categoryService.GetById(id);
            
            if(id == null)
            {
                return NotFound();
            }

            if(categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(categoryDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception)
                {
                    throw;
                }
            }

            return View(categoryDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoryDTO = await _categoryService.GetById(id);

            if(categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            var categoryDTO = await _categoryService.GetById(id);

            if(id == null)
            {
                return NotFound();
            }
            if(categoryDTO == null)
            {
                return NotFound();
            }
            return View(categoryDTO);

        }

        
    }
}