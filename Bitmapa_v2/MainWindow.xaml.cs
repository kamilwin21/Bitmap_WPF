using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Drawing;
using Color = System.Drawing.Color;

namespace Bitmapa_v2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
           

            if ((bool)dlg.ShowDialog())
            {
                label1.Content = dlg.FileName;

                //ImageSource imageSource = new BitmapImage(new Uri(dlg.FileName));
                //image1.Source = imageSource;
                Bitmap bm = new Bitmap(dlg.FileName);
                image1.Source= Convert(bm);
                Brightness(bm, dlg);
                Contrast(bm,dlg);



            }
        }

        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public void Brightness(Bitmap bm, OpenFileDialog dlg)
        {

            Bitmap brightness = new Bitmap(dlg.FileName);
            int value_slider = (int)slider1.Value;
            label2.Content = slider1.Value;
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    Color color = bm.GetPixel(x, y);
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    //double s = 0.299 * r + 0.587 * g + 0.114 * b;
                    r += value_slider;
                    g += value_slider;
                    b += value_slider;
                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;

                    brightness.SetPixel(x, y, Color.FromArgb(r, g, b));


                }

            }
            image2.Source = Convert(brightness);

        }


        public void Contrast(Bitmap bm, OpenFileDialog dlg)
        {
            Bitmap contrast = new Bitmap(dlg.FileName);

            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {

                    Color color = bm.GetPixel(x, y);
                    int value_slider2 = (int)slider2.Value;
                    label3.Content = value_slider2;
                   

                    double r = ((((color.R / 255.0) - 0.5) * value_slider2) + 0.5) * 255.0;
                    double g = ((((color.G / 255.0) - 0.5) * value_slider2) + 0.5) * 255.0;
                    double b = ((((color.B / 255.0) - 0.5) * value_slider2) + 0.5) * 255.0;

                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;

                    contrast.SetPixel(x, y, Color.FromArgb((int)r,(int)g,(int)b));

                }
            }
            image3.Source = Convert(contrast);
        }
    }
}
