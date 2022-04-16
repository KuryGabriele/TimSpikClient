using NAudio.Wave;
using NAudio.Mixer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WindowsFormsApp1 {
    public class KURY_Receiver {
        public UdpClient socket; //Input socket
        IPEndPoint gEP; //Audio endpoint
        public static float volume; //Volume value
        public bool keepAlive = true; //false to stop receiver
        string kuryString; //stores the fist 4 bytes of every packet
        string serverAddress;
        int port;

        List<DirectSoundOut> audioOutputs;//Audio outputs for each user
        List<BufferedWaveProvider> audioSources;
        List<String> users; //Users nicknames
        List<WaveOut> userVolumes; //Used to change volume per user
        List<float> volumes;

        public KURY_Receiver(List<String> users, String address, int port) {
            this.users = users;
            this.serverAddress = address;
            this.port = port;   
            audioSources = new List<BufferedWaveProvider> ();
            audioOutputs = new List<DirectSoundOut> ();
            userVolumes = new List<WaveOut> ();
            volumes = new List<float> ();

            foreach (String usr in this.users) {
                //Audio buffer initialization
                var buffer = new BufferedWaveProvider(new WaveFormat(48000, 16, 2));
                buffer.BufferLength = 2560 * 24;
                buffer.DiscardOnBufferOverflow = true;

                audioSources.Add(buffer);

                //Audio output initialization
                var audioOut = new WaveOut();
                Guid deviceID = Form1.OUT_ID;
                var audioDevice = new DirectSoundOut(deviceID);
                audioDevice.Init(buffer);

                audioOutputs.Add(audioDevice);
                userVolumes.Add(audioOut);
            }
        }
        
        public void changeVolume(string nick, float volume) {
            int index = users.FindIndex(x => x.StartsWith(nick));
            userVolumes.ElementAt(index).Volume = volume;
        }

        public float getVolume(string nick) {
            int index = users.FindIndex(x => x.StartsWith(nick));
            return userVolumes.ElementAt(index).Volume;
        }

        public void startListening() {
            //Open socket
            socket = new UdpClient(port);
            //Prepare endpoint
            gEP = new IPEndPoint(IPAddress.Parse(serverAddress), port);
            try {
                //Start getting packets
                start();
                socket.Close();
                socket.Dispose();
            } catch (Exception e) {
                //If error close everything
                Console.WriteLine(e.Message);
                socket.Close();
                for (int i = 0; i < audioOutputs.Count; i++) {
                    audioOutputs.ElementAt(i).Stop();
                }
            }
        }

        private void start() {
            while (keepAlive) {
                try {
                    //Get packet from endpoint
                    parsePacket(socket.Receive(ref gEP));
                } catch (IndexOutOfRangeException e) {
                    //If error skip
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        private void parsePacket(Byte[] data) {
            //Verify if packet is a KURY packet
            kuryString = Encoding.UTF8.GetString(data, 0, 4);
            if(!kuryString.Equals("KURY")) {
                throw new IndexOutOfRangeException("Not a KURY packet");
            }

            //Get users nickname
            var streamNick = Encoding.UTF8.GetString(data, 4, 16);
            streamNick = streamNick.Trim();
            int index = users.FindIndex(x => x.StartsWith(streamNick));
            //Push audio to right device
            audioSources.ElementAt(index).AddSamples(data, 20, data.Length - 20);
            //Start the audio output
            audioOutputs.ElementAt(index).Play();
        }
    }
}
