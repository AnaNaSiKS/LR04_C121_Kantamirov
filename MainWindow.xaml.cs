using LR04_C121_Kantamirov.Модель;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Clock = LR04_C121_Kantamirov.Модель.Clock;

namespace LR04_C121_Kantamirov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Clock> _Clocks = new List<Clock>();
        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            GetDayNow();
            GetTimeNow();
            GetCountOfClocks();
        }
        //Получить текующую дату + время
        public void GetDayNow()
        {
            DateText.Text = " Сегодня: " + DateTime.Now.ToString("D");
        }
        public void GetTimeNow(){
            TimeText.Text = " Текущее время: " + DateTime.Now.ToString("t");
        }

        public void GetCountOfClocks()
        {
            CountOfClocksTextBox.Text = "Количество записей: " + _Clocks.Count.ToString();
        }
        //Проверка данных 2
        public bool DataEntryValidation(string model,
                     string brand,
                     string producerFactory,
                     string typeOfWatch,
                     string presenceOfDefects,
                     DateTime releaceDate,
                     double price,
                     int countOfClock)
        {

            int pass = 0;

            #region Проверка данных

            if (releaceDate > DateTime.Parse("01.01.1334") && releaceDate < DateTime.Now)
            {
                pass++;
            }

            if (price > 0)
            {
                pass++;
            }

            if (countOfClock > 0)
            {
                pass++;
            }
            #endregion Проверка данных

            if (pass == 3)
                return true;
            else return false;
        }
        public string SelectedValueTypeOfClock { get; private set; }
        private void RadioButtonTypeOfWatch_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                SelectedValueTypeOfClock = item.Content.ToString();
            }

        }
        public string SelectedValuePresenceOfDefects { get; private set; }
        private void RadioButtonPresenceOfDefects_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                SelectedValuePresenceOfDefects = item.Content.ToString();
            }

        }
        //получение объекта класса Клок и проверка данных 1
        public Clock GetClock()
        {
            int pass = 0;
            string model = ModelTextBox.Text;
            string brand = BrandTextBox.Text;
            string producerFactory = ProducerFactoryTextBox.Text;
            string typeOfWatch = SelectedValueTypeOfClock;
            string presenceOfDefects = SelectedValuePresenceOfDefects;

            if (model != "") pass++;
            else ModelTextBox.Text = "Введите значение";

            if (brand != "") pass++;
            else BrandTextBox.Text = "Введите значение";

            if (producerFactory != "") pass++;
            else ProducerFactoryTextBox.Text = "Введите значение";

            if (typeOfWatch != null) pass++;
            else
            {
                RadioButtonTypeOfClock1.Background = new SolidColorBrush(Colors.Red);
                RadioButtonTypeOfClock2.Background = new SolidColorBrush(Colors.Red);
                RadioButtonTypeOfClock3.Background = new SolidColorBrush(Colors.Red);
                RadioButtonTypeOfClock4.Background = new SolidColorBrush(Colors.Red);
            }

            if (presenceOfDefects != null) pass++;
            else
            {
                RadioButtonPresenceOfDefects1.Background = new SolidColorBrush(Colors.Red);
                RadioButtonPresenceOfDefects2.Background = new SolidColorBrush(Colors.Red);
            }

            if (DateTime.TryParse(ReleaceDatePickDate.Text, out DateTime releaceDate)) { pass++; }
            else ReleaceDatePickDate.Text = "Неверный формат даты";

            if (double.TryParse(PriceTextBox.Text, out double price)) { pass++; }
            else PriceTextBox.Text = "Неверный фомат числа (double)";

            if (int.TryParse(CountTextBox.Text, out int countOfClock)) { pass++; }
            else CountTextBox.Text = "Неверный формат числа (int)";

            if (pass == 8)
                if (DataEntryValidation(model, brand, producerFactory, typeOfWatch, presenceOfDefects, releaceDate, price, countOfClock))
                {
                    Clock clock = new Clock(model, brand, producerFactory, typeOfWatch, presenceOfDefects, releaceDate, price, countOfClock);
                    return clock;
                }
                else return null;
            else return null;
        }
        //кнопка закрытия
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //Кнопка сохранения в listbox
        private void SaveInfo_Click(object sender, RoutedEventArgs e)
        {
            Clock clock = GetClock();
            if (clock != null)
            {
                _Clocks.Add(clock);
                ViewClocksList();
            }
            else MessageBox.Show("Введите все данные о часах");
        }
        //Показать все объекта класса Клок
        public void ViewClocksList(){
            ListClocks.Items.Clear();
            foreach (Clock clock in _Clocks) ListClocks.Items.Add(clock);
        }

        private void CopyElementListBox_Click(object sender, RoutedEventArgs e) {
            int index = ListClocks.SelectedIndex;
            string textData;
            if (index > -1)
            {
                Clock clock = _Clocks[index];
                textData = clock.ToString();
                Clipboard.Clear();
                Clipboard.SetText(textData);
            }
        }

        //Удалить нужный элемент
        private void DeleteElement_Click(object sender, RoutedEventArgs e){
            int index = ListClocks.SelectedIndex;
            if (index > -1)
            {
                _Clocks.RemoveAt(index);
                ListClocks.Items.RemoveAt(ListClocks.SelectedIndex);
            }
        }
        #region MouseMove обработчик исчезновения 
        private void RadioButtonTypeOfClockMouseMove(object sender, MouseEventArgs e)
        {
            RadioButtonTypeOfClock1.Background = new SolidColorBrush(Colors.White);
            RadioButtonTypeOfClock2.Background = new SolidColorBrush(Colors.White);
            RadioButtonTypeOfClock3.Background = new SolidColorBrush(Colors.White);
            RadioButtonTypeOfClock4.Background = new SolidColorBrush(Colors.White);
        }

        private void RadioButtonPresenceOfDefectsMouseMove(object sender, MouseEventArgs e)
        {
            RadioButtonPresenceOfDefects1.Background = new SolidColorBrush(Colors.White);
            RadioButtonPresenceOfDefects2.Background = new SolidColorBrush(Colors.White);
        }
        private void ModelTextBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (ModelTextBox.Text == "Введите значение") ModelTextBox.Text = null;
        }

        private void BrandTextBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (BrandTextBox.Text == "Введите значение") BrandTextBox.Text = null;
        }

        private void ProducerFactoryTextBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (ProducerFactoryTextBox.Text == "Введите значение") ProducerFactoryTextBox.Text = null;
        }

        private void PriceTextBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (PriceTextBox.Text == "Неверный фомат числа (double)") PriceTextBox.Text = null;
        }

        private void CountTextBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (CountTextBox.Text == "Неверный формат числа (int)") CountTextBox.Text = null;
        }
        #endregion
        public void SetTheme(string color) {
            DockPanelButton.Style = (Style)DockPanelButton.FindResource(color+"ThemeDockPanel");
            DockPanelTitle.Style = (Style)DockPanelTitle.FindResource(color+"ThemeDockPanel");
            StackPanelButton.Style = (Style)StackPanelButton.FindResource(color+"ThemeStackPanel");
            DockPanelMainTextBox1.Style = (Style)DockPanelMainTextBox1.FindResource(color + "ThemeDockPanel");
            DockPanelMainTextBox2.Style = (Style)DockPanelMainTextBox2.FindResource(color + "ThemeDockPanel");
            ProducerFactoryTextBox.Style = (Style)ProducerFactoryTextBox.FindResource(color+"ThemeTextBox");
            ModelTextBox.Style = (Style)ModelTextBox.FindResource(color+"ThemeTextBox");
            BrandTextBox.Style = (Style)BrandTextBox.FindResource(color+"ThemeTextBox");
            ReleaceDatePickDate.Style = (Style)ReleaceDatePickDate.FindResource(color+"ThemeTextBox");
            CountTextBox.Style = (Style)CountTextBox.FindResource(color+"ThemeTextBox");
            PriceTextBox.Style = (Style)PriceTextBox.FindResource(color+"ThemeTextBox");
            MainGrid.Style = (Style)MainGrid.FindResource(color+"ThemeGrid");
            ListClocks.Style = (Style)ListClocks.FindResource(color+"ThemeListBox");
        }

        private void BlackTheme_Click(object sender, RoutedEventArgs e){
            SetTheme("Black");
        }
        private void WhiteTheme_Click(object sender, RoutedEventArgs e) {
            SetTheme("White");
        }
        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {
            SetTheme("Blue");
        }

        private void KhakiTheme_Click(object sender, RoutedEventArgs e)
        {
            SetTheme("Khaki");
        }
  
        private void DarkSeaGreenTheme_Click(object sender, RoutedEventArgs e)
        {
            SetTheme("DarkSeaGreen");
        }
        private void SelectedComboBox(object sender, RoutedEventArgs e)
        {
            double size;
            string text;
            text = ComboBoxSize.Text;
            switch (text) {
                case "14":
                    size = 14;
                    TitleText.FontSize = size;
                    break;
                case "16":
                    size = 16;
                    TitleText.FontSize = size;
                    break;
                case "18":
                    size = 18;
                    TitleText.FontSize = size;
                    break;
                case "22":
                    size = 22;
                    TitleText.FontSize = size;
                    break;
                case "26":
                    size = 26;
                    TitleText.FontSize = size;
                    break;
            }
        }


        private void ToggleButtonBold_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButtonBold.IsChecked == true)
                TitleText.FontWeight = FontWeights.UltraBold;
            else TitleText.FontWeight = FontWeights.Normal;
        }   

        private void ToggleButtonItalic_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButtonItalic.IsChecked == true)
                TitleText.FontStyle = FontStyles.Italic;
            else TitleText.FontStyle= FontStyles.Normal;
        }

        private void ToggleButtonUnderline_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButtonUnderline.IsChecked == true)
                TitleText.TextDecorations= TextDecorations.Underline;
            else TitleText.TextDecorations = null;
        }

        private void ButtonLeftAlign_Click(object sender, RoutedEventArgs e)
        {
                TitleText.TextAlignment = TextAlignment.Left;
        }

        private void ButtonCenterAlign_Click(object sender, RoutedEventArgs e)
        {
                TitleText.TextAlignment = TextAlignment.Center;
        }

        private void ButtonRightAlign_Click(object sender, RoutedEventArgs e)
        {
                TitleText.TextAlignment = TextAlignment.Right;
        }

        private void CopyList_Click(object sender, RoutedEventArgs e)
        {
            string textData = " ";
        }
    }
}
