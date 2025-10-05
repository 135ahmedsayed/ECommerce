using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;
[ApiController]
[Route("api/[Controller]")]
public class ProductsController : ControllerBase
{
    #region Restful API Endpoints
    //[Route("{id}")] // variable segment
    //[HttpGet]
    ////[HttpGet("{id}")]
    //public ActionResult Get(int id)
    //{
    //    return Ok(new products { Id = id, Name = "Product "});
    //}

    //[HttpGet]
    //public ActionResult GetAll()
    //{
    //    return Ok(new products { });
    //}

    //[HttpPost]
    //public ActionResult Create(products product)
    //{
    //    return Created("Test", product);
    //}

    //[HttpPut]
    //public ActionResult Update(products product)
    //{
    //    return Ok( product);
    //}

    //[HttpDelete]
    //public ActionResult Delete(int id)
    //{
    //    return NoContent();
    //}
    #endregion

}

//type Testing 
public class products
{
    public int Id { get; set; } = 1;
    public string Name { get; set; } = "qqwdqwd";
}