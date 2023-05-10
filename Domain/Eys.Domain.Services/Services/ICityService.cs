using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Models;
using Eys.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Services
{
    public interface ICityService
    {
        Task<List<CityViewModel>> GetAll();
        Task<CityViewModel> GetCityById(int id);
        Task<ServiceResult<City>> Add(City model);
        Task<ServiceResult<City>> Update(City model);
        Task<ServiceResult<bool>> Delete(int id);
        (List<CityViewModel> list, int total) GetAllForDatatables(DataTableBaseModel model, CityDTParameter dTParameter);
    }
}
