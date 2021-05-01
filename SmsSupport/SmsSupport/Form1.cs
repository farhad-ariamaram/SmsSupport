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
                foreach (var item in q.ToList())
                {

                    var qu = _db.Tbl_SmsReceiveds.Where(a => a.Phone.Equals(item)).Where(a=>a.IsVisit==false || a.IsVisit == null);
                    if (qu.Any())
                    {
                        listView1.Items.Add(item);
                        listView1.Items[listView1.Items.Count - 1].ForeColor = Color.Blue;
                    }
                    else
                    {
                        listView1.Items.Add(item);
                    }

                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string currentPhone = listView1.SelectedItems[0].Text;
                var q = _db.Tbl_SmsReceiveds.Where(a => a.Phone.Equals(currentPhone));
                listView2.Clear();
                foreach (var item in q.ToList())
                {
                    listView2.Items.Add(item.Message);
                    if(item.IsVisit == false || item.IsVisit==null)
                    {
                        item.IsVisit = true;
                    }
                }
                _db.SubmitChanges();
                listView1.SelectedItems[0].ForeColor = Color.Black;
            }
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                MessageBox.Show(listView2.SelectedItems[0].Text);
            }
        }
    }

}
