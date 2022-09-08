using LauncherModelLib.HotKey;
using MahApps.Metro.Controls;
using MLauncherApp.ViewModels;

//for hotkey
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace MLauncherApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IDisposable _hotKeyHandle;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var vm = (MainWindowViewModel)this.DataContext;

            try
            {
                IntPtr windowHandle = new WindowInteropHelper(this).Handle;
                _hotKeyHandle = GlobalHotKeyFactory.Register(windowHandle, ActivateHotKeyCallBack);
            }
            catch (Exception hotkeyException)
            {
                vm.NotifyFailureOfHotkey(hotkeyException);
            }
        }

        private void ActivateHotKeyCallBack()
        {
            this.Activate();
        }

        protected override void OnClosed(EventArgs e)
        {
            _hotKeyHandle.Dispose();
            base.OnClosed(e);
        }

        /// <summary>
        /// FIX ME
        /// AutoCompleteBoxから、候補選択しない状態でEnterキー入力を取得する方法がこれしか見つけられていない
        /// できればMVVMにしてコードビハインドを消し去りたい
        /// 
        /// 作業時のメモ↓
        /// - 操作性の改善：選択してない状態でのEnterに対応したい。
        ///- 例
        ///    - 候補が1つしかない状態で、候補を選択しなくてもEnterをおしたら実行してほしい
        ///    - 候補が2個以上ある状態で、候補を選択しなくてもEnterをおしたらリスト表示してほしい
        ///- メモ
        ///    - しかしうまくいかない
        ///    - PreviewKeyDownを直接取ると、文字を確定させるEnterと、確定後の更なるEnterの区別がつかない？
        ///        - KeyUpだとうまくいった(?)のでこれでいく
        /// </summary>
        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                var vm = this.DataContext as MainWindowViewModel;
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    vm.RunParentCommand.Execute();
                }
                else
                {
                    vm.RunCommand.Execute();
                }
            }
        }
    }
}
