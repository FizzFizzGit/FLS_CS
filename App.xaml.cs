using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GD_URIConvert
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class MainWindow : Window{

        private void SetClipboard(int itemIndex){
            if(itemIndex < 0){return;}
            string url = ListItems[itemIndex].URLText;
            Clipboard.SetData(DataFormats.Text,url);
            MessageBox.Show("Copy : " + url);
            
        }

        private void SubViewOpen(int itemIndex){
            if(itemIndex < 0){return;}
            SubWindow sub = new SubWindow();
            ImageItem imageitem = new ImageItem();
            imageitem.Graphic = WebGraphic.toBitmapImage(uri: ListItems[itemIndex].URLText);
            sub.DataContext = imageitem;
            sub.ShowDialog();

        }

        private void AddList(string uri){
            ListItem listitem = generateItem(uri);
            if(listitem != null){
                ListItems.Add(listitem);
                URL_Text.Text = TEXTBOX_DEFAULT_TEXT;
                URL_Text.Background = Brushes.Transparent;
                
            }else{
                URL_Text.Background = Brushes.Pink;

            }

        }

        private ListItem generateItem(string uri){
            string UrlString = UrlConverter.ConvetUrl(uri);

            if(UrlString == "" | existURI(UrlString)){return null;}

            ListItem listitem = new ListItem();
            listitem.Graphic = WebGraphic.toBitmapImage(uri: UrlString);
            listitem.URLText = UrlString;
            return listitem;

        }

        private bool existURI(string uri){
            foreach(ListItem item in ListItems){
                if(item.URLText == uri){return true;}

            }

            return false;

        }

    }

    //リストの子要素を格納するクラス
    public class ListItem{
        public BitmapImage Graphic{get;set;}
        public string URLText{get;set;}

    }

    public class ImageItem{
        public BitmapImage Graphic{get;set;}

    }
    
    //URLを直接リンクに書き換えるクラス
    static internal partial class UrlConverter{
        private const string KEY_WORD = "open?id=";
        private const string KEY_WORD2 = "file/d/";
        private const string REPLACE_WORD = "uc?export=view&id=";
        
        public static string ConvetUrl(string inputString){
            string temp = inputString;

            if(temp.Length <= 0){return "";}

            if(temp.Contains(KEY_WORD)){
                return temp.Replace(KEY_WORD,REPLACE_WORD);
                
            }else if(temp.Contains(KEY_WORD2)){
                string[] splitTemp = temp.Split(KEY_WORD2);
                string domainWord = splitTemp[0];
                string urlParamater = splitTemp[1].Split("/")[0];
                return domainWord + REPLACE_WORD + urlParamater;

            }

            return "";

        }

    }

    //URLからBitmapImageを生成するクラス
    static internal partial class WebGraphic{
        public static BitmapImage toBitmapImage(string uri){
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(uri);
            bi.EndInit();
            return bi;

        }

    }

}
