using System.Collections.Generic;

namespace Todo.Models.TodoItems
{
    public class UpdateItemRanksModel
    {
        public List<ItemIdRankPair> NewItemRanks { get; set; }
    }
}
