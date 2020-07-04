using Microsoft.AspNetCore.Identity;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Xunit;

namespace Todo.Tests
{
    // Note: I'm using the naming convention for unit tests that I'm used to using but I'm happy to switch to using any 
    // convention that Storm ID would normally use instead. These tests are for a static class that doesn't need any dependencies mocked
    // but if I did need to mock dependencies I would normally use Moq to do it. The naming convention I'm using is: Action_DoesSomething_WhenCondition
    // I know that some companies also use the Given/When/Then convention
    public class TodoListDetailViewmodelFactoryTests
    {
        private readonly TodoList _todoList;

        public TodoListDetailViewmodelFactoryTests()
        {
            // Arrange
            var responsibleParty = new IdentityUser("person@website.com");

            _todoList = new TestTodoListBuilder(responsibleParty, "shopping")
                .WithItem("oranges", Importance.High, null, responsibleParty)
                .WithItem("raspberries", Importance.Medium, 2, responsibleParty)
                .WithItem("bananas", Importance.Low, 1, responsibleParty)
                .Build();
        }

        [Fact]
        public void Create_OrdersByRank_WhenOrderByRankIsTrue()
        {
            // Act
            var viewmodel = TodoListDetailViewmodelFactory.Create(_todoList, true);

            // Assert
            var items = viewmodel.Items.ToList();
            Assert.Equal(1, items[0].Rank);
            Assert.Equal(2, items[1].Rank);
            Assert.Null(items[2].Rank);
            // I'm using Xunit's Assert to do the assertions because the other unit tests do the same 
            // but I also quite like using the FluentAssertions nuget package https://fluentassertions.com/introduction
        }

        [Fact]
        public void Create_OrdersByImportance_WhenOrderByRankIsFalse()
        {
            // Act
            var viewmodel = TodoListDetailViewmodelFactory.Create(_todoList, false);

            // Assert
            var items = viewmodel.Items.ToList();
            Assert.Equal(Importance.High, items[0].Importance);
            Assert.Equal(Importance.Medium, items[1].Importance);
            Assert.Equal(Importance.Low, items[2].Importance);
        }
    }
}
