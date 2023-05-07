using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Command;

namespace WpfApp2.Model
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { 
                _isChecked = value;
                // プロパティ変更時にバインドされているコントロールを変更する（Converterを使う場合もこれが必要）
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
                // IsCheckVisiblityに変更を通知
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheckVisiblity)));
            }
        }

        // StackPanelの表示切替
        // ラムダ式を使った読み取り専用プロパティの書き方
        public Visibility IsCheckVisiblity =>
            IsChecked ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// ListBoxのItemsSourceにバインドする
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<SelectionItem> Selection { get; } = new ObservableCollection<SelectionItem>();

        public DelegateCommand<ItemViewModel> RemoveCommand { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel()
        {
            int id = 0;
            Items.Add(new ItemViewModel(++id) { ItemName = "aaa" });
            Items.Add(new ItemViewModel(++id) { ItemName = "bbb" });
            Items.Add(new ItemViewModel(++id) { ItemName = "ccc" });

            id = 0;
            Selection.Add(new SelectionItem(++id, "SDC"));
            Selection.Add(new SelectionItem(++id, "BET"));
            Selection.Add(new SelectionItem(++id, "QMN"));


            RemoveCommand = new DelegateCommand<ItemViewModel>((item) =>
            {
                Items.Remove(item);
            });
        }

        // これがないとバインドに失敗するので、FallBackValueが発動する
        //public bool IsExpandedBtns {  get; set; }
    }

    internal class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// バインドする
        /// ItemNameはリスト上で入力可能
        /// </summary>
        private string _itemName;
        public string ItemName
        {
            get => _itemName;
            set
            {
                //_itemName = value;
                SetProperty(ref _itemName, value);
            }
        }

        /// <summary>
        /// バインドする
        /// Idはコンストラクタでのみセット
        /// </summary>
        //private int _id;
        public int Id { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id"></param>
        public ItemViewModel(int id)
        {
            Id = id;
        }

        /// <summary>
        /// リスト上のテキストボックスを編集した時
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // 変更前と同じだったら更新しない
            if (EqualityComparer<T>.Default.Equals(field, value)) return;

            // 違ったらrefに代入して返却する
            field = value;

            // 画面へ通知
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class SelectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SelectionItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }

}
