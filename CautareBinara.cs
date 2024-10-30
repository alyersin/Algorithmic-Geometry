using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComputationalGeometry
{
    internal class CautareBinara
    {
        public void ShowCautareBinaraPanel(StackPanel contentPanel)
        {
            contentPanel.Children.Clear();

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            panel.Margin = new System.Windows.Thickness(5);
            panel.Background = Brushes.CadetBlue;

            Label label1 = new Label();
            label1.Content = "Cautare Binara";
            label1.FontSize = 16;
            label1.FontWeight = FontWeights.Bold;


            panel.Children.Add(label1);

            TextBox textBox1 = new TextBox();
            textBox1.Width = 200;
            textBox1.Height = 30;
            textBox1.Margin = new System.Windows.Thickness(5);

            panel.Children.Add(textBox1);

            
            Button button1 = new Button();
            button1.Content = "Search";
            button1.Width = 100;
            button1.Margin = new System.Windows.Thickness(5);

            button1.Click += (sender, e) =>
            {
                MessageBox.Show("Hello from the Binary Search");
            };

            panel.Children.Add(button1);

            contentPanel.Children.Add(panel);



        }

    }
}
