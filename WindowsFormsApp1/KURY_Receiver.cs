using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WindowsFormsApp1 {
    public class VBAN_Receiver {
        UdpClient socket;
        BufferedWaveProvider bwf;
        IPEndPoint gEP;
        public static float volume;
        public bool keepAlive = true;
        int c = 0;
        string kuryString;
        WaveOut wo;
        DirectSoundOut dso;

        public VBAN_Receiver() {
            Console.WriteLine("Listener started");
            bwf = new BufferedWaveProvider(new WaveFormat(96000, 16, 2));
            bwf.BufferLength = 2560 * 24;
            bwf.DiscardOnBufferOverflow = true;
            wo = new WaveOut();
            Guid idout = Form1.OUT_ID;
            dso = new DirectSoundOut(idout);
            dso.Init(bwf);           
        }

        public void startListening() {
            socket = new UdpClient(6981);
            gEP = new IPEndPoint(IPAddress.Any, 6981);
            try {
                start();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                socket.Close();
                dso.Stop();
            }
        }

        private void start() {
            while (keepAlive) {
                try {
                    parsePacket(socket.Receive(ref gEP));
                } catch (IndexOutOfRangeException e) {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        private void parsePacket(Byte[] data) {
            //KURY string
            kuryString = Encoding.UTF8.GetString(data, 0, 4);
            if(!kuryString.Equals("KURY")) {
                throw new IndexOutOfRangeException("Not a KURY packet");
            }

            //Push audio from byte 4 (28 for voicemeeter)
            bwf.AddSamples(data, 4, data.Length-4);
            c++;

            if(c == 1) {
                dso.Play();
            }

            wo.Volume = volume;
        }
    }
}
