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
    public partial class FormMotherboard : Form
    {
        public FormMotherboard()
        {
            InitializeComponent();
        }
        public FormMotherboard(string value)
        {
            InitializeComponent();
            label2.Text = value;
        }
        private void FormMotherboard_Load(object sender, EventArgs e)
        {
            if (label2.Text != "---")
            {
                SqlConnection connection = new SqlConnection("server=DESKTOP-Q05O5DB; initial catalog=magazin; user id=user1; password=sa");
                string command = "SELECT * FROM Motherboards where ID="+label2.Text;
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
                textBox3.Text = row["PowerConnectors"].ToString();
                textBox4.Text = row["PCIEExpress"].ToString();
                textBox5.Text = row["SATA"].ToString();
                textBox6.Text = row["BackPanelPorts"].ToString();
                textBox7.Text = row["FormFactor"].ToString();
                textBox8.Text = row["Price"].ToString();
            }
        }
        public byte[] ConvertImageToBytes(Image img)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                img.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public Image ConvertBytesToImage(byte[] data)
        {
            using(MemoryStream ms =new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    var cmd = new SqlCommand("insert into Motherboards values(@Article,@Name,@Picture,@Socket,@PowerConnectors,@PCIE,@Sata,@BackPP,@FormFactor,@Price)", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Article", row["Article"].ToString());
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Picture", ConvertImageToBytes(pictureBox1.Image));
                    cmd.Parameters.AddWithValue("@Socket", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PowerConnectors", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PCIE", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Sata", textBox5.Text);
                    cmd.Parameters.AddWithValue("@BackPP", textBox6.Text);
                    cmd.Parameters.AddWithValue("@FormFactor", textBox7.Text);
                    cmd.Parameters.AddWithValue("@Price", textBox8.Text);
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
                    var cmd = new SqlCommand("update Motherboards set Name=@Name,Picture=@Picture,Socket=@Socket,PowerConnectors=@PowerConnectors,PCIEExpress=@PCIE,SATA=@Sata,BackPanelPorts=@BackPP,FormFactor=@FormFactor,Price=@Price where ID=@Article", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Article", label2.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Picture", ConvertImageToBytes(pictureBox1.Image));
                    cmd.Parameters.AddWithValue("@Socket", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PowerConnectors", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PCIE", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Sata", textBox5.Text);
                    cmd.Parameters.AddWithValue("@BackPP", textBox6.Text);
                    cmd.Parameters.AddWithValue("@FormFactor", textBox7.Text);
                    cmd.Parameters.AddWithValue("@Price", textBox8.Text);
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
        private void button1_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog() { Filter="Image files(*.jpg;*.jpeg)|*.jpg;*.jpeg",Multiselect=false})
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image=Image.FromFile(dialog.FileName);
                }
            }
        }
    }
}
