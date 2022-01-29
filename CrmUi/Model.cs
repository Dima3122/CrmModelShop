using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class Model : Form
    {
        ShopComputerModel model = new ShopComputerModel();
        public Model()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var cashBoxes = new List<CashBoxView>();
            for (int i = 0; i < model.CashDesks.Count; i++)
            {
                var box = new CashBoxView(model.CashDesks[i], i, 10, 26 * i);
                cashBoxes.Add(box);
                Controls.Add(box.CashDeskName);
                Controls.Add(box.Price);
                Controls.Add(box.QueueLenght);
                Controls.Add(box.Leave);
            }
            model.Start();
        }
        private void Model_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.Stop();
        }
        private void Model_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = model.CustomerSpeed;
            numericUpDown2.Value = model.CashDeskSpeed;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.CashDeskSpeed = (int)numericUpDown2.Value;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.CustomerSpeed = (int)numericUpDown1.Value;
        }
    }
}