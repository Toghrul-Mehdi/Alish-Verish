﻿using Alish_Verish.Context;
using Alish_Verish.Models;
using Alish_Verish.MyException;
using Alish_Verish.UserService;

namespace Alish_Verish
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Users> users = [];
            List<Products> products = [];
            List<Baskets> baskets = [];
            bool f = false;
            string username;
            string password;
            string name;
            string surname;
            int user_id = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("1.Login");
                Console.WriteLine("2.Register");
                Console.WriteLine("0.Exit");
                string choice = Console.ReadLine();
                switch (choice)
                {

                    case "1":
                        try
                        {
                            using (AppDbContext sql = new AppDbContext())
                            {
                                bool username_check = false;
                                do
                                {
                                    Console.WriteLine("-------------------");
                                    Console.WriteLine("Username daxil edin:");
                                    username = Console.ReadLine();
                                    if (username.Length <= 20 && username.Length >= 3)
                                    {
                                        username_check = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Username parametrleri uygun deyil!");
                                        username_check = false;
                                    }
                                } while (!username_check);

                                bool password_check = false;
                                do
                                {
                                    Console.WriteLine("-------------------");
                                    Console.WriteLine("Password daxil edin:");
                                    password = Console.ReadLine();
                                    if (password.Length >= 8)
                                    {
                                        password_check = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Password parametrleri uygun deyil!");
                                        password_check = false;
                                    }
                                } while (!password_check);
                                Console.Clear();

                                users = sql.Users.ToList();
                                bool login_check = false;
                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (username == users[i].Username && password == users[i].Password)
                                    {
                                        user_id = users[i].Id;
                                        login_check = true;
                                    }
                                }
                                if (login_check == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Sisteme Ugurla daxil oldunuz!");
                                    bool f1 = false;
                                    do
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine("1.Mehsullara Bax");
                                        Console.WriteLine("2.Sebete Bax");
                                        Console.WriteLine("3.Hesabdan Cix");
                                        choice = Console.ReadLine();
                                        switch (choice)
                                        {
                                            case "1":
                                                products = sql.Products.ToList();
                                                foreach (var product in products)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine();
                                                    Console.WriteLine($"Product ID : {product.Id}, Product Name : {product.Name}, Product Price : {product.Price}");
                                                    Console.WriteLine();
                                                }
                                                bool f2 = false;
                                                do
                                                {
                                                    Console.WriteLine("1. Mehsul elave et    2. Mehsul sil    0. Exit");
                                                    choice = Console.ReadLine();

                                                    switch (choice)
                                                    {
                                                        case "1":
                                                            Console.ForegroundColor = ConsoleColor.Green;
                                                            bool isProductAdd = false;
                                                            int productAddId = 0;

                                                            do
                                                            {
                                                                try
                                                                {
                                                                    Console.WriteLine("Elave etmek istediyiniz mehsulun ID-sini daxil edin:");
                                                                    if (int.TryParse(Console.ReadLine(), out productAddId))
                                                                    {

                                                                        var product = sql.Products.FirstOrDefault(p => p.Id == productAddId);
                                                                        if (product != null)
                                                                        {
                                                                            isProductAdd = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new ProductNotFoundException("Mehsul tapilmadi! ID-ni duzgun daxil edin.");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FormatException("Zehmet olmasa reqem daxil edin.");
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Console.WriteLine(e.Message);
                                                                }
                                                            } while (!isProductAdd);


                                                            sql.Baskets.Add(new Baskets
                                                            {
                                                                ProductsId = productAddId,
                                                                UsersId = user_id,
                                                            });
                                                            sql.SaveChanges();
                                                            Console.Clear();
                                                            Console.WriteLine("Mehsul ugurla elave olundu!");
                                                            break;

                                                        case "2":
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            bool isProductRemove = false;
                                                            int productRemoveId = 0;
                                                            do
                                                            {
                                                                try
                                                                {
                                                                    Console.WriteLine("Silmek istediyiniz mehsulun ID-sini daxil edin:");
                                                                    if (int.TryParse(Console.ReadLine(), out productRemoveId))
                                                                    {
                                                                        var basketItem = sql.Baskets.FirstOrDefault(b => b.ProductsId == productRemoveId && b.UsersId == user_id);
                                                                        if (basketItem != null)
                                                                        {
                                                                            isProductRemove = true;
                                                                            sql.Baskets.Remove(basketItem);
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new ProductNotFoundException("Sebetde bele bir mehsul tapilmadi! ID-ni duzgun daxil edin.");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new FormatException("Zehmet olmasa reqem daxil edin.");
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Console.WriteLine(e.Message);
                                                                }
                                                            } while (!isProductRemove);

                                                            sql.SaveChanges();
                                                            Console.Clear();
                                                            Console.WriteLine("Mehsul ugurla silindi!");
                                                            break;

                                                        case "0":
                                                            f2 = true;
                                                            break;

                                                        default:
                                                            break;
                                                    }
                                                } while (!f2);

                                                break;


                                            case "2":
                                                decimal total_sebet = 0;
                                                baskets = sql.Baskets.ToList();
                                                Console.WriteLine(user_id + "-ci istifadecinin sebeti:");
                                                Console.WriteLine();
                                                foreach (var basket in baskets)
                                                {
                                                    if (basket.UsersId == user_id)
                                                    {
                                                        var product = sql.Products.FirstOrDefault(p => p.Id == basket.ProductsId);
                                                        if (product != null)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Blue;
                                                            Console.WriteLine($"Product Name: {product.Name}");
                                                            total_sebet += product.Price;
                                                            Console.WriteLine();
                                                        }
                                                    }
                                                }
                                                Console.ForegroundColor= ConsoleColor.Yellow;
                                                Console.WriteLine("Sebetin Umumi Qiymeti: "+total_sebet);
                                                break;
                                            case "3":
                                                f1 = true;
                                                break;
                                            default:
                                                break;
                                        }
                                    } while (!f1);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    throw new UserNotFoundException("Istifadeci adi ve ye sifre yanlisdir!");
                                }
                            }
                        }
                        catch (UserNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "2":
                        using (AppDbContext sql = new AppDbContext())
                        {
                            bool name_check = false;
                            do
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine("Adinizi daxil edin:");
                                name = Console.ReadLine();
                                if (name.Length <= 20 && name.Length >= 3)
                                {
                                    name_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Ad parametrleri uygun deyil!");
                                    name_check = false;
                                }
                            } while (!name_check);

                            bool surname_check = false;
                            do
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine("Soyadinizi daxil edin:");
                                surname = Console.ReadLine();
                                if (surname.Length <= 20 && surname.Length >= 3)
                                {
                                    surname_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Soyad parametrleri uygun deyil!");
                                    surname_check = false;
                                }
                            } while (!surname_check);

                            bool username_check = false;
                            do
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine("Username daxil edin:");
                                username = Console.ReadLine();
                                if (username.Length <= 20 && username.Length >= 3)
                                {
                                    username_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Username parametrleri uygun deyil!");
                                    username_check = false;
                                }
                            } while (!username_check);

                            bool password_check = false;
                            do
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine("Password daxil edin:");
                                password = Console.ReadLine();
                                if (password.Length >= 8)
                                {
                                    password_check = true;
                                }
                                else
                                {
                                    Console.WriteLine("Password parametrleri uygun deyil!");
                                    password_check = false;
                                }
                            } while (!password_check);
                            Console.Clear();
                            bool check_username = true;
                            users = sql.Users.ToList();
                            for (int i = 0; i < users.Count; i++)
                            {
                                if (users[i].Username == username)
                                {
                                    check_username = false;
                                }
                            }
                            if (check_username)
                            {
                                sql.Users.Add(new Users
                                {
                                    Name = name,
                                    Surname = surname,
                                    Username = username,
                                    Password = password,
                                });
                                sql.SaveChanges();
                                Console.WriteLine("Successfully Created!");
                            }
                            else
                            {
                                Console.WriteLine("Username sistemde movcuddur!");
                            }
                        }
                        break;
                    case "0":
                        return;
                    default:
                        break;
                }
            } while (!f);

        }

    }
}
