﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Audio;
using Windows.Media.Render;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace awesomeAlphabetThingy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Dictionary<char, int> letters;
        List<char> keys;
        MediaElement mediaElement = new MediaElement();
        char current;
        int counter = 0;
        Boolean playAgain;
        Boolean currentlyPlaying = true; 

        public MainPage()
        {
            this.InitializeComponent();
               SetupBackground();
            Setup();
            Intro();
        }
          public void SetupBackground()
          {
               BitmapImage background;
               ImageBrush brush = new ImageBrush();
               brush.Stretch = Stretch.UniformToFill;
               brush.AlignmentX = AlignmentX.Left;
               brush.AlignmentY = AlignmentY.Top;
               int christmasM =  12 ;
               int halloweenM =  11 ;
               int[] winterM = { 1, 2 };
               int[] autumnM = { 9, 10 };
               int[] summerM = { 6, 7, 8 };
               int[] springM = { 3, 4, 5 };

               int month = DateTime.Today.Month;
               
               background = new BitmapImage(new Uri(BaseUri, "/Seasons/christmas.jpg"));
               
               if(month == halloweenM)
               {

               }
               else if (winterM.Contains(month))
               {
                    background = new BitmapImage(new Uri(BaseUri, "/Seasons/winter.jpg"));
               }
               else if (autumnM.Contains(month))
               {
                    background = new BitmapImage(new Uri(BaseUri, "/Seasons/autumn.jpg"));
               }
               else if (summerM.Contains(month))
               {
                    background = new BitmapImage(new Uri(BaseUri, "/Seasons/summer.jpg"));
               }
               brush.ImageSource = background;
               BackgroundImg.Background = brush;


          }
        public async void Intro()
        {
            await Task.Delay(2000);
            Say("Hello and welcome to the awesome alphabet thingy!");
            await Task.Delay(3000);
            Say("Let us start learning some letters together!");
            await Task.Delay(3000);
            Say("Let's begin!");
            await Task.Delay(2000);
            this.currentlyPlaying = false;
            Next();
        }
        public async void Next()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, keys.Count - 1);
            letterBox.Text = "?";
            this.current = keys[index];
            this.currentlyPlaying = true;
            Play(current);
        }

        public async void Play(char current)
        {

            var mp3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///sounds/" + this.current.ToString() + ".mp3"));
            var player = new AudioPlayer();
            await player.LoadFileAsync(mp3);
            player.Play(this.current.ToString() + ".mp3", 0.5);
            await Task.Delay(2000);
            this.currentlyPlaying = false;

        }

        public void Setup()
        {
            this.keys = new List<char>();
            this.letters = new Dictionary<char, int>
        {
            {'a', 2}, {'b', 2}, {'c', 2}, {'d', 2}, {'e', 2}, {'f', 2}, {'g', 2}, {'h', 2}, {'i', 2}, {'j', 2},
            {'k', 2}, {'l', 2}, {'m', 2}, {'n', 2}, {'o', 2}, {'p', 2}, {'q', 2}, {'r', 2}, {'s', 2}, {'t', 2},
            {'u', 2}, {'v', 2}, {'w', 2}, {'x', 2}, {'y', 2}, {'z', 2}
        };
            foreach (KeyValuePair<char, int> pair in this.letters)
            {
                keys.Add(pair.Key);
            }
        }
          public async void Say(string letter)
          {
               var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
               Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(letter);
               mediaElement.SetSource(stream, stream.ContentType);
               mediaElement.Play();
          }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentlyPlaying)
            {
                return;
            }
            var button = (Button)sender;
            checkLetter(button.Content.ToString().ToCharArray()[0]);

        }

        public async void checkLetter(char letter)
        {
            this.currentlyPlaying = true;
            letterBox.Text = current.ToString().ToUpper();
            if (letter == Char.ToUpper(current))
            {
                if (letters[current] == 1)
                {
                    letters.Remove(current);
                    keys.Remove(current);
                }
                else {
                    letters[current] -= 1;
                }
                if (this.counter <= 0)
                {
                    this.counter = 1;
                    Say("Good!");
                }
                else if (this.counter == 1)
                {
                    this.counter++;
                    Say("Super!");
                }
                else if (this.counter > 3)
                {
                    this.counter++;
                    Say("Magnificent!");
                }
                else
                {
                    this.counter++;
                    Say("You are on a roll!");
                }
            }
            else {
                if (this.counter >= 0)
                {
                    this.counter = -1;
                    Say("Incorrect!");
                }
                else if (this.counter == -1)
                {
                    this.counter--;
                    Say("You have to keep practicing");
                }
                else if (this.counter < -3)
                {
                    this.counter--;
                    Say("I will help you get these right");
                }
                else
                {
                    this.counter--;
                    Say("Don't be sad! You will get better");
                }

            }


            await Task.Delay(3000);
            this.currentlyPlaying = false;
            Next();
        }

    private void playAgainButton_Click(object sender, RoutedEventArgs e)
        {
            this.currentlyPlaying = true;
        Play(current);
            this.currentlyPlaying = false;
        }
    }


    public class AudioPlayer
    {
        private AudioGraph _graph;
        private Dictionary<string, AudioFileInputNode> _fileInputs = new Dictionary<string, AudioFileInputNode>();
        private AudioDeviceOutputNode _deviceOutput;

        public async Task LoadFileAsync(IStorageFile file)
        {
            if (_deviceOutput == null)
            {
                await CreateAudioGraph();
            }

            var fileInputResult = await _graph.CreateFileInputNodeAsync(file);

            _fileInputs.Add(file.Name, fileInputResult.FileInputNode);
            fileInputResult.FileInputNode.Stop();
            fileInputResult.FileInputNode.AddOutgoingConnection(_deviceOutput);
        }

        public void Play(string key, double gain)
        {
            var sound = _fileInputs[key];
            sound.OutgoingGain = gain;
            sound.Seek(TimeSpan.Zero);
            sound.Start();
        }

        private async Task CreateAudioGraph()
        {
            var settings = new AudioGraphSettings(AudioRenderCategory.Media);
            var result = await AudioGraph.CreateAsync(settings);
            _graph = result.Graph;
            var deviceOutputNodeResult = await _graph.CreateDeviceOutputNodeAsync();
            _deviceOutput = deviceOutputNodeResult.DeviceOutputNode;
            _graph.ResetAllNodes();
            _graph.Start();
        }
    }
}
