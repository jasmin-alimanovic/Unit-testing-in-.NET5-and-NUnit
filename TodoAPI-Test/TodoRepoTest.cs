using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Data.Models;
using TodoAPI.Repositories;

namespace TodoAPI_Test
{
    public class TodoRepoTest
    {

        private static readonly DbContextOptions<TodoContext> dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoDBTest")
            .Options;

        private TodoContext context;

        TodoRepo todoRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new TodoContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            todoRepo = new TodoRepo(context);
        }

        [Test]
        public async Task GetTodosAsync_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            List<Todo> result = (List<Todo>)await todoRepo.GetTodosAsync("", "", null);

            Assert.That(result.Count, Is.EqualTo(4));
            Assert.AreEqual(result.Count, 4);
        }

        [Test]
        public async Task GetTodosAsync_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            List<Todo> result = (List<Todo>)await todoRepo.GetTodosAsync("", "", 2);

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetTodosAsync_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            List<Todo> result = (List<Todo>)await todoRepo.GetTodosAsync("", "3", null);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Title, Is.EqualTo("Todo 3"));
        }

        [Test]
        public async Task GetTodosAsync_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            List<Todo> result = (List<Todo>)await todoRepo.GetTodosAsync("name_desc", "", null);

            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.FirstOrDefault().Title, Is.EqualTo("Todo 7"));
        }

        [Test]
        public async Task GetTodosAsync_WithSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            List<Todo> result = (List<Todo>)await todoRepo.GetTodosAsync("name_desc", "", 2);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.ElementAt(result.Count - 1).Title, Is.EqualTo("Todo 1"));
        }

        [Test]
        public async Task GetTodoByIdAsync_WithResponse_Test()
        {
            var result = await todoRepo.GetTodoByIdAsync(1);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("Todo 1"));

        }

        [Test]
        public async Task GetTodoByIdAsync_WithNoResponse_Test()
        {
            var result = await todoRepo.GetTodoByIdAsync(8);
            Assert.That(result, Is.Null);

        }

        [Test]
        public async Task HTTPPOST_AddTodoAsyncWithNoResponseTest()
        {
            Todo todoCreateDTO = new Todo()
            {
                Title = "Todo 8",
                Description = "Test 8",
                TodoCategoryId = 10
            };

            

            var actionResult = await todoRepo.AddTodoAsync(todoCreateDTO);
            Console.WriteLine(todoRepo.GetTodosSize());
            //Assert.That(await todoRepo.GetTodosSize(), Is.EqualTo(7));
            Assert.That(actionResult.Id, Is.EqualTo(7));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
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