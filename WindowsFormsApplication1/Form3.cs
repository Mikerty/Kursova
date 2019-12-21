using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDataSet2.t1". При необходимости она может быть перемещена или удалена.
            this.userTableAdapter1.Fill(this.kursDataSet2.user);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = comboBox1.SelectedIndex;
            if (x == -1)
            {
                label4.Visible = true;
                label4.Text = "Не вибраний рівень доступу";
            }
            else
            {
                
                if (dataGridView1.DataSource == userBindingSource)
                {
                    userBindingSource.Filter = " [login] LIKE'" + textBox1.Text + "%'";
                    //где Искать - название столбца в DatagridView
                }

                string logins;
                string login = textBox1.Text;
                bool isn = false;

                int index = dataGridView1.Rows.Count;

                for (int i = 0; i < index; i++)
                {

                    logins = (string)dataGridView1.Rows[i].Cells[1].Value;

                    if (login == logins)
                    {
                        isn = true;
                        break;
                    }
                }
               
                if (isn == false)
                {
                    
                    string Wlogin, Wpassword;
                    int Wdostup;

                    Wlogin = textBox1.Text;
                    Wpassword = textBox2.Text;
                    Wdostup = comboBox1.SelectedIndex + 1;
                    this.userTableAdapter1.Insert(Wlogin, Wpassword, Wdostup);
                    this.userTableAdapter1.Fill(this.kursDataSet2.user);

                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.SelectedIndex = -1;
                    label4.Visible = false;

                }
                else
                {
                    label4.Visible = true;
                    label4.Text = "Користувач з таким логіном вже існує";
                }


            }
            this.userTableAdapter1.Fill(this.kursDataSet2.user);

            if (dataGridView1.DataSource == userBindingSource)
            {
                userBindingSource.Filter = " [login] LIKE'" + textBox1.Text + "%'";
                //где Искать - название столбца в DatagridView
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index;
            string Wlogin, Wpassword;
            int Wdostup;

            if (dataGridView1.RowCount <= 0) return;
            
                // отримати позицію виділеного рядка в dataGridView1
                index = dataGridView1.CurrentRow.Index;
            

            // отримати дані рядка
            Wlogin = (string)dataGridView1.Rows[index].Cells[1].Value;
            Wpassword = (string)dataGridView1.Rows[index].Cells[2].Value;
            Wdostup = (int)dataGridView1.Rows[index].Cells[3].Value;

            // заповнити поля
            textBox1.Text = Wlogin;
            textBox2.Text = Wpassword;
            comboBox1.SelectedIndex = Wdostup - 1;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            dataGridView1.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id_worker;
            string Wlogin, Wpassword;
            int Wdostup;
            int index;

            if (dataGridView1.RowCount <= 0) return;
            // взяти номер поточного (виділеного) рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;

            // заповнити внутрішні змінні з поточного рядка dataGridView1
            id_worker = Convert.ToInt32(dataGridView1[0, index].Value);
            Wlogin    = Convert.ToString(dataGridView1[1, index].Value);
            Wpassword = Convert.ToString(dataGridView1[2, index].Value);
            Wdostup   = Convert.ToInt16(dataGridView1[3, index].Value);

            // сформувати інформаційний рядок

            string massege = "Дійсно видалити користувача " + Wlogin + " ?";

            DialogResult dialogResult = MessageBox.Show(massege, "Видалити", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.userTableAdapter1.Delete(id_worker, Wlogin, Wpassword , Wdostup); // метод Delete
                this.userTableAdapter1.Fill(this.kursDataSet2.user);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int    index;
            string Wlogin, Wpassword;
            int    Wdostup;
            int    id_worker;

            if (dataGridView1.RowCount <= 0) return;

            // отримати позицію виділеного рядка в dataGridView1
            index = dataGridView1.CurrentRow.Index;

          //  if (index == dataGridView1.RowCount - 1) return; //

            

            // отримати дані рядка
            id_worker = (int)    dataGridView1.Rows[index].Cells[0].Value;
            Wlogin    = (string) dataGridView1.Rows[index].Cells[1].Value;
            Wpassword = (string) dataGridView1.Rows[index].Cells[2].Value;
            Wdostup   = (int)    dataGridView1.Rows[index].Cells[3].Value;

            string nWlogin, nWpassword;
            int    nWdostup;

            // отримати нові (змінені) значення з форми
            nWlogin    = textBox1.Text;
            nWpassword = textBox2.Text;
            nWdostup   = comboBox1.SelectedIndex + 1;

            string massege = "Дійсно зробити змінити користувача " + Wlogin + " ?";

            DialogResult dialogResult = MessageBox.Show(massege, "Змінит", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // змінити в адаптері
                this.userTableAdapter1.Update(nWlogin, nWpassword, nWdostup, id_worker, Wlogin, Wpassword, Wdostup);
                this.userTableAdapter1.Fill(this.kursDataSet2.user);
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedIndex = -1;
                dataGridView1.Enabled = true;
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
                button5.Visible = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
            dataGridView1.Enabled = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
        }

       
    }
}
