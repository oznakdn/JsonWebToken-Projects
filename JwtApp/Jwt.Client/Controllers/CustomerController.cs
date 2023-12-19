namespace Jwt.Client.Controllers;


public class CustomerController : Controller
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    #region READ

    [CustomAuthorizationFilter]
    public async Task<IActionResult> Index()
    {
        var result = await _customerService.GetCustomersAsync();
        return View(result);
    }

    #endregion

    #region CREATE

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [CustomAuthorizationFilter]
    public async Task<IActionResult> Create(CreateCustomerViewModel customerViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _customerService.CreateCustomerAsync(customerViewModel);
            if (result != null)
            {
                TempData["customerCreated"] = "Customer was created.";
                return RedirectToAction(nameof(Index));
            }
            return View(customerViewModel);
        }
        return View(customerViewModel);
    }

    #endregion

    #region EDIT

    [CustomAuthorizationFilter]
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        var viewModel = new UpdateCustomerViewModel(customer.id, customer.fullName, customer.age, customer.email, customer.phone);
        return View(viewModel);
    }

    [HttpPost]
    [CustomAuthorizationFilter]
    public async Task<IActionResult> Edit(UpdateCustomerViewModel updateCustomerView)
    {
        if (ModelState.IsValid)
        {
            await _customerService.UpdateCustomerAsync(updateCustomerView);
            TempData["customerUpdated"] = "Customer was updated.";
            return RedirectToAction(nameof(Index));
        }

        return View(updateCustomerView);
    }

    #endregion

    #region DELETE

    [CustomAuthorizationFilter]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return RedirectToAction(nameof(Index));
    }


    #endregion

}
