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
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            var customer = new Customer()
            {
                Name = "test"
            };
            var product1 = new Product()
            {
                Name = "pr1",
                Price = 200,
                Count = 10
            };
            var product2 = new Product()
            {
                Name = "pr2",
                Price = 100,
                Count = 20
            };
            var cart = new Cart(customer);
            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product2);
            var cartResult = cart.GetAll();
            var expectedresult = new List<Product>()
            { 
                product1, product1, product1, product2
            };
            Assert.AreEqual(expectedresult.Count, cart.GetAll().Count);
            for (int i = 0; i < expectedresult.Count; i++)
            {
                Assert.AreEqual(expectedresult[i], cartResult[i]);
            }
        }     
    }
}