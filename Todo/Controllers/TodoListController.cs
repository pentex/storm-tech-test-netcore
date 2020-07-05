using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(ApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = dbContext.RelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        public IActionResult Detail(int todoListId, bool orderByRank = false)
        {
            var todoList = dbContext.SingleTodoList(todoListId);
            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, orderByRank);
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

            var todoList = new TodoList(currentUser, fields.Title);

            await dbContext.AddAsync(todoList);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Create", "TodoItem", new { todoList.TodoListId });
        }

        [HttpPost]
        [Route("[controller]/{todoListId}")]
        public async Task<IActionResult> UpdateItemRanks(int todoListId, [FromBody] UpdateItemRanksModel updateItemRanksModel)
        {
            var todoList = dbContext.SingleTodoList(todoListId);

            foreach (var item in todoList.Items)
            {
                foreach (var itemIdRankPair in updateItemRanksModel.NewItemRanks)
                {
                    if (item.TodoItemId == itemIdRankPair.Id)
                    {
                        item.Rank = itemIdRankPair.Rank;
                        break;
                    }
                }
            }

            dbContext.Update(todoList);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}