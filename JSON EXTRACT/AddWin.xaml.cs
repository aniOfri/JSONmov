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
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;

namespace JSON_EXTRACT
{
    /// <summary>
    /// Interaction logic for AddWin.xaml
    /// </summary>
    public partial class AddWin : Window
    {

        string NameTxt;
        bool NameBool;
        Dictionary<string, int> AiredOnTxt = new Dictionary<string, int>();
        List<string> days30 = new List<string>() { "4", "6", "9", "11" };
        bool AiredBool;
        string Year;
        string Day;
        string Month;
        string RatingTxt;
        bool RatingBool;
        string MainCharsTxt;
        bool MainCharsBool;
        string GeneresTxt;
        bool GeneresBool;
        bool InputSuccess;
        

        public AddWin()
        {
            InitializeComponent();
        }

        private void ButtonJson2_Click(object sender, RoutedEventArgs e)
        {
            AiredOnMethod();
            if (InputSuccess)
            {
                Movie movie = new Movie();
                movie.Name = NameTxt;
                movie.Airdate = new DateTime(AiredOnTxt["Year"], AiredOnTxt["Day"], AiredOnTxt["Month"]);
                movie.RottenTomatoRating = Convert.ToInt32(RatingTxt);
                movie.MainCharacters = MainCharsTxt.Split(',').ToArray();
                movie.Genres = GeneresTxt;
                string moviedetails = JsonConvert.SerializeObject(movie);
                File.WriteAllText($@".\Jsons\{NameTxt}.txt", moviedetails);
                LabelJson2.Content = "Status: Success!, " + Environment.NewLine + "Find the movie by typing it's name.";
            }
            else
            {
                LabelJson2.Content = "Status: Error, " + Environment.NewLine + "Invalid Input, Whitespace or invalid format.";
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameTxt = Name.Text;
            NameBool = !string.IsNullOrWhiteSpace(NameTxt);
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GeneresBool);
        }

        private void AiredOnMethod()
        {
            Year = AiredOnYear.Text;
            Day = AiredOnDay.Text;
            Month = AiredOnMonth.Text;
            AiredBool = !string.IsNullOrWhiteSpace(Year) &&
                !string.IsNullOrWhiteSpace(Day) && !string.IsNullOrWhiteSpace(Month);
            if (AiredBool) if ((days30.Contains(Month) && Day == "31")
                || (Convert.ToInt32(Year) % 4 != 0 && Day == "29" && Month == "2")
                || (Convert.ToInt32(Day) > 31 || Convert.ToInt32(Month) > 12)) AiredBool = false;
                if (AiredBool)
                {
                    AiredOnTxt.Add("Year", Convert.ToInt32(Year));
                    AiredOnTxt.Add("Day", Convert.ToInt32(Day));
                    AiredOnTxt.Add("Month",Convert.ToInt32(Month));
                }
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GeneresBool);
        }

        private void RTRating_TextChanged(object sender, TextChangedEventArgs e)
        {
            RatingTxt = RTRating.Text;
            RatingBool = !string.IsNullOrWhiteSpace(RatingTxt);
            if (RatingBool) if (Convert.ToInt32(RatingTxt) > 100) RatingBool = false;
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GeneresBool);
        }

        private void MainChars_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainCharsTxt = MainChars.Text;
            MainCharsBool = !string.IsNullOrWhiteSpace(MainCharsTxt);
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GeneresBool);
        }

        private void Generes_TextChanged(object sender, TextChangedEventArgs e)
        {
            GeneresTxt = Generes.Text;
            GeneresBool = !string.IsNullOrWhiteSpace(GeneresTxt);
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GeneresBool);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }

}
