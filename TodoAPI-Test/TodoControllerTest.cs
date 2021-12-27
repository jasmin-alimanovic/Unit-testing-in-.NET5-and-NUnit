using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Controllers;
using TodoAPI.Data;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Mapping_Profiles;
using TodoAPI.Repositories;
using TodoAPI.Services;

namespace TodoAPI_Test
{
    class TodoControllerTest
    {
        ITodoRepo todoRepo;
        TodoService todoService;
        TodoController todoController;
        private static DbContextOptions<TodoContext> dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
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
            todoController = new TodoController(todoService);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// get all todos with no passed sort by string, no search string and no page number
        /// </summary>
        /// <returns> all todos succesfully</returns>
        /// 
        [Test, Order(1)]
        public async Task HTTPGET_GetTodosAsyncWithNoSortByWithNoSearchStringWithNoPageNumberTest()
        {
            IActionResult actionResult = await todoController.GetTodosAsync("", "", null);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as ReadWithOptionsDTO<TodoReadDTO>;

            Assert.That(actionResultData.Results.First().Title, Is.EqualTo("Todo 7"));
            Assert.That(actionResultData.Results.First().Id, Is.EqualTo(7));
            Assert.That(actionResultData.Results.Last().Id, Is.EqualTo(4));
            Assert.That(actionResultData.Results.Last().Title, Is.EqualTo("Todo 4"));
            Assert.That(actionResultData.Results.Count, Is.EqualTo(4));
            Assert.That(actionResultData.Count, Is.EqualTo(7));
            Assert.That(actionResultData.Next, Is.EqualTo("https://localhost:5001/api/Todo?pageNumber=2"));
            Assert.That(actionResultData.Previous, Is.Null);
        }

        /// <summary>
        /// get all todos with no passed sort by string, no search string and no page number
        /// </summary>
        /// <returns> all todos succesfully</returns>
        /// 
        [Test, Order(5)]
        public async Task HTTPGET_GetTodosAsyncWithNoSortByWithNoSearchStringWithPageNumberTest()
        {
            IActionResult actionResult = await todoController.GetTodosAsync("", "", 2);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as ReadWithOptionsDTO<TodoReadDTO>;

            Assert.That(actionResultData.Results.First().Title, Is.EqualTo("Todo 3"));
            Assert.That(actionResultData.Results.First().Id, Is.EqualTo(3));
            Assert.That(actionResultData.Results.Last().Id, Is.EqualTo(1));
            Assert.That(actionResultData.Results.Last().Title, Is.EqualTo("Todo 1"));
            Assert.That(actionResultData.Results.Count, Is.EqualTo(3));
            Assert.That(actionResultData.Count, Is.EqualTo(7));
            Assert.That(actionResultData.Next, Is.Null);
            Assert.That(actionResultData.Previous, Is.EqualTo("https://localhost:5001/api/Todo?pageNumber=1") );
        }
        /// <summary>
        /// get all todos with  sort by string, with search string and no page number
        /// </summary>
        /// <returns>filtered todos succesfully</returns>
        /// 
        [Test, Order(2)]
        public async Task HTTPGET_GetTodosAsyncWithSortByWithSearchStringWithNoPageNumberTest()
        {
            IActionResult actionResult = await todoController.GetTodosAsync("title", "tOdO", null);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as ReadWithOptionsDTO<TodoReadDTO>;

            Assert.That(actionResultData.Results.First().Title, Is.EqualTo("Todo 1"));
            Assert.That(actionResultData.Results.First().Id, Is.EqualTo(1));
            Assert.That(actionResultData.Results.Count, Is.EqualTo(4));
        }

        /// <summary>
        /// get todo by id 
        /// 
        /// </summary>
        /// <returns>
        ///     successfully return todo with id passed as parametar
        /// </returns>
        [Test, Order(3)]
        public async Task HTTPGET_GetTodosAsyncByIdWithResponseTest()
        {
            IActionResult actionResult = await todoController.GetTodoByIdAsync(4);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as TodoReadDTO;

            Assert.That(actionResultData.Title, Is.EqualTo("Todo 4"));
            Assert.That(actionResultData.Id, Is.EqualTo(4));
        }
        /// <summary>
        /// get todo by id
        /// throws not found exception
        /// </summary>
        /// <returns></returns>
        [Test, Order(4)]
        public async Task HTTPGET_GetTodosAsyncByIdWithNoResponseTest()
        {
            IActionResult actionResult = await todoController.GetTodoByIdAsync(14);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }


        [Test, Order(6)]
        public async Task HTTPPOST_AddTodoAsyncWithResponseTest()
        {
            TodoCreateDTO todoCreateDTO = new TodoCreateDTO()
            {
                Title = "Todo 8",
                Description = "Test 8",
                StatusId = 1,
                TodoCategoryId = 1
            };

            var actionResult = await todoController.AddTodoAsync(todoCreateDTO);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());

            var savedTodo = (actionResult as CreatedResult).Value as TodoReadDTO;

            Assert.That(todoCreateDTO.Title, Is.EqualTo(savedTodo.Title));
            Assert.That(todoCreateDTO.Description, Is.EqualTo(savedTodo.Description));
            Assert.That(todoCreateDTO.StatusId, Is.EqualTo(savedTodo.Status.Id));
        }

        [Test, Order(7)]
        public async Task HTTPPOST_AddTodoAsyncWithNoResponseTest()
        {
            TodoCreateDTO todoCreateDTO = new TodoCreateDTO()
            {
                Title = "Todo 8",
                Description = "Test 8",
            };

            var actionResult = await todoController.AddTodoAsync(todoCreateDTO);
            Console.WriteLine(todoService.GetTodosSize());
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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
                    TodoCategoryId = 2
                }
            };
            context.Todos.AddRange(todos);

            context.SaveChanges();
        }
    }
}
