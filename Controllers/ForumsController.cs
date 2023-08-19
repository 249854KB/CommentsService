using System;
using System.Collections.Generic;
using AutoMapper;
using ForumsService.Data;
using ForumsService.Dtos;
using ForumsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumsService.Controllers
{
    [Route("api/f/users/{userId}/[controller]")]
    [ApiController]
    public class ForumsController : ControllerBase
    {
        private readonly IForumRepo _repository;
        private readonly IMapper _mapper;

        public ForumsController(IForumRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ForumReadDto>> GetForumsForUser(int userId)
        {
            Console.WriteLine($"--> Hit GetForumsForUser: {userId}");

            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }

            var forums = _repository.GetForumsForUser(userId);

            return Ok(_mapper.Map<IEnumerable<ForumReadDto>>(forums));
        }

        [HttpGet("{forumId}", Name = "GetForumForUser")]
        public ActionResult<ForumReadDto> GetForumForUser(int userId, int forumId)
        {
            Console.WriteLine($"--> Hit GetForumForUser: {userId} / {forumId}");

            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }

            var forum = _repository.GetForum(userId, forumId);

            if(forum == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ForumReadDto>(forum));
        }

        [HttpPost]
        public ActionResult<ForumReadDto> CreateForumForUser(int userId, ForumCreateDto forumDto)
        {
             Console.WriteLine($"--> Hit CreateForumForUser: {userId}");

            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }
            var forum = _mapper.Map<Forum>(forumDto);
            forum.Time = DateTime.Now;
            _repository.CreateForum(userId, forum);
            _repository.SaveChanges();

            var forumReadDto = _mapper.Map<ForumReadDto>(forum);

            return CreatedAtRoute(nameof(GetForumForUser),
                new {userId = userId, forumId = forumReadDto.Id}, forumReadDto);
        }

    }
}