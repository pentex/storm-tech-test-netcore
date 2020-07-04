using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool orderByRank)
        {
            var items = new List<TodoItemSummaryViewmodel>();

            // Initially in task 2 the requirement seemed to state that the ordering should be done in Detail.cshtml but for this task I've refactored it
            // so that all the ordering is now done on the backend so that the processing is done on the server rather than the client
            // and so the view layer is less cluttered with business logic
            if (orderByRank)
            {
                items = todoList.Items
                    .Select(TodoItemSummaryViewmodelFactory.Create)
                    .OrderByDescending(item => item.Rank.HasValue) // Make it so items that the user hasn't set a rank for yet get prioritised at the bottom of the list
                    .ThenBy(item => item.Rank)
                    .ToList();
            }
            else
            {
                items = todoList.Items
                    .Select(TodoItemSummaryViewmodelFactory.Create)
                    .OrderBy(item => item.Importance)
                    .ToList();
            }

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items, orderByRank);
        }
    }
}