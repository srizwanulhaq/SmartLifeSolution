//using Microsoft.EntityFrameworkCore;
//using SmartLifeSolution.BLL.Helpers;
//using SmartLifeSolution.DAL.Dao.Paging;
//using SmartLifeSolution.DAL.Dao.SmartBuildings;
//using SmartLifeSolution.DAL.DBContexts;
//using SmartLifeSolution.DAL.Dto.Appliances;
//using SmartLifeSolution.DAL.Dto.SmartBuildings;
//using SmartLifeSolution.DAL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SmartLifeSolution.BLL.Repositories.SmartBuildings
//{
//    public class SmartBuildingRepository : ISmartBuildingRepository
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly JwtHelper _jwtHelper;
//        public SmartBuildingRepository(ApplicationDbContext context, JwtHelper jwtHelper)
//        {
//            _context = context;
//            _jwtHelper = jwtHelper;
//        }
//        public async Task<string> AddBuilding(SmartBuildingDto Dto)
//        {
//            var objBuilding = new SmartBuilding
//            {
//                Name = Dto.Name,
//                CreatedByUserId = _jwtHelper.UserId
//            };

//            _context.SmartBuildings.Add(objBuilding);
//            await _context.SaveChangesAsync();

//            return objBuilding.Id;
//        }
//        public async Task<string> AddFloor(FloorDto Dto)
//        {
//            var objFloor = new Floor
//            {
//                Name = Dto.Name,
//                BuildingId = Dto.BuildingId,
//                CreatedByUserId = _jwtHelper.UserId,
//            };

//            _context.Floors.Add(objFloor);
//            await _context.SaveChangesAsync();

//            return objFloor.Id;
//        }
//        public async Task<string> AddRoom(RoomDto Dto)
//        {
//            var objRoom = new Room
//            {
//                Name = Dto.Name,
//                FloorId = Dto.FloorId,
//                CreatedByUserId = _jwtHelper.UserId
//            };

//            _context.Rooms.Add(objRoom);
//            await _context.SaveChangesAsync();
//            return objRoom.Id;
//        }
//        public async Task<string> AddAppliances(ApplianceDto Dto)
//        {
//            var objAppliance = new Appliance
//            {
//                Name = Dto.Name,
//                DeviceId = Dto.DeviceId,
//                RoomId = Dto.RoomId,
//                CreatedByUserId = _jwtHelper.UserId
//            };

//            _context.Appliances.Add(objAppliance);
//            await _context.SaveChangesAsync();

//            return objAppliance.Id;
//        }
//        public async Task<PagingDao<SmartBuildingDao>> GetAllBuildings(int pageNumber, int pageSize)
//        {
//            var query = _context.SmartBuildings.AsQueryable();

//            var totalCount = await query.CountAsync();

//            var items = await query.OrderByDescending(r => r.CreatedDate)
//                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
//                .Select(r => new SmartBuildingDao
//                {
//                    Id = r.Id,
//                    Name = r.Name,
//                    CreatedDate = r.CreatedDate
//                }).ToListAsync();

//            return new PagingDao<SmartBuildingDao>
//            {
//                Items = items,
//                TotalCount = totalCount,
//                PageNumber = pageNumber,
//                PageSize = pageSize
//            };

//        }

//        public async Task<SmartBuildingDao> EditBuilding(string BuildingId)
//        {
//            return await _context.SmartBuildings.Where(x => x.Id == BuildingId)
//                .Select(x => new SmartBuildingDao
//                {
//                    Id = x.Id,
//                    Name = x.Name,
//                    CreatedDate = x.CreatedDate,
//                }).FirstOrDefaultAsync();
//        }

//        public List<FloorDao> GetFloorsByBuildingId(string buildingId)
//        {
//            var data = _context.Floors.Where(x => x.BuildingId == buildingId
//            && x.CreatedByUserId == _jwtHelper.UserId)
//                .Select(x => new FloorDao
//                {
//                    Id = x.Id,
//                    Name = x.Name,
//                }).ToList();

//            return data;
//        }
//        public List<RoomDao> GetRoomsByFloorId(string floorId)
//        {
//            var data = _context.Rooms.Where(x => x.FloorId == floorId
//            && x.CreatedByUserId == _jwtHelper.UserId)
//                .Select(x => new RoomDao
//                {
//                    Id = x.Id,
//                    Name = x.Name,
//                }).ToList();

//            return data;
//        }
//        public List<ApplianceDao> GetAllAppliances(string roomId)
//        {
//            var lst = _context.Appliances.Where(x => x.RoomId == roomId
//            && x.CreatedByUserId == _jwtHelper.UserId)
//                .Select(x => new ApplianceDao
//                {
//                    ApplianceId = x.Id,
//                    ApplianceName = x.Name,
//                }).ToList();

//            return lst;
//        }

//        public void AddAppliance()
//        {


//        }


//    }
//}
