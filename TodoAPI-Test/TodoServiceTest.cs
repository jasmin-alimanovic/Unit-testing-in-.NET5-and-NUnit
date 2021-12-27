using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Mapping_Profiles;
using TodoAPI.Repositories;
using TodoAPI.Services;

namespace TodoAPI_Test
{
    class TodoServiceTest
    {
        ITodoRepo todoRepo;
        TodoService todoService;
        private static readonly DbContextOptions<TodoContext> dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoDBTest")
            .Options;

        private TodoContext context;

        //auto mapper configuration
        
        

        [OneTimeSetUp]
        public void Setup()
        {
            context = new TodoContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoProfile()); //your automapperprofile 
                cfg.AddProfile(new TodoCategoryProfile());
                cfg.AddProfile(new StatusProfile());
            });
            var mapper = mockMapper.CreateMapper();
            todoRepo = new TodoRepo(context);
            todoService = new TodoService(todoRepo, mapper);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }


        [Test, Order(1)]
        public async Task GetTodosAsync_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            IEnumerable<TodoReadDTO> result = await todoService.GetTodosAsync("name", "t", null);

            Assert.That(result.Count, Is.EqualTo(4));
        }
        [Test, Order(2)]
        public async Task HTTPPOST_AddTodoAsyncWithNoResponseTest()
        {
            TodoCreateDTO todoCreateDTO = new TodoCreateDTO()
            {
                Title = "Todo 8",
                Description = "Test 8",
                StatusId = 10,
                TodoCategoryId = 10
            };

            var actionResult = await todoService.AddTodoAsync(todoCreateDTO);
            Console.WriteLine(todoService.GetTodosSize());
            //Assert.That(await todoService.GetTodosSize(), Is.EqualTo(7));
            Assert.That(actionResult, Is.Null);
        }

        private void SeedDatabase()
        {
            var todoCategory = new List<TodoCategory>
            {
                new TodoCategory()
                {
                    Id=1,
                    Name = "Cat 1"
                },
                new TodoCategory()
                {
                    Id=2,
                    Name = "Cat 2"
                },
                new TodoCategory()
                {
                    Id=3,
                    Name = "Cat 3"
                }
            };

            context.TodoCategory.AddRange(todoCategory);

            var statuses = new List<Status>
            {
                new Status()
                {
                    Id=1,
                    Name="Status 1"
                },
                new Status()
                {
                    Id=2,
                    Name="Status 2"
                },
                new Status()
                {
                    Id=3,
                    Name="Status 3"
                }
            };

            context.Statuses.AddRange(statuses);

            var todos = new List<Todo>
            {
                new Todo()
                {
                    Id = 1,
                    Title = "Todo 1",
                    Description = "Test 1",
                    StatusId = 1,
                    TodoCategoryId = 1
                },
                new Todo()
                {
                    Id = 2,
                    Title = "Todo 2",
                    Description = "Test 2",
                    StatusId = 2,
                    TodoCategoryId = 2
                },
                new Todo()
                {
                    Id = 3,
                    Title = "Todo 3",
                    Description = "Test 3",
                    StatusId = 3,
                    TodoCategoryId = 3
                },
                new Todo()
                {
                    Id = 4,
                    Title = "Todo 4",
                    Description = "Test 4",
                    StatusId = 1,
                    TodoCategoryId = 2
                },
                new Todo()
                {
                    Id = 5,
                    Title = "Todo 5",
                    Description = "Test 5",
                    StatusId = 1,
                    TodoCategoryId = 1
                },
                new Todo()
                {
                    Id = 6,
                    Title = "Todo 6",
                    Description = "Test 6",
                    StatusId = 2,
                    TodoCategoryId = 2
                },
                new Todo()
                {
                    Id = 7,
                    Title = "Todo 7",
                    Description = "Test 7",
                    StatusId = 3,
                    TodoCategoryId = 3
                }
            };
            context.Todos.AddRange(todos);

            context.SaveChanges();
        }
    }
}
