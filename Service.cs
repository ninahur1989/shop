using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magaz
{
    internal class Service
    {
        private Shop _shop = new Shop();
        public void Add(int sum, List<Item> list, List<Item> cart)
        {
            if (_shop.Check())
            {
                return;
            }

            if (sum == 10)
            {
                Console.WriteLine("\n  remove item if you want to continue or confirm your order");
                return;
            }

            _shop.ShowItemes(list, "current katalog include: ");
            Console.WriteLine("Chose item which you want to buy");

            string name = Console.ReadLine();

            Item item = list.Find(item => item.Name == name);

            if (item == default)
            {
                Console.WriteLine("not found this item in list");
                return;
            }

            Console.Clear();
            item.Show(item.Name);

            int amountOfItem;
            while (true)
            {
                Console.WriteLine($"how many {name} do you need?");
                if (!int.TryParse(Console.ReadLine(), out amountOfItem))
                {
                    Console.WriteLine("it isnt number!!");
                    continue;
                }

                if (amountOfItem > 0 && amountOfItem <= item.Amount)
                {
                    if (!cart.Contains(item))
                    {
                        cart.Add(item);
                    }

                    for (int i = 0; i < amountOfItem; amountOfItem--, item.Amount--)
                    {
                        item.CardAmount++;
                        sum++;
                    }

                    if (item.Amount == 0)
                    {
                        list.Remove(item);
                        break;
                    }

                    break;
                }
            }
        }

        public void Remove(int sum, List<Item> list, List<Item> cart)
        {
            Console.Clear();
            _shop.ShowItemes(cart, " what do you want to remove from your Shopping basket : ");
            string name = Console.ReadLine();
            Item item = cart.Find(item => item.Name == name);

            if (item == default)
            {
                Console.WriteLine("not found this item in list");
                return;
            }

            item.Show(item.Name);

            int amountOfItem;

            while (true)
            {
                Console.WriteLine($"how many {name} do you need?");
                if (!int.TryParse(Console.ReadLine(), out amountOfItem))
                {
                    Console.WriteLine("it isnt number!!");
                    continue;
                }

                if (amountOfItem > 0 && amountOfItem <= item.CardAmount)
                {
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }

                    for (int i = 0; i < amountOfItem; amountOfItem--, item.CardAmount--)
                    {
                        item.Amount++;
                        sum--;
                    }

                    if (item.CardAmount == 0)
                    {
                        cart.Remove(item);
                        break;
                    }

                    break;
                }
            }
        }

        public void Confirm(int sum, int numofOrder, List<Item> list, List<Item> cart)
        {
            if (_shop.Check())
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("Your order is confirmed");
            string msg = " ";
            foreach (Item item in cart)
            {
                Console.WriteLine($"{item.Name} in you card in amount {item.CardAmount}  ");
            }

            foreach (Item it in cart)
            {
                it.CardAmount = 0;
                msg += it + "\n";
            }

            sum = 0;
            cart.Clear();

            Console.WriteLine("num od your order is: " + numofOrder + " \n");
            numofOrder++;

            Console.WriteLine("Press any button if you want to continue");
            Console.ReadKey();
        }
    }
}
