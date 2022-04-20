using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioForge.Libs.Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

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
        private int sr = 48000;
        private int bits = 16;
        private int channels = 2;


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

        internal class AudioSettings {
            public int sampleRate { get; set; }
            public int bits { get; set; }
            public int channels { get; set; }
        }

        internal class UserAudioSettings {
            public string nickname { get; set; }
            public float volume { get; set; }
        }

        internal class UpdaterVersion {
            public int version { get; set; }
            public string url { get; set; }
        }

        public Form1() {
            InitializeComponent();
            fetchLastUpdaterVersion();
            checkTimSpikVersion();
            //User lists
            usersOnline = new List<string>();
            usrImagesUrl = new List<string>();
            usersImages = new List<FlowLayoutPanel>();
            checkOnline();

            //Fetch online users
            updateOnlineUsers();

            //Retrive info from user settings
            kus = new KURYUserSettings();
            OUT_ID = kus.defaultGUID; //Ouput device

            //Fetch ipAddress and port from api
            fetchServerAddress();
            fetchAudioSettigns();

            //Nickname
            if (kus.Nick == "User") {
                NickRequest nr = new NickRequest();
                nr.ShowDialog();
                kus.Nick = nickname;
                kus.ipAddr = ipAddr;
                kus.Save();
            }
            nickname = kus.Nick;
        }

        private async void checkOnline() {
            try {
                HttpClient apiClient = new HttpClient();
                string response = await apiClient.GetStringAsync("https://timspik.ddns.net/test");

                if (!response.Contains("working")) {
                    MessageBox.Show("La API non risponde correttamente, scrivi a Kury :D", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception ex) {
                MessageBox.Show("La API non risponde correttamente, scrivi a Kury :D", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async void fetchServerAddress() {
            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getAudioServerAddress");
            var data = JsonConvert.DeserializeObject<AudioServerAddressRequest>(response);

            ipAddr = data.address;
            port = data.port;
        }

        private async void fetchAudioSettigns() {
            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getAudioSettings");
            var data = JsonConvert.DeserializeObject<AudioSettings>(response);

            sr = data.sampleRate;
            bits = data.bits;
            channels = data.channels;
        }

        private async void fetchLastUpdaterVersion() {
            //Fetch version from api
            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getLastUpdaterVersion");
            var data = JsonConvert.DeserializeObject<UpdaterVersion>(response);
            int version = data.version;
            string url = data.url;

            if (File.Exists(@"updaterVersion.json")) {
                //Get local version
                string json = File.ReadAllText(@"updaterVersion.json");
                var localData = JsonConvert.DeserializeObject<UpdaterVersion>(json);
                int localVersion = localData.version;

                if (localVersion < version) {
                    //If new version available download it
                    MessageBox.Show("C'è un aggiornamento disponibile. Non mi interessa se vuoi parlare con i tuoi amici, sono peggio di Windows e mi aggiorno. Fottiti 😸", "Aggiornamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    using (var client = new WebClient()) {
                        client.DownloadFile(url, "TimSpikUpdater.exe");
                    }
                    //Update local file
                    File.WriteAllText(@"updaterVersion.json", response);
                    //Open updater
                    Process.Start(@"TimSpikUpdater.exe");
                    closeApp();
                }
            } else {
                //If file not present update anyway.
                File.WriteAllText(@"updaterVersion.json", response);
                using (var client = new WebClient()) {
                    client.DownloadFile(url, "TimSpikUpdater.exe");
                }
            }
        }

        private async void checkTimSpikVersion() {
            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getLastAppVersion");
            var data = JsonConvert.DeserializeObject<UpdaterVersion>(response);
            int version = data.version;

            if (File.Exists(@"timSpikVersion.json")) {
                //Get local version
                string json = File.ReadAllText(@"timSpikVersion.json");
                var localData = JsonConvert.DeserializeObject<UpdaterVersion>(json);
                int localVersion = localData.version;

                if (localVersion < version) {
                    MessageBox.Show("C'è un aggiornamento disponibile. Non mi interessa se vuoi parlare con i tuoi amici, sono peggio di Windows e mi aggiorno. Fottiti 😸", "Aggiornamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(@"TimSpikUpdater.exe");
                    closeApp();
                }
            } else {
                MessageBox.Show("C'è un aggiornamento disponibile. Non mi interessa se vuoi parlare con i tuoi amici, sono peggio di Windows e mi aggiorno. Fottiti 😸", "Aggiornamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"TimSpikUpdater.exe");
                closeApp();
            }
        }

        public async Task<bool> updateOnlineUsers() {
            userPane.Controls.Clear();

            HttpClient apiClient = new HttpClient();
            string response = await apiClient.GetStringAsync("https://timspik.ddns.net/getOnlineUsers");
            var data = JsonConvert.DeserializeObject<List<OnlineUser>>(response);

            usersOnline.Clear();
            usrImagesUrl.Clear();
            foreach (var usr in data) {
                usersOnline.Add(usr.nick);
                usrImagesUrl.Add(usr.img);
                addUser(usr.nick);
            }

            return true;
        }

        private void closeBtn_Click(object sender, EventArgs e) {
            closeApp();
        }

        private void closeApp() {
            //Used to close the app
            if(!closeConfirmation || MessageBox.Show("Sei sicuro di voler abbandonare i tuoi amici?", "Chiudere?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                if (inRoom) {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://timspik.ddns.net/setOnline/" + nickname + "/F");
                    request.GetResponse();

                    //stop the audio
                    vt.sendQuit();
                    trd.Abort();
                    trd2.Abort();
                }

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

                vt.sendQuit();

                //Remove from ui
                updateOnlineUsers();
                
                //Stop the receiver
                rcv.socket.Close();
                rcv.socket.Dispose();

                //Stop the transmitter
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

                bool done = await updateOnlineUsers();
                settingsBtn.Enabled = false;

                //Start audio streams
                rcv = new KURY_Receiver(usersOnline, ipAddr, port, this, sr, bits, channels);
                trd = new Thread(new ThreadStart(startListener));

                vt = new KURY_Transmitter(ipAddr, port, nickname, sr, bits, channels);
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

                foreach (var usr in usersOnline) {
                    rcv.changeVolume(usr, 0);
                }

                //Change ui
                isMuted = true;
                muteAudioBtn.Text = "Smuta audio";
            }            
        }
    }
}
