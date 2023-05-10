using Eys.Domain.Helper;
using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Models;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Database;
using Eys.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Eys.Domain.Services.Impl.Services
{
    public class CityService : ICityService
    {
        private readonly EysBaseContext context;
        public CityService(EysBaseContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResult<City>> Add(City model)
        {
            var result = new ServiceResult<City>();
            try
            {
                if (model != null)
                {
                    await context.City.AddAsync(model);
                    var Saveresult = await context.SaveChangesAsync();
                    if (Saveresult > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Şehir Başarıyla Eklendi";
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
        public async Task<ServiceResult<City>> Update(City model)
        {
            var result = new ServiceResult<City>();
            try
            {
                var repositoryResponse = await context.City.FirstOrDefaultAsync(x => x.id == model.id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Name = model.Name;
                    context.City.Update(repositoryResponse);
                    var SaveResult = await context.SaveChangesAsync();
                    if (SaveResult > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Şehir Başarıyla Güncellendi";

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

        public async Task<ServiceResult<bool>> Delete(int id)
        {
            var result = new ServiceResult<bool>();
            var repoResponse = await context.City.FirstOrDefaultAsync(x => x.id == id);

            result.IsSuccess = false;
            if (repoResponse != null)
            {
                repoResponse.isActive = false;
                context.City.Update(repoResponse);
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

        public async Task<List<CityViewModel>> GetAll()
        {
            var model = await context.City.Where(p =>p.isActive).Select(c => new CityViewModel
            {
                Name = c.Name,
                id = c.id,
            }).OrderBy(x => x.Name).ToListAsync();
            return model;
        }

        public (List<CityViewModel> list, int total) GetAllForDatatables(DataTableBaseModel model, CityDTParameter dTParameter)
        {
            var data = GetAll().Result.AsQueryable();
            //data = model.OrderAscDirection ? data.OrderByDynamic(model.OrderCriteria, LinqExtensions.Order.Asc) : data.OrderByDynamic(model.OrderCriteria, LinqExtensions.Order.Desc);
            if (!string.IsNullOrEmpty(dTParameter.Name))
            {
                data = data.Where(r => r.Name.ToLower().Contains(dTParameter.Name.ToLower()));
            }
            var totalResultsCount = data.Count();
            data = data.Skip(model.Start).Take(model.Length);

            return (data.ToList(), totalResultsCount);
        }

        public async Task<CityViewModel> GetCityById(int id)
        {
            var model = new CityViewModel();

            var getCity = await context.City.FirstOrDefaultAsync(x => x.id == id);
            if (getCity != null)
            {
                model.id = getCity.id;
                model.Name = getCity.Name;
                model.isActive = getCity.isActive;
            }
            return model;
        }

    }
}
