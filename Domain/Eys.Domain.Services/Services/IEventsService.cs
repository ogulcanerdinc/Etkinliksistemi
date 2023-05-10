using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Models;
using Eys.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eys.Domain.Models.FilterModel;

namespace Eys.Domain.Services.Services
{
    public interface IEventsService
    {
        Task<List<EventsViewModel>> GetAll();
        Task<EventsViewModel> GetEventsById(Guid id);
        Task<ServiceResult<Events>> Add(EventsViewModel model);
        Task<ServiceResult<Events>> Update(EventsViewModel model);
        Task<ServiceResult<bool>> Delete(Guid id,string UserId);
		Task<ServiceResult<bool>> EventCancelled(Guid id, string UserId);
		(List<EventsViewModel> list, int total) GetAllForDatatables(DataTableBaseModel model, EventsDTParameter dTParameter);
        Task<ServiceResult<List<EventsViewModel>>> GetAllByFilterModel(EventFilterModel model);
    }
}
