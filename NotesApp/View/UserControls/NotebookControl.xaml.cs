using System.Windows;
using System.Windows.Controls;
using NotesApp.Model;

namespace NotesApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for NotebookControl.xaml
    /// </summary>
    public partial class NotebookControl : UserControl
    {


        public Notebook DisplayNotebook
        {
            get => (Notebook)GetValue(DisplayNotebookProperty);
            set => SetValue(DisplayNotebookProperty, value);
        }

        // Using a DependencyProperty as the backing store for DisplayNotebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNotebookProperty =
            DependencyProperty.Register("DisplayNotebook", typeof(Notebook), typeof(NotebookControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotebookControl notebook)
            {
                notebook.NotebookNameTextBlock.Text = ((Notebook) e.NewValue).Name;
            }
        }


        public NotebookControl()
        {
            InitializeComponent();
        }
    }
}
