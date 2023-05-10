using Eys.Domain.Helper;
using Eys.Domain.Models;
using Eys.Domain.Models.Base;
using Eys.Domain.Models.DataTableModel;
using Eys.Domain.Models.FilterModel;
using Eys.Domain.Services.Services;
using Eys.Infra.Data.Database;
using Eys.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eys.Domain.Services.Impl.Services
{
    public class EventsService : IEventsService
    {

        private readonly EysBaseContext context;
        private readonly IFileService _fileService;
        private readonly IConfiguration _config;
        public EventsService(EysBaseContext context, IFileService fileService, IConfiguration config)
        {
            this.context = context;
            _fileService = fileService;
            _config = config;
        }

        public async Task<ServiceResult<Events>> Add(EventsViewModel model)
        {
            var result = new ServiceResult<Events>();
            try
            {
                if (model != null)
                {
                    var Events = new Events
                    {
                        EventName = model.EventName,
                        EventDescription = model.EventDescription,
                        EventShortDescription = model.EventShortDescription,
                        EventRules = model.EventRules,
                        EventStartDate = model.EventStartDate,
                        EventEndDate = model.EventEndDate,
                        EventAdress = model.EventAdress,
                        IsActive = model.IsActive,
                        CategoryId = model.CategoryId,
                        CityId = model.CityId,
                        Latitude = "0",
                        Longitude = "0",
                        Quota = model.Quota,
                        EventImages = new List<EventImages>()
                    };
                    int Order = 1;
                    foreach (var item in model.Images)
                    {
                        var ImageUpload = _fileService.AddImage(item).Result.Result;
                        Events.EventImages.Add(new EventImages
                        {
                            UploadedImage = ImageUpload,
                            UploadedImageId = ImageUpload.Id,
                            Order = Order
                        });
                        Order++;
                    }
                    await context.Events.AddAsync(Events);
                    var Saveresult = await context.SaveChangesAsync();
                    if (Saveresult > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = "Event Başarıyla Eklendi";
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
        public async Task<ServiceResult<Events>> Update(EventsViewModel model)
        {
            var result = new ServiceResult<Events>();
            try
            {
                var repositoryResponse = await context.Events.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (repositoryResponse != null)
                {
                    if(Convert.ToInt32(repositoryResponse.EventStartDate-DateTime.Now)>5)
					{
						repositoryResponse.EventName = model.EventName;
						repositoryResponse.EventDescription = model.EventDescription;
						repositoryResponse.EventShortDescription = model.EventShortDescription;
						repositoryResponse.EventRules = model.EventRules;
						repositoryResponse.EventStartDate = model.EventStartDate;
						repositoryResponse.EventEndDate = model.EventEndDate;
						repositoryResponse.EventAdress = model.EventAdress;
						repositoryResponse.IsActive = model.IsActive;
						repositoryResponse.CategoryId = model.CategoryId;
						repositoryResponse.CityId = model.CityId;
						repositoryResponse.Latitude = model.Latitude;
						repositoryResponse.Longitude = model.Longitude;
						repositoryResponse.Quota = model.Quota;
						foreach (var item in model.Images)
						{
							var ImageUpload = _fileService.AddImage(item).Result.Result;
							repositoryResponse.EventImages.Add(new EventImages
							{
								UploadedImage = ImageUpload,
								UploadedImageId = ImageUpload.Id,
							});

						}
						context.Events.Update(repositoryResponse);
						var SaveResult = await context.SaveChangesAsync();
						if (SaveResult > 0)
						{
							result.IsSuccess = true;
							result.Message = "Event Başarıyla Güncellendi";

						}
					}
                    else
					{
						result.IsSuccess = false;
						result.Message = "Event Başlangıcına 5 gün ve daha az kaldığı için güncellenemez.";
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
        public async Task<ServiceResult<bool>> Delete(Guid id, string UserId)
        {
            var result = new ServiceResult<bool>();
            var repoResponse = await context.Events.FirstOrDefaultAsync(x => x.Id == id);

            result.IsSuccess = false;
            if (repoResponse != null)
            {

				if (Convert.ToInt32(repoResponse.EventStartDate - DateTime.Now) > 5)
				{

					repoResponse.IsActive = false;
					repoResponse.IsDeleted = true;
					context.Events.Update(repoResponse);
					var SaveResult = await context.SaveChangesAsync();
					if (SaveResult > 0)
						result.IsSuccess = true;
					result.Message = "Silme İşlemi Başarılı.";

				}
				else
				{
					result.IsSuccess = false;
					result.Message = "Event Başlangıcına 5 gün ve daha az kaldığı için silinemez.";
				}
			}
            else
            {
                result.Message = ("Kayıt bulunamadı");
            }
            return result;
        }

        public async Task<List<EventsViewModel>> GetAll()
        {
            var model = await context.Events.Include(x => x.Category).Include(x=>x.EventImages).ThenInclude(x=>x.UploadedImage).Where(p => p.IsDeleted == false && p.IsActive).Select(c => new EventsViewModel
            {                
                Id = c.Id,
                EventName = c.EventName,
                EventDescription = c.EventDescription,
                EventShortDescription = c.EventShortDescription,
                EventRules = c.EventRules,
                EventStartDate = c.EventStartDate,
                EventEndDate = c.EventEndDate,
                EventAdress = c.EventAdress,
                IsActive = c.IsActive,
                Category = new CategoryViewModel
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName,
                    IsActive = c.Category.IsActive
                },
                City = c.City,
                Quota = c.Quota,
				EventImage = c.EventImages.FirstOrDefault(x=>x.IsActive),
                Latitude = c.Latitude,
                Longitude = c.Longitude
            }).OrderBy(x => x.EventName).ToListAsync();

            return model;
        }

        public (List<EventsViewModel> list, int total) GetAllForDatatables(DataTableBaseModel model, EventsDTParameter dTParameter)
        {
            var data = GetAll().Result.AsQueryable();
            //data = model.OrderAscDirection ? data.OrderByDynamic(model.OrderCriteria, LinqExtensions.Order.Asc) : data.OrderByDynamic(model.OrderCriteria, LinqExtensions.Order.Desc);
            if(!string.IsNullOrEmpty(dTParameter.UserId))
            {
                var getUserTickets = context.EventTickets.Where(x => x.UserId == dTParameter.UserId);
                data = data.Where(x => getUserTickets.Any(c => c.EventsId == x.Id));
            }
            if (!string.IsNullOrEmpty(dTParameter.EventName))
            {
                data = data.Where(r => r.EventName.ToLower().Contains(dTParameter.EventName.ToLower()));
            }
            if (dTParameter.EventStartDate != null)
            {
                data = data.Where(r => r.EventStartDate > dTParameter.EventStartDate);
            }
            if (dTParameter.EventEndDate != null)
            {
                data = data.Where(r => r.EventEndDate < dTParameter.EventEndDate);
            }
            if (dTParameter.EventEndDate != null)
            {
                data = data.Where(r => r.CategoryId == dTParameter.CategoryId);
            }
            var totalResultsCount = data.Count();
            data = data.Skip(model.Start).Take(model.Length);

			return (data.ToList(), totalResultsCount);
        }

        public async Task<EventsViewModel> GetEventsById(Guid id)
        {
            var model = new EventsViewModel();

            var getEvents = await context.Events.Include(c=>c.Category).Include(c=>c.City).Include(c=>c.EventImages).ThenInclude(c=>c.UploadedImage).FirstOrDefaultAsync(x => x.Id == id);
            if (getEvents != null)
			{
				model.Id = getEvents.Id;
                model.EventName = getEvents.EventName;
                model.EventDescription = getEvents.EventDescription;
                model.EventShortDescription = getEvents.EventShortDescription;
                model.EventRules = getEvents.EventRules;
                model.EventStartDate = getEvents.EventStartDate;
                model.EventEndDate = getEvents.EventEndDate;
                model.EventAdress = getEvents.EventAdress;
                model.Quota = getEvents.Quota;
                model.IsActive = getEvents.IsActive;
                model.Category = new CategoryViewModel
                {
                    Id = getEvents.Category.Id,
                    CategoryName = getEvents.Category.CategoryName
                };
                model.City = new City { 
                id=getEvents.City.id,
                Name = getEvents.City.Name,
                };
                model.EventImages=getEvents.EventImages.Where(x=>x.UploadedImage.IsActive).ToList();
            }
            return model;
        }

		public async Task<ServiceResult<bool>> EventCancelled(Guid id, string UserId)
		{
			var result = new ServiceResult<bool>();
			var repoResponse = await context.Events.FirstOrDefaultAsync(x => x.Id == id);

			result.IsSuccess = false;
			if (repoResponse != null)
			{
                if(Convert.ToInt32(repoResponse.EventStartDate - DateTime.Now) > 5)
				{
					repoResponse.IsCancelled = true;
					context.Events.Update(repoResponse);
					var SaveResult = await context.SaveChangesAsync();
					if (SaveResult > 0)
						result.IsSuccess = true;
					result.Message = "Silme İşlemi Başarılı.";

				}
				else
				{
					result.IsSuccess = false;
					result.Message = "Event Başlangıcına 5 gün ve daha az kaldığı için iptal edilemez.";
				}
			}
			else
			{
				result.Message = ("Kayıt bulunamadı");
			}
			return result;
		}

        public async Task<ServiceResult<List<EventsViewModel>>> GetAllByFilterModel(EventFilterModel model)
        {

            var list = await context.Events.Include(x => x.Category).Include(x => x.EventImages).ThenInclude(x => x.UploadedImage).Where(p => p.IsDeleted == false && p.IsActive).Select(c => new EventsViewModel
            {
                Id = c.Id,
                EventName = c.EventName,
                EventDescription = c.EventDescription,
                EventShortDescription = c.EventShortDescription,
                EventRules = c.EventRules,
                EventStartDate = c.EventStartDate,
                EventEndDate = c.EventEndDate,
                EventAdress = c.EventAdress,
                IsActive = c.IsActive,
                Category = new CategoryViewModel
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName,
                    IsActive = c.Category.IsActive
                },
                City = c.City,
                Quota = c.Quota,
                EventImage = c.EventImages.FirstOrDefault(x => x.IsActive),
                Latitude = c.Latitude,
                Longitude = c.Longitude
            }).OrderBy(x => x.EventName).ToListAsync();
            if (!string.IsNullOrEmpty(model.UserId))
            {
                var getUserTickets = context.EventTickets.Where(x => x.UserId == model.UserId);
                list = list.Where(x => getUserTickets.Any(c => c.EventsId == x.Id)).ToList();
            }
            if (!string.IsNullOrEmpty(model.EventName))
            {
                list = list.Where(r => r.EventName.ToLower().Contains(model.EventName.ToLower())).ToList();
            }
            if (model.EventStartDate != null)
            {
                list = list.Where(r => r.EventStartDate > model.EventStartDate).ToList();
            }
            if (model.EventEndDate != null)
            {
                list = list.Where(r => r.EventEndDate < model.EventEndDate).ToList();
            }
            if (model.EventEndDate != null)
            {
                list = list.Where(r => r.CategoryId == model.CategoryId).ToList();
            }
            var response =new ServiceResult<List<EventsViewModel>>();
            response.Result = list;
            return response;
        }
    }
}
