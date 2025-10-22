using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.api.Controllers;
public class BasketsController(IBasketService basketService)
    : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<CustomerBasketDTO>> Update(CustomerBasketDTO basketDTO)
    {
        return Ok(await basketService.CreateOrUpdateAsync(basketDTO));
    }
    [HttpGet]
    public async Task<ActionResult<CustomerBasketDTO>> Get(string id)
    { 
        return Ok(await basketService.GetByIdAsync(id));
    }
    [HttpDelete]
    public async Task<ActionResult> Delete(string id)
    {
        await basketService.DeleteAsync(id);
        return NoContent();
    }
}
