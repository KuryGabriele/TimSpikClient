using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioForge.Libs.Newtonsoft.Json;

namespace WindowsFormsApp1 {
    public partial class NickRequest : Form {
        public NickRequest() {
            InitializeComponent();
        }

        private void saveBtn_ClickAsync(object sender, EventArgs e) {
            Form1.nickname = nickBox.Text;
            addUser();
            this.Close();
        }

        private async void addUser() {
            bool isUri = Uri.IsWellFormedUriString(imgUrl.Text, UriKind.RelativeOrAbsolute);

            if (isUri) {
                HttpClient apiClient = new HttpClient();
                string responseVolume = await apiClient.GetStringAsync("https://timspik.ddns.net/addUser/" + nickBox.Text + "/" + imgUrl.Text);
            }
        }
    }
}
