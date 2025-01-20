using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NovaPostServer.Data.Entities;
using NovaPostServer.Models.Area;
using NovaPostServer.Models.City;
using NovaPostServer.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovaPostServer.Data;
using Microsoft.EntityFrameworkCore;
using NovaPostServer.Constants;
using AutoMapper;
using NovaPostServer.Mapping;

namespace NovaPostServer.Services
{
    public class NovaPostService
    {
        public NovaPostService()
        {
            _httpClient = new HttpClient();
            _url = "https://api.novaposhta.ua/v2.0/json/";
            _context = new NovaPostDbContext();
            _context.Database.Migrate();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _procesLimit = 10;
        }

        public async Task SeedAreasAsync()
        {
            if (!_context.Areas.Any())
            {
                var modelRequest = new AreaPostModel
                {
                    ApiKey = AppDatabase.NovaPostApiKey,
                    MethodProperties = new MethodProperties() { }
                };

                var result = await SendRequestAsync<AreaResponse>(modelRequest);
                if (result?.Data != null && result.Success)
                {
                    Console.WriteLine("Seed Areas...");

                    using var semaphore = new SemaphoreSlim(_procesLimit);
                    await Parallel.ForEachAsync(result.Data, async (item, x) =>
                    {
                        await semaphore.WaitAsync();
                        try
                        {
                            var entity = _mapper.Map<AreaEntity>(item);

                            using var localContext = new NovaPostDbContext();
                            await localContext.Areas.AddAsync(entity);
                            await localContext.SaveChangesAsync();
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    });
                }
            }
        }

        public async Task SeedCitiesAsync()
        {
            if (!_context.Cities.Any())
            {
                var listAreas = _context.Areas.ToList();

                using var semaphore = new SemaphoreSlim(_procesLimit);
                await Parallel.ForEachAsync(listAreas, async (area, x) =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        Console.WriteLine("Seed area {0}...", area.Description);

                        var modelRequest = new CityPostModel
                        {
                            ApiKey = AppDatabase.NovaPostApiKey,
                            MethodProperties = new MethodCityProperties() { AreaRef = area.Ref }
                        };

                        var result = await SendRequestAsync<CityResponse>(modelRequest);
                        if (result?.Data != null && result.Success)
                        {
                            var cityEntities = result.Data.Select(city =>
                            {
                                var entity = _mapper.Map<CityEntity>(city);
                                entity.AreaId = area.Id;
                                return entity;
                            });

                            using var localContext = new NovaPostDbContext();
                            await localContext.Cities.AddRangeAsync(cityEntities);
                            await localContext.SaveChangesAsync();
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
            }
        }

        public async Task SeedDepartmentsAsync()
        {
            if (!_context.Departments.Any())
            {
                var listCities = _context.Cities.ToList();

                using var semaphore = new SemaphoreSlim(_procesLimit);
                await Parallel.ForEachAsync(listCities, async (city, _) =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        Console.WriteLine("Seed city {0}...", city.Description);

                        var modelRequest = new DepartmentPostModel
                        {
                            ApiKey = AppDatabase.NovaPostApiKey,
                            MethodProperties = new MethodDepatmentProperties() { CityRef = city.Ref }
                        };

                        var result = await SendRequestAsync<DepartmentResponse>(modelRequest);
                        if (result?.Data != null && result.Success)
                        {
                            var departmentEntities = result.Data.Select(dep =>
                            {
                                var entity = _mapper.Map<DepartmentEntity>(dep);
                                entity.CityId = city.Id;
                                return entity;
                            });

                            using var localContext = new NovaPostDbContext();
                            await localContext.Departments.AddRangeAsync(departmentEntities);
                            await localContext.SaveChangesAsync();
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
            }
        }

        private async Task<T?> SendRequestAsync<T>(object modelRequest) where T : class
        {
            string json = JsonConvert.SerializeObject(modelRequest, new JsonSerializerSettings
            {
                ContractResolver = (modelRequest is AreaPostModel) ? new CamelCasePropertyNamesContractResolver() : null,
                Formatting = Formatting.Indented
            });

            HttpContent context = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_url, context);

            if (response.IsSuccessStatusCode)
            {
                string jsonResp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonResp);
            }

            return null;
        }

        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly NovaPostDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _procesLimit;
    }
}
