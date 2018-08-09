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
using System.Globalization;
using Microsoft.Speech.Recognition;

namespace Speec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //static TextBlock label;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // label = RecordTxtBlock;
           CultureInfo culture = new CultureInfo("ru-ru");
            SpeechRecognitionEngine speech = new SpeechRecognitionEngine(culture);
            speech.SetInputToDefaultAudioDevice();

            speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeecRecognizEvent);

            Choices choices = new Choices();
            choices.Add(new string[] { "один", "два", "три", "четыре", "пять", "яблоко", "привет" });
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = culture;
            grammarBuilder.Append(choices);

            Grammar grammar = new Grammar(grammarBuilder);
            speech.LoadGrammar(grammar);

            speech.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void SpeecRecognizEvent(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.7)
                RecordTxtBlock.Text = e.Result.Text;
        }
    }
}
