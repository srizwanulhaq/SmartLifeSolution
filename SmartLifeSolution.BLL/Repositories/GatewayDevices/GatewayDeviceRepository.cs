using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.DAL.Dao.Gateway;
using SmartLifeSolution.DAL.Dao.GatewayDevices;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.GatewayDevice;
using SmartLifeSolution.DAL.Dto.Paging;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.GatewayDevices
{
    public class GatewayDeviceRepository : IGatewayDeviceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly MqttClt _mqttClient;
        private readonly JwtHelper _jwtHelper;
        public GatewayDeviceRepository(ApplicationDbContext context, MqttClt mqttClient, JwtHelper jwtHelper)
        {
            _context = context;
            _mqttClient = mqttClient;
            _jwtHelper = jwtHelper;
        }
        public async Task<List<GetAllDeviceDao>> GetAll()
        {
            return await _context.GatewayDevices
                 .Where(x => x.Active)
                 .Include(x => x.Gateway)
                 .Include(x => x.Manufacturer)
                 .Include(x => x.Application)
                 .Include(x => x.Profile)
                 .Select(x => new GetAllDeviceDao
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     AppKey = x.AppKey,
                     DeviceEUI = x.DeviceEUI,
                     GatewayName = x.Gateway.Name,
                     TurnOffCode = x.TurnOffCode,
                     TurnOnCode = x.TurnOnCode,
                     ApplicationName = x.Application.Name,
                     ManufacturerName = x.Manufacturer.Name,
                     ProfileName = x.Profile.Name,
                 }).ToListAsync();
        }

        public async Task<PagingDao<GetAllDeviceDao>> GetPagedData(PagingDto Dto)
        {
            var query = _context.GatewayDevices
                 .Where(x => x.Active)
                 .Include(x => x.Gateway)
                 .Include(x => x.Manufacturer)
                 .Include(x => x.Application)
                 .Include(x => x.Profile)
                 .Select(x => new GetAllDeviceDao
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     AppKey = x.AppKey,
                     DeviceEUI = x.DeviceEUI,
                     GatewayName = x.Gateway.Name,
                     TurnOffCode = x.TurnOffCode,
                     TurnOnCode = x.TurnOnCode,
                     ApplicationName = x.Application.Name,
                     ManufacturerName = x.Manufacturer.Name,
                     ProfileName = x.Profile.Name,
                 });

            var result = new PagingDao<GetAllDeviceDao>
            {
                TotalCount = query.Count(),
                Page = Dto.Page,
                Size = Dto.Size,
                Data = await query.Skip(Dto.Skip).Take(Dto.Size).ToListAsync()
            };

            return result;
        }
        public async Task<GetDeviceDao> Edit(string Id)
        {
            var data = await _context.GatewayDevices.Where(x => x.Active).Include(x => x.ManufacturerId)
                .Include(x => x.Application)
                .Include(x => x.Gateway).Where(x => x.Id == Id)
                .Select(x => new GetDeviceDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    DeviceEUI = x.DeviceEUI,
                    AppKey = x.AppKey,
                    TurnOffCode = x.TurnOffCode,
                    TurnOnCode = x.TurnOnCode,
                    ApplicationId = x.Application.Id,
                    ManufacturerId = x.Manufacturer.Id,
                    ProfileId = x.Profile.Id,
                    GatewayId = x.Gateway.Id,
                }).FirstOrDefaultAsync();

            data.ApplicationList = _context.Applications.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList();

            data.GatewayList = _context.Gateways.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList();

            data.ManufacturerList = _context.Manufacturers.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList();

            data.ProfileList = _context.Profiles.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList();

            return data;
        }
        public async Task<bool> AddOrUpdate(DevicesDto Dto)
        {
            if (Dto.Id == null)
            {
                var objGatewayDevice = new GatewayDevice
                {
                    AppKey = Dto.AppKey,
                    ApplicationId = Dto.ApplicationId,
                    Description = Dto.Description,
                    DeviceEUI = Dto.DeviceEUI,
                    Name = Dto.Name,
                    GatewayId = Dto.GatewayId,
                    ProfileId = Dto.ProfileId,
                    ManufacturerId = Dto.ManufacturerId,
                    TurnOnCode = Dto.TurnOnCode,
                    TurnOffCode = Dto.TurnOffCode,
                    //   RoomId = Dto.RoomId,
                    CreatedByUserId = _jwtHelper.UserId

                };

                _context.GatewayDevices.Add(objGatewayDevice);
            }
            else
            {
                var obj = _context.GatewayDevices.Where(x => x.Id == Dto.Id).FirstOrDefault();

                if (obj != null)
                {
                    obj.AppKey = Dto.AppKey;
                    obj.ApplicationId = Dto.ApplicationId;
                    obj.Description = Dto.Description;
                    obj.DeviceEUI = Dto.DeviceEUI;
                    obj.Name = Dto.Name;
                    obj.GatewayId = Dto.GatewayId;
                    obj.ProfileId = Dto.ProfileId;
                    obj.ManufacturerId = Dto.ManufacturerId;
                    obj.TurnOnCode = Dto.TurnOnCode;
                    obj.TurnOffCode = Dto.TurnOffCode;
                    //  obj.RoomId = Dto.RoomId;
                    _context.GatewayDevices.Update(obj);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(string Id)
        {
            var obj = _context.GatewayDevices.Where(x => x.Id == Id).FirstOrDefault();
            obj.Active = false;
            _context.GatewayDevices.Update(obj);
            await _context.SaveChangesAsync();
            return true;
        }
        public DeviceDropdownDao GetDropdowns()
        {
            var data = new DeviceDropdownDao
            {
                ApplicationList = _context.Applications.Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList(),

                GatewayList = _context.Gateways.Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList(),

                ManufacturerList = _context.Manufacturers.Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList(),

                ProfileList = _context.Profiles.Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList()

            };

            return data;
        }
        public async Task<bool> Downlink(string deviceEUI, bool IsTurnOn)
        {
            //  string lighttopic = "/milesight/downlink/24E124756E433039";
            // string doortopic = "/milesight/downlink/24e124141C274046";

            var clientId = "3cb81efd-868e-46ee-9296-0b12b77c494d";
            var topic = "/milesight/downlink/" + deviceEUI;

            var obj = _context.GatewayDevices.FirstOrDefault(x => x.DeviceEUI == deviceEUI);
            // var topic = lighttopic;

            string hexStr = IsTurnOn ? obj.TurnOnCode : obj.TurnOffCode;

            //string oneMinCode = "ff03b004";

            //var oneminPayload = new
            //{
            //    confirmed = true,
            //    fport = 85,
            //    data = oneMinCode.FromHexToBase64()
            //};

            //var response = await _mqttClient.Publish(clientId, topic, JsonSerializer.Serialize(oneminPayload));

            var payload = new
            {
                confirmed = true,
                fPort = 85,
                data = hexStr.FromHexToBase64()
            };

            var downlinkResponse = await _mqttClient.Publish(clientId, topic, JsonSerializer.Serialize(payload));

            return true;
        }

    }
}
