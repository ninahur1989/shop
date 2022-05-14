using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Magaz
{
    internal class Shop : Service
    {
        private List<Item> _list = new List<Item>();
        private List<Item> _cart = new List<Item>();
        private int _sum = 0;
        private int _numofOrder = 1;
        private Service _service = new Service();
        public Shop()
        {
            Random random = new Random();

            List<string> name = new List<string>() { "phone", "cake", "car", "balloon", "radio", "piano", "fork", "chocolate", "fridge", "pillow", "bottle", "piano", "bananas", "candle" };
            for (int y = 0; y < name.Count; y++)
            {
                _list.Add(new Item(name[y], y, random.Next(10, 10000), random.Next(1, 11), 0));
            }
        }

        public void Starter()
        {
            bool program = true;

            while (program)
            {
                Console.WriteLine("Write one of case below");
                Console.WriteLine("Write what do ( add / remove / confirm)");
                string choise = Console.ReadLine();

                switch (choise.ToLower())
                {
                    case "add":
                        _service.Add(_sum, _list, _cart);
                        break;
                    case "remove":
                        _service.Remove(_sum, _list, _cart);
                        break;
                    case "confirm":
                        _service.Confirm(_sum, _numofOrder, _list, _cart);
                        break;
                }
            }
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
