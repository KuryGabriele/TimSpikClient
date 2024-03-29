﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Media;

namespace WindowsFormsApp1 {
    public class KURY_Receiver {
        public UdpClient socket; //Input socket
        IPEndPoint gEP; //Audio endpoint
        public static float volume; //Volume value
        public bool keepAlive = true; //false to stop receiver
        string kuryString; //stores the fist 4 bytes of every packet
        string serverAddress;
        private int sr;
        private int bits;
        private int channels;
        int port;

        List<DirectSoundOut> audioOutputs;//Audio outputs for each user
        List<BufferedWaveProvider> audioSources;
        List<String> users; //Users nicknames
        List<WaveOut> userVolumes; //Used to change volume per user
        List<float> volumes;
        Form1 form;

        public KURY_Receiver(List<String> users, String address, int port, Form1 form, int sr, int bits, int channels) {
            this.users = users;
            this.serverAddress = address;
            this.port = port;   
            this.form = form;
            this.sr = sr;
            this.bits = bits;
            this.channels = channels;

            audioSources = new List<BufferedWaveProvider> ();
            audioOutputs = new List<DirectSoundOut> ();
            userVolumes = new List<WaveOut> ();
            volumes = new List<float> ();

            foreach (String usr in this.users) {
                //Audio buffer initialization
                var buffer = new BufferedWaveProvider(new WaveFormat(sr, bits, channels));
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

            playSoundQueue("connected.wav");
        }

        public void playSoundQueue(string filename) {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Volume = 0.2;
            mediaPlayer.Open(new Uri(Form1.tsPath + "\\audio\\" + Form1.soundPack + "\\" + filename));
            mediaPlayer.Play();
        }

        public void changeVolume(string nick, float volume) {
            int index = users.FindIndex(x => x.StartsWith(nick));
            if(index != -1) {
                userVolumes.ElementAt(index).Volume = volume;
            }
        }

        public float getVolume(string nick) {
            int index = users.FindIndex(x => x.StartsWith(nick));
            return userVolumes.ElementAt(index).Volume;
        }

        public void startListening() {
            //Open socket
            socket = new UdpClient(port);
            //Prepare endpoint
            gEP = new IPEndPoint(IPAddress.Any, port);
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

        private async void parsePacket(Byte[] data) {
            //Verify if packet is a KURY packet
            kuryString = Encoding.UTF8.GetString(data, 0, 4);
            if(kuryString.Equals("KURY")) {
                //Get users nickname
                var streamNick = Encoding.UTF8.GetString(data, 4, 16);
                streamNick = streamNick.Trim();
                //Find the right index for the player
                int index = users.FindIndex(x => x.StartsWith(streamNick));
                //If found push audio
                if(index >= 0) {
                    //Push audio to right device
                    audioSources.ElementAt(index).AddSamples(data, 20, data.Length - 20);
                    //Start the audio output if it has at least 3 packets already, if not
                    //audio will not play for some reason
                    if (audioSources.ElementAt(index).BufferedBytes >= data.Length * 3) {
                        audioOutputs.ElementAt(index).Play();
                    }
                }
            } else if (kuryString.Contains("JOIN") || kuryString.Contains("QUIT")) {
                //Update ui
                Action safeUpdate = delegate { form.updateOnlineUsers(); };
                form.Invoke(safeUpdate);

                //Find sender
                var streamNick = Encoding.UTF8.GetString(data, 4, 16);
                streamNick = streamNick.Trim();
                int index = users.FindIndex(x => x.StartsWith(streamNick));

                if (kuryString.Contains("JOIN")) {
                    if (index < 0) {
                        //Add user to list
                        users.Add(streamNick);

                        //Create a buffer
                        var buffer = new BufferedWaveProvider(new WaveFormat(sr, bits, channels));
                        buffer.BufferLength = 2560 * 24;
                        buffer.DiscardOnBufferOverflow = true;
                        audioSources.Add(buffer);

                        //Initialize audio output
                        var audioOut = new WaveOut();
                        Guid deviceID = Form1.OUT_ID;
                        var audioDevice = new DirectSoundOut(deviceID);
                        audioDevice.Init(buffer);

                        //Add to list
                        audioOutputs.Add(audioDevice);
                        userVolumes.Add(audioOut);
                        playSoundQueue("user_enter.wav");
                    }
                } else {
                    if (index >= 0) {
                        //Remove user
                        playSoundQueue("user_left.wav");
                        users.RemoveAt(index);
                        audioSources.RemoveAt(index);
                        audioOutputs.ElementAt(index).Stop();
                        audioOutputs.ElementAt(index).Dispose();
                        audioOutputs.RemoveAt(index);
                        userVolumes.RemoveAt(index);
                    }
                }
            } else {
                throw new IndexOutOfRangeException("Not a KURY packet");
            }
        }
    }
}
