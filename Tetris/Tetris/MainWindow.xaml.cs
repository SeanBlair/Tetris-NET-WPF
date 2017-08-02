using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Tetris
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		int width, height;
		WriteableBitmap writeableBitmap;
		TetrisGame tetrisGame;
		int gameSpeed;
		DispatcherTimer timer;

		public MainWindow()
		{
			InitializeComponent();
			gameSpeed = 1000;

			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(tickGame);
			timer.Interval = new TimeSpan(0, 0, 0, 0, gameSpeed);
			timer.Start();
			
			this.KeyDown += new KeyEventHandler(OnKeyDown);
		}

		private void tickGame(object sender, EventArgs e)
		{
			tetrisGame.changeState();
			tetrisGame.render();
			this.tbScore.Text = "Score is: " + tetrisGame.score;
			adjustGameSpeed(tetrisGame.score);
		}

		private void adjustGameSpeed(int score)
		{
			if (gameSpeed > 200)
			{
				var factor = score / 10;
				gameSpeed = 1000 - factor * 200;
			}
			timer.Interval = new TimeSpan(0, 0, 0, 0, gameSpeed);
			timer.Start();
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.Left))
			{
				tetrisGame.moveLeft();
			}
			else if (Keyboard.IsKeyDown(Key.Right))
			{
				tetrisGame.moveRight();
			}
			else if (Keyboard.IsKeyDown(Key.Down))
			{
				tetrisGame.changeState();
			}
			else if (Keyboard.IsKeyDown(Key.Up))
			{
				tetrisGame.rotate();
			}
			tetrisGame.render();
		}

		private void ViewPort_Loaded(object sender, RoutedEventArgs e)
		{
			// TODO should they be width vs ActualWidth??
			width = (int)this.ViewPortContainer.ActualWidth;
			height = (int)this.ViewPortContainer.ActualHeight;
			writeableBitmap = BitmapFactory.New(width, height);
			ViewPort.Source = writeableBitmap;
			tetrisGame = new TetrisGame(writeableBitmap, 20);
			tetrisGame.init();
		}
	}
}
