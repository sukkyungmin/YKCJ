using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Shapes;


namespace DMSGalaxy.Common.Helper
{
    public class UIHelper
    {

        //윈도우 닫기
        public delegate void D_CloseWindow(Window wd);
        public void CloseWindow(Window wd)
        {
            try
            {
                wd.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        //트루면 해당 이미지 비지블로 폴스면 히든으로 함
        public delegate void D_ChangePicture(Image img, bool visible); //img 비지블 함수
        public void ChangePicture(Image img, bool visible)
        {
            try
            {
                if (visible == true)
                {
                    if (img.Visibility != System.Windows.Visibility.Visible)
                    {
                        img.Visibility = System.Windows.Visibility.Visible;
                    }

                }
                else
                {
                    if (img.Visibility != System.Windows.Visibility.Hidden)
                    {
                        img.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        //해당 레이블 컨텐츠에 해당 텍스트 넣주기
        public delegate void D_ChangeLabelTXT(Label lb, string txt);
        public void ChangeLabelTXT(Label lb, string txt)
        {
            try
            {
                lb.Content = txt;
            }
            catch (Exception ex) { throw ex; }
        }

        //해당 레이블 글씨색 녹색으로 바꿔주기
        public delegate void D_ChangeLblGreen(Label lb);
        public void ChangeLblGreen(Label lb)
        {
            try
            {
                //lb.Foreground = new SolidColorBrush(Color.FromRgb(34, 224, 3));
                lb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22E003"));
            }
            catch (Exception ex) { throw ex; }
        }

        //해당 레이블 글씨색 빨간색으로 바꿔주기
        public delegate void D_ChangeLblRed(Label lb);
        public void ChangeLblRed(Label lb)
        {
            try
            {
                lb.Foreground = Brushes.Red;
            }
            catch (Exception ex) { throw ex; }
        }

        //해당 레이블 배경 해당색으로
        public delegate void D_ChangeLabelColor(Label lb, Color color);
        public void ChangeLabelColor(Label lb, Color color)
        {
            try
            {
                if (lb.Background != new SolidColorBrush(color))
                {
                    lb.Background = new SolidColorBrush(color);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        //트루면 해당 레이블 비지블로 폴스면 히든으로 함
        public delegate void D_ChangeLabel(Label label, bool visible); //Label 비지블 함수
        public void ChangeLabel(Label label, bool visible)
        {
            try
            {
                if (visible == true)
                    label.Visibility = System.Windows.Visibility.Visible;
                else
                    label.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex) { throw ex; }
        }

        //트루면 해당 그리드 비지블로 폴스면 히든으로 함
        public delegate void D_ChangeGrid(Grid grid, bool visible); //grid 비지블 함수
        public void ChangeGrid(Grid grid, bool visible)
        {
            try
            {
                if (visible == true)
                    grid.Visibility = System.Windows.Visibility.Visible;
                else
                    grid.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex) { throw ex; }
        }

        //그리드 배경색깔 바꿔주기
        public delegate void D_ChangeGridColor(Grid grid, string color);
        public void ChangeGridColor(Grid grid, string color)
        {
            try
            {
                if (color == "yellow")
                {
                    grid.Background = Brushes.Yellow;
                }
                else
                {
                    grid.Background = Brushes.White;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        //그리드 배경색깔 바꿔주기argb
        public delegate void D_ArgbGridColor(Grid grid, byte aVal, byte rVal, byte gVal, byte bVal);
        public void ArgbGridColor(Grid grid, byte aVal, byte rVal, byte gVal, byte bVal)
        {
            try
            {
                grid.Background = new SolidColorBrush(Color.FromArgb(aVal, rVal, gVal, bVal));

            }
            catch (Exception ex) { throw ex; }
        }

        //Frame Page 변경(string 으로 구역 정해서 변경)
        public void Navigate(Frame Frm, string menuID, string menuitem)
        {           
            Page pg = (Page)Application.LoadComponent(new Uri(@"/DMSGalaxy.Main;component/F_" + menuID.Substring(0, 1) + "/" + menuID.Substring(2, 2)
                                                           + "/F" + menuID.Substring(0, 4) + menuitem + ".xaml", UriKind.RelativeOrAbsolute));

            pg.Width = Double.NaN;
            pg.Height = Double.NaN;
            pg.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            pg.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Frm.NavigationService.Navigate(pg);
        }

        //Frame Page 변경(Page 할당 후 변경)
        public void NavigateMain(Frame Frm, Page pg)
        {
            pg.Width = Double.NaN;
            pg.Height = Double.NaN;
            pg.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            pg.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Frm.NavigationService.Navigate(pg);
        }

        public void RichBox(RichTextBox RichBox ,double SSize , double ESize ,string Stext , string Etext ,string SCol, string ECol)
        {
            var bc = new BrushConverter();

            TextRange rangeOfStext = new TextRange(RichBox.Document.ContentEnd, RichBox.Document.ContentEnd);
            rangeOfStext.Text = Stext;
            rangeOfStext.ApplyPropertyValue(TextElement.ForegroundProperty, (Brush)bc.ConvertFrom(SCol));
            rangeOfStext.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            rangeOfStext.ApplyPropertyValue(TextElement.FontSizeProperty, SSize);

            TextRange rangeOfEtext = new TextRange(RichBox.Document.ContentEnd, RichBox.Document.ContentEnd);
            rangeOfEtext.Text = Etext;
            rangeOfEtext.ApplyPropertyValue(TextElement.ForegroundProperty, (Brush)bc.ConvertFrom(ECol));
            rangeOfEtext.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
            rangeOfEtext.ApplyPropertyValue(TextElement.FontSizeProperty, ESize);
        }

        public Line CreateLine(double x1, double y1, double x2, double y2, Brush brush, double thickness, DoubleCollection dashStyle)
        {
            Line line = new Line();

            //첫번째 좌표 설정
            line.X1 = x1;
            line.Y1 = y1;

            //두번째 좌표 설정
            line.X2 = x2;
            line.Y2 = y2;

            line.Stroke = brush;//선색 지정
            line.StrokeThickness = thickness;//선 두께 지정
            line.StrokeDashArray = dashStyle;//점선 설정 - new DoubleCollection { 점 길이, 점 간격}

            return line;
        }

    }
}
