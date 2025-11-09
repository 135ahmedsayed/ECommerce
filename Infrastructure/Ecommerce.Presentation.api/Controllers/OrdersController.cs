using System.Security.Claims;
using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.UserOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.api.Controllers;

[Authorize]
public class OrdersController(IOrderService orderService)
    :ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.CreateAsync(request, email);
        return HandleResult(result);
    }
}
