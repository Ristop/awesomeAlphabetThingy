using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace awesomeAlphabetThingy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Dictionary<char, int> letters;

        public MainPage()
        {
            this.InitializeComponent();
            Setup();
            Intro();
        }
        public void Intro()
        {

        }
        public void Setup()
        {
            this.letters = new Dictionary<char, int>
        {
            {'a', 4}, {'b', 4}, {'c', 4}, {'d', 4}, {'e', 4}, {'f', 4}, {'g', 4}, {'h', 4}, {'i', 4}, {'j', 4},
            {'k', 4}, {'l', 4}, {'m', 4}, {'n', 4}, {'o', 4}, {'p', 4}, {'q', 4}, {'r', 4}, {'s', 4}, {'t', 4},
            {'u', 4}, {'v', 4}, {'w', 4}, {'x', 4}, {'y', 4}, {'z', 4}
        };
        }


        
    }
    }
