using AutoMapper;
using CommentsService.Data;
using CommentsService.Dtos;
using CommentsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CommentsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class ForumsController: ControllerBase
    {
        private readonly ICommentRepo _repository;
        private readonly IMapper _mapper;

        public ForumsController(ICommentRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ForumReadDto>> GetForums()
        {
            Console.WriteLine("-->> Getting Forum From Comment service");
            var forumItems = _repository.GetAllForums();
            return Ok(_mapper.Map<IEnumerable<ForumReadDto>>(forumItems));
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inboud POST # Command Service");
            return Ok("Inmbound test ok for comments controller");
        }
        //Https and grcp is synchronius
    }
}