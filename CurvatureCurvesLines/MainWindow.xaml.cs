using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using Emgu.CV.Util;

namespace CurvatureCurvesLines
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Mat _frame;
        private Mat _grayFrame;
        private Mat _smallGrayFrame;
        private Mat _smoothedGrayFrame;
        private Mat _cannyFrame;
        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            CurvatureDraw mine = new CurvatureDraw();
            ImageContours imgCont = new ImageContours();
            Image<Bgr, byte> img;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png|TIFF files (*.tif)|*tif|JPEG files (*.jpg)|*.jpg |All files (*.*)|*.*";
            ofd.FilterIndex = 5;
            ofd.RestoreDirectory = true;
            BitmapImage bmb = new BitmapImage();
            MemoryStream strm = new MemoryStream();
            BitmapImage bmImage = new BitmapImage();
            if (ofd.ShowDialog() == true)
                /////////////////////////
                
            {

                try
                {

                    MessageBox.Show("it is OK");
                    img = new Image<Bgr, byte>(ofd.FileName);
                  
                    bmImage.BeginInit();
                    bmImage.UriSource = new Uri(ofd.FileName, UriKind.Absolute);
                    bmImage.EndInit();
                   
                    original.Height = 400;
                    original.Width = 400;
                    original.Source = bmImage;
                    _frame = img.Mat;
                 //CvInvoke.CvtColor(_frame, _grayFrame, ColorConversion.Bgr2Gray);

                 //   CvInvoke.PyrDown(_grayFrame, _smallGrayFrame);

                 //   CvInvoke.PyrUp(_smallGrayFrame, _smoothedGrayFrame);
                   Image<Gray, byte> cannyCV ;
                    //   CvInvoke.Canny(_smoothedGrayFrame, _cannyFrame, 100, 60);
                   
                    cannyCV= img.Canny(20, 100);
                   
                    
                   
                    canny.Width = 400;
                    canny.Height = 400;
                    canny.Source = BitmapSourceConvert.ToBitmapSource(cannyCV);
                   
                   // mine.convertToCurves(cannyCV,ofd.FileName);
                   // IInputOutputArray x = cannyCV;
                   // IInputOutputArray y =cannyCV;
                   //Mat yy;
                   // ImageContours.FindLargestContour(x, y);
                   // yy = (Mat)y;
                   // System.Drawing.Bitmap bmp ;
                   // bmp = yy.Bitmap;
                   // bmp.Save(strm, System.Drawing.Imaging.ImageFormat.Bmp);
                   // BitmapImage imgB = new BitmapImage();
                   // imgB.BeginInit();
                   // imgB.StreamSource = strm;
                   // imgB.EndInit();
                    filePlot.Width = 400;
                    filePlot.Height = 400;
                    

                    MessageBox.Show("It is Done, Check Values");
                    filePlot.Source = mine.plotImage(ofd.FileName, cannyCV);

                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                }
        
               
            }

        }
    }
}
