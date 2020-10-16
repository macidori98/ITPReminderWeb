using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.API.Interfaces;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly DataContext context;

        public CarsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cars>>> GetAllCars()
        {
            return await this.context.Cars.ToListAsync();
        }

        [Authorize]
        [HttpPost("addnew")]
        public async Task<ActionResult<CarDto>> AddCar(CarDto carDto)
        {
            if (carDto == null)
            {
                return Unauthorized("Empty entity");
            }

            if (CarAlreadyAdded(carDto.PlateNumber))
            {
                return BadRequest("Car already exists");
            }

            var user = await this.context.Users.Where(x => x.Name.ToLower().Equals(carDto.AddedBy.ToLower())).SingleOrDefaultAsync();

            var car = new Cars()
            {
                AddedBy = user.ID,
                CarType = carDto.CarType,
                Comments = carDto.Comments,
                InspectedAt = carDto.InspectedAt,
                InspectedUntil = carDto.InspectedAt.AddMonths(carDto.InspectionInterval),
                InspectionInterval = carDto.InspectionInterval,
                OwnerName = carDto.OwnerName,
                OwnerTelNr = carDto.OwnerTelNr,
                PlateNumber = carDto.PlateNumber
            };

            this.context.Cars.Add(car);
            await this.context.SaveChangesAsync();

            return carDto;
        }

        [Authorize]
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetUsersCar(string name)
        {
            var user = await this.context.Users.Where(x => x.Name.ToLower().Equals(name.ToLower())).SingleOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User doesn't exist");
            }

            List<Cars> carList = await this.context.Cars.Where(x => x.AddedBy == user.ID).ToListAsync();
            List<CarDto> userCars = new List<CarDto>();
            foreach (Cars item in carList)
            {
                userCars.Add(new CarDto() {
                    AddedBy = user.Name,
                    CarType = item.CarType,
                    Comments = item.Comments,
                    InspectedAt = item.InspectedAt,
                    InspectionInterval = item.InspectionInterval,
                    OwnerName = item.OwnerName,
                    OwnerTelNr = item.OwnerTelNr, 
                    PlateNumber = item.PlateNumber
                });
            }

            return userCars;
        }


        private bool CarAlreadyAdded(string plateNumber)
        {
            var car = this.context.Cars.Where(x => x.PlateNumber.ToLower().Equals(plateNumber.ToLower())).SingleOrDefault();
            if (car == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}