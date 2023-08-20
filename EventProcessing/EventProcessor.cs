using System;
using System.Text.Json;
using AutoMapper;
using CommentsService.Data;
using CommentsService.Dtos;
using CommentsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommentsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ForumPublished:
                    addForum(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Forum_Published":
                    Console.WriteLine("--> Forum Published Event Detected");
                    return EventType.ForumPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addForum(string forumPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommentRepo>();
                
                var forumPublishedDto = JsonSerializer.Deserialize<ForumPublishedDto>(forumPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Forum>(forumPublishedDto);
                    if(!repo.ExternalForumExists(plat.UserId, plat.ExternalID))
                    {
                        repo.CreateForum(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Forum added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Forum already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Forum to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        ForumPublished,
        Undetermined
    }
}