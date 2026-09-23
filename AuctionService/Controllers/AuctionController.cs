using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController : Controller
{
    // GET
    public ActionResult<string> Index()
    {
        return "Hi (:";
    }
}