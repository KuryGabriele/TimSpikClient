﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using TimSpik;
using VisioForge.Libs.Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool closeConfirmation = false; //Spostare in user settings
        private bool mouseDown; // Used for windows moving
        private Point lastLocation; //Same as above :D
        private List<string> usersOnline; //List of online users
        private List<string> usrImagesUrl; //List of online users images url's
        private List<FlowLayoutPanel> usersImages; //List of profile images for users
        private bool inRoom = false; //True if user in room
        private Thread trd; //Receiver thread
        private Thread trd2; //Transmitter thread
        KURY_Receiver rcv; //Receiver instance
        KURY_Transmitter vt; //Transmitter instance
        public static Guid OUT_ID = new Guid(); //Ouput device GUID
        public static int IN_ID; //Input device id
        KURYUserSettings kus; //User settings instance
        public static string nickname; //Users nickname
        public static bool isMuted = false; //Is muted
        public string ipAddr = ""; //Server's ip address
        public int port = 6981;
        float lastVolume; //Used to mute input audio and reset it


        internal class OnlineUser {
            public string nick { get; set; }
            public string img { get; set; }
            public int stanza { get; set; }
            public string lastIp { get; set; }
        }

        internal class AudioServerAddressRequest {
            public string address { get; set; }
            public int port { get; set; }
        }

        internal class UserAudioSettings {
            public string nickname { get; set; }
            public float volume { get; set; }
        }

        public Form1() {

            InitializeComponent();
            //User lists
            usersOnline = new List<string>();
            usrImagesUrl = new List<string>();
            usersImages = new List<FlowLayoutPanel>();

            //Fetch online users
            updateOnlineUsers();

            //Retrive info from user settings
            kus = new KURYUserSettings();
            OUT_ID = kus.defaultGUID; //Ouput device

            //Fetch ipAddress and port from api
            fetchServerAddress();
            

            //Nickname
            if(kus.Nick == "User") {
                NickRequest nr = new NickRequest();
                nr.ShowDialog();
                kus.Nick = nickname;
                kus.ipAddr = ipAddr;
                kus.Save();
            }
            nickname = kus.Nick;
            
            //Volume
            //TODO richiesta api
        }

        private async void fetchServerAddress() {
            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getAudioServerAddress");
            var data = JsonConvert.DeserializeObject<AudioServerAddressRequest>(response);

            ipAddr = data.address;
            port = data.port;
        }

        public async void updateOnlineUsers() {
            userPane.Controls.Clear();

            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getOnlineUsers");
            var data = JsonConvert.DeserializeObject<List<OnlineUser>>(response);

            foreach (var usr in data) {
                usersOnline.Add(usr.nick);
                usrImagesUrl.Add(usr.img);
                addUser(usr.nick);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e) {
            closeApp();
        }

        private void closeApp() {
            //Used to close the app
            if(!closeConfirmation || MessageBox.Show("Sei sicuro di voler abbandonare i tuoi amici?", "Chiudere?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                if (inRoom) {
                    //stop the audio
                    vt.sendQuit();
                    trd.Abort();
                    trd2.Abort();
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://timspik.ddns.net/setOnline/" + nickname + "/F");
                request.GetResponse();

                this.Close();
            }
        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                //If mouse clicked move window
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void topPanel_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void minimizeBtn_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void addUser(String nick) {
            //Container
            var pane = new FlowLayoutPanel {
                Size = new Size(55, 80),
                FlowDirection = FlowDirection.TopDown,
            };
            //Image
            var picture = new UserImage {
                Name = nick + "Img",
                Size = new Size(50, 50),
                Location = new Point(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            //Get image from website
            int index = usersOnline.FindIndex(x => x.StartsWith(nick));
            try {
                picture.Load(usrImagesUrl.ElementAt(index));
            } catch (Exception ex) {
                picture.Load("https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwowsciencecamp.org%2Fwp-content%2Fuploads%2F2018%2F07%2Fdummy-user-img-1-800x800.png&f=1&nofb=1");
            }

            //Add click event
            picture.Click += (sender, eventArgs) => {
                //On click show UserOptions
                UserOptions ou = new UserOptions(nick, usrImagesUrl.ElementAt(index), rcv);
                ou.ShowDialog();
            };
            pane.Controls.Add(picture);

            //Nick
            var txt = new Label {
                Text = nick,
                Size = new Size(50, 15),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
            };
            //Add click event

            txt.Click += (sender, eventArgs) => {
                UserOptions ou = new UserOptions(nick, usrImagesUrl.ElementAt(index), rcv);
                ou.Show();
            };
            pane.Controls.Add(txt);

            //Add click event
            pane.Click += (sender, eventArgs) => {
                UserOptions ou = new UserOptions(nick, usrImagesUrl.ElementAt(index), rcv);
                ou.Show();
            };

            //Add to ui
            usersImages.Add(pane);
            userPane.Controls.Add(pane);
        }

        private void removeUser(String nick) {
            //Remove user from ui and list
            int index = usersOnline.IndexOf(nick);
            if (index != -1) {
                FlowLayoutPanel elem = usersImages.ElementAt(index);

                //Lo rimuovo
                userPane.Controls.Remove(elem);
                usersOnline.Remove(nick);
                usersImages.RemoveAt(index);
            }
        }

        private void logoBox_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                //Move window if mouse clicked
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void logoBox_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void logoBox_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void clearMsgBox(object sender, EventArgs e) {
            if (txtBox.Text == "Insulta i tuoi amici...") {
                txtBox.Text = "";
            }
        }

        private void fillPlaceholder(object sender, EventArgs e) {
            if (txtBox.Text == "") {
                txtBox.Text = "Insulta i tuoi amici...";
            }
        }

        private void invioMsg_Click(object sender, EventArgs e) {
            sendMessage();
        }

        private void startListener() {
            rcv.startListening();
        }

        private void startTransmitter() {
            vt.startMic();
        }

        private void sendMessage() {
            String msg = txtBox.Text;
            txtBox.Text = "";
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && e.Modifiers != Keys.Shift) {
                sendMessage();
                e.Handled = true;
            }
        }

        private void rmBtn_Click(object sender, EventArgs e) {
            removeUser("Kury");
        }

        private async void joinBtn_Click(object sender, EventArgs e) {
            if (inRoom) {
                //If already in a channel exit
                HttpClient apiClient = new HttpClient();
                string response = await apiClient.GetStringAsync("https://timspik.ddns.net/setOnline/" + nickname + "/F");

                //Remove from ui
                updateOnlineUsers();
                
                //Stop the receiver
                rcv.socket.Close();
                rcv.socket.Dispose();

                //Stop the transmitter
                vt.sendQuit();
                KURY_Transmitter.wi.StopRecording();

                //Stop the threads
                trd.Abort();
                trd2.Abort();

                //Change ui buttons
                joinBtn.Text = "Entra";
                inRoom = false;
                settingsBtn.Enabled = true;
            } else {
                //Join channel
                HttpClient apiClient = new HttpClient();
                string response = await apiClient.GetStringAsync("https://timspik.ddns.net/setOnline/" + nickname + "/T");

                updateOnlineUsers();
                settingsBtn.Enabled = false;

                //Start audio streams
                rcv = new KURY_Receiver(usersOnline, ipAddr, port, this);
                trd = new Thread(new ThreadStart(startListener));

                vt = new KURY_Transmitter(ipAddr, port, nickname);
                trd2 = new Thread(new ThreadStart(startTransmitter));

                trd.IsBackground = true;
                trd.Start();
                trd2.IsBackground = true;
                trd2.Start();

                //Change ui
                Console.WriteLine("Thread started");
                joinBtn.Text = "Esci";
                inRoom = true;
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e) {
            //Open settings windows
            Opzioni frm = new Opzioni();
            frm.ShowDialog();

            //Get new value and save them
            kus.defaultGUID = OUT_ID;
            kus.defaultInputDevice = IN_ID;
            kus.Save();
        }

        private async void muteBtn_Click(object sender, EventArgs e) {
            if (isMuted) {
                //If user muted unmute

                HttpClient apiClient = new HttpClient();
                string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getUserVolumes/" + nickname);
                var data = JsonConvert.DeserializeObject<List<UserAudioSettings>>(response);

                foreach (var usr in data) {
                    rcv.changeVolume(usr.nickname, usr.volume);
                }

                //Change ui
                isMuted = false;
                muteAudioBtn.Text = "Muta audio";
            } else {
                //If user unmuted mute
                
                //Retrive last volume

                //TODO Richista api

                //Change ui
                isMuted = true;
                muteAudioBtn.Text = "Smuta audio";
            }            
        }
    }
}
