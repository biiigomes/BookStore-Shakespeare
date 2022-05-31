using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Application.DTOs;

namespace CleanArch.Application.Interfaces
{
    public interface ICategoryService //definir contrato(métodos) de categoria
    {
         Task<IEnumerable<CategoryDTO>> GetCategories();
         Task<CategoryDTO> GetById(int? id);
         Task Add(CategoryDTO categoryDTO);
         Task Update(CategoryDTO categoryDTO);
         Task Remove(int? id);
    }
}