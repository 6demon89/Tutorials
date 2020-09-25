using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    public class GameField
    {
        public int[,] field = new int[10, 6];
        public Rect tileSize = new Rect(new Point(16, 16), new Size(16, 16));
        public GameField()
        {

        }
    }

    public class Enemy
    {
        readonly int row;
        public int column;
        public Enemy(int _row)
        {
            row = _row * 75;
            column = 0;
        }
        public void Draw(CanvasDrawEventArgs _args, CanvasBitmap _bitmap)
        {
            _args.DrawingSession.DrawImage(_bitmap, column, row);
        }

        public void Move(int _column) => column = _column;
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Win2DPage : LayoutAwarePage
    {
        private GameField field = new GameField();

        private List<Enemy> Enemies = new List<Enemy>();
        private CanvasBitmap enemyBitmap;

        public DispatcherTimer DrawTimer { get; private set; }
        public DispatcherTimer UpdateTimer { get; private set; }

        public Win2DPage()
        {
            this.InitializeComponent();

            // Set the score to zero - the game doesn't currently check that all dots are eaten!
            // But if this is 122, the player has cleared the level.
            //player.Score = 0;

            // Start the timer going - this will update the display and move the characters around.
            // When the baddies catch the player, the timer is stopped. In a real game, the "game over"
            // message would be displayed.

            DrawTimer = new DispatcherTimer();
            DrawTimer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            DrawTimer.Tick += Redraw_Tick;
            DrawTimer.Start();

            UpdateTimer = new DispatcherTimer();
            UpdateTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            UpdateTimer.Tick += Physics_Tick;
            UpdateTimer.Start();
        }

        private void Physics_Tick(object sender, object e)
        {
            throw new NotImplementedException();
        }

        private void Redraw_Tick(object sender, object e)
        {
            // Draw everything by making the canvas "invalid", triggering the redraw.
            canvas.Invalidate();
        }

        Random rnd = new Random();
      
        private Color RndColor()
        {
            var c = new Color();
            c.G = (byte)rnd.Next(230, 256);
            c.B = 0;
            c.R = 0;
            c.A = 255;
            return c;
        }

        // This is the Draw method used by the CanvasControl. The CanvasControl gives us the ability
        // to quickly draw images, other shapes and text. 
        void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            CanvasCommandList cl = new CanvasCommandList(sender);
            using (CanvasDrawingSession clds = cl.CreateDrawingSession())
            {
                for (int i = 0; i < 100; i++)
                {
                    for (int x = 0; x < field.field.GetLength(0); x++)
                    {
                        for (int y = 0; y < field.field.GetLength(1); y++)
                        {
                            clds.Antialiasing = CanvasAntialiasing.Antialiased;
                            clds.FillRectangle(x * (0 + 75), y * (0 + 75), 70, 70, RndColor());
                        }
                    }
                }
            }
            args.DrawingSession.DrawImage(cl);

            if (Enemies.Count < 2)
            {
                Enemies.Add(new Enemy(rnd.Next(0, 6)));
                Enemies.Add(new Enemy(rnd.Next(0, 6)));
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw(args, enemyBitmap);
                if (Enemies[i].column > 600)
                    Enemies.RemoveAt(i);
            }
        }



        // This is a method called by the CanvasControl, and we use it to call the routine that loads the graphics
        // for the player and baddies.
        private void canvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        // Load the images. Only needs doing once, when the CanvasControl is initialized.
        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            enemyBitmap = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/ninjacat.png"));
        }

        // The CanvasControl calls this method when the keyboard is pressed. 
        private void canvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            int d = 0;

            switch (e.Key)
            {
                case Windows.System.VirtualKey.W: d = 1; break;
                case Windows.System.VirtualKey.D: d = 2; break;
                case Windows.System.VirtualKey.S: d = 4; break;
                case Windows.System.VirtualKey.A: d = 8; break;
                default: d = 0; break;
            }
        }

        // A 'clean up' method required by the CanvasConrol
        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }
    }

}
