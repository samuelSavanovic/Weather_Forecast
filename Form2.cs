using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_Forecast {
    public partial class Form2 : Form {
        Form1 form1;
        public Form2() {

            InitializeComponent();
            checkBox1.Checked = Properties.Settings.Default.showAgain == "True" ? true : false;
            if (checkBox1.Checked) {
                hideAndRun();
            }
        }

        private void Button1_Click(object sender, EventArgs e) {
            Close();
        }
        private void hideAndRun() {
            Properties.Settings.Default.showAgain = checkBox1.Checked.ToString();
            Properties.Settings.Default.City = cityBox.Text;
            Properties.Settings.Default.CountryCode = countryCodeBox.Text;
            Properties.Settings.Default.Save();
            this.WindowState = FormWindowState.Minimized;
            if(form1 == null) {
                form1 = new Form1();
                form1.Show();
            } else {
                form1 = null;
                form1 = new Form1();
                form1.Show();
            }
            
        }
        private void Button2_Click(object sender, EventArgs e) {
            enterPressed();
            
        }

        private void Form2_Resize(object sender, EventArgs e) {
            if (FormWindowState.Minimized == this.WindowState) {
                notifyIcon1.Visible = true;
                this.Hide();
            } else if (FormWindowState.Normal == this.WindowState) {
                notifyIcon1.Visible = false;
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            Show();
            WindowState = FormWindowState.Normal;
        }
        private void enterPressed() {
            if (form1 != null) {
                form1.Close();
                hideAndRun();
            } else {
                hideAndRun();
            }
        }
        private void CityBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                enterPressed();
            }
        }

        private void CountryCodeBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                enterPressed();
            }
        }
    }
}
