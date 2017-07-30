using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		Random random = new Random();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void ViewPort_Loaded(object sender, RoutedEventArgs e)
		{
			width = (int)this.ViewPortContainer.ActualWidth;
			height = (int)this.ViewPortContainer.ActualHeight;
			writeableBitmap = BitmapFactory.New(width, height);
			ViewPort.Source = writeableBitmap;
			CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			writeableBitmap.DrawEllipseCentered(
										random.Next(width),
										random.Next(height),
										20,
										20,
										Color.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)));
		}
	}
}
