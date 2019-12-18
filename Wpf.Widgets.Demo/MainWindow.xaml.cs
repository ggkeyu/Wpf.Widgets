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

namespace Wpf.Widgets.Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchBox_OnSearching(object sender, SearchBoxSearchingEventArgs e)
        {
            //e.Keyword就是搜索框中的文本
            //e.Result是返回的结果集合
            //循环添加12个选项..
            for (int i = 0; i < 13; ++i)
            {
                e.Result.Add(e.Keyword + i.ToString());
            }
        }

        private void SearchBox_OnSearchResultCommitted(object sender, SearchBoxResultCommittedEventArgs e)
        {
            //键盘回车键按下或者鼠标左键点击选项调用
        }

        private void SearchBox_Search_OnTextCommitted(object sender, SearchBoxTextCommittedEventArgs e)
        {
            //在搜索框中按下回车键调用的事件
            SearchBox_Search1.WidthDisplay = SearchResultWindowWidthDisplay.Auto;
        }

        private void Window_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
