using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void dataGridView1_Layout(object sender, LayoutEventArgs e)
        {

        }
        private void DataGridView1OnLoad()
        {
            SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
            var cmd = new SqlCommand("SELECT TABLE_NAME as Tables FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME!='sysdiagrams' and TABLE_NAME!='Orders' and TABLE_NAME!='Korzina' and TABLE_NAME!='Articles'", connection);
            var adapter = new SqlDataAdapter(cmd);
            var tables = new DataTable("tables");
            connection.Open();
            adapter.Fill(tables);
            connection.Close();
            dataGridView1.DataSource = tables;
            dataGridView1.Columns[0].AutoSizeMode=DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.MultiSelect = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridView1OnLoad();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                if (row.Cells[0].Value.ToString() == "Motherboards")
                {
                    FormMotherboard formMotherboard = new FormMotherboard();
                    formMotherboard.ShowDialog();
                }else if (row.Cells[0].Value.ToString() == "Storages")
                {
                    FormStorages form = new FormStorages();
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "RAMs")
                {
                    FormRAMs form = new FormRAMs();
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "Processors")
                {
                    var form = new FormProcessors();
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "PowerSupply")
                {
                    var form = new FormPowerSupply();
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "GraphickCards")
                {
                    var form = new FormGraphickCards();
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "Corpuses")
                {
                    var form = new FormCorpuses();
                    form.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Gabella");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[index];
                SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
                string command = "SELECT * INTO temporar FROM " + row.Cells[0].Value.ToString() + " ALTER TABLE temporar DROP COLUMN Picture SELECT * FROM temporar DROP TABLE temporar";
                var cmd = new SqlCommand(command, connection);
                var adapter = new SqlDataAdapter(cmd);
                var tables = new DataTable("tables");
                connection.Open();
                adapter.Fill(tables);
                connection.Close();
                dataGridView2.DataSource = tables;
            }
            catch (Exception)
            {
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                var row2=dataGridView2.CurrentRow;
                if (row.Cells[0].Value.ToString() == "Motherboards")
                {
                    FormMotherboard formMotherboard = new FormMotherboard(row2.Cells[0].Value.ToString());
                    formMotherboard.ShowDialog();
                }
                else if (row.Cells[0].Value.ToString() == "Storages")
                {
                    FormStorages form = new FormStorages(row2.Cells[0].Value.ToString());
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "RAMs")
                {
                    FormRAMs form = new FormRAMs(row2.Cells[0].Value.ToString());
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "Processors")
                {
                    var form = new FormProcessors(row2.Cells[0].Value.ToString());
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "PowerSupply")
                {
                    var form = new FormPowerSupply(row2.Cells[0].Value.ToString());
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "GraphickCards")
                {
                    var form = new FormGraphickCards(row2.Cells[0].Value.ToString());
                    form.Show();
                }
                else if (row.Cells[0].Value.ToString() == "Corpuses")
                {
                    var form = new FormCorpuses(row2.Cells[0].Value.ToString());
                    form.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Gabella");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var row = dataGridView1.CurrentRow;
                var row1 = dataGridView2.CurrentRow;
                var str = "delete from " + row.Cells[0].Value.ToString() + " where ID=@id";
                var cmd = new SqlCommand(str, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", row1.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                var cmd1 = new SqlCommand("delete from Articles where Article=@id", connection);
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@id", row1.Cells[0].Value.ToString());
                cmd1.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Succesfull!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
