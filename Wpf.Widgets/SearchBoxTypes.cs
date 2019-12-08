using System;
using System.Collections.Generic;
using System.Windows;

namespace Wpf.Widgets
{
    /// <summary>
    /// 搜索框搜索结果
    /// </summary>
    public sealed class SearchBoxSearchingEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; internal set; } = "";

        /// <summary>
        /// 结果集
        /// </summary>
        public List<object> Result { get; } = new List<object>();

        /// <summary>
        /// 最佳的选择项索引值
        /// </summary>
        public int OptimalSelection { get; set; } = -1;

        public SearchBoxSearchingEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            if (genericHandler is EventHandler<SearchBoxSearchingEventArgs> handler)
            {
                handler(genericTarget, this);
            }
        }
    }

    /// <summary>
    /// 搜索结果已经提交
    /// </summary>
    public sealed class SearchBoxResultCommittedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 提交的对象数据
        /// </summary>
        public object Selected { get; set; }
        public SearchBoxResultCommittedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            if (genericHandler is EventHandler<SearchBoxResultCommittedEventArgs> handler)
            {
                handler(genericTarget, this);
            }
        }
    }

    /// <summary>
    /// 搜索框文本已经提交
    /// </summary>
    public sealed class SearchBoxTextCommittedEventArgs : RoutedEventArgs
    {
        public string Text { get; internal set; }

        public SearchBoxTextCommittedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            if (genericHandler is EventHandler<SearchBoxTextCommittedEventArgs> handler)
            {
                handler(genericTarget, this);
            }
        }
    }
}
