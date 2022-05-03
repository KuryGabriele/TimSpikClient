using NAudio.Wave;
using System;
using System.Net.Sockets;
using System.Text;

namespace WindowsFormsApp1 {
    public class KURY_Transmitter {
        public static UdpClient socket;
        public static WaveIn wi;
        public static float volume; //Not used
        private int port = 6981;
        private string ipAddr = "192.168.1.3";
        private string nick;
        private int sr;
        private int bits;
        private int channels;
        public bool muted = false;

        public KURY_Transmitter(string ipAddr, int port, string nick, int sr, int bits, int channels) {
            //Audio stuff
            Console.WriteLine("Transmitter started");
            this.ipAddr = ipAddr;
            this.port = port;
            this.sr = sr;
            this.bits = bits;
            this.channels = channels;

            wi = new WaveIn();
            wi.WaveFormat = new WaveFormat(sr, bits, channels);
            wi.DeviceNumber = 0;

            wi.DataAvailable += new EventHandler<WaveInEventArgs>(sendData);

            this.nick = nick;
        }

        public void startMic() {
            try {
                socket = new UdpClient(ipAddr, port);

                byte[] packet = new byte[20];
                var b = Encoding.UTF8.GetBytes("JOIN".ToCharArray(), 0, 4);
                b.CopyTo(packet, 0);

                var nickChars = nick.ToCharArray();
                var nickByte = Encoding.UTF8.GetBytes(nickChars, 0, nickChars.Length);
                nickByte.CopyTo(packet, 4);

                var space = new char[1];
                space[0] = '\t';

                var spacesBytes = Encoding.UTF8.GetBytes(space, 0, 1);
                for (int i = nick.Length+4; i < 16; i++) {
                    spacesBytes.CopyTo(packet, i);
                }

                socket.Send(packet, packet.Length);
                wi.StartRecording();
            } catch (Exception e) {
                wi.StopRecording();
                socket.Close();
            }
            
        }

        void sendData(object sender, WaveInEventArgs e) {
            //KURY string
            var b = Encoding.UTF8.GetBytes("KURY".ToCharArray(), 0, 4);

            var n = new byte[16];
            var nickChars = nick.ToCharArray();
            var nickByte = Encoding.UTF8.GetBytes(nickChars, 0, nickChars.Length);
            nickByte.CopyTo(n, 0);

            var space = new char[1];
            space[0] = '\t';

            var spacesBytes = Encoding.UTF8.GetBytes(space, 0, 1);
            for (int i = nick.Length; i < 16; i++) {
                spacesBytes.CopyTo(n, i);
            }

            //Audio data
            byte[] packet = new byte[b.Length + n.Length + e.Buffer.Length];
            Buffer.BlockCopy(b, 0, packet, 0, b.Length);
            Buffer.BlockCopy(n, 0, packet, 4, n.Length);

            if (!muted) { 
                Buffer.BlockCopy(e.Buffer, 0, packet, b.Length + n.Length, e.Buffer.Length);
            }
            //Send
            socket.Send(packet, packet.Length);
        }

        public async void sendQuit() {
            byte[] packet = new byte[20];
            var b = Encoding.UTF8.GetBytes("QUIT".ToCharArray(), 0, 4);
            b.CopyTo(packet, 0);

            var nickChars = nick.ToCharArray();
            var nickByte = Encoding.UTF8.GetBytes(nickChars, 0, nickChars.Length);
            nickByte.CopyTo(packet, 4);

            var space = new char[1];
            space[0] = '\t';

            var spacesBytes = Encoding.UTF8.GetBytes(space, 0, 1);
            for (int i = nick.Length+4; i < 16; i++) {
                spacesBytes.CopyTo(packet, i);
            }

            socket.Send(packet, packet.Length);
        }
    }
}
