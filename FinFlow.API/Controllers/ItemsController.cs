using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinFlow.Modules.Items.Items.BL.Interfaces;
using FinFlow.Modules.Items.Items.BL.Classes;

namespace FinFlow.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemBL _itemsBL;
        public ItemsController(IItemBL itemsBL) 
        {
            _itemsBL = itemsBL;
        }

        [AllowAnonymous]
        [HttpGet("GetAllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _itemsBL.GetAllItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}