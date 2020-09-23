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
using Windows.UI;
using System.Security.Cryptography;
using SharpDX.XAudio2;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{

    public class RenderingArena
    {
        Rect[,] arenaTile;
        readonly int TileSize;
        Random rnd;

        public RenderingArena(int _x, int _y,int _tileSize)
        {
            arenaTile = new Rect[_x, _y];
            TileSize = _tileSize;
            Init();
        }

        private void Init()
        {
            for (int x = 0; x < arenaTile.GetLength(0); x++)
            {
                for (int y = 0; y < arenaTile.GetLength(1); y++)
                {
                    var temp =new Rect(
                        TileSize * x,
                        TileSize * y,
                        TileSize,
                        TileSize
                        );
                    arenaTile[x, y] = temp;
                }
            }
            rnd = new Random();
        }
        public void Render(ref Scenario1ImageSource _scene)
        {
            for (int x = 0; x < arenaTile.GetLength(0); x++)
            {
                for (int y = 0; y < arenaTile.GetLength(1); y++)
                {
                    var color = Color.FromArgb
                        (
                        (byte)rnd.Next(0, 255),
                        (byte)rnd.Next(0, 255),
                        (byte)rnd.Next(0, 255),
                        (byte)rnd.Next(0, 255)
                        );
                    _scene.FillSolidRect(color, arenaTile[x, y]);
                }
            }
        }


    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : LayoutAwarePage
    {
        // A pointer back to the main page.  This is needed if you want to call methods in MainPage such
        // as NotifyUser()
        MainPage rootPage = MainPage.Current;

        public DispatcherTimer Timer { get; private set; }
        
        // An image source derived from SurfaceImageSource, used to draw DirectX content
        private Scenario1ImageSource Scenario1Drawing;
        private RenderingArena arena;
        public BlankPage1()
        {
            this.InitializeComponent();
            arena = new RenderingArena(10, 200, 50);
            Scenario1Drawing = new Scenario1ImageSource(500, 1000, true);
            Image1.Source = Scenario1Drawing;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Scenario1DrawRectangles_Click(object sender, RoutedEventArgs e)
        {
            if (Timer is null)
                StartTimer();
            else
                Stop();
        }
        
      
        private void StartTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            Timer.Tick += timer_Tick;
            Timer.Start();
        }

        private void Stop()
        {
            Timer.Stop();
            Timer = null;
        }

        private void timer_Tick(object sender, object e)
        {
            DrawFrame();
        }

        private void DrawFrame()
        {
            // Begin updating the SurfaceImageSource
            Scenario1Drawing.BeginDraw();
            Scenario1Drawing.Clear(Colors.Red);

            arena.Render(ref Scenario1Drawing);
            Scenario1Drawing.EndDraw();
        }
    }
}
