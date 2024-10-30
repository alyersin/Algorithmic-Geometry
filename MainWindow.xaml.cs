using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComputationalGeometry
{
    public partial class MainWindow : Window
    {
        private StackPanel contentPanel;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Title = "Aplicatii in Geometria Computationala";

            // Create the grid with full height and two columns
            Grid grid1 = new Grid
            {
                Background = Brushes.CadetBlue,
                RowDefinitions = { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
                }
            };

            // Create the vertical StackPanel for the left side
            StackPanel verticalPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Background = Brushes.SteelBlue,
                Margin = new Thickness(5)
            };

            // Add label to the StackPanel
            verticalPanel.Children.Add(new Label
            {
                Content = "Geometrie Computationala \n \t Ali Ersin",
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Margin = new Thickness(5)
            });

            // Buttons for the left panel
            string[] buttonNames = { "Cautare Binara", "Merge Sort", "Coliniaritatea", "Localizare in Triunghi", "Localizare in Poligon Convex", "PSLG" };

            foreach (string name in buttonNames)
            {
                Button button = new Button
                {
                    Content = name,
                    Margin = new Thickness(0, 9, 0, 0),
                    Padding = new Thickness(3),
                    Background = Brushes.Bisque
                };

                button.Click += (sender, e) => ShowPanel(name);
                verticalPanel.Children.Add(button);
            }

            // Place the StackPanel in the first column of the grid
            Grid.SetColumn(verticalPanel, 0);
            grid1.Children.Add(verticalPanel);

            // Create the contentPanel for dynamic content on the right
            contentPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetColumn(contentPanel, 1);
            grid1.Children.Add(contentPanel);

            // Set the grid as the content of the window
            this.Content = grid1;
        }

        private void ShowPanel(string panelName)
        {
            contentPanel.Children.Clear();

            switch (panelName)
            {
                case "Cautare Binara":
                    new CautareBinara().ShowCautareBinaraPanel(contentPanel);
                    break;
                case "Coliniaritatea":
                    new Coliniaritatea().ShowColiniaritateaPanel(contentPanel);
                    break;
                case "Localizare in Triunghi":
                    new LocalizareInTriunghi().ShowLocalizareInTriunghiPanel(contentPanel);
                    break;
                case "Localizare in Poligon Convex":
                    var poligonPanel = new LocalizareInPoligonConvex();
                    poligonPanel.ShowMainPanel(contentPanel);
                    break;
            }
        }
    }
}
