using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using NotesApp.ViewModel;

namespace NotesApp.View
{
    /// <summary>
    ///     Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private readonly SpeechRecognitionEngine _recognizer;
        private readonly NotesViewModel _viewModel;

        public NotesWindow()
        {
            InitializeComponent();

            _viewModel = new NotesViewModel();
            Container.DataContext = _viewModel;
            _viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                select r).FirstOrDefault();
            if (currentCulture != null)
            {
                _recognizer = new SpeechRecognitionEngine(currentCulture);

                var builder = new GrammarBuilder();
                builder.AppendDictation();
                var grammar = new Grammar(builder);
                _recognizer.LoadGrammar(grammar);
                _recognizer.SetInputToDefaultAudioDevice();

                _recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            }

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);

            FontSizeComboBox.ItemsSource = new List<double>
                {8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72};
        }

        private void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_viewModel.SelectedNote.FileLocation))
            {
                var fileStream = new FileStream(_viewModel.SelectedNote.FileLocation, FileMode.Open);
                var range = new TextRange(ContentRichTextBox.Document.ContentStart, ContentRichTextBox.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
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
            if (_recognizer == null)
            {
                MessageBox.Show("There's no speech recognizers installed.");
                return;
            }

            if ((sender as ToggleButton)?.IsChecked ?? false)
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                _recognizer.RecognizeAsyncStop();
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
            if ((sender as ToggleButton)?.IsChecked ?? false)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            BoldButton.IsChecked = selectedWeight != DependencyProperty.UnsetValue &&
                                   selectedWeight.Equals(FontWeights.Bold);

            var selectedStyle = ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            ItalicButton.IsChecked =
                selectedStyle != DependencyProperty.UnsetValue && selectedStyle.Equals(FontStyles.Italic);

            var selectedDecorations = ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineButton.IsChecked = selectedDecorations != DependencyProperty.UnsetValue &&
                                        selectedDecorations.Equals(TextDecorations.Underline);

            FontFamilyComboBox.SelectedItem =
                ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontFamilyProperty);

            FontSizeComboBox.Text =
                ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty).ToString();
        }

        private void ItalicButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton)?.IsChecked ?? false)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void UnderlineButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton)?.IsChecked ?? false)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    TextDecorations.Underline);
            }
            else
            {
                ((TextDecorationCollection) ContentRichTextBox.Selection.GetPropertyValue(
                    Inline.TextDecorationsProperty)).TryRemove(TextDecorations.Underline,
                    out var textDecorationCollection);
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    textDecorationCollection);
            }
        }

        private void FontFamilyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty,
                    FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSizeComboBox.Text);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            string rtfFile = Path.Combine(Environment.CurrentDirectory, $"Note{_viewModel.SelectedNotebook.Id}.rtf");
            _viewModel.SelectedNote.FileLocation = rtfFile;

            var fileStream = new FileStream(rtfFile, FileMode.Create);
            var range = new TextRange(ContentRichTextBox.Document.ContentStart, ContentRichTextBox.Document.ContentEnd);
            range.Save(fileStream, DataFormats.Rtf);

            _viewModel.UpdateSelectedNote();
        }
    }
}