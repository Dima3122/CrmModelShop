using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class Cart : IEnumerable
    {
        public List<Product> GetAll()
        {
            var result = new List<Product>();
            foreach (Product item in this)
            {
                result.Add(item);
            }
            return result;
        }
        public Customer customer { get; set; }
        public Dictionary<Product, int> products { get; set; }
        public decimal Price => GetAll().Sum(p => p.Price);
        public Cart(Customer customer)
        {
            this.customer = customer;
            products = new Dictionary<Product, int>();
        }
        public void Add(Product product)
        {
            if (products.TryGetValue(product, out int count))
            {
                products[product] = ++count;
            }
            else
            {
                products.Add(product, 1);
            }
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var product in products.Keys)
            {
                for (int i = 0; i < products[product]; i++)
                {
                    yield return product;
                }
            }                   
        }
    }
}
