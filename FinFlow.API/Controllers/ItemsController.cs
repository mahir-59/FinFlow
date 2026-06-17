using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinFlow.Modules.Items.Items.BL.Interfaces;
using FinFlow.Modules.Items.Items.BL.Classes;
using FinFlow.Modules.Common.Models;
using FinFlow.Modules.Auth.Auth.Model.Classes.Responses;
using FinFlow.Modules.Items.Items.Model.Classes;
using FinFlow.Modules.Items.Items.Model.Interfaces;

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
                return Ok(
                    new ApiResponse<List<IItemResponse>>
                    {
                        StatusCode = 200,

                        IsSuccess = true,

                        Message = "Items retrieved successfully",

                        Data = items
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}