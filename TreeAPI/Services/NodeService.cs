using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreeAPI.Context;
using TreeAPI.Dtos;
using TreeAPI.Helpers;
using TreeAPI.Models;

namespace TreeAPI.Services
{
    public interface INodeService
    {

        Task<Node> CreateNode(AddNodeDto node);
        Task<Node> ChangeParent(int NoteId, int NewParentId);
        Task<IList<NodeWithChilds>> GetTree();
         Task<IList<NodeWithChilds>> GetTreeSorted(int SortBy);
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
                throw new System.NotImplementedException(); 
            }

            if(!_treeDbContext.Nodes.Any(x => x.Id == NewParentId))
            {
                 throw new System.NotImplementedException(); 
            }
            var node = await _treeDbContext.Nodes.Where(x => x.Id == NoteId).FirstOrDefaultAsync();
            node.ParentId = NewParentId;

            _treeDbContext.Update(node);
            await _treeDbContext.SaveChangesAsync();
            return node;
        }

        public async Task<Node> CreateNode(AddNodeDto nodeDto)
        {
            var node = new Node{
                Value = nodeDto.Value,
                ParentId = nodeDto.ParentId
            };
           

            
            _treeDbContext.Nodes.Add(node);
            await _treeDbContext.SaveChangesAsync();
            return node;
        }



        public async Task DeleteNode(int NodeId)
        {
           var node = await _treeDbContext.Nodes.FirstOrDefaultAsync(x => x.Id == NodeId);
            await RemoveChildren(NodeId);
           _treeDbContext.Remove(node);
           await _treeDbContext.SaveChangesAsync();
        }

        public Task DeleteTree()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<NodeWithChilds>> GetTree()
        {
          var nodes = await _treeDbContext.Nodes.ToListAsync();
          var nodesWithChilds = _mapper.Map<List<NodeWithChilds>>(nodes); 

            return nodesWithChilds.BuilldTree(); 
        }

        public async Task<IList<NodeWithChilds>> GetTreeSorted(int SortBy)
        {
            if(SortBy == 1)
            {
                var nodesSortedByValue = await _treeDbContext.Nodes.OrderBy(x => x.Value).ToListAsync();
                 var nodesWithChildsSortedByValue = _mapper.Map<List<NodeWithChilds>>(nodesSortedByValue); 

                return nodesWithChildsSortedByValue.BuilldTree(); 
            }
                var nodesSortedById = await _treeDbContext.Nodes.OrderBy(x => x.Id).ToListAsync();
                 var nodesWithChildsSortedById = _mapper.Map<List<NodeWithChilds>>(nodesSortedById); 

                return nodesWithChildsSortedById.BuilldTree(); 
        }

        
        public async Task RemoveChildren(int id)
        {
            var children = await _treeDbContext.Nodes.Where(x => x.ParentId == id).ToListAsync();
            foreach(var child in children)
            {
                await RemoveChildren(child.Id);
                _treeDbContext.Remove(child);
                await _treeDbContext.SaveChangesAsync();
            }
        }
    }
}