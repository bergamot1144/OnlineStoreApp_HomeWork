namespace OnlineStoreApp_HomeWork
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Console.WriteLine("Hello, World!");


            DataBaseManager dbManager = new DataBaseManager();
            if (!dbManager.TestConnection())
            {
                Console.WriteLine("No connection");
                return;
            }
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Add Category");
                Console.WriteLine("3. Connect product to category");
                Console.WriteLine("4. Show All Products");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите название продукта: ");
                        string? name = Console.ReadLine();

                        Console.Write("Введите описание продукта: ");
                        string? description = Console.ReadLine();

                        Console.Write("Введите цену продукта: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод цены!\n");
                            break;
                        }

                        Console.Write("Введите количество на складе: ");
                        if (!int.TryParse(Console.ReadLine(), out int stock))
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод количества!\n");
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
                        {
                            Console.WriteLine("Ошибка: Название и описание не могут быть пустыми!\n");
                            break;
                        }

                        dbManager.AddProduct(name, description, price, stock);
                        Console.WriteLine("Продукт добавлен!\n");
                        break;

                    

                    case "2":
                        Console.Write("Введите название категории: ");
                        string? categoryName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(categoryName))
                        {
                            Console.WriteLine("Ошибка: Название категории не может быть пустым!\n");
                            break;
                        }

                        dbManager.AddCategory(categoryName);
                        Console.WriteLine("Категория добавлена!\n");
                        break;

                    case "3":
                        Console.Write("Введите ID продукта: ");
                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод ID продукта!\n");
                            break;
                        }

                        Console.Write("Введите ID категории: ");
                        if (!int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод ID категории!\n");
                            break;
                        }

                        dbManager.LinkProductToCategory(productId, categoryId);
                        Console.WriteLine("Продукт привязан к категории!\n");
                        break;
                    case "4":
                        dbManager.GetAllProducts();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Wrong number. Try again.\n");
                        break;
                }
            }
        }
    }
}