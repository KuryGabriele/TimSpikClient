using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using VisioForge.Libs.Newtonsoft.Json;

namespace WindowsFormsApp1 {
    public partial class UserOptions : Form {
        //User settings
        private bool mouseDown; //Used for window moving
        private Point lastLocation; //Same as above
        string nick; //Nickname of user whos picture got clicked
        String pictureUrl;
        float volume; //Volume for the audio stream
        KURYUserSettings kus; //Kury user settings instance
        KURY_Receiver rcv;

        internal class UserAudioSettings {
            public string nick { get; set; }
            public float volume { get; set; }
        }

        public UserOptions(String nick, String pictureUrl, KURY_Receiver rcv) {
            //Create ui
            InitializeComponent();
            this.nick = nick;
            this.pictureUrl = pictureUrl;
            this.rcv = rcv;

            //Get user settings
            kus = new KURYUserSettings();
            if (nick == Form1.nickname) {
                //If clicked on myself change my volume
                volume = kus.VolumeMic;
                KURY_Transmitter.volume = volume;
                volumeSlider.Volume = volume;
                volumeSlider.Enabled = !Form1.isMuted;
            } else {
                //If clicked on someone else change input volume
                fetchVolume(nick);
                volumeSlider.Enabled = !Form1.isMuted;
            }
        }

        private async void fetchVolume(string nickname) {
            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getUserVolume/" + Form1.nickname + "/" + nickname);
            var data = JsonConvert.DeserializeObject<List<UserAudioSettings>>(response);

            foreach (var usr in data) {
                rcv.changeVolume(usr.nick, usr.volume);
                volumeSlider.Volume = usr.volume;
            }
        }

        private void addUser() {
            //Profile picture
            var picture = new UserImage {
                Name = nick + "Img",
                Size = new Size(100, 100),
                Location = new Point(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            //Windows moving events
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

            //Get picture from webstire
            try {
                picture.Load(pictureUrl);
            } catch (Exception ex) {
                picture.Load("https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwowsciencecamp.org%2Fwp-content%2Fuploads%2F2018%2F07%2Fdummy-user-img-1-800x800.png&f=1&nofb=1");
            }

            //Setup nick
            var txt = new Label {
                Text = nick,
                Size = new Size(200, 100),
                Font = new Font("Arial", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
            };
            //Add events
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
            //Add all to ui
            img_container.Controls.Add(picture);
            img_container.Controls.Add(txt);
        }

        private void close_btn_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void img_container_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                //Move windows with mouse
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

        private async void cambiaVolume(object sender, EventArgs e) {
            //Save new volumes
            if(nick == Form1.nickname) {
                KURY_Transmitter.volume = volumeSlider.Volume;
                kus.VolumeMic = volumeSlider.Volume;
            } else {
                rcv.changeVolume(nick, volumeSlider.Volume);
                
                HttpClient apiClient = new HttpClient();
                string url = "https://timspik.ddns.net/setUserVolume/" + Form1.nickname + "/" + nick + "/" + volumeSlider.Volume.ToString().Replace(',', '.');
                string response = await apiClient.GetStringAsync(url);
            };
        }
    }
}
