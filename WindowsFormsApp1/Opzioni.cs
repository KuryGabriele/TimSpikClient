using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class Opzioni : Form {
        List<Guid> ids;
        private bool mouseDown;
        private Point lastLocation;

        public Opzioni() {
            InitializeComponent();
            ids = new List<Guid>();
            Guid defGuid = Form1.OUT_ID;
            int indiceMic;
            int j = 0;

            for (int i = 0; i < WaveIn.DeviceCount; i++) {
                var disp = WaveIn.GetCapabilities(i);
                listaEntrata.Items.Add(disp.ProductName);
            }
            listaEntrata.SelectedIndex = Form1.IN_ID;

            foreach (DirectSoundDeviceInfo i in DirectSoundOut.Devices) {
                ids.Add(i.Guid);
                listaUscita.Items.Add(i.Description);
                listaUscita.Update();
                //Seleziono quello preselezionato
                if (defGuid == i.Guid) {
                    listaUscita.SelectedIndex = j;
                }
                j++;
            }
        }

        private void okBtn_Click(object sender, EventArgs e) {
            if(listaUscita.SelectedIndex >= 0) {
                Form1.OUT_ID = ids[listaUscita.SelectedIndex];
            }

            if (listaEntrata.SelectedIndex >= 0) {
                Form1.IN_ID = listaEntrata.SelectedIndex;
            }

            if(audioPack.SelectedIndex >= 0) {
                Form1.soundPack = (string)audioPack.SelectedItem;
            }

            this.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}
