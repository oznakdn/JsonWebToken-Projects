namespace Jwt.Client.ClientServices;

public class CustomerService : ClientService
{
    public CustomerService(IHttpClientFactory httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<GetCustomerViewModel>> GetCustomersAsync()
    {
        string url = $"customers/getCustomers";
        var result = await base.GetAsync(url);
        IEnumerable<GetCustomerViewModel> customers = JsonSerializer.Deserialize<IEnumerable<GetCustomerViewModel>>(result)!;
        return customers;
    }

    public async Task<GetCustomerViewModel>GetCustomerAsync(int id)
    {
        string url = $"customers/getCustomerById/{id}";
        var result = await base.GetAsync(url);
        GetCustomerViewModel customer = JsonSerializer.Deserialize<GetCustomerViewModel>(result)!;
        return customer;
    }

    public async Task<CreateCustomerViewModel> CreateCustomerAsync(CreateCustomerViewModel createCustomer)
    {
        string url = $"customers/createCustomer";
        var result = await base.PostAsync(url, createCustomer);
        CreateCustomerViewModel createdCustomer = JsonSerializer.Deserialize<CreateCustomerViewModel>(result)!;
        return createdCustomer;
    }

    public async Task UpdateCustomerAsync(UpdateCustomerViewModel updateCustomer)
    {

        string url = $"customers/updateCustomer";
        await base.PutAsync<UpdateCustomerViewModel>(url, updateCustomer);
  
    }

    public async Task DeleteCustomerAsync(int id)
    {
        string url = $"customers/deleteCustomer/{id}";
        await base.DeleteAsync(url);
    }
}