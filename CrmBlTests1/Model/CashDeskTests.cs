using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            var customer1 = new Customer()
            {
                Name = "testuser1",
                CustomerId = 1
            };
            var customer2 = new Customer()
            {
                Name = "testuser2",
                CustomerId = 1
            };
            var seller = new Seller()
            {
                Name = "testseller",
                SellerId = 1
            };
            var product1 = new Product()
            {
                Name = "pr1",
                Price = 100,
                Count = 10
            };
            var product2 = new Product()
            {
                Name = "pr2",
                Price = 200,
                Count = 20
            };
            var cart1 = new Cart(customer1);
            var cart2 = new Cart(customer2);
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product2);
            cart2.Add(product1);
            cart2.Add(product1);
            cart2.Add(product1);
            cart2.Add(product2);
            var cashdesk = new CashDesk(1, seller);
            cashdesk.AddQueue(cart1);
            cashdesk.AddQueue(cart2);    

            var CartResult = 400;

            var cartactualresult = cashdesk.Dequeue();
            Assert.AreEqual(CartResult, cartactualresult);
            cartactualresult = cashdesk.Dequeue();
            Assert.AreEqual(CartResult, cartactualresult);
        }     
    }
}