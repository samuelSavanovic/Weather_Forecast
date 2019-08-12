using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Weather_Forecast {
    public partial class Form1 : Form {
        dynamic data;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xf020;
        private void init() {
            WeatherInfo weatherData = new WeatherInfo(Properties.Settings.Default.City, Properties.Settings.Default.CountryCode);
            data = weatherData.getCurrentWeatherData();
            if (data == null) {
                Environment.Exit(0);
            } else {
                tempLabel.Text = data.main.temp + " °C";
                wDescriptionLabel.Text = data.weather[0].description;
                pressureLabel.Text = data.main.pressure + " hPa";
                humidityLabel.Text = data.main.humidity + "%";
                cityLabel.Text = data.name + ", " + data.sys.country;
                visibilityLabel.Text = (string)(data.visibility / 1000) + " km";
                lonLabel.Text = data.coord.lon;
                latLabel.Text = data.coord.lat;
                windLabel.Text = data.wind.speed + " m/s";
                string iconUrl = string.Format("http://openweathermap.org/img/wn/{0}@2x.png", data.weather[0].icon);
                string storagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WeatherForecast");
                Directory.CreateDirectory(storagePath);
                string icon = storagePath + "\\" + data.weather[0].icon + ".png";
                if (!File.Exists(icon)) {
                    using (WebClient wc = new WebClient()) {
                        wc.DownloadFile(iconUrl, icon);
                    }
                }
                pictureBox1.Image = Image.FromFile(icon);
            }
            
        }
        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_SYSCOMMAND) {
                if (m.WParam.ToInt32() == SC_MINIMIZE) {
                    m.Result = IntPtr.Zero;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
        public Form1() {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.DimGray;
            TransparencyKey = Color.DimGray;
            ShowInTaskbar = false;
            Rectangle res = Screen.PrimaryScreen.Bounds;
            Location = new Point(res.Width - Size.Width);
            init();
        }
        private void Timer1_Tick(object sender, EventArgs e) {
            init();
        }
    }
}
