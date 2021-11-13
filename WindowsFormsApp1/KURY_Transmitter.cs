using NAudio.Wave;
using System;
using System.Net.Sockets;
using System.Text;

namespace WindowsFormsApp1 {
    public class VBAN_Transmitter {
        public static UdpClient socket;
        public static WaveIn wi;
        public static float volume;
        private const int PORT = 6981;
        private const string IP = "127.0.0.1";

        public VBAN_Transmitter() {
            //Audio stuff
            Console.WriteLine("Transmitter started");
            wi = new WaveIn();
            wi.WaveFormat = new WaveFormat(96000, 16, 2);
            wi.DeviceNumber = 0;

            wi.DataAvailable += new EventHandler<WaveInEventArgs>(sendData);
        }

        public void startMic() {
            try {
                socket = new UdpClient(IP, PORT);
                wi.StartRecording();
            } catch (Exception e) {
                wi.StopRecording();
                socket.Close();
            }
            
        }

        void sendData(object sender, WaveInEventArgs e) {
            //KURY string
            byte[] b = Encoding.UTF8.GetBytes("KURY".ToCharArray(), 0, 4);
            //Audio data
            byte[] packet = new byte[b.Length + e.Buffer.Length];
            Buffer.BlockCopy(b, 0, packet, 0, b.Length);
            Buffer.BlockCopy(e.Buffer, 0, packet, b.Length, e.Buffer.Length);
            //Send
            socket.Send(packet, packet.Length);
        }
    }
}
