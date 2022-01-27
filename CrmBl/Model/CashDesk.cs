using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class CashDesk
    {
        CrmContext db = new CrmContext();
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int Number { get; set; }
        public int MaxQueueLenght { get; set; }
        public int ExitCustomer { get; set; }
        public bool IsModel { get; set; }
        public CashDesk(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLenght = 5;
        }
        public void AddQueue(Cart cart)
        {
            if (Queue.Count <= MaxQueueLenght )
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }
        public decimal Dequeue()
        {
            decimal sum = 0;
            if (Queue.Count == 0)
            {
                return 0;
            }
            var card = Queue.Dequeue();
            if (card != null)
            {
                var check = new Check()
                {
                    SellerId = Seller.SellerId,
                    Seller = Seller,
                    CustomerId = card.customer.CustomerId,
                    Customer = card.customer,
                    Created = DateTime.Now,
                };
                if (!IsModel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.CheckId = 0;
                }
                var sells = new List<Sell>();
                foreach (Product product in card)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };
                        sells.Add(sell);
                        product.Count--;
                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                        }
                        sum += product.Price;
                    }
                }
                if (!IsModel)
                {  
                    db.SaveChanges();
                }
            }
            return sum;
        }
    }
}