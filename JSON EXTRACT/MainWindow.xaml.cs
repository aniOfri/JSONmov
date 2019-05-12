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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;

namespace JSON_EXTRACT
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (File.Exists($@".\Jsons\{TextboxJson.Text}.txt"))
            {
                Output();
                LabelJson.Content = "Status: Success.";                
            }
            else
            {
                LabelJson.Content = "Status: Error, Invalid Input.";
                NameOutput.Text = "";
                AiredOnOutput.Text = "";
                RTOutput.Text = "";
                MainCharsOutput.Text = "";
                GenreOutput.Text = "";
            }
            
        }
        private void Output()
        {
            string mainChars = "";
            string JsonInput = File.ReadAllText($@".\Jsons\{TextboxJson.Text}.txt");
            getset.Movie m = JsonConvert.DeserializeObject<getset.Movie>(JsonInput);
            foreach (var i in m.MainCharacters)
            {
                mainChars = mainChars.Insert(mainChars.Length, i).Insert(mainChars.Length, ", ");
            }
            NameOutput.Text = $"Name: {m.Name}";
            AiredOnOutput.Text = $"Aired on: {m.Airdate.ToShortDateString()}";
            RTOutput.Text = $"Rotten Tomatoes Rating: {m.RottenTomatoRating.ToString()}";
            MainCharsOutput.Text = $"Main Characters: {mainChars.TrimStart(',')}";
            GenreOutput.Text = $"Genre: {m.Genres}";
        }
        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            AddWin Add = new AddWin();
            Add.Show();
            this.Close();
        }
    }
}
