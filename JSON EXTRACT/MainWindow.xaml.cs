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
        string Input = "";
        bool Check = false;               
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var LineBreaker = (Environment.NewLine + Environment.NewLine);
            if (File.Exists($@".\Jsons\{Input}.txt")) Check = true;
            else
            {
                LabelJson.Content = "Status: Error, Invalid Input.";
                Result.Text = "Name:" + LineBreaker +
                "Aired on:" + LineBreaker +
                "Rotten Tomatoes Rating:" + LineBreaker +
                "Main Characters:" + LineBreaker +
                "Genres:";
            }
            if (Check)
            {                
                LabelJson.Content = "Status: Success.";
                string JsonInput = File.ReadAllText($@".\Jsons\{Input}.txt");
                Movie m = JsonConvert.DeserializeObject<Movie>(JsonInput);
                string AirdateNoTime = m.Airdate.ToShortDateString();
                Result.Text = $"Name: {m.Name}" + LineBreaker +
                $"Aired on: { AirdateNoTime }" + LineBreaker +
                $"Rotten Tomatoes Rating: { m.RottenTomatoRating }" + LineBreaker +
                $"Main Characters: { m.MainCharacters[0] }, { m.MainCharacters[1] }" + LineBreaker +
                $"Genres: { m.Genres }";
                Check = false;
            }
            
        }

        private void InputText(object sender, TextChangedEventArgs e)
        {
            Input = TextboxJson.Text;
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            AddWin Add = new AddWin();
            Add.Show();
            this.Close();
        }
    }
    public class Movie
    {
        public string Name { get; set; }
        public DateTime Airdate { get; set; }
        public int RottenTomatoRating { get; set; }
        public string[] MainCharacters { get; set; }
        public string Genres { get; set; }
    }
}
