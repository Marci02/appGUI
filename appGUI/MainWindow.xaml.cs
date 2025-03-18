using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace appGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int change = 0;
        List<AppClass> apps = AppClass.LoadFromCsv("apps.csv");
        public MainWindow()
        {
            InitializeComponent();
            foreach (var app in apps)
            {
                if (!CategoryList.Items.Contains(app.Category.CategoryName))
                {
                    CategoryList.Items.Add(app.Category.CategoryName);
                }
            }

            CategoryList.SelectedIndex = 0;
            AppsCombo.SelectedIndex = 0;
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppsCombo.Items.Clear();
            AppsCombo.SelectedIndex = 0;

            var selectedCategory = CategoryList.SelectedItem.ToString();

            var filteredApps = apps.Where(app => app.Category.CategoryName == selectedCategory);

            foreach (var app in filteredApps)
            {
                   AppsCombo.Items.Add(app);
            }

            change = 0;
            AjanlatBtn.IsEnabled = false;
        }

        private void AppsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VersionNumber.Content = "";
            ContentRateName.Content = "";
            ReviewLbl.Content = "";

            if (AppsCombo.SelectedItem is AppClass selectedApp)
            {
                VersionNumber.Content = selectedApp.currentVer;
                ContentRateName.Content = selectedApp.ContentRating.ContentRatingName;
                ReviewLbl.Content = selectedApp.Reviews;
            }

            change++;
            if(change >= 2)
            {
               AjanlatBtn.IsEnabled = true;
            }
        }

        private void AjanlatBtn_Click(object sender, RoutedEventArgs e)
        {
            
                Random random = new Random();
                int index = random.Next(AppsCombo.Items.Count);
                AppClass selectedApp = (AppClass)AppsCombo.Items[index];

                AppsCombo.SelectedIndex = index;
                VersionNumber.Content = selectedApp.currentVer;
                ContentRateName.Content = selectedApp.ContentRating.ContentRatingName;
                ReviewLbl.Content = selectedApp.Reviews;

                change = 0;
                AjanlatBtn.IsEnabled = false;
            
        }
    }
}