using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsSupport
{
    public partial class Form1 : Form
    {
        DCDataContext _db;
        public Form1()
        {
            InitializeComponent();
            _db = new DCDataContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var q = _db.Tbl_SmsReceiveds.GroupBy(a => a.Phone).Select(b => b.Key);
            if (q.Any())
            {
                listBox1.DataSource = q;
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                MessageBox.Show(listBox2.SelectedItem.ToString());
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string currentPhone = listBox1.SelectedItem.ToString();
                var q = _db.Tbl_SmsReceiveds.Where(a => a.Phone.Equals(currentPhone)).Select(b => b.Message);

                listBox2.DataSource = q;
            }
        }
    }
}
