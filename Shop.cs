using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Magaz
{
    internal class Shop
    {
        private List<Item> _list = new List<Item>();
        private List<Item> _cart = new List<Item>();
        private int _sum = 0;
        private int _numofOrder = 1;
        public Shop()
        {
            Random random = new Random();

            List<string> name = new List<string>() { "phone", "cake", "car", "balloon", "radio", "piano", "fork", "chocolate", "fridge", "pillow", "bottle", "piano", "bananas", "candle" };
            for (int y = 0; y < name.Count; y++)
            {
                _list.Add(new Item(name[y], y, random.Next(10, 10000), random.Next(1, 11), 0));
            }
        }

        public void Add()
        {
            if (Check())
            {
                return;
            }

            if (_sum == 10)
            {
                Console.WriteLine("\n  remove item if you want to continue or confirm your order");
                return;
            }

            ShowItemes(_list, "current katalog include: ");
            Console.WriteLine("Chose item which you want to buy");

            string name = Console.ReadLine();

            Item item = _list.Find(item => item.Name == name);

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
                    if (!_cart.Contains(item))
                    {
                        _cart.Add(item);
                    }

                    for (int i = 0; i < amountOfItem; amountOfItem--, item.Amount--)
                    {
                        item.CardAmount++;
                        _sum++;
                    }

                    if (item.Amount == 0)
                    {
                        _list.Remove(item);
                        break;
                    }

                    break;
                }
            }
        }

        public void Remove()
        {
            Console.Clear();
            ShowItemes(_cart, " what do you want to remove from your Shopping basket : ");
            string name = Console.ReadLine();
            Item item = _cart.Find(item => item.Name == name);

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
                    if (!_list.Contains(item))
                    {
                        _list.Add(item);
                    }

                    for (int i = 0; i < amountOfItem; amountOfItem--, item.CardAmount--)
                    {
                        item.Amount++;
                        _sum--;
                    }

                    if (item.CardAmount == 0)
                    {
                        _cart.Remove(item);
                        break;
                    }

                    break;
                }
            }
        }

        public void Confirm()
        {
            if (Check())
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("Your order is confirmed");
            string msg = " ";
            foreach (Item item in _cart)
            {
                Console.WriteLine($"{item.Name} in you card in amount {item.CardAmount}  ");
            }

            foreach (Item it in _cart)
            {
                it.CardAmount = 0;
                msg += it + "\n";
            }

            _sum = 0;
            _cart.Clear();

            Console.WriteLine("num od your order is: " + _numofOrder + " \n");
            _numofOrder++;

            Console.WriteLine("Press any button if you want to continue");
            Console.ReadKey();
        }

        public void ShowItemes(List<Item> list, string b)
        {
            Console.WriteLine(b);

            foreach (Item it in list)
            {
                Console.WriteLine(it.Name);
            }

            Console.WriteLine("------------------------------------------------");
        }

        public bool Check()
        {
            if (_sum > 10)
            {
                Console.WriteLine("Your Shopping basket is overloaded\n");
                foreach (Item d in _cart)
                {
                    Console.WriteLine($"{d.Name} in you card in amount {d.CardAmount}  ");
                }

                Console.WriteLine($"\n you have to remove {_sum - 10} items from your card ");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
