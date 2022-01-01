using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> GetTree()
        {
            var tree = await _nodeService.GetTree();
            return Ok(tree);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNode(Node node)
        {
            await _nodeService.CreateNode(node);

            return Ok();
        }

        [HttpPost("{nodeId}/{newParentId}")]
        public async Task<IActionResult> ChangeParent(int nodeId, int newParentId)
        {
            await _nodeService.ChangeParent(nodeId, newParentId);

            return Ok();
        }

        [HttpDelete("{nodeId}")]
        public async Task<IActionResult> DeleteNode(int nodeId)
        {
            await _nodeService.DeleteNode(nodeId);
            return NoContent();
        }
        
    }
}