using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetCategoryById(Guid id); 
        Task<CategoryViewModel> GetCategoryBySlug(string CategoryName);

		Task<ServiceResult<Category>> Add(Category model);
        Task<ServiceResult<Category>> Update(Category model);
        Task<ServiceResult<bool>> Delete(Guid id);
        (List<CategoryViewModel> list, int total) GetAllForDatatables(DataTableBaseModel model, CategoryDTParameter dTParameter);
    }
}
