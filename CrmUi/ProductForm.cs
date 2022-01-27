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
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var p = Product ?? new Product();
            p.Name = textBox1.Text;
            p.Count = Convert.ToInt32(numericUpDown2.Value);
            p.Price = numericUpDown1.Value;
            Product = p;
            Close();
        }
    }
}
