using Jwt.Console.ClientService;
using Jwt.Console.ClientServices;
using Jwt.Console.Models.AuthModels.LoginModels;
using Jwt.Console.Models.AuthModels.RegisterModels;

Console.WriteLine("********** MENU **********");
Console.WriteLine("1 - Login");
Console.WriteLine("2 - Register");

string token = string.Empty;
int menuNumber = Convert.ToInt16(Console.ReadLine());
switch (menuNumber)
{
    case 1:
        Login(); break;
    case 2:
        Register(); break;
    default:
        Console.WriteLine("This number is invalid!");
        break;
}

void Login()
{
    Console.WriteLine("----------------- Login -----------------");
    Console.Write("Email: ");
    string email = Console.ReadLine();
    Console.Write("Password: ");
    string password = Console.ReadLine();

    var response = AuthService.Login(new LoginRequest(email, password)).Result;
    if (response.message != null)
    {
        Console.WriteLine(response.message);
    }
    else
    {
        Console.WriteLine(response.result.token);
        token = response.result.token;
        Console.WriteLine("********** MENU *********");
        Console.WriteLine("1 - Categories");
        Console.WriteLine("2 - Products");
        int menuNumberMenu = Convert.ToInt16(Console.ReadLine());
        Menu(menuNumberMenu);

    }

    Console.ReadLine();
}

void Register()
{
    Console.WriteLine("----------------- Register -----------------");
    Console.Write("First Name: ");
    string firstName = Console.ReadLine();
    Console.Write("Last Name: ");
    string lastName = Console.ReadLine();
    Console.Write("FEmail: ");
    string email = Console.ReadLine();
    Console.Write("Password: ");
    string password = Console.ReadLine();

    var response = AuthService.Register(new RegisterRequest(firstName, lastName, email, password)).Result;
    Console.WriteLine(response);
    Console.ReadLine();

}

void GetCategories()
{
    var categories = CategoryService.GetCategories(token).Result;
    if(categories!= null)
    {
        foreach (var category in categories)
        {
            Console.Write($"Id: {category.categoryId} Category Name:{category.categoryName}");
        }
    }
    else
    {
        Console.WriteLine("Not found category!");
    }
    Console.ReadLine();


}

void Menu(int menuNumber)
{
    switch (menuNumber)
    {
        case 1:
            GetCategories(); break;
        case 2:
            Register(); break;
        default:
            Console.WriteLine("This number is invalid!");
            break;
    }
}


