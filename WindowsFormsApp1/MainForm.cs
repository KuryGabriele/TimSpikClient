using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool chiediConfermaChiusura = false;
        private bool mouseDown;
        private Point lastLocation;
        private List<string> utenti;
        private List<FlowLayoutPanel> iconcineUtenti;
        private bool inStanza = false;
        private Thread trd;
        private Thread trd2;
        VBAN_Receiver rcv;
        VBAN_Transmitter vt;
        public static Guid OUT_ID = new Guid();
        public static int IN_ID;
        VBANUserSettings vus;
        public static string nickUtente;
        public static bool mutato = false;
        float volPrecedente;

        public Form1() {
            InitializeComponent();
            utenti = new List<string>();
            iconcineUtenti = new List<FlowLayoutPanel>();
            utenti.Add("Dallas");
            vus = new VBANUserSettings();
            OUT_ID = vus.defaultGUID;
            if(vus.Nick == "User") {
                NickRequest nr = new NickRequest();
                nr.ShowDialog();
                vus.Nick = nickUtente;
                vus.Save();
            }

            nickUtente = vus.Nick;

            float volume = vus.Volume;
            VBAN_Receiver.volume = volume;

            foreach (string u in utenti) {
                aggiungiPersona(u);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e) {
            chiudiApp();
        }

        private void chiudiApp() {
            if(!chiediConfermaChiusura || MessageBox.Show("Sei sicuro di voler abbandonare i tuoi amici?", "Chiudere?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                if (inStanza) {
                    trd.Abort();
                }
                this.Close();
            }
        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
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

        private void aggiungiPersona(String nick) {
            //Contenitore
            var pannello = new FlowLayoutPanel {
                Size = new Size(55, 80),
                FlowDirection = FlowDirection.TopDown,
            };
            //Immagine
            var picture = new ImmagineUtente {
                Name = nick + "Img",
                Size = new Size(50, 50),
                Location = new Point(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            picture.Load("https://kurickigabriele2020.altervista.org/" + nick +".jpg");
            picture.Click += (sender, eventArgs) => {
                OpzioniUtente ou = new OpzioniUtente(nick);
                ou.ShowDialog();
            };
            pannello.Controls.Add(picture);
            //Nick
            var txt = new Label {
                Text = nick,
                Size = new Size(50, 15),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
            };
            txt.Click += (sender, eventArgs) => {
                OpzioniUtente ou = new OpzioniUtente(nick);
                ou.Show();
            };
            pannello.Controls.Add(txt);
            pannello.Click += (sender, eventArgs) => {
                OpzioniUtente ou = new OpzioniUtente(nick);
                ou.Show();
            };

            //Aggiungo tutto
            iconcineUtenti.Add(pannello);
            pannelloUtenti.Controls.Add(pannello);
        }

        private void rimuoviPersona(String nick) {
            int indice = utenti.IndexOf(nick);
            if (indice != -1) {
                FlowLayoutPanel elem = iconcineUtenti.ElementAt(indice);

                //Lo rimuovo
                pannelloUtenti.Controls.Remove(elem);
                utenti.Remove(nick);
                iconcineUtenti.RemoveAt(indice);
            }
        }

        private void logoBox_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
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
            inviaMessaggio();
        }

        private void startListener() {
            rcv.startListening();
        }

        private void startTransmitter() {
            vt.startMic();
        }

        private void inviaMessaggio() {
            String msg = txtBox.Text;
            txtBox.Text = "";
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && e.Modifiers != Keys.Shift) {
                inviaMessaggio();
                e.Handled = true;
            }
        }

        private void rmBtn_Click(object sender, EventArgs e) {
            rimuoviPersona("Kury");
        }

        private void joinBtn_Click(object sender, EventArgs e) {
            if (inStanza) {
                rimuoviPersona(nickUtente);
                rcv.keepAlive = false;

                VBAN_Transmitter.wi.StopRecording();

                trd.Abort();
                trd2.Abort();

                joinBtn.Text = "Entra";
                inStanza = false;
                settingsBtn.Enabled = true;
            } else {
                //Aggiungo a interfaccia
                aggiungiPersona(nickUtente);
                utenti.Add(nickUtente);
                settingsBtn.Enabled = false;
                rcv = new VBAN_Receiver();
                trd = new Thread(new ThreadStart(startListener));

                vt = new VBAN_Transmitter();
                trd2 = new Thread(new ThreadStart(startTransmitter));

                trd.IsBackground = true;
                trd.Start();
                trd2.IsBackground = true;
                trd2.Start();

                Console.WriteLine("Thread started");
                joinBtn.Text = "Esci";
                inStanza = true;
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e) {
            Opzioni frm = new Opzioni();
            frm.ShowDialog();

            vus.defaultGUID = OUT_ID;
            vus.defaultInputDevice = IN_ID;
            vus.Save();
        }

        private void muteBtn_Click(object sender, EventArgs e) {
            if (mutato) {
                VBAN_Receiver.volume = volPrecedente;
                vus.Volume = volPrecedente;
                vus.Save();
                mutato = false;
                muteAudioBtn.Text = "Muta audio";
            } else {
                mutato = true;
                volPrecedente = VBAN_Receiver.volume;
                VBAN_Receiver.volume = 0;
                vus.Volume = 0;
                vus.Save();
                muteAudioBtn.Text = "Smuta audio";
            }            
        }
    }
}
