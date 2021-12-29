using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeAPI.Models
{
    public class Node
    {
        public int Id { get; set; }
        public int Value { get; set; }
          public  int? ParentId { get; set; }
        public  Node Parent { get; set; }
        public virtual List<Node> Childs { get; set; }
    }
}