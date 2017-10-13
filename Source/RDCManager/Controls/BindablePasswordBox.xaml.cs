using System.Windows;
using System.Windows.Controls;

namespace RDCManager.Controls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _ignoreUpdate;

        public BindablePasswordBox()
        {
            InitializeComponent();

            PasswordBox.PasswordChanged += delegate
            {
                _ignoreUpdate = true;

                PasswordPlainText = PasswordBox.Password;
            };
        }

        public string PasswordPlainText
        {
            get { return (string)GetValue(PasswordPlainTextProperty); }
            set { SetValue(PasswordPlainTextProperty, value); }
        }

        public static readonly DependencyProperty PasswordPlainTextProperty =
            DependencyProperty.Register(
            "PasswordPlainText", typeof(string), typeof(BindablePasswordBox),
            new PropertyMetadata(default(string), PasswordPlainTextPropertyChanged));

        private static void PasswordPlainTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bindablePasswordBox = d as BindablePasswordBox;

            if (bindablePasswordBox._ignoreUpdate)
            {
                bindablePasswordBox._ignoreUpdate = false;

                return;
            }

            bindablePasswordBox.PasswordBox.Password = bindablePasswordBox.PasswordPlainText;
        }
    }
}