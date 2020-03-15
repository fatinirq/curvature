using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace CurvatureCurvesLines
{
    public struct LinkedListData
    {
        public int pointSeq;
        public Point point;
        public double data;
        public double defX;
        public double defY;
        public double doubDefX;
        public double doubDefY;
        public double curvature;
    }
    class CurvatureDraw
    {
        public void convertToCurves(Image<Gray, byte> img, string filename)
        {

            MessageBox.Show("Height and Width and first data" + " " + img.Height.ToString() + " " + img.Width.ToString());
            int llCounter = 0;
            byte[,] passed = new byte[img.Rows , img.Cols ];
            //passed = null;
            LinkedListData lstTemp;
          
            List<LinkedListData>[] list = new List<LinkedListData>[img.Height * img.Width];
            List<LinkedListData>[] list2 = new List<LinkedListData>[img.Height * img.Width];
           
                    for (int i = 1; i < img.Rows - 1; i++)
                for (int j = 1; j < img.Cols - 1; j++)
                {

                    // passed[i, j] = img.Data[i, j, 0];
                    //// checking the two direction of the begining of the edge
                    bool chk = false, chl = false, chm = false, chn = false, loopkl = false, loopmn = false;
                    int k = 0, l = 0, m = 0, n = 0;
                    if (((img.Data[i, j, 0] == 255 && passed[i, j] != 255)))
                    {
                        llCounter++;
                        list[llCounter - 1] = new List<LinkedListData>();
                        list2[llCounter - 1] = new List<LinkedListData>();
                        passed[i, j] = img.Data[i, j, 0];
                        lstTemp = new LinkedListData();
                       
                        Point tempP = new Point(i, j);
                        lstTemp.pointSeq = 0;
                        lstTemp.point = tempP;
                        lstTemp.data = img.Data[k, l, 0];
                        lstTemp.defX = 0;
                        lstTemp.defY = 0;
                        lstTemp.doubDefX = 0;
                        lstTemp.doubDefY = 0;
                        lstTemp.curvature = 0;
                        list[llCounter - 1].Add(lstTemp);
                        list2[llCounter - 1].Add(lstTemp);
                        if ((img.Data[i + 1, j + 1, 0] == 255 && passed[i + 1, j + 1] != 255) ||  (img.Data[i, j + 1, 0] == 255 && passed[i, j + 1] != 255 ))
                        {
                            chl = true; chk = true;
                            k = i; l = j;
                            loopkl = true;
                        }
                        //    if (img.Data[i, j + 1, 0] == 255 && passed[i, j + 1] != 255)
                        //{
                        //    chl = false; chk = true;
                        //    k = i; l = j;
                        //    loopkl = true;
                        //}
                       
                        if ((img.Data[i + 1, j - 1, 0] == 255 && passed[i + 1, j - 1] != 255)|| (img.Data[i, j - 1, 0] == 255 && passed[i, j - 1] != 255))
                        {
                            chm = true; chn = false;
                            m = i; n = j;
                            loopmn = true;
                        }
                           //if  (img.Data[i, j - 1, 0] == 255 && passed[i, j - 1] != 255)
                        //{
                        //    chm = false; chn = false;
                        //    m = i; n = j;
                        //    loopmn = true;
                        //}

                        else
                            passed[i, j] = img.Data[i, j, 0];
                    }

                    while (loopmn || loopkl)
                    {
                        #region loopmn

                        if (loopmn && chm && chn)
                        {
                            // MessageBox.Show(list[llCounter - 1].Count.ToString());
                            int countLst = (int)list2[llCounter - 1].Count - 1;
                            if ( m + 1 < img.Rows && n + 1 < img.Cols && img.Data[m + 1, n + 1, 0] == 255 && passed[m + 1, n + 1] != 255)
                            {
                                m++; n++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                chm = true; chn = true;
                                
                                passed[m, n] = 255;
                            }

                            else if (n + 1 < img.Cols && img.Data[m, n + 1, 0] == 255 && passed[m, n + 1] != 255)
                            {
                                n++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;
                            }
                            else if ( m + 1 < img.Rows && img.Data[m + 1, n, 0] == 255 && passed[m + 1, n] != 255)
                            {
                                m++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;
                            }
                            else if (n - 1 > 0 && m + 1 < img.Rows && img.Data[m + 1, n - 1, 0] == 255 && passed[m + 1, n - 1] != 255)
                            {
                                m++; n--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);

                                chm = true; chn = false;
                               
                                passed[m, n] = 255;
                            }

                            else loopmn = false;
                        }
                        //////
                        if (loopmn && chm && !chn)
                        {
                            int countLst = (int)list2[llCounter - 1].Count - 1;
                            if ( n - 1 > 0 && m + 1 < img.Rows && img.Data[m + 1, n - 1, 0] == 255 && passed[m + 1, n - 1] != 255)
                            {
                                m++; n--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);

                                chm = true; chn = false;
                               
                                passed[m, n] = 255;
                            }
                            else if (m + 1 < img.Rows && img.Data[m + 1, n, 0] == 255 && passed[m + 1, n] != 255)
                            {
                                m++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;
                            }
                            else if (n - 1 > 0 && img.Data[m, n - 1, 0] == 255 && passed[m, n - 1] != 255  )
                            {
                                n--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;

                            }
                            else if ( m - 1 > 0 && n - 1 > 0 && img.Data[m - 1, n - 1, 0] == 255 && passed[m - 1, n - 1] != 255 )
                            {
                                m--; n--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);

                                chm = false; chn = false;
                               
                                passed[m, n] = 255;
                            }
                            else loopmn = false;
                        }
                        ////////////
                        if (loopmn && !chm && !chn)
                        {
                            int countLst = list2[llCounter - 1].Count - 1;
                            if (m - 1 > 0 && n - 1 > 0 && img.Data[m - 1, n - 1, 0] == 255 && passed[m - 1, n - 1] != 255)
                            {
                                m--; n--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);

                                chm = false; chn = false;
                               
                                passed[m, n] = 255;
                            }
                            else if (m - 1 > 0 && img.Data[m - 1, n, 0] == 255 && passed[m - 1, n] != 255)
                            {
                                m--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;

                            }
                            else if (n - 1 > 0 && img.Data[m, n - 1, 0] == 255 && passed[m, n - 1] != 255)
                            {
                                n--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;
                            }
                            else if (n + 1 < img.Cols && m - 1 > 0 && img.Data[m - 1, n + 1, 0] == 255 && passed[m - 1, n + 1] != 255 )
                            {
                                m--; n++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);

                                chn = true; chm = false;
                               
                                passed[m, n ] = 255;
                            }
                            else loopmn = false;
                        }
                        //////////////////
                        if (loopmn && !chm && chn)
                        {
                            int countLst = list2[llCounter - 1].Count - 1;
                            if (m - 1 > 0 && n + 1 < img.Cols && img.Data[m - 1, n + 1, 0] == 255 && passed[m - 1, n + 1] != 255)
                            {
                                m--; n++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);

                                chm = false; chn = true;
                                passed[m, n] = 255;
                            }
                            else if (m - 1 > 0 && img.Data[m - 1, n, 0] == 255 && passed[m - 1, n] != 255)
                            {
                                m--;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m , n] = 255;

                            }
                            else if (n + 1 < img.Cols && img.Data[m, n + 1, 0] == 255 && passed[m, n + 1] != 255)
                            {
                                n++;
                                lstTemp = new LinkedListData();

                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));

                                list2[llCounter - 1].Add(lstTemp);
                                passed[m, n] = 255;
                            }
                            else if ( m + 1 < img.Rows && n + 1 < img.Cols && img.Data[m + 1, n + 1, 0] == 255 && passed[m + 1, n + 1] != 255)
                            {
                                m++; n++;
                                lstTemp = new LinkedListData();
                              
                                lstTemp.point = new Point(m, n);
                                lstTemp.data = img.Data[m, n, 0];
                                lstTemp.pointSeq = list2[llCounter - 1][countLst].pointSeq - 1;
                                lstTemp.defX = lstTemp.point.X - list2[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list2[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list2[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list2[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                              
                                list2[llCounter - 1].Add(lstTemp);
                                chm = true;chn = true;
                                passed[m, n] = 255;
                            }

                            else loopmn = false;

                            ///////////////////////////////////////////



                            //MessageBox.Show(llCounter.ToString());

                        }///end of while
                        #endregion
                        #region loopkl    
                        ///find other neighbors

                        //chl && chk
                        if (loopkl && chl && chk)
                        {
                            // MessageBox.Show(list[llCounter - 1].Count.ToString());
                            int countLst = (int)list[llCounter - 1].Count - 1;
                            if ( k+1<img.Rows-1 && l + 1 < img.Cols-1 && img.Data[k + 1, l + 1, 0] == 255 && passed[k + 1, l + 1] != 255)
                            {
                                k++; l++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq+1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chk = true; chl = true;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }

                            else if (l + 1 < img.Cols && img.Data[k, l + 1, 0] == 255 && passed[k, l + 1] != 255 )
                            {
                                l++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (k + 1 < img.Rows && img.Data[k + 1, l, 0] == 255 && passed[k + 1, l] != 255 )
                            {
                                k++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (l - 1 > 0 && k + 1 < img.Rows && img.Data[k + 1, l - 1, 0] == 255 && passed[k + 1, l - 1] != 255  )
                            {
                                k++; l--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chk = true; chl = false;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }

                            else loopkl = false;
                        }
                        //////
                        if (loopkl && chk && !chl)
                        {
                            int countLst = (int)list[llCounter - 1].Count - 1;
                            if (l - 1 > 0 && k + 1 < img.Rows && img.Data[k + 1, l - 1, 0] == 255 && passed[k + 1, l - 1] != 255  )
                            {
                                k++; l--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chk = true; chl = false;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (k + 1 < img.Rows && img.Data[k + 1, l, 0] == 255 && passed[k + 1, l] != 255)
                            {
                                k++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (l - 1 > 0 && img.Data[k, l - 1, 0] == 255 && passed[k, l - 1] != 255)
                            {
                                l--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;

                            }
                            else if (k - 1 > 0 && l - 1 > 0 && img.Data[k - 1, l - 1, 0] == 255 && passed[k - 1, l - 1] != 255)
                            {
                                k--; l--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chl = false; chk = false;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else loopkl = false;
                        }
                        ////////////
                        if (loopkl && !chk && !chl)
                        {
                            int countLst = list[llCounter - 1].Count - 1;
                            if (k - 1 > 0 && l - 1 > 0 && img.Data[k - 1, l - 1, 0] == 255 && passed[k - 1, l - 1] != 255)
                            {
                                k--; l--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chk = false; chl = false;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (k - 1 > 0 && img.Data[k - 1, l, 0] == 255 && passed[k - 1, l] != 255 )
                            {
                                k--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;

                            }
                            else if (l - 1 > 0 && img.Data[k, l - 1, 0] == 255 && passed[k, l - 1] != 255)
                            {
                                l--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (k - 1 > 0 && l + 1 < img.Cols && img.Data[k - 1, l + 1, 0] == 255 && passed[k - 1, l + 1] != 255)
                            {
                                k--; l++;

                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chl = true; chk = false;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else loopkl = false;
                        }
                        //////////////////
                        if (loopkl && !chk && chl)
                        {
                            int countLst = list[llCounter - 1].Count - 1;
                            if (k - 1 > 0 && l + 1 < img.Cols && img.Data[k - 1, l + 1, 0] == 255 && passed[k - 1, l + 1] != 255 )
                            {
                                k--; l++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                chk = false; chl = true;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (k - 1 > 0 && img.Data[k - 1, l, 0] == 255 && passed[k - 1, l] != 255)
                            {
                                k--;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;

                            }
                            else if (l + 1 < img.Cols && img.Data[k, l + 1, 0] == 255 && passed[k, l + 1] != 255)
                            {
                                l++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3));
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;
                            }
                            else if (l + 1 < img.Cols && k + 1 < img.Rows && img.Data[k + 1, l + 1, 0] == 255 && passed[k + 1, l + 1] != 255 )
                            {
                                k++; l++;
                                lstTemp = new LinkedListData();
                                lstTemp.point = new Point(k, l);
                                lstTemp.pointSeq = list[llCounter - 1][countLst].pointSeq + 1;
                                lstTemp.data = img.Data[k, l, 0];
                                lstTemp.defX = lstTemp.point.X - list[llCounter - 1][countLst].point.X;
                                lstTemp.defY = lstTemp.point.Y - list[llCounter - 1][countLst].point.Y;
                                lstTemp.doubDefX = lstTemp.defX - list[llCounter - 1][countLst].defX;
                                lstTemp.doubDefY = lstTemp.defY - list[llCounter - 1][countLst].defY;
                                lstTemp.curvature = (lstTemp.defX * lstTemp.doubDefY - lstTemp.defY * lstTemp.doubDefX) / Math.Sqrt(Math.Sqrt(Math.Pow(Math.Pow(lstTemp.defX, 2) + Math.Pow(lstTemp.defY, 2), 3)));
                                chl = true; chk = true;
                                list[llCounter - 1].Add(lstTemp);
                                passed[k, l] = 255;

                            }

                            else loopkl = false;
                        }
                        ///////////////////////////////////////////



                        //MessageBox.Show(llCounter.ToString());

                    }///end of while

                    #endregion
                }/// end of for loop
            
            int sum = 0;
            for (int i = 0; i < img.Rows - 1; i++)
                for (int j = 0; j < img.Cols - 1; j++)
                    if (passed[i, j] == 255)
                        sum++;
          
            string pathStringList1 = System.IO.Path.Combine(filename.Substring(0, filename.IndexOf('.')), "List1");
            string pathStringList2 = System.IO.Path.Combine(filename.Substring(0, filename.IndexOf('.')), "List2");
            string pathStringData = System.IO.Path.Combine(filename.Substring(0, filename.IndexOf('.')), "Data");
            string pathstringStat= System.IO.Path.Combine(filename.Substring(0, filename.IndexOf('.')), "Statistics");
            MessageBox.Show("No of Lists of Connected Points = "+llCounter.ToString() + "   " +"All Points"+ sum.ToString());
            if (!Directory.Exists(filename.Substring(0, filename.IndexOf('.'))))
            {byte[,] y = new byte[img.Rows, img.Cols];
                Directory.CreateDirectory(filename.Substring(0, filename.IndexOf('.')));
            }
            for (int i = 0; i < llCounter; i++)
            {
                File.AppendAllText(pathStringList2, list2[i].Count.ToString() + " ");
                File.AppendAllText(pathStringList1, list[i].Count.ToString() + " ");
                List<LinkedListData> listAll = new List<LinkedListData>();
                
               // MessageBox.Show("List2.Count = " + list2[i].Count.ToString()+ " List.Count = " + list[i].Count.ToString());
                int count;
               
                list2[i].Reverse();
                
                list2[i].RemoveAt(list2[i].Count - 1);
                list2[i].AddRange(list[i]);
                
                string[] lines = new string[list2[i].Count + 1];
                File.AppendAllText(pathstringStat, list2[i].Count.ToString()+" ");
                Random rnd = new Random();
               
                lines[0] = "0 0 "+list2[i].Count+" "+ Color.FromRgb((byte) list2[i][0].point.X, (byte)list2[i][0].point.Y, (byte)list2[i][0].pointSeq);
                count = 1;
                LinkedListData tempData=new LinkedListData();
                for(int j=0;j< list2[i].Count;j++)
                {
                    tempData.pointSeq= list2[i][j].pointSeq + Math.Abs(list2[i][0].pointSeq);
                    tempData.point = list2[i][j].point;
                    if (tempData.pointSeq > 0)
                    { 
                        tempData.defX = (tempData.point.X - list2[i][j - 1].point.X) / (tempData.pointSeq - list2[i][j - 1].pointSeq);
                        tempData.defY = (tempData.point.Y - list2[i][j - 1].point.Y) / (tempData.pointSeq - list2[i][j - 1].pointSeq);
                    }

                    if (tempData.pointSeq > 1)
                    {
                        tempData.doubDefX  = (tempData.defX - list2[i][j - 1].defX) / (tempData.pointSeq - list2[i][j - 1].pointSeq);
                        tempData.doubDefY = (tempData.defY - list2[i][j - 1].defY) / (tempData.pointSeq - list2[i][j - 1].pointSeq);
                    }
                    else {
                        tempData.doubDefX = 0;
                        tempData.doubDefY = 0;
                    }
                    if (tempData.pointSeq > 2)
                    {
                        tempData.curvature = (tempData.defX * tempData.doubDefY - tempData.defY * tempData.doubDefX) / Math.Sqrt(Math.Sqrt(Math.Pow(Math.Pow(tempData.defX, 2) + Math.Pow(tempData.defY, 2), 3)));
                    }
                    else
                        tempData.curvature = 0;
                    listAll.Add(tempData);

                }

                foreach (LinkedListData temp in listAll)
                {
                   // temp.pointSeq += Math.Abs(list[i][0].pointSeq);

                    lines[count] = temp.point.X + " " + temp.point.Y + " " + temp.defX + " " + temp.defY + " " + temp.doubDefX + " " + temp.doubDefY+" "+temp.curvature+" "+temp.pointSeq ;
                    count++;
                }
                File.AppendAllLines(pathStringData,lines );
            }
        }

        public System.Windows.Shapes.Path plotFile(string filename,Image<Gray, byte> img )
        {
         Canvas canvas = new Canvas {Width=img.Width, Height = img.Height };
        System.Windows.Size size = new System.Windows.Size(canvas.Width, canvas.Height);
        // Measure and arrange the surface
        canvas.Measure( size );
        canvas.Arrange( new Rect(size ) );
        canvas.Background = new SolidColorBrush(Colors.White);
            ///////////////////////drawing file
            //////////////////////___________________________
            string text = System.IO.File.ReadAllText(filename.Substring(0, filename.IndexOf('.'))+ "\\Data");
            Regex r1 = new Regex(@"\n"); //specify delimiter (spaces)
            Regex r2 = new Regex(@" +"); //specify delimiter (spaces)
            Regex r3 = new Regex(@"\d+");
            string[] words = r1.Split(text); //(convert string to array of words)
            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();                               //foreach (string s in words)
            GeometryGroup toBmb = new GeometryGroup();
            PathGeometry test = new PathGeometry();
            PathFigure testFig = new PathFigure();
            LineSegment line = new LineSegment();
            bool flag = false;
            Color clr = new Color();
            
            foreach (String W in words)
            {
                int i;
                string[] digits = r2.Split(W);
                // MessageBox.Show(digits[0].Length.ToString()
                

                if (r3.IsMatch(digits[0]))
                {
                    if (Int32.Parse(digits[0])==0 && Int32.Parse(digits[1]) == 0 && Int32.Parse(digits[2])>10)
                    {
                        testFig.Segments.Add(line);
                        test.Figures.Add(testFig);
                        testFig  = new PathFigure();
                        line = new LineSegment();
                        
                        flag = true;
                    }
                    
                        if (flag)
                        {
                            testFig.StartPoint = (new Point(double.Parse(digits[0]), double.Parse(digits[1])));
                            flag = false;
                        }
                        else if (Int32.Parse(digits[0]) != 0 | Int32.Parse(digits[1]) != 0)
                    {
                        line.Point=(new Point(double.Parse(digits[0]), double.Parse(digits[1])));
                        testFig.Segments.Add(line);
                        line = new LineSegment();

                    }

                   
                }
            }
            SolidColorBrush brush = new SolidColorBrush(clr);
            myPath.Stroke = brush;
            myPath.StrokeThickness = 3;
            test.Figures.Add(testFig);
            toBmb.Children.Add(test);
            myPath.Data = toBmb;
                   
            
            
            myPath.Stroke = Brushes.DarkOrange;
            myPath.StrokeThickness = 3;
            myPath.Data = toBmb;
            canvas.Background = Brushes.White;
           // canvas.Children.Add(myPath);

            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)canvas.Width, (int)canvas.Height, 96d, 96d, PixelFormats.Pbgra32);
        bitmap.Render( canvas );
        BitmapEncoder encoder = new BmpBitmapEncoder();
            BitmapImage bmp = new BitmapImage { CacheOption = BitmapCacheOption.OnLoad };
            encoder.Frames.Add( BitmapFrame.Create( bitmap ) );
            using (MemoryStream outStream = new MemoryStream())
            {

                encoder.Save(outStream);

                outStream.Seek(0, SeekOrigin.Begin);
                
                

                bmp.BeginInit();
                bmp.StreamSource = outStream;
                bmp.EndInit();
          
            }
            return myPath;
        }
        public BitmapSource plotImage(string filename, Image<Gray, byte> img)
        {
            string text = System.IO.File.ReadAllText(filename.Substring(0, filename.IndexOf('.')) + "\\" +"Data");
            Regex r1 = new Regex(@"\n"); //specify delimiter (spaces)
            Regex r2 = new Regex(@" +"); //specify delimiter (spaces)
            Regex r3 = new Regex(@"\d+");
            string[] words = r1.Split(text); //(convert string to array of words)
            Image<Gray, byte> imgTest = new Image<Gray, byte>(img.Width,img.Height);
            foreach (String W in words)
            {
                
                string[] digits = r2.Split(W);
                // MessageBox.Show(digits[0].Length.ToString()

                if (r3.IsMatch(digits[0]))
                {
                   if (Int32.Parse(digits[0]) != 0 | Int32.Parse(digits[1]) != 0)
                    {
                        imgTest.Data[Int32.Parse(digits[0]) ,Int32.Parse(digits[1]),0] = 200;

                    }


                }
            }
            return (BitmapSourceConvert.ToBitmapSource(imgTest));
        }

    }
}
         