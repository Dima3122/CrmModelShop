using CrmBl.Model;
using System;
using System.Windows.Forms;

namespace CrmUi
{
    class CashBoxView
    {
        private CashDesk cashDesk;
        public Label CashDeskName { get; set; }
        public NumericUpDown Price { get; set; }
        public ProgressBar QueueLenght { get; set; }
        public Label Leave { get; set; }
        public CashBoxView(CashDesk cashDesk,int number ,int x, int y)
        {
            this.cashDesk = cashDesk;
            QueueLenght = new ProgressBar();
            CashDeskName = new Label();
            Price = new NumericUpDown();
            Leave = new Label();
            //----------------------------
            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y);
            CashDeskName.Name = cashDesk.ToString();
            CashDeskName.Size = new System.Drawing.Size(35, 22);
            CashDeskName.TabIndex = 1;
            CashDeskName.Text = cashDesk.ToString();
            // 
            // numericUpDown1
            // 
            Price.Location = new System.Drawing.Point(x + 70, y);
            Price.Name = "numericUpDown" + number;
            Price.Size = new System.Drawing.Size(120, 22);
            Price.TabIndex = number;
            Price.Maximum = 100000000000000;
            cashDesk.CheckClosed += CashDesk_CheckClosed;
            //Progress BAr
            QueueLenght.Location = new System.Drawing.Point(x + 300, y);
            QueueLenght.Maximum = cashDesk.MaxQueueLenght;
            QueueLenght.Name = "progressBar" + number;
            QueueLenght.Size = new System.Drawing.Size(91, 23);
            QueueLenght.TabIndex = 1;
            QueueLenght.Value = 1;
            //---------------------------
            Leave.AutoSize = true;
            Leave.Location = new System.Drawing.Point(x + 200, y);
            Leave.Name = cashDesk.ToString();
            Leave.Size = new System.Drawing.Size(35, 22);
            Leave.TabIndex = 1;
            Leave.Text = cashDesk.ToString();
        }
        private void CashDesk_CheckClosed(object sender, Check e)
        {
            Price.Invoke((Action)delegate 
            {
                Price.Value += e.Price;
                QueueLenght.Value = cashDesk.Count;
                Leave.Text = cashDesk.ExitCustomer.ToString();
            });
        }
    }
}