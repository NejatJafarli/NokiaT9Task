using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NokiaT9Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Words { get; set; }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MainWindow));

        public ObservableCollection<string> SameWords { get; set; } = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Words = new List<string>();
            Words.Add("Mellim");
            Words.Add("Tural-Novruzov");
            Words.Add("Nicat-Ceferli");
            Words.Add("Masin");
            Words.Add("Computer-Academy");
            Words.Add("Salam");
            Words.Add("Step-It-Academy");
            Words.Add("Salam-Aleykum");
            Words.Add("Aleykum-Salam");
            Words.Add("C#-Best");
            Words.Add("Dot.Net-Best");
            Words.Add("Sagol");
            Words.Add("Hello-World");
            Words.Add("Hello");
            Words.Add("Telefon");
            Words.Add("Phone");
            Words.Add("Elon-Musk");
            Words.Add("Tesla");
            Words.Add("Bmw");
            Words.Add("Mercedes");
            Words.Add("Visual-Studio-2022-Is-Best");
            Words.Add("IntelliCode");
            Words.Add("NokiaT9");
            Words.Add("Threads");
            Words.Add("Tpl-Task");

            SameWords = new ObservableCollection<string>(Words);

        }
        public bool Ready { get; set; } = false;

        private void Any_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Ready)
            {
                e.Handled = true;
                return;
            }
            if (IsBack)
            {
                e.Handled = true;
                return;
            }
            var StartPosition = Text.Length;
            var NewText = MyText.Text.Split(' ')[MyText.Text.Split(' ').Length - 1];
            if (NewText == "")
            {
                e.Handled = true;
                return;
            }
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    SameWords.Clear();
                    Ready = true;
                    for (int i = 0; i < Words.Count; i++)
                        if (Words[i].StartsWith(NewText))
                            SameWords.Add(Words[i]);
                    if (SameWords.Count != 0)
                    {
                        var Word = SameWords[0];
                        var Remove = MyText.Text.Split(' ')[MyText.Text.Split(' ').Length - 1];
                        var MyIndex = Word.IndexOf(Remove);
                        Word = Word.Remove(MyIndex, Remove.Length);
                        Text += Word;
                        MyText.Select(StartPosition, Word.Length);
                    }
                    Ready = false;
                });
            });
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
            //var check = new TextRange(Any.Document.ContentStart,
            //     Any.Document.ContentEnd);
            //TextPointer start = Any.Document.ContentStart;
            //var ofset = Any.Document.ContentStart.GetOffsetToPosition(Any.Document.ContentEnd);
            //start = Any.Document.ContentStart.GetPositionAtOffset(ofset-5, LogicalDirection.Forward);
            ////-5 // last 3 char Changed
            //var end = Any.Document.ContentEnd;
            //var range = new TextRange(start, end);

            //range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Black);
            //range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)=>MyText.Text = "";
        public bool IsBack { get; set; }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var NewText = MyText.SelectedText;
            if (!string.IsNullOrWhiteSpace(NewText))
                Words.Add(NewText.Replace(" ", "-"));
        }

        private void MyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                IsBack = true;
            else
                IsBack = false;
        }
    }
}
