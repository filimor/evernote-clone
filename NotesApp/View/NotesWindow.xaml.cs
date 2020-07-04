using System.Linq;
using System.Speech.Recognition;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NotesApp.View
{
    /// <summary>
    ///     Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private SpeechRecognitionEngine _recognizer;
        private bool _isRecognizing = false;

        public NotesWindow()
        {
            InitializeComponent();

            RecognizerInfo currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                select r).FirstOrDefault();
            _recognizer = new SpeechRecognitionEngine(currentCulture);

            var builder = new GrammarBuilder();
            builder.AppendDictation();
            var grammar = new Grammar(builder);
            _recognizer.LoadGrammar(grammar);
            _recognizer.SetInputToDefaultAudioDevice();

            _recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            ContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(e.Result.Text)));
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SpeechButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_isRecognizing)
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                _isRecognizing = true;
            }
            else
            {
                _recognizer.RecognizeAsyncStop();
                _isRecognizing = false;
            }
        }

        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters =
                new TextRange(ContentRichTextBox.Document.ContentStart, ContentRichTextBox.Document.ContentEnd).Text
                    .Length;
            StatusTextBlock.Text = $"Document length: {amountOfCharacters} characters";
        }

        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }
    }
}