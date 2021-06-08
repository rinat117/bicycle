using bicycle.ModelBD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bicycle
{
    public partial class Form1 : Form
    {
        ModelBD.Model1 connect = new ModelBD.Model1();
        public Form1()
        {
            InitializeComponent();
            connect.BEST.Load();
            dataGridView1.DataSource = connect.BEST.Local.ToBindingList();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            DialogResult result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ModelBD.BEST cliadd = new BEST();
                cliadd.Company = form.textBox1.Text;
                cliadd.Cost = form.textBox3.Text;
                cliadd.Type = form.comboBox1.Text;
                connect.BEST.Add(cliadd);
                connect.SaveChanges();
                MessageBox.Show("Запись добавлена");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 formedit = new Form2();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);

                BEST Clientedit = connect.BEST.Find(id);

                formedit.textBox1.Text = Clientedit.Company;
                formedit.textBox3.Text = Clientedit.Cost;
                formedit.comboBox1.Text = Clientedit.Type;

                DialogResult resultedit = formedit.ShowDialog(this);
                if (resultedit == DialogResult.OK)
                {
                    Clientedit.Company = formedit.textBox1.Text;
                    Clientedit.Cost = formedit.textBox3.Text;
                    Clientedit.Type = formedit.comboBox1.SelectedItem.ToString();

                    connect.SaveChanges();
                    MessageBox.Show("Запись обновлена");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == true)
                {
                    BEST Clientdel = connect.BEST.Find(id);
                    connect.BEST.Remove(Clientdel);
                    connect.SaveChanges();
                    string buff = Clientdel.Company;
                    MessageBox.Show("запись " + buff + " удалена");
                }
            }
            else
            {
                MessageBox.Show("Запись не выбрана!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {

                double Mxi = 0;
                int sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    Mxi += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                }
                textBox1.Text = Convert.ToString(Mxi);
                MessageBox.Show(" Спасибо за выбор нашего магазина");
            }
        }
        internal class textBox4
        {
            public static string Text { get; internal set; }
        }
    }
}