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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDataSet2.user". При необходимости она может быть перемещена или удалена.
            this.userTableAdapter.Fill(this.kursDataSet2.user);

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="1111" & textBox2.Text=="1111" )
            {

                label3.Visible = true;
                label3.Text = "Правельні дані.";

                Form1 form1 = new Form1();
                form1.button7.Visible = true;
                form1.groupBox1.Visible = true;
                form1.textBox3.Enabled = true;
                form1.FormClosed += formClosed;
                form1.Show();

                this.Hide();
                cle();

                return;
            }


            if (dataGridView1.DataSource == userBindingSource)
            {
                userBindingSource.Filter = " [login] LIKE'" + textBox1.Text + "%'";
                //где Искать - название столбца в DatagridView
            }

            string login, password;
            int dostup = 0;
            string passwords = textBox2.Text;

            int index = dataGridView1.Rows.Count;

            for (int i = 0; i < index; i++)
            {

                login = (string)dataGridView1.Rows[i].Cells[1].Value;
                password = (string)dataGridView1.Rows[i].Cells[2].Value;

                if (password == passwords)
                {
                    dostup = (int)dataGridView1.Rows[i].Cells[3].Value;
                    break;
                }

            }
            switch (dostup)
            {
                case 0:
                    label3.Visible = true;
                    label3.Text = "Помилкові дані. Не правильний логін або пароль!";
                    break;
                case 1:
                    label3.Visible = true;
                    label3.Text = "Правельні дані.";

                    Form1 form1 = new Form1();
                    form1.button7.Visible = true;
                    form1.groupBox1.Visible = true;
                    form1.textBox3.Enabled = true;
                    form1.FormClosed += formClosed;
                    form1.Show();

                    this.Hide();
                    cle();

                    break;
                case 2:
                    label3.Visible = true;
                    label3.Text = "Правельні дані.";

                    Form1 form1_ = new Form1();
                    form1_.FormClosed += formClosed;
                    form1_.Show();

                    this.Hide();
                    cle();

                    break;
                default:
                    label3.Visible = true;
                    label3.Text = "Помилкові дані. Не правильний логін або пароль!";
                    break;
            }

        }



        void formClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            this.userTableAdapter.Fill(this.kursDataSet2.user);
        }

        void cle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            label3.Text = "";
        }

     
    }
}