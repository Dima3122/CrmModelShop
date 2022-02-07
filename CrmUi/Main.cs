using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBl;
using CrmBl.Model;

namespace CrmUi
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart Cart;
        Customer Customer;
        CashDesk cashDesk;
        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
            Cart = new Cart(Customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(),db)
            {
                IsModel = false
            };
        }
        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products, db);
            catalogProduct.Show();
        }
        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers, db);
            catalogSeller.Show();
        }
        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers, db);
            catalogCustomer.Show();
        }
        private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkCustomer = new Catalog<Check>(db.Checks, db);
            checkCustomer.Show();
        }
        private void CustomerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }
        private void sellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }
        }

        private void productAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var modelform = new Model();
            modelform.Show();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            listBox1.Invoke((Action)delegate
            {
                listBox1.Items.AddRange(db.Products.ToArray());
                listBox2.Items.AddRange(Cart.GetAll().ToArray());
            });
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product product)
            {
                Cart.Add(product);
                listBox2.Items.Add(product);
            }
            Update_lists();
        }
        private void Update_lists()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(Cart.GetAll().ToArray());
            label1.Text = "Итого: " + Cart.Price;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            var form = new login();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var tempCustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if (tempCustomer != null)
                {
                    Customer = tempCustomer;
                    Cart.customer = Customer;
                }
                else
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    Customer = form.Customer;
                    Cart.customer = form.Customer;
                }
            }
            label2.Text = $"Здравствуй, {Customer.Name}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Customer != null)
            {
                cashDesk.AddQueue(Cart);
                cashDesk.Dequeue();
                listBox2.Items.Clear();
                label1.Text = "Итого: " + 0;
                MessageBox.Show("Покупка выполнена успешно");
            }
            else
            {
                MessageBox.Show("Авторизуйся!!!");
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product product)
            {
                Cart.Remove(product);
                listBox2.Items.Remove(product);
            }
            Update_lists();
        }
    }
}