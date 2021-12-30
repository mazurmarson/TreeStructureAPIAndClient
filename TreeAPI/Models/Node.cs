using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace TreeAPI.Models
{
    public class Node
    {
        public int Id { get; set; }
        public int Value { get; set; }
          public  int? ParentId { get; set; }
       //   public virtual List<Node> Childs {get; set;}
      //    public virtual List<Node> Childs {get; set;}
          // public IList<Node> childs = new List<Node>();

          // public void AddChildren(Node node)
          // {
          //   this.childs.Add(node);
          // }
    //       public List<Node> Children { get; set; }
    //    public  Node Parent { get; set; }
    //    public virtual List<Node> Childs { get; set; }

    
    }
}