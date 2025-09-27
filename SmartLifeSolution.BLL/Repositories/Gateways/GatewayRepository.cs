using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartLifeSolution.DAL.Dao.Gateway;
using SmartLifeSolution.DAL.Dao.GatewayDevices;
using SmartLifeSolution.DAL.Dao.Manufacturer;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Dto.Gateway;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Repositories.Gateways
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GatewayRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GatewayDao>> GetAll()
        {
            var data = await _context.Gateways
                .Include(x => x.Manufacturer).Select(x => new GatewayDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    DeviceEUI = x.DeviceEUI,
                    IPAddress = x.IPAddress,
                    ManufacturerName = x.Manufacturer.Name
                }).ToListAsync();

            return data;
        }
        public async Task<GatewayDao> Edit(string Id)
        {
            var objGateway = await _context.Gateways.Include(x => x.Manufacturer)
                .Where(x => x.Id == Id).Select(x => new GatewayDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    ManufacturerId = x.Manufacturer.Id,
                    ManufacturerName = x.Manufacturer.Name,
                    DeviceEUI = x.DeviceEUI,
                    IPAddress = x.IPAddress
                }).FirstOrDefaultAsync();

            objGateway.ManufacturerList = _context.Manufacturers.Select(x => new ManufacturerDao
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return objGateway;
        }
        public async Task<string> AddOrUpdate(GatewayDto Dto)
        {
            if (string.IsNullOrEmpty(Dto.Id))
            {
                var objGateway = new Gateway();
                objGateway.Name = Dto.Name;
                objGateway.IPAddress = Dto.IPAddress;
                objGateway.DeviceEUI = Dto.DeviceEUI;
                objGateway.ManufacturerId = Dto.ManufacturerId;

                _context.Gateways.Add(objGateway);
                await _context.SaveChangesAsync();
                return objGateway.Id;
            }
            else
            {
                var objGateway = _context.Gateways.FirstOrDefault(x => x.Id == Dto.Id);
                objGateway.Name = Dto.Name;
                objGateway.IPAddress = Dto.IPAddress;
                objGateway.DeviceEUI = Dto.DeviceEUI;
                objGateway.ManufacturerId = Dto.ManufacturerId;
                objGateway.UpdatedDate = DateTime.Now;
                _context.Gateways.Update(objGateway);
                await _context.SaveChangesAsync();
                return objGateway.Id;
            }
        }
        public async Task<bool> Delete(string Id)
        {
            var objGateway = _context.Gateways.Where(x => x.Id == Id).FirstOrDefault();
            objGateway.Active = false;
            _context.Gateways.Update(objGateway);
            await _context.SaveChangesAsync();

            return true;
        }
        public GatewayDropdownsDao GetDropdowns()
        {
            var data = new GatewayDropdownsDao
            {
                ManufacturerList = _context.Manufacturers.Select(x => new ManufacturerDao
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList()
            };

            return data;
        }
        public List<GetDeviceDao> GetDevicesByGatewayId(string gatewayId)
        {
            var data = _context.GatewayDevices.Include(x => x.Gateway)
                 .Where(x => x.GatewayId == gatewayId)
                 .Select(x => new GetDeviceDao
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     DeviceEUI = x.DeviceEUI,
                     GatewayName = x.Gateway.Name,
                 }).ToList();

            return data;
        }

    }
}
