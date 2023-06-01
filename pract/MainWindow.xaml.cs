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
using System.Windows.Shapes;
using Entity;
using Controller;

namespace pract
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

        public List<Researcher> Researchers
        {
            get
            {
                ResearcherController researcherController = new ResearcherController();
                return researcherController.LoadAllResearchers();
            }
        }

       public List<Publication> Publications
{
    get
    {
        if (ResearcherListView.SelectedItem != null)
        {
            Researcher selectedResearcher = ResearcherListView.SelectedItem as Researcher;
            PublicationController publicationController = new PublicationController();
            List<Publication> publications = publicationController.SearchByResearcher(selectedResearcher);
            return publications;
        }
        else
        {
            return null;
        }
    }
}


        private void EmploymentLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null)
            {
                if (e.AddedItems[0].ToString() == "Student")
                {
                    ResearcherListView.ItemsSource = ResearcherController.FilterByType(true, Researchers);
                }
                else if (e.AddedItems[0].ToString() == "Staff")
                {
                    ResearcherListView.ItemsSource = ResearcherController.FilterByType(false, Researchers);
                }
                else
                {
                    ResearcherListView.ItemsSource = Researchers;
                }
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputName = NameTextBox.Text;
            // Perform the database query using the inputName and retrieve the information
            // Update the ResearcherListView.ItemsSource with the retrieved information
            // You can use LINQ or any other database access method to query the database
        }
private void PublicationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
{

    
}




private void ResearcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (e.AddedItems.Count > 0)
    {
        Researcher selectedResearcher = e.AddedItems[0] as Researcher;

        // Update the PublicationsListView with publications for the selected researcher
        PublicationController publicationController = new PublicationController();
        List<Publication> publications = publicationController.SearchByResearcher(selectedResearcher);
        PublicationsListView.ItemsSource = publications;

        if (selectedResearcher != null)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedResearcher.Photo);
            bitmap.EndInit();
            ImageSource imageSource = bitmap;

            ResearcherPhoto.Source = imageSource;
        }
    }
}

    }
}
