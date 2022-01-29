using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ProductForm : Form
    {
        public Product Product { get; set; }

        public ProductForm(Product product) : this() 
        {
            Product = product;
            textBox1.Text = product.Name;
            numericUpDown1.Value = product.Price;
            numericUpDown2.Value = product.Count;
        }
        public ProductForm()
        {
            InitializeComponent();
        }
        private void CustomerForm_Load(object sender, EventArgs e) {}
        private void button1_Click(object sender, EventArgs e)
        {
            Product = Product ?? new Product();
            Product.Name = textBox1.Text;
            Product.Count = Convert.ToInt32(numericUpDown2.Value);
            Product.Price = numericUpDown1.Value;           
            Close();
        }
    }
}
