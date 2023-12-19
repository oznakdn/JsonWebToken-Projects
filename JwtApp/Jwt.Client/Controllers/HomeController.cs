namespace Jwt.Client.Controllers;

public class HomeController : Controller
{

    private readonly CustomerService _customerService;

    public HomeController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public IActionResult Index()
    {
        return View();
    }

}
