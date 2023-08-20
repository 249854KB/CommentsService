using System;
using System.Collections.Generic;
using AutoMapper;
using CommentsService.Data;
using CommentsService.Dtos;
using CommentsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comments.Controllers
{
    [Route("api/c/users/{userId}/forums/{forumId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepo _repository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommentReadDto>> GetCommentsForForum(int userId, int forumId)
        {
            Console.WriteLine($"--> Hit GetCommentsForForum: {forumId} by user: {userId}");

            if (!_repository.ForumExists(userId,forumId))
            {
                Console.WriteLine($"--> Forum {forumId} for user {userId} has not been found");
                return NotFound();
            }

            var comments = _repository.GetCommentsForForum(userId,forumId);

            return Ok(_mapper.Map<IEnumerable<CommentReadDto>>(comments));
        }

        [HttpGet("{commentId}", Name = "GetCommentForForum")]
        public ActionResult<CommentReadDto> GetCommentForForum(int userId, int forumId, int commentId)
        {
            Console.WriteLine($"--> Hit GetCommentForForum: {userId} / {forumId} / {commentId}");

            if (!_repository.ForumExists(userId,forumId))
            {
                return NotFound();
            }

            var comment = _repository.GetComment(userId,forumId, commentId);

            if(comment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommentReadDto>(comment));
        }

        [HttpPost]
        public ActionResult<CommentReadDto> CreateCommentForForum(int userId, int forumId, CommentCreateDto commentDto)
        {
             Console.WriteLine($"--> Hit CreateCommentForForum: {userId} / {forumId}");

            if (!_repository.ForumExists(userId,forumId))
            {
                return NotFound();
            }
            var comment = _mapper.Map<Comment>(commentDto);
            comment.Time = DateTime.Now;
            comment.ForumId = forumId;

            _repository.CreateComment(userId,forumId, comment);
            _repository.SaveChanges();

            var commentReadDto = _mapper.Map<CommentReadDto>(comment);

            return CreatedAtRoute(nameof(GetCommentForForum),
                new {forumId = forumId, commentId = commentReadDto.Id, userId = userId}, commentReadDto);
        }

    }
}