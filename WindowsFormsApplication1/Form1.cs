using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kursDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDataSet1.t1". При необходимости она может быть перемещена или удалена.
            this.t1TableAdapter.Fill(this.kursDataSet2.t1);

            string Text;
         
            // отримати дані рядка
            Text = (string)dataGridView1.Rows[0].Cells[2].Value;

            // заповнити поля
            textBox3.Text = Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Wname;

            Wname = textBox1.Text;
            if (textBox3.Text != "")
            {
                this.t1TableAdapter.Insert(Wname, textBox3.Text);
                this.t1TableAdapter.Fill(this.kursDataSet2.t1);

                textBox1.Text = "";
            }
            else {
                string massege = "Заповніть інформацію про місто!";

                DialogResult dialogResult = MessageBox.Show(massege, "Помилка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index;
            string WName;
            string Text;

            if (dataGridView1.RowCount <= 0) return;
            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;

            // отримати дані рядка
            WName = (string)dataGridView1.Rows[index].Cells[1].Value;
            Text = (string)dataGridView1.Rows[index].Cells[2].Value;
            

            // заповнити поля
            textBox1.Text = WName;
            textBox2.Enabled = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            dataGridView1.Enabled = false;
            bindingNavigator2.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id_worker;
            string WName;
            int index;

            // взяти номер поточного (виділеного) рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;

            // заповнити внутрішні змінні з поточного рядка dataGridView1
            id_worker = Convert.ToInt32(dataGridView1[0, index].Value);
            WName = Convert.ToString(dataGridView1[1, index].Value);

            // сформувати інформаційний рядок

            string massege = "Дійсно видалити місто "+ WName + " ?";

            DialogResult dialogResult = MessageBox.Show(massege, "Видалити", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.t1TableAdapter.Delete(id_worker, WName); // метод Delete
                this.t1TableAdapter.Fill(this.kursDataSet2.t1);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index;
            string WName;

            int id_worker;

            if (dataGridView1.RowCount <= 0) return;

            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;

          //  if (index == dataGridView1.RowCount - 1) return; //

            // отримати дані рядка
            id_worker = (int)dataGridView1.Rows[index].Cells[0].Value;
            WName = (string)dataGridView1.Rows[index].Cells[1].Value;


            string nWName;

            // отримати нові (змінені) значення з форми
            nWName = textBox1.Text;

            string massege = "Дійсно змінити місто " + WName + " на "+ nWName +" ?";

            DialogResult dialogResult = MessageBox.Show(massege, "Змінит", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (textBox3.Text != "")
                {
                    // змінити в адаптері
                    this.t1TableAdapter.Update(nWName, textBox3.Text, id_worker, WName);
                    this.t1TableAdapter.Fill(this.kursDataSet2.t1);
                    textBox1.Text = "";
                    textBox2.Enabled = true;
                    dataGridView1.Enabled = true;
                    bindingNavigator2.Enabled = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = false;
                    button5.Visible = false;
                }
                else
                {
                    string massege2 = "Заповніть інформацію про місто!";

                    DialogResult dialogResult1 = MessageBox.Show(massege2, "Помилка", MessageBoxButtons.OK);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Enabled = true;
            dataGridView1.Enabled = true;
            bindingNavigator2.Enabled = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))
                            {
                                dataGridView1.FirstDisplayedScrollingRowIndex = i;
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                }
            }
            else
            {
                if (dataGridView1.DataSource == t1BindingSource)
                {
                    t1BindingSource.Filter = " [names] LIKE'" + textBox2.Text + "%'";
                    //где Искать - название столбца в DatagridView
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int index;
            if (dataGridView1.RowCount <= 0) return;
            index = dataGridView1.CurrentRow.Index;
            string Text;
            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;
            // отримати дані рядка
            Text = (string)dataGridView1.Rows[index].Cells[2].Value;
            // заповнити поля
            textBox3.Text = Text;
        }

        private void bindingNavigatorMoveNextItem1_Click(object sender, EventArgs e)
        {
            int index;
            if (dataGridView1.RowCount <= 0) return;
            index = dataGridView1.CurrentRow.Index;
            string Text;
            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;
            // отримати дані рядка
            Text = (string)dataGridView1.Rows[index].Cells[2].Value;
            // заповнити поля
            textBox3.Text = Text;

        }

        private void bindingNavigatorMovePreviousItem1_Click(object sender, EventArgs e)
        {
            int index;
            if (dataGridView1.RowCount <= 0) return;
            index = dataGridView1.CurrentRow.Index;
            string Text;
            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;
            // отримати дані рядка
            Text = (string)dataGridView1.Rows[index].Cells[2].Value;
            // заповнити поля
            textBox3.Text = Text;
        }

        private void bindingNavigatorMoveLastItem1_Click(object sender, EventArgs e)
        {
            int index;
            if (dataGridView1.RowCount <= 0) return;
            index = dataGridView1.CurrentRow.Index;
            string Text;
            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;
            // отримати дані рядка
            Text = (string)dataGridView1.Rows[index].Cells[2].Value;
            // заповнити поля
            textBox3.Text = Text;
        }

        private void bindingNavigatorMoveFirstItem1_Click(object sender, EventArgs e)
        {
            int index;
            if (dataGridView1.RowCount <= 0) return;
            index = dataGridView1.CurrentRow.Index;
            string Text;
            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;
            // отримати дані рядка
            Text = (string)dataGridView1.Rows[index].Cells[2].Value;
            // заповнити поля
            textBox3.Text = Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
