using System;

// Базовий клас гарячих напоїв
public abstract class HotBeverage
{
    public string name { get; set; }

    public abstract void Prepare();
}

// Клас для виготовлення кави
public class Coffee : HotBeverage
{
    public override void Prepare()
    {
        Console.WriteLine("Preparing coffee...");
    }
}

// Клас для виготовлення чаю
public class Tea : HotBeverage
{
    public override void Prepare()
    {
        Console.WriteLine("Preparing tea...");
    }
}

// Клас "автомат"
public class VendingMachine
{
    private bool isAdminMode = false;
    private bool isWaterLoaded = false;
    private bool isCoffeeLoaded = false;
    private bool isTeaLoaded = false;
    private decimal coffeePrice = 2.0m;
    private decimal teaPrice = 1.5m;
    private decimal cashBalance = 0;

    // адмінка
    public void SwitchToAdminMode(string password)
    {
        if (password == "admin123")
        {
            isAdminMode = true;
            Console.WriteLine("Switched to admin mode");
        }
        else
        {
            Console.WriteLine("Invalid password. Access denied");
        }
    }

    // юзер
    public void SwitchToUserMode()
    {
        isAdminMode = false;
        Console.WriteLine("Switched to user mode");
    }
    // загрузка води
    public void LoadWater()
    {
        if (isAdminMode)
        {
            isWaterLoaded = true;
            Console.WriteLine("Admin mode: Water loaded");
        }
        else
        {
            Console.WriteLine("Water loading functionality is only available in admin mode");
        }
    }

    // загрузка кофе
    public void LoadCoffee()
    {
        if (isAdminMode)
        {
            isCoffeeLoaded = true;
            Console.WriteLine("Admin mode: Coffee loaded");
        }
        else
        {
            Console.WriteLine("Coffee loading functionality is only available in admin mode");
        }
    }

    // загрузка чаю
    public void LoadTea()
    {
        if (isAdminMode)
        {
            isTeaLoaded = true;
            Console.WriteLine("Admin mode: Tea loaded");
        }
        else
        {
            Console.WriteLine("Tea loading functionality is only available in admin mode");
        }
    }

    // заказ
    public void ServeHotBeverage(HotBeverage beverage)
    {
        if (isAdminMode)
        {
            Console.WriteLine("Admin mode: Serving " + beverage.name);
        }
        else
        {
            if (isWaterLoaded && ((beverage is Coffee && isCoffeeLoaded) || (beverage is Tea && isTeaLoaded)))
            {
                Console.WriteLine("User mode: Serving " + beverage.name);
                beverage.Prepare();
                PayForBeverage(beverage);
            }
            else
            {
                Console.WriteLine("Required ingredients not loaded. Cannot serve the beverage.");
            }
        }
    }

    // оплата
    private void PayForBeverage(HotBeverage beverage)
    {
        Console.Write("Please enter the payment amount: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        decimal price = 0;
        if (beverage is Coffee)
        {
            price = coffeePrice;
        }
        else if (beverage is Tea)
        {
            price = teaPrice;
        }

        if (amount >= price)
        {
            cashBalance = amount - price;
            Console.WriteLine("Payment successful. Enjoy your beverage!");
            Console.WriteLine("Change: " + cashBalance);
        }
        else
        {
            Console.WriteLine("Insufficient payment. Please insert the correct amount.");
        }
    }

    // меню
    public void ShowMenu()
    {
        Console.WriteLine("========== Vending Machine Menu ==========");
        Console.WriteLine("1. Serve Coffee");
        Console.WriteLine("2. Serve Tea");
        Console.WriteLine("3. Switch to Admin Mode");
        Console.WriteLine("4. Switch to User Mode");
        Console.WriteLine("5. Process Payment (Admin)");
        Console.WriteLine("6. Withdraw Cash (Admin)");
        Console.WriteLine("7. Load Water (Admin)");
        Console.WriteLine("8. Load Coffee (Admin)");
        Console.WriteLine("9. Load Tea (Admin)");
        Console.WriteLine("0. Exit");
        Console.WriteLine("==========================================");
        Console.Write("Enter your choice: ");
    }

    // реалізація до меню
    public void ProcessUserInput(string choice)
    {
        switch (choice)
        {
            case "1":
                HotBeverage coffee = new Coffee();
                coffee.name = "Black Coffee";
                ServeHotBeverage(coffee);
                break;
            case "2":
                HotBeverage tea = new Tea();
                tea.name = "Green Tea";
                ServeHotBeverage(tea);
                break;
            case "3":
                Console.Write("Enter admin password: ");
                string password = Console.ReadLine();
                SwitchToAdminMode(password);
                break;
            case "4":
                SwitchToUserMode();
                break;
            case "5":
                Console.Write("Enter payment amount: ");
                decimal paymentAmount = Convert.ToDecimal(Console.ReadLine());
                ProcessPayment(paymentAmount);
                break;
            case "6":
                Console.WriteLine("Balance: " + cashBalance);
                Console.Write("Enter cash withdrawal amount: ");
                decimal withdrawalAmount = Convert.ToDecimal(Console.ReadLine());
                WithdrawCash(withdrawalAmount);
                break;
            case "7":
                LoadWater();
                break;
            case "8":
                LoadCoffee();
                break;
            case "9":
                LoadTea();
                break;
            case "0":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    // загрузка грошей(адмінка)
    public void ProcessPayment(decimal amount)
    {
        if (isAdminMode)
        {
            Console.WriteLine("Admin mode: Payment received - " + amount);
            // Логіка для обробки оплати в режимі адміна
            cashBalance += amount;
        }
        else
        {
            Console.WriteLine("Payment functionality is only available in admin mode");
        }
    }

    // знять гроші(адмінка)
    public void WithdrawCash(decimal amount)
    {
        if (isAdminMode)
        {
            if (amount <= cashBalance)
            {

                Console.WriteLine("Admin mode: Withdrawing cash - " + amount);
                cashBalance -= amount;
                Console.WriteLine("Withdrawal successful");
            }
            else
            {
                Console.WriteLine("Insufficient cash balance");
            }
        }
        else
        {
            Console.WriteLine("Withdrawal functionality is only available in admin mode");
        }
    }
}