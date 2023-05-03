using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WpfApp2.Converter
{
    internal class InverseBoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// プロパティ→画面表示に反映時
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="targetType">ターゲットの型</param>
        /// <param name="parameter">XAMLからパラメーターとして渡されてくる値</param>
        /// <param name="culture"></param>
        /// <returns>変換後の返す値</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool bValue))
            {
                // 値が対象でない場合、伝達しない
                return DependencyProperty.UnsetValue;
            }

            if (bValue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        /// <summary>
        /// 画面表示→プロパティに反映時
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility visibility))
            {
                return DependencyProperty.UnsetValue;
            }

            if (visibility == Visibility.Visible)
                return true;
            else if (visibility == Visibility.Collapsed)
                return false;
            //else if (visibility == Visibility.Hidden)
            //    return false;
            else
                return DependencyProperty.UnsetValue;
        }
    }
}
