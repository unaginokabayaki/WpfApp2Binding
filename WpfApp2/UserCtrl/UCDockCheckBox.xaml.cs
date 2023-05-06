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

namespace WpfApp2.UserCtrl
{
    /// <summary>
    /// UCDockCheckBox.xaml の相互作用ロジック
    /// </summary>
    public partial class UCDockCheckBox : UserControl
    {
        public UCDockCheckBox()
        {
            InitializeComponent();

            // xamlではなくコードでセット
            // TextBlockに追加するプロパティをバインドする
            // {Binding CheckBoxComment, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type local:UCDockCheckBox}}}
            CommentTextBlock.SetBinding(TextBlock.TextProperty, 
                                        new Binding(nameof(CheckBoxComment)) 
                                        { 
                                            Source = this, 
                                        });
        }


        // 依存関係プロパティ
        public string CheckBoxComment
        {
            get { return (string)GetValue(CheckBoxCommentProperty); }
            set { SetValue(CheckBoxCommentProperty, value); }
        }

        // CheckBoxComment のバッキング ストアとして DependencyProperty を使用すると、アニメーション、スタイリング、バインディングなどが有効になります。
        public static readonly DependencyProperty CheckBoxCommentProperty =
            DependencyProperty.Register("CheckBoxComment", typeof(string), typeof(UCDockCheckBox), new PropertyMetadata(string.Empty));



    }
}
