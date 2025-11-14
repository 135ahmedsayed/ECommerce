using System.Security.Claims;
using System.Threading;
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
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request , CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.CreateAsync(request, email , cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id , CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.GetOrderAsync(id ,email, cancellationToken);
        return HandleResult(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<OrderResponse>> GetAll(CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.GetAllAsync(email , cancellationToken);
        return Ok(result);
    }

    [HttpGet("DeliveryMrthods")]
    public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods(CancellationToken cancellationToken)
    {
        var methods = await orderService.GetDeliveryMethodsync(cancellationToken);
        return Ok(methods);
    }
}
