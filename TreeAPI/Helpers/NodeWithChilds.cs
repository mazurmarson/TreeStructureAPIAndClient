using System.Collections.Generic;
using TreeAPI.Models;

namespace TreeAPI.Helpers
{
    public class NodeWithChilds
    {
                public int Id { get; set; }
        public int Value { get; set; }
          public  int? ParentId { get; set; }
          public List<NodeWithChilds> Childs { get; set; }

        //  public void AddChildern(Node node)
    }
}