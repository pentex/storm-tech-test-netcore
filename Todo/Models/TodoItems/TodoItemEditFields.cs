﻿using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemEditFields
    {
        public int TodoListId { get; set; }
        public string Title { get; set; }
        public string TodoListTitle { get; set; }
        public int TodoItemId { get; set; }
        public bool IsDone { get; set; }
        [Display(Name = "Email Address")]
        public string ResponsiblePartyId { get; set; }
        public Importance Importance { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Rank can't be higher priority than 1")]
        public int? Rank { get; set; }

        public TodoItemEditFields() { }

        public TodoItemEditFields(int todoListId, string todoListTitle, int todoItemId, string title, bool isDone, string responsiblePartyId, Importance importance, int? rank)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            TodoItemId = todoItemId;
            Title = title;
            IsDone = isDone;
            ResponsiblePartyId = responsiblePartyId;
            Importance = importance;
            Rank = rank;
        }
    }
}