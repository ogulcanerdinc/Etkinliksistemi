using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Helper;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Database;
using Eys.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Impl.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EysBaseContext context;
        public CategoryService(EysBaseContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResult<Category>> Add(Category model)
        {
            var result = new ServiceResult<Category>();
            try
            {
                if (model != null)
                {
                    await context.Category.AddAsync(model);
                    var Saveresult = await context.SaveChangesAsync();
                    if (Saveresult > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Kategori Başarıyla Eklendi";
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
        public async Task<ServiceResult<Category>> Update(Category model)
        {
            var result = new ServiceResult<Category>();
            try
            {
                var repositoryResponse = await context.Category.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.CategoryName = model.CategoryName;
                    repositoryResponse.DateModified = DateTime.Now;
                    context.Category.Update(repositoryResponse);
                    var SaveResult = await context.SaveChangesAsync();
                    if (SaveResult > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Kategori Başarıyla Güncellendi";

                    }
                }
                else
                {
                    result.Message = "Kayıt Bulunamadı.";
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return result;
        }

        public async Task<ServiceResult<bool>> Delete(Guid id)
        {
            var result = new ServiceResult<bool>();
            var repoResponse = await context.Category.FirstOrDefaultAsync(x => x.Id == id);

            result.IsSuccess = false;
            if (repoResponse != null)
            {
                repoResponse.IsActive = false;
                repoResponse.IsDeleted = true;
                context.Category.Update(repoResponse);
                var SaveResult = await context.SaveChangesAsync();
                if (SaveResult > 0)
                    result.IsSuccess = true;
                result.Message = "Silme İşlemi Başarılı.";
            }
            else
            {
                result.Message = ("Kayıt bulunamadı");
            }
            return result;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var model = await context.Category.Where(p => p.IsDeleted == false && p.IsActive).Select(c => new CategoryViewModel
            {
                CategoryName = c.CategoryName,
                Id = c.Id,
            }).OrderBy(x => x.CategoryName).ToListAsync();
            return model;
        }

        public (List<CategoryViewModel> list, int total) GetAllForDatatables(DataTableBaseModel model, CategoryDTParameter dTParameter)
        {
            var data = GetAll().Result.AsQueryable();
            data = model.OrderAscDirection ? data.OrderByDynamic(model.OrderCriteria, LinqExtensions.Order.Asc) : data.OrderByDynamic(model.OrderCriteria, LinqExtensions.Order.Desc);
            if (!string.IsNullOrEmpty(dTParameter.CategoryName))
            {
                data = data.Where(r => r.CategoryName.ToLower().Contains(dTParameter.CategoryName.ToLower()));
            }
            var totalResultsCount = data.Count();
            data = data.Skip(model.Start).Take(model.Length);

            return (data.ToList(), totalResultsCount);
        }

        public async Task<CategoryViewModel> GetCategoryById(Guid id)
        {
            var model = new CategoryViewModel();

            var getCategory = await context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (getCategory != null)
            {
                model.Id = getCategory.Id;
                model.CategoryName = getCategory.CategoryName;
                model.IsActive = getCategory.IsActive;
            }
            return model;
		}
		public async Task<CategoryViewModel> GetCategoryBySlug(string CategoryName)
		{
			var model = new CategoryViewModel();

			var getCategory = await context.Category.FirstOrDefaultAsync(x => x.CategoryName == CategoryName);
			if (getCategory != null)
			{
				model.Id = getCategory.Id;
				model.CategoryName = getCategory.CategoryName;
				model.IsActive = getCategory.IsActive;
			}
			return model;
		}

	}
}
