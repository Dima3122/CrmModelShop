using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random rnd = new Random();
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sell { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        public int CustomerSpeed { get; set; } = 100;
        public int CashDeskSpeed { get; set; } = 100;
        public int Count => Sellers.Count;
        private bool isWorking = false;
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
                CashDesks.Add(new CashDesk(i, Sellers.Dequeue(), null));
            }
        }
        private void CashDeskWork(CashDesk cashDesk, int sleep)
        {
            while (isWorking)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(sleep);
                }
            }
        }
        public void Start()
        {
            isWorking = true;
            Task.Run(()=> CreateCarts(10, CustomerSpeed));
            var cashDeskTasks = CashDesks.Select(c => new Task(() => CashDeskWork(c, CashDeskSpeed)));
            foreach (var task in cashDeskTasks)
            {
                task.Start();
            }
        }
        public void Stop()
        {
            isWorking = false;
        }
        private void CreateCarts(int customerCounts, int slepp)
        {
            while (isWorking)
            {
                var customers = Generator.GetCustomers(customerCounts);
                var cards = new Queue<Cart>();
                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);
                    foreach (var product in Generator.GetRandomProduct(10,30))
                    {
                        cart.Add(product);
                    }
                    var cash = CashDesks[rnd.Next(CashDesks.Count)];
                    cash.AddQueue(cart);
                }
                Thread.Sleep(slepp);
            }
        }
    }
}
