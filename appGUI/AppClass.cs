using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appGUI
{
    internal class AppClass
    {
        public string appName { get; set; }
        public Category Category { get; set; }
        public ContentRating ContentRating { get; set; }
        public string currentVer { get; set; }
        public double rating { get; set; }
        public int Reviews { get; set; }
        public int updateMonth { get; set; }
        public int updateYear { get; set; }


        public static List<AppClass> LoadFromCsv(string fileName)
        {
            List<AppClass> apps = new List<AppClass>();
            using (StreamReader sr = new StreamReader($@"..\..\..\src\{fileName}"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string[] sor = sr.ReadLine().Split(';');
                        string name = sor[0];
                        Category category = new Category(int.Parse(sor[1]), sor[2]);
                        ContentRating contentRating = new ContentRating(int.Parse(sor[3]), sor[4]);
                        double rating = double.Parse(sor[5].Replace('.', ','));
                        int reviews = int.Parse(sor[6]);
                        string currentVer = sor[7];
                        int year = int.Parse(sor[8]);
                        int month = int.Parse(sor[9]);
                        apps.Add(new AppClass(name, category, contentRating, currentVer, rating, reviews, month, year));
                    }
                }

                return apps;
            }
        }

        public override string ToString()
        {
            return $"{appName} {ConvertToStars()}";
        }

        private string ConvertToStars()
        {
            int starCount = (int)Math.Round(rating);
            if (starCount < 0)
            {
                return "-";
            }
            return new string('*', starCount);
        }


        public AppClass(string name, Category category, ContentRating contentRating, string currentVer, double rating, int reviews, int month, int year)
        {
            appName = name;
            Category = category;
            ContentRating = contentRating;
            this.currentVer = currentVer;
            this.rating = rating;
            Reviews = reviews;
            updateMonth = month;
            updateYear = year;
        }

    }
}
