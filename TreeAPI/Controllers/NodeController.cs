using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreeAPI.Models;
using TreeAPI.Services;

namespace TreeAPI.Controllers
{
        [ApiController]
    [Route("api/[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
    {
            _nodeService = nodeService;
        }
        
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTree()
        {
            var tree = await _nodeService.GetTree();
            return Ok(tree);
        }

        [Authorize]
        [HttpGet("sort/{sortBy}")]
        public async Task<IActionResult> GetTreeSorted(int sortBy)
        {
            if(sortBy == 0 || sortBy == 1)
            {
                    var tree = await _nodeService.GetTreeSorted(sortBy);
                    return Ok(tree);
            }
            return BadRequest();

        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNode(Node node)
        {
            await _nodeService.CreateNode(node);

            return Ok();
        }
        [Authorize(Roles ="admin")]
        [HttpPost("{nodeId}/{newParentId}")]
        public async Task<IActionResult> ChangeParent(int nodeId, int newParentId)
        {
            await _nodeService.ChangeParent(nodeId, newParentId);

            return Ok();
        }
        [Authorize(Roles ="admin")]
        [HttpDelete("{nodeId}")]
        public async Task<IActionResult> DeleteNode(int nodeId)
        {
            await _nodeService.DeleteNode(nodeId);
            return NoContent();
        }
        
    }
}