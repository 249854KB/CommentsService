using System;
using System.Collections.Generic;
using AutoMapper;
using CommentsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ForumsService;

namespace CommentsService.SyncDataServices.Grpc
{
    public class ForumDataClient : IForumDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ForumDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Forum> ReturnAllForums()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcForum"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcForum"]);
            var client = new GrpcForum.GrpcForumClient(channel);
            var request = new GetAllRequestForum();

            try
            {
                var reply = client.GetAllForums(request);
                return _mapper.Map<IEnumerable<Forum>>(reply.Forum);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}