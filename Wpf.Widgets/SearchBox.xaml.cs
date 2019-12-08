using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Wpf.Widgets
{
    /// <summary>
    /// AutoCompleteTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBox : Control
    {
        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        //注册搜索中事件        
        public static readonly RoutedEvent OnSearchingEvent = EventManager.RegisterRoutedEvent("OnSearchingEvent",
            RoutingStrategy.Bubble, typeof(EventHandler<SearchBoxSearchingEventArgs>), typeof(SearchBox));

        /// <summary>
        /// Searching事件
        /// </summary>
        public event EventHandler<SearchBoxSearchingEventArgs> OnSearching
        {
            add
            {
                AddHandler(OnSearchingEvent, value);
            }
            remove
            {
                RemoveHandler(OnSearchingEvent, value);
            }
        }

        //注册搜索结果提交事件        
        public static readonly RoutedEvent OnSearchResultCommittedEvent = EventManager.RegisterRoutedEvent("OnSearchResultCommittedEvent",
            RoutingStrategy.Bubble, typeof(EventHandler<SearchBoxResultCommittedEventArgs>), typeof(SearchBox));

        /// <summary>
        /// SearchResultCommitted事件
        /// </summary>
        public event EventHandler<SearchBoxResultCommittedEventArgs> OnSearchResultCommitted
        {
            add
            {
                AddHandler(OnSearchResultCommittedEvent, value);
            }
            remove
            {
                RemoveHandler(OnSearchResultCommittedEvent, value);
            }
        }

        //文本提交事件        
        public static readonly RoutedEvent OnTextCommittedEvent = EventManager.RegisterRoutedEvent("OnTextCommittedEvent",
            RoutingStrategy.Bubble, typeof(EventHandler<SearchBoxTextCommittedEventArgs>), typeof(SearchBox));

        /// <summary>
        /// SearchResultCommitted事件
        /// </summary>
        public event EventHandler<SearchBoxTextCommittedEventArgs> OnTextCommitted
        {
            add
            {
                AddHandler(OnTextCommittedEvent, value);
            }
            remove
            {
                RemoveHandler(OnTextCommittedEvent, value);
            }
        }

        /// <summary>
        /// 选项选中的颜色
        /// </summary>
        public Brush PopupContentSelectionBrush
        {
            get { return (Brush)GetValue(PopupContentSelectionProperty); }
            set { SetValue(PopupContentSelectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupContentSelectionProperty =
            DependencyProperty.Register("PopupContentSelectionBrush", typeof(Brush), typeof(SearchBox), new PropertyMetadata(Brushes.DodgerBlue));


        /// <summary>
        /// 选项前景色
        /// </summary>
        public Brush PopupContentForegroundBrush
        {
            get { return (Brush)GetValue(PopupContentForegroundProperty); }
            set { SetValue(PopupContentForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupContentForegroundProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupContentForegroundProperty =
            DependencyProperty.Register("PopupContentForegroundBrush", typeof(Brush), typeof(SearchBox), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 搜索的图标
        /// </summary>
        public ImageSource SearchIcon
        {
            get { return (ImageSource)GetValue(SearchIconProperty); }
            set { SetValue(SearchIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchIconProperty =
            DependencyProperty.Register("SearchIcon",
                typeof(ImageSource),
                typeof(SearchBox),
                new PropertyMetadata(null));

        /// <summary>
        /// 文本选中的颜色
        /// </summary>
        public Brush TextSelectionBrush
        {
            get { return (Brush)GetValue(TextSelectionBrushProperty); }
            set { SetValue(TextSelectionBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextSelectionBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextSelectionBrushProperty =
            DependencyProperty.Register("TextSelectionBrush", typeof(Brush), typeof(SearchBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 120, 215))));

        /// <summary>
        /// 提示文本
        /// </summary>
        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HintText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register("HintText", typeof(string), typeof(SearchBox), new PropertyMetadata("Search"));

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SearchBox), new PropertyMetadata(""));


        // 
        private static readonly DependencyPropertyKey HasTextPropertyKey = DependencyProperty.RegisterReadOnly("HasText", typeof(bool), typeof(SearchBox), new PropertyMetadata(false));

        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        // 搜索框文本是否为空
        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
        }


        /// <summary>
        /// 显示的选项最大数值
        /// </summary>
        public int PopupItemLimitCount
        {
            get { return (int)GetValue(PopupItemLimitCountProperty); }
            set 
            {
                if (value < DefaultPopupDisplayItemCount)
                {
                    value = DefaultPopupDisplayItemCount;
                }
                SetValue(PopupItemLimitCountProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for PopupItemLimitCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupItemLimitCountProperty =
            DependencyProperty.Register("PopupItemLimitCount", typeof(int), typeof(SearchBox), new PropertyMetadata(DefaultPopupDisplayItemCount));


        /// <summary>
        /// 强制调用提交事件(对于上一次的提交内容，如果这次提交相同的文本内容依旧会被处理)
        /// </summary>
        public bool ForceCallTextCommittedEvent
        {
            get { return (bool)GetValue(ForceCallTextCommittedEventProperty); }
            set { SetValue(ForceCallTextCommittedEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForceCallTextCommittedEvent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForceCallTextCommittedEventProperty =
            DependencyProperty.Register("ForceCallTextCommittedEvent", typeof(bool), typeof(SearchBox), new PropertyMetadata(false));



        private bool fromSearchResult = false;

        private Point mousePosition = new Point(0, 0);

        private const int DefaultPopupDisplayItemCount = 6;

        private string lastCommittedText = "";

        private TextBox PART_TextBox = null;
        private Popup PART_Popup = null;
        private ListBox PART_ListBox = null;

        public SearchBox()
        {
            InitializeComponent();

            Loaded += SearchBox_Loaded;
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_TextBox = GetTemplateChild("PART_TextBox") as TextBox;
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            PART_ListBox = GetTemplateChild("PART_ListBox") as ListBox;
        }

        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            PART_TextBox.TextChanged += PART_TextBox_TextChanged;
        }

        private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //如果是来自于搜索结果提交返回
            if (fromSearchResult)
            {
                return;
            }
            TryOpenopupWindow();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CommitCurrentText();
        }

        private object[] TryGetSearchResult(string Keyword, out int OptimalIndex)
        {
            SearchBoxSearchingEventArgs searchingEventArgs = new SearchBoxSearchingEventArgs(OnSearchingEvent, this)
            {
                Keyword = Keyword
            };

            RaiseEvent(searchingEventArgs);

            OptimalIndex = searchingEventArgs.OptimalSelection;

            return searchingEventArgs.Result.ToArray();
        }

        private void TryOpenopupWindow()
        {
            //获取文本
            string input = PART_TextBox.Text;
            //为空则关闭
            if (string.IsNullOrEmpty(input))
            {
                TryClosePopupWindow();
                return;
            }
            //
            if (!TryFillSearchResult(input))
            {
                TryClosePopupWindow();
                return;
            }

            if (PART_Popup.IsOpen)
            {
                return;
            }

            PART_Popup.IsOpen = true;

            mousePosition = Mouse.GetPosition(this);
        }

        private bool TryFillSearchResult(string Input)
        {
            object[] Result = TryGetSearchResult(Input, out int OptimalIndex);
            if (Result.Length == 0)
            {
                return false;
            }
            PART_ListBox.Items.Clear();
            foreach (var s in Result)
            {
                var res = new ListBoxItem() { Content = s };
                res.MouseEnter += Res_MouseEnter;
                PART_ListBox.Items.Add(res);
            }
            if (PART_ListBox.Items.Count != 0)
            {
                if (OptimalIndex >= 0 && OptimalIndex < PART_ListBox.Items.Count)
                {
                    PART_ListBox.SelectedIndex = OptimalIndex;
                }
                else
                {
                    PART_ListBox.SelectedIndex = 0;
                }
            }
            PART_ListBox.ScrollIntoView(PART_ListBox.SelectedItem);
            return true;
        }

        private void Res_MouseEnter(object sender, MouseEventArgs e)
        {
            var curPos = e.GetPosition(PART_TextBox);
            if (curPos == mousePosition)
            {
                return;
            }
            if (PART_ListBox.SelectedItem != sender)
            {
                if (sender is ListBoxItem listBoxItem)
                {
                    PART_ListBox.SelectedItem = listBoxItem;

                    mousePosition = curPos;
                }
            }
        }

        private void TryCommitSearchResult(object sender)
        {
            if (sender is ListBoxItem listBoxItem)
            {
                PART_Popup.IsOpen = false;
                fromSearchResult = true;
                PART_TextBox.Text = listBoxItem.Content.ToString();
                PART_TextBox.CaretIndex = PART_TextBox.Text.Length;
                fromSearchResult = false;
                SearchBoxResultCommittedEventArgs eventArgs = new SearchBoxResultCommittedEventArgs(OnSearchResultCommittedEvent, this)
                {
                    Selected = listBoxItem.Content
                };

                RaiseEvent(eventArgs);
                PART_TextBox.Focus();
                PART_TextBox.CaretIndex = PART_TextBox.Text.Length;
            }
        }

        private void TryClosePopupWindow()
        {
            if (PART_Popup.IsOpen)
            {
                PART_ListBox.Items.Clear();
                PART_Popup.IsOpen = false;
            }
        }

        private void TryMoveSelection(int Step)
        {
            int Curr = PART_ListBox.SelectedIndex;
            Curr += Step;
            if (Curr < 0 || Curr >= PART_ListBox.Items.Count)
            {
                return;
            }
            PART_ListBox.SelectedItem = PART_ListBox.Items[Curr];
            PART_ListBox.ScrollIntoView(PART_ListBox.SelectedItem);
            mousePosition = Mouse.GetPosition(PART_TextBox);
        }

        private void PART_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!PART_Popup.IsOpen)
            {
                return;
            }
            if (e.Key == Key.Enter)
            {
                TryCommitSearchResult(PART_ListBox.SelectedItem);
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                TryMoveSelection(-1);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                TryMoveSelection(1);
                e.Handled = true;
            }
        }

        private void PART_ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            double height = 0.00d;
            int maxDisplayRow = PopupItemLimitCount;
            foreach (ListBoxItem listBoxItem in PART_ListBox.Items)
            {
                if (maxDisplayRow-- <= 0)
                {
                    break;
                }
                height += listBoxItem.ActualHeight;
            }
            height += PopupItemLimitCount;
            PART_ListBox.Height = height;
        }

        private void PART_Popup_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed ||
                Mouse.RightButton == MouseButtonState.Pressed)
            {
                TryCommitSearchResult(PART_ListBox.SelectedItem);
            }
            else
            {
                PART_TextBox.Focus();
            }
        }

        private void CommitCurrentText()
        {
            if (!ForceCallTextCommittedEvent)
            {
                if (0 == string.Compare(lastCommittedText, Text, StringComparison.CurrentCulture))
                {
                    return;
                }
            }

            SearchBoxTextCommittedEventArgs eventArgs = new SearchBoxTextCommittedEventArgs(OnTextCommittedEvent, this)
            {
                Text = Text
            };

            RaiseEvent(eventArgs);

            lastCommittedText = Text;
        }

        private void PART_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (PART_Popup.IsOpen)
            {
                return;
            }
            if(e.Key == Key.Enter)
            {
                CommitCurrentText();
                e.Handled = true;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == TextProperty)
            {
                SetValue(HasTextPropertyKey, !string.IsNullOrEmpty(Text));
            }
        }
    }
}
