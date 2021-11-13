using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class OpzioniUtente : Form {
        private bool mouseDown;
        private Point lastLocation;
        string nick;
        float volume;
        VBANUserSettings vus;

        public OpzioniUtente(String nick) {
            InitializeComponent();
            this.nick = nick;
            aggiungiPersona();
            vus = new VBANUserSettings();
            if (nick == Form1.nickUtente) {
                volume = vus.VolumeMic;
                VBAN_Transmitter.volume = volume;
                volumeSlider.Volume = volume;
                volumeSlider.Enabled = !Form1.mutato;
            } else {
                volume = vus.Volume;
                VBAN_Receiver.volume = volume;
                volumeSlider.Volume = volume;
                volumeSlider.Enabled = !Form1.mutato;
            }
        }

        private void aggiungiPersona() {
            //Contenitore
            //Immagine
            var picture = new ImmagineUtente {
                Name = nick + "Img",
                Size = new Size(100, 100),
                Location = new Point(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            picture.MouseDown += (sender, eventArgs) => {
                mouseDown = true;
                lastLocation = eventArgs.Location;
            };
            picture.MouseUp += (sender, eventArgs) => {
                mouseDown = false;
            };
            picture.MouseMove += (sender, eventArgs) => {
                if (mouseDown) {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + eventArgs.X, (this.Location.Y - lastLocation.Y) + eventArgs.Y);

                    this.Update();
                }
            };
            picture.Load("https://kurickigabriele2020.altervista.org/" + nick + ".jpg");
            //Nick
            var txt = new Label {
                Text = nick,
                Size = new Size(200, 100),
                Font = new Font("Arial", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
            };
            txt.MouseDown += (sender, eventArgs) => {
                mouseDown = true;
                lastLocation = eventArgs.Location;
            };
            txt.MouseUp += (sender, eventArgs) => {
                mouseDown = false;
            };
            txt.MouseMove += (sender, eventArgs) => {
                if (mouseDown) {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + eventArgs.X, (this.Location.Y - lastLocation.Y) + eventArgs.Y);

                    this.Update();
                }
            };
            //Aggiungo tutto
            img_container.Controls.Add(picture);
            img_container.Controls.Add(txt);
        }

        private void close_btn_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void img_container_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void img_container_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void img_container_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void cambiaVolume(object sender, EventArgs e) {
            if(nick == Form1.nickUtente) {
                VBAN_Transmitter.volume = volumeSlider.Volume;
                vus.VolumeMic = volumeSlider.Volume;
            } else {
                VBAN_Receiver.volume = volumeSlider.Volume;
                vus.Volume = volumeSlider.Volume;
            }

            vus.Save();
        }
    }
}
