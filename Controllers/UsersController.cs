using AutoMapper;
using ForumsService.Data;
using ForumsService.Dtos;
using ForumsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ForumsService.Controllers
{
    [Route("api/f/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IForumRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IForumRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetUsers()
        {
            Console.WriteLine("-->> Getting User From Forum service");
            var userItems = _repository.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inboud POST # Command Service");
            return Ok("Inmbound test ok for forums controller");
        }
        //Https and grcp is synchronius
    }
}