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
        // Text
        Dictionary<string, int> AiredOnTxt = new Dictionary<string, int>();
        string NameTxt;
        string RatingTxt;
        string MainCharsTxt;        
        string GenresTxt;
        // Bools
        bool NameBool;
        bool AiredBool;
        bool RatingBool;
        bool MainCharsBool;
        bool GenresBool;
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
                LabelJson2.Content = $"Status: Success!, \n Find the movie by typing {NameTxt}";
                SuccessfulInput();
            }
            else
            {
                LabelJson2.Content = "Status: Error, Invalid Input \n Whitespace or invalid format.";
                AiredOnTxt.Clear();
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameTxt = Name.Text;
            NameBool = !string.IsNullOrWhiteSpace(NameTxt);
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GenresBool);
        }

        private void AiredOnMethod()
        {

            List<string> days30 = new List<string>() { "4", "6", "9", "11" };
            string Year = AiredOnYear.Text;
            string Day = AiredOnDay.Text;
            string Month = AiredOnMonth.Text;
            var invalidDate = (days30.Contains(Month) && Day == "31")
                || (Convert.ToInt32(Year) % 4 != 0 && Day == "29" && Month == "2")
                || (Convert.ToInt32(Day) > 31 || Convert.ToInt32(Month) > 12);
            AiredBool = !string.IsNullOrWhiteSpace(Year) &&
                !string.IsNullOrWhiteSpace(Day) && !string.IsNullOrWhiteSpace(Month);
            if (AiredBool) if (invalidDate) AiredBool = false;
                if (AiredBool)
                {
                    AiredOnTxt.Add("Year", Convert.ToInt32(Year));
                    AiredOnTxt.Add("Day", Convert.ToInt32(Day));
                    AiredOnTxt.Add("Month",Convert.ToInt32(Month));
                }
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GenresBool);
        }

        private void RTRating_TextChanged(object sender, TextChangedEventArgs e)
        {
            RatingTxt = RTRating.Text;
            RatingBool = !string.IsNullOrWhiteSpace(RatingTxt) && Convert.ToInt32(RatingTxt) > 100;           
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GenresBool);
        }

        private void MainChars_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainCharsTxt = MainChars.Text;
            MainCharsBool = !string.IsNullOrWhiteSpace(MainCharsTxt);
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GenresBool);
        }

        private void Genres_TextChanged(object sender, TextChangedEventArgs e)
        {
            GenresTxt = Genres.Text;
            GenresBool = !string.IsNullOrWhiteSpace(GenresTxt);
            InputSuccess = (NameBool && AiredBool && RatingBool
                && MainCharsBool && GenresBool);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void SuccessfulInput()
        {
            getset.Movie movie = new getset.Movie();
            movie.Name = NameTxt;
            movie.Airdate = new DateTime(AiredOnTxt["Year"], AiredOnTxt["Day"], AiredOnTxt["Month"]);
            movie.RottenTomatoRating = Convert.ToInt32(RatingTxt);
            movie.MainCharacters = MainCharsTxt.Split(',').ToArray();
            movie.Genres = GenresTxt;
            string moviedetails = JsonConvert.SerializeObject(movie);
            File.WriteAllText($@".\Jsons\{NameTxt}.txt", moviedetails);
            AiredOnTxt.Clear();
        }
    }

}
