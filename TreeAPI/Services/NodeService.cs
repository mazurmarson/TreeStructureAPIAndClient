using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreeAPI.Context;
using TreeAPI.Helpers;
using TreeAPI.Models;

namespace TreeAPI.Services
{
    public interface INodeService
    {

        Task<Node> CreateNode(Node node);
        Task<Node> ChangeParent(int NoteId, int NewParentId);
        Task<IList<NodeWithChilds>> GetTree();
        Task DeleteTree();
        Task DeleteNode(int NodeId);
        


    }
    public class NodeService : INodeService
    {
        private readonly TreeDbContext _treeDbContext;
        private readonly IMapper _mapper;

        public NodeService(TreeDbContext treeDbContext, IMapper mapper)
        {
            _treeDbContext = treeDbContext;
            _mapper = mapper;
        }
        public async Task<Node> ChangeParent(int NoteId, int NewParentId)
        {
            if(!_treeDbContext.Nodes.Any(x => x.Id == NoteId))
            {
                throw new System.NotImplementedException(); //Wezeł o wskazanym ID nie istnieje
            }

            if(!_treeDbContext.Nodes.Any(x => x.Id == NewParentId))
            {
                 throw new System.NotImplementedException(); //Wezeł rodzic o wskazanym ID nie istnieje
            }
            var node = await _treeDbContext.Nodes.Where(x => x.Id == NoteId).FirstOrDefaultAsync();
            node.ParentId = NewParentId;

            _treeDbContext.Update(node);
            await _treeDbContext.SaveChangesAsync();
            return node;
        }

        public async Task<Node> CreateNode(Node node)
        {
           
            if(node.ParentId == null)
            {
                if(_treeDbContext.Nodes.Any())
                {
                    throw new System.NotImplementedException();
                }

                _treeDbContext.Nodes.Add(node);
                await _treeDbContext.SaveChangesAsync();

                return node;
            }

            if(!_treeDbContext.Nodes.Any(x => x.Id == node.ParentId))
            {
                 throw new System.NotImplementedException();
            }
           
            if(_treeDbContext.Nodes.Any(x => x.Value == node.Value))
            {
                throw new System.NotImplementedException();
            }
            
            _treeDbContext.Nodes.Add(node);
            await _treeDbContext.SaveChangesAsync();
            return node;
        }



        public async Task DeleteNode(int NodeId)
        {
           var node = await _treeDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == NodeId);

           _treeDbContext.Remove(node);
           await _treeDbContext.SaveChangesAsync();
        }

        public Task DeleteTree()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<NodeWithChilds>> GetTree()
        {
           // var rootNode = await _treeDbContext.Nodes.FirstOrDefaultAsync(x => x.ParentId == null);
         //  var tree = await _treeDbContext.Nodes.ToListAsync();
          var nodes = await _treeDbContext.Nodes.ToListAsync();
          var nodesWithChilds = _mapper.Map<List<NodeWithChilds>>(nodes); 

            return nodesWithChilds.BuilldTree(); 
        }

        // public async Task<List<NodeWithChilds>> GetNodes()
        // {
        //     var root = await _treeDbContext.Nodes.FirstOrDefaultAsync(x => x.ParentId == null);
        //     List<NodeWithChilds> nodes = new List<NodeWithChilds>();
        //     NodeWithChilds rootWithChilds = new NodeWithChilds{
        //         Id = root.Id,
        //         Value = root.Value
        //     } ;
        //     LoadChilds(rootWithChilds ,ref nodes);

        //     return nodes;

        // }

        // private void LoadChilds(NodeWithChilds parent ,ref List<NodeWithChilds> nodes)
        // {
            
        //     nodes.Add(parent);
        //     var childs = _treeDbContext.Nodes.Where(x => x.ParentId == parent.Id);

        //     foreach(var node in childs)
        //     {
        //         parent.Childs.Add(node);
        //         NodeWithChilds nodeWithChilds = new NodeWithChilds{
        //             Id = node.Id,
        //             Value = node.Id,
        //             ParentId = node.ParentId
        //         };
        //         LoadChilds(nodeWithChilds, ref nodes);
        //     }
        // }

        // public void BindTree(List<Node> nodes, Node node, int parentId)
        // {

        // }

        // public async Task<NodeWithChilds> CreateTree()
        // {
        //     var root = await _treeDbContext.Nodes.FirstOrDefaultAsync(x => x.ParentId == null);
        //     var rootWithChilds = _mapper.Map<NodeWithChilds>(root);

        //     LoadChilds(ref rootWithChilds);

        //     return rootWithChilds;
        // }

        // public void LoadChilds(ref NodeWithChilds nodeWithChilds)
        // {
        //     int parentId = nodeWithChilds.Id;
        //     var childs =  _treeDbContext.Nodes.Where(x => x.ParentId == parentId).ToList();
        //     if(nodeWithChilds.ParentId != null)
        //         nodeWithChilds.Childs.Add(nodeWithChilds);

        //     foreach(var child in childs)
        //     {
        //         var childWithChilds = _mapper.Map<NodeWithChilds>(child);
        //         LoadChilds(ref childWithChilds);
        //     }
        // }
    }
}