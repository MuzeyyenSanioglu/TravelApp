using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TravelApp.API.Model;
using TravelApp.Domain.Entities;
using TravelApp.Domain.Model;
using TravelApp.Domain.Repositories;

namespace TravelApp.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly ITravelRepository _travelRespository;
        private readonly IMapper _mapper;

        public TravelController(ITravelRepository travelRespository, IMapper mapper)
        {
            _travelRespository = travelRespository;
            _mapper = mapper;
           
        }
        [HttpPost]
        public ActionResult<Result> Create(CreateTravelDto createTravelDto)
        {
            Result result = new Result();
            Travel entity = _mapper.Map<Travel>(createTravelDto);
            entity.CreatedDate =DateTime.Now;
            entity.UserId = int.Parse(this.User.Claims.First(i => i.Type == "Id").Value);
            entity.NumberOfAvailebleSeat = createTravelDto.NumberOfSeat;
            Result<Travel> travelCreateResult = _travelRespository.Add(entity);
            if (!travelCreateResult.IsSuccess)
            {
                result.SetFailure();
                return NotFound(travelCreateResult.ErrorMessage);
            }
               
            result.SetSuccess();
            result.ObjectId = travelCreateResult.Data.Id.ToString();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult<Result> ApplicationTravel(int travelId)
        {
            Result result = new Result();
            Result<Travel> travel = _travelRespository.GetById(travelId);
            if (!travel.IsSuccess)
            {
                result.SetFailure(travel.ErrorMessage);
                return NotFound(result);
            }
            if (!travel.Data.IsPublication)
            {
                result.SetFailure("There is not publication travel");
                return NotFound(result);
            }
            if(travel.Data.NumberOfAvailebleSeat < 1)
            {
                result.SetFailure("There is not available seat");
                return NotFound(result);
            }
            travel.Data.NumberOfAvailebleSeat = travel.Data.NumberOfAvailebleSeat - 1;
            Result updateResult = _travelRespository.Update(travel.Data);
            if (!updateResult.IsSuccess)
            {
                result.SetFailure(updateResult.ErrorMessage);
                return NotFound(result);
            }

            result.SetSuccess();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<Result<List<TravelsDto>>> GetPublicitionTravels()
        {
            Result<List<TravelsDto>> result = new Result<List<TravelsDto>>();
            Result<List<Travel>> travelsResult = _travelRespository.GetlAll();
            if (!travelsResult.IsSuccess)
            {
                result.SetFailure(travelsResult.ErrorMessage);
                return NotFound(result);

            }

            List<TravelsDto> travelesDtos =_mapper.Map<List<TravelsDto>>(travelsResult.Data.Where(s => s.IsPublication).ToList());
            result.SetData(travelesDtos);
            return Ok(result);
        }


        [HttpGet]
        public ActionResult<Result<List<TravelsDto>>> GetTravelsForRoad(string startCity,string endCity)
        {
            Result<List<TravelsDto>> result = new Result<List<TravelsDto>>();
            Result<List<Travel>> travelsResult = _travelRespository.GetTravelsFiltersByRoadFilter(startCity,endCity);
            if (!travelsResult.IsSuccess)
            {
                result.SetFailure(travelsResult.ErrorMessage);
                return NotFound(result);

            }

            List<TravelsDto> travelesDtos = _mapper.Map<List<TravelsDto>>(travelsResult.Data.ToList());
            result.SetData(travelesDtos);
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<Result> UpdateTravelsPublicition(int travelId, bool ispublication)
        {
            Result result = new Result();
            Result<Travel> travel = _travelRespository.GetById(travelId);
            if (!travel.IsSuccess)
            {
                result.SetFailure(travel.ErrorMessage);
                return NotFound(result);
            }
            if(travel.Data.UserId != int.Parse(this.User.Claims.First(i => i.Type == "Id").Value))
            {
                result.SetFailure("You have not permission");
                return NotFound(result);
            }
            travel.Data.IsPublication = ispublication;
            Result updateResult = _travelRespository.Update(travel.Data);
            if (!updateResult.IsSuccess)
            {
                result.SetFailure(updateResult.ErrorMessage);
                return NotFound(result);
            }
            result.SetSuccess();
            return Ok(result);
        }
    }
}
