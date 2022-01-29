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

namespace CrmBl
{
    public partial class login : Form
    {
        public Customer Customer { get; set; } 
        public login()
        {
            InitializeComponent();   
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Customer = new Customer()
            {
                Name = textBox1.Text
            };
            DialogResult = DialogResult.OK;
        }
    }
}
