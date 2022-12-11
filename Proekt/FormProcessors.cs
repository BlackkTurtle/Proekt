using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proekt
{
    public partial class FormProcessors : Form
    {
        public FormProcessors()
        {
            InitializeComponent();
        }
        public FormProcessors(string value)
        {
            InitializeComponent();
            label2.Text = value;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (label2.Text == "---")
                {
                    SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    var cmd1 = new SqlCommand("insert into Articles values(@Letter)", connection);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Parameters.AddWithValue("@Letter", "1");
                    cmd1.ExecuteNonQuery();
                    var cmd2 = new SqlCommand("select max(Article) as Article from Articles", connection);
                    var adapter = new SqlDataAdapter(cmd2);
                    var tables = new DataTable("tables");
                    adapter.Fill(tables);
                    var row = tables.Rows[0];
                    var cmd = new SqlCommand("insert into Processors values(@Article,@Name,@Picture,@Socket,@Cores,@Threads,@Freq,@Cash,@MicroArch,@GraphCore,@Memory,@TechProcess,@TDP,@Price)", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Article", row["Article"].ToString());
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Picture", ConvertImageToBytes(pictureBox1.Image));
                    cmd.Parameters.AddWithValue("@Socket", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Cores", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Threads", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Freq", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Cash", textBox6.Text);
                    cmd.Parameters.AddWithValue("@MicroArch", textBox7.Text);
                    cmd.Parameters.AddWithValue("@GraphCore", textBox8.Text);
                    cmd.Parameters.AddWithValue("@Memory", textBox9.Text);
                    cmd.Parameters.AddWithValue("@TechProcess", textBox10.Text);
                    cmd.Parameters.AddWithValue("@TDP", textBox11.Text);
                    cmd.Parameters.AddWithValue("@Price", textBox12.Text); ;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    var cmd = new SqlCommand("update Processors set Name=@Name,Picture=@Picture,Socket=@Socket,Cores=@Cores,Threads=@Threads,Frequency=@Freq,Cash=@Cash,MicroArchitecture=@MicroArch,GrafickCore=@GraphCore,Memory=@Memory,TechProcess=@TechProcess,TDP=@TDP,Price=@Price where ID=@Article", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Article", label2.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Picture", ConvertImageToBytes(pictureBox1.Image));
                    cmd.Parameters.AddWithValue("@Socket", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Cores", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Threads", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Freq", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Cash", textBox6.Text);
                    cmd.Parameters.AddWithValue("@MicroArch", textBox7.Text);
                    cmd.Parameters.AddWithValue("@GraphCore",textBox8.Text);
                    cmd.Parameters.AddWithValue("@Memory", textBox9.Text);
                    cmd.Parameters.AddWithValue("@TechProcess", textBox10.Text);
                    cmd.Parameters.AddWithValue("@TDP", textBox11.Text);
                    cmd.Parameters.AddWithValue("@Price", textBox12.Text);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public Image ConvertBytesToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg)|*.jpg;*.jpeg", Multiselect = false })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(dialog.FileName);
                }
            }
        }
        private void FormProcessors_Load(object sender, EventArgs e)
        {
            if (label2.Text != "---")
            {
                SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
                string command = "SELECT * FROM Processors where ID=" + label2.Text;
                var cmd = new SqlCommand(command, connection);
                var adapter = new SqlDataAdapter(cmd);
                var tables = new DataTable("tables");
                connection.Open();
                adapter.Fill(tables);
                connection.Close();
                var row = tables.Rows[0];
                textBox1.Text = row["Name"].ToString();
                pictureBox1.Image = ConvertBytesToImage((byte[])row["Picture"]);
                textBox2.Text = row["Socket"].ToString();
                textBox3.Text = row["Cores"].ToString();
                textBox4.Text = row["Threads"].ToString();
                textBox5.Text = row["Frequency"].ToString();
                textBox6.Text = row["Cash"].ToString();
                textBox7.Text = row["MicroArchitecture"].ToString();
                textBox8.Text = row["GrafickCore"].ToString();
                textBox9.Text = row["Memory"].ToString();
                textBox10.Text = row["TechProcess"].ToString();
                textBox11.Text = row["TDP"].ToString();
                textBox12.Text = row["Price"].ToString();
            }
        }
    }
}
