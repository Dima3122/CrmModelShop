using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random rnd = new Random();
        public List<CashDesk> cashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sell { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        public int Count => Sellers.Count;
        public ShopComputerModel()
        {
            var sellers = Generator.GetSellers(30);
            Generator.GetProducts(1000);
            Generator.GetCustomers(100);
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            for (int i = 0; i < 3; i++)
            {
                cashDesks.Add(new CashDesk(i, Sellers.Dequeue()));
            }
        }
        public void Start()
        {
            var customers = Generator.GetCustomers(10);
            var cards = new Queue<Cart>();
            foreach (var customer in customers)
            {
                var card = new Cart(customer);
                foreach (var prod in Generator.GetRandomProduct(10,30))
                {
                    card.Add(prod);
                }
                cards.Enqueue(card);
            }
            while (cards.Count > 0)
            {
                var cash = cashDesks[rnd.Next(cashDesks.Count - 1)];
                cash.AddQueue(cards.Dequeue());
            }         
            while (true)
            {
                var cash = cashDesks[rnd.Next(cashDesks.Count - 1)];
                cash.Dequeue();
            }
        }
    }
}
