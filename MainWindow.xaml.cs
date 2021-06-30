using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace GD_URIConvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window{

        //メンバ変数
        private const string HELP_DESCRIPTION
            = "説明\n\n"
            + "このアプリケーションはGoogleDriveにアップロードされた画像の共有URLを直接リンクに変換します。\n\n"
            + "例\n\n"
            + "変換前\n"
            + "https://drive.google.com/file/d/{ContentsID}\n"
            + "or\n"
            + "https://drive.google.com/open?id={ContentsID}\n\n"
            + "変換後\n"
            + "https://drive.google.com/uc?export=view&id={ContentsID}";
            
        private const string TEXTBOX_DEFAULT_TEXT = "URLを入力してください。";

        public ObservableCollection<ListItem> ListItems;

        //コンストラクタ
        public MainWindow(){
            InitializeComponent();
            ListItems = new ObservableCollection<ListItem>();
            this.DataContext = ListItems;

        }

        //イベントハンドラここから
        private void Button_Submit_Click(object sender, RoutedEventArgs e){
            AddList(URL_Text.Text);

        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e){
            SetClipboard(URLList.SelectedIndex);

        }

        private void Button_Erase_Click(object sender, RoutedEventArgs e){
            int itemIndex = URLList.SelectedIndex;
            
            if(itemIndex >= 0){ListItems.RemoveAt(index: itemIndex);}

        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e){
            ListItems.Clear();

        }
        
        private void Button_Help_Click(object sender, RoutedEventArgs e){
            MessageBox.Show(messageBoxText: HELP_DESCRIPTION, caption: "Help");

        }

        private void Button_SubWindowOpen_Click(object sender, RoutedEventArgs e){
            SubViewOpen(URLList.SelectedIndex);

        }

        private void OnGotFocusHandler(object sender, RoutedEventArgs e){
            if(URL_Text.Text == TEXTBOX_DEFAULT_TEXT){URL_Text.Text = "";}

        }

        private void OnLostFocusHandler(object sender, RoutedEventArgs e){
            if(URL_Text.Text == ""){
                URL_Text.Text = TEXTBOX_DEFAULT_TEXT;
                URL_Text.Background = Brushes.Transparent;
                
            }

        }
        //イベントハンドラここまで

    }

}
