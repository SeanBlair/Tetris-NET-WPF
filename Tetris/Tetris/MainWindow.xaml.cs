﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

		public MainWindow()
		{
			InitializeComponent();

			DispatcherTimer timer = new DispatcherTimer();
			timer.Tick += new EventHandler(MoveSquare);

			timer.Interval = new TimeSpan(0, 0, 0, 0, 500);

			timer.Start();
		}

		// I think this method has to be called in this class...
		private void MoveSquare(object sender, EventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.Left))
			{
				tetrisGame.theSquare.x -= 10;
			}
			else if (Keyboard.IsKeyDown(Key.Right))
			{
				tetrisGame.theSquare.x += 10;
			}
			else
			{
				tetrisGame.theSquare.y += 10;
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
			tetrisGame = new TetrisGame(writeableBitmap);
			tetrisGame.init();
		}
	}
}
