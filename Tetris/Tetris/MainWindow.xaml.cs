using System;
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
			
			CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			writeableBitmap.Clear();
			tetrisGame.tick();
			tetrisGame.nextState();
			tetrisGame.render();
		}
	}
}
