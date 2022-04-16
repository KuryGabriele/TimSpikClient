using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class NickRequest : Form {
        public NickRequest() {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            Form1.nickname = nickBox.Text;
            this.Close();
        }
    }
}
