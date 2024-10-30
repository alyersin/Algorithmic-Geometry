using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComputationalGeometry
{
    internal class LocalizareInTriunghi
    {
        public void ShowLocalizareInTriunghiPanel(StackPanel contentPanel)
        {
            contentPanel.Children.Clear();

            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5),
                Background = Brushes.CadetBlue,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            Label label1 = new Label
            {
                Content = "Introduceti Coordonatele Triunghiului",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5),
            };

            panel.Children.Add(label1);

            // Create a Grid for two columns (left for x-coordinates, right for y-coordinates)
            Grid grid = new Grid
            {
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top
            };

            // Define four columns: one for labels, one for textboxes (x-coordinates), then the same for y-coordinates
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Define four rows
            for (int i = 0; i < 4; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            // Define labels and corresponding textboxes for x and y coordinates
            string[] labelsLeft = { "Ax:", "Bx:", "Cx:", "Mx:" };
            string[] labelsRight = { "Ay:", "By:", "Cy:", "My:" };
            TextBox[] textBoxesLeft = new TextBox[4];
            TextBox[] textBoxesRight = new TextBox[4];

            // Create the left side (Ax, Bx, Cx, Mx)
            for (int i = 0; i < 4; i++)
            {
                Label labelLeft = new Label
                {
                    Content = labelsLeft[i],
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5),
                };
                Grid.SetRow(labelLeft, i);
                Grid.SetColumn(labelLeft, 0);
                grid.Children.Add(labelLeft);

                TextBox textBoxLeft = new TextBox
                {
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                };
                Grid.SetRow(textBoxLeft, i);
                Grid.SetColumn(textBoxLeft, 1);
                grid.Children.Add(textBoxLeft);

                textBoxesLeft[i] = textBoxLeft;
            }

            // Create the right side (Ay, By, Cy, My)
            for (int i = 0; i < 4; i++)
            {
                Label labelRight = new Label
                {
                    Content = labelsRight[i],
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5),
                };
                Grid.SetRow(labelRight, i);
                Grid.SetColumn(labelRight, 2);
                grid.Children.Add(labelRight);

                TextBox textBoxRight = new TextBox
                {
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                };
                Grid.SetRow(textBoxRight, i);
                Grid.SetColumn(textBoxRight, 3);
                grid.Children.Add(textBoxRight);

                textBoxesRight[i] = textBoxRight;
            }

            panel.Children.Add(grid);

            // Create and add a button at the bottom
            Button submitButton = new Button
            {
                Content = "Submit",
                Width = 100,
                Height = 30,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            panel.Children.Add(submitButton);

            submitButton.Click += (sender, e) =>
            {
                // Parse input values from textboxes
                if (int.TryParse(textBoxesLeft[0].Text, out int Ax) && int.TryParse(textBoxesRight[0].Text, out int Ay) &&
                    int.TryParse(textBoxesLeft[1].Text, out int Bx) && int.TryParse(textBoxesRight[1].Text, out int By) &&
                    int.TryParse(textBoxesLeft[2].Text, out int Cx) && int.TryParse(textBoxesRight[2].Text, out int Cy) &&
                    int.TryParse(textBoxesLeft[3].Text, out int Mx) && int.TryParse(textBoxesRight[3].Text, out int My))
                {
                    // Calculate areas using determinant method
                    int tMAB = CalculateArea(Mx, My, Ax, Ay, Bx, By);
                    int tMBC = CalculateArea(Mx, My, Bx, By, Cx, Cy);
                    int tMCA = CalculateArea(Mx, My, Cx, Cy, Ax, Ay);

                    string message;
                    if (tMAB > 0 && tMBC > 0 && tMCA > 0)
                        message = "M este in interiorul triunghiului (Zona 1)";
                    else if (tMAB == 0 && tMBC == 0 && tMCA > 0)
                        message = "M coincide cu punctul B";
                    else if (tMAB == 0 && tMBC > 0 && tMCA == 0)
                        message = "M coincide cu punctul A";
                    else if (tMAB > 0 && tMBC == 0 && tMCA == 0)
                        message = "M coincide cu punctul C";
                    else
                        message = "M este in afara sau pe frontiera triunghiului";

                    MessageBox.Show(message); // Display the result message

                    // Open a new window for displaying the triangle and point M
                    Window drawingWindow = new Window
                    {
                        Title = "Vizualizare Triunghi",
                        Width = 400,
                        Height = 400,
                        Content = CreateDrawingCanvas(Ax, Ay, Bx, By, Cx, Cy, Mx, My)
                    };
                    drawingWindow.Show();
                }
                else
                {
                    MessageBox.Show("Introduceti numere valide."); // Show error message
                }
            };

            contentPanel.Children.Add(panel);
        }

        private int CalculateArea(int Ax, int Ay, int Bx, int By, int Cx, int Cy)
        {
            return Ax * By + Bx * Cy + Cx * Ay - Cx * By - Ax * Cy - Bx * Ay;
        }

        private Canvas CreateDrawingCanvas(int Ax, int Ay, int Bx, int By, int Cx, int Cy, int Mx, int My)
        {
            Canvas canvas = new Canvas
            {
                Width = 400,
                Height = 400,
                Background = Brushes.White
            };

            // Define center of the canvas and scale
            int centerX = (int)canvas.Width / 2;
            int centerY = (int)canvas.Height / 2;
            int scale = 5;

            // Calculate positions with scaling
            Point A = new Point(Ax * scale + centerX, centerY - Ay * scale);
            Point B = new Point(Bx * scale + centerX, centerY - By * scale);
            Point C = new Point(Cx * scale + centerX, centerY - Cy * scale);
            Point M = new Point(Mx * scale + centerX, centerY - My * scale);

            // Draw triangle ABC
            Polygon triangle = new Polygon
            {
                Points = new PointCollection { A, B, C },
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            canvas.Children.Add(triangle);

            // Draw point M
            Ellipse pointM = new Ellipse
            {
                Width = 6,
                Height = 6,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(pointM, M.X - 3);
            Canvas.SetTop(pointM, M.Y - 3);
            canvas.Children.Add(pointM);

            // Add labels for points
            AddLabel(canvas, "A", A);
            AddLabel(canvas, "B", B);
            AddLabel(canvas, "C", C);
            AddLabel(canvas, "M", M);

            return canvas;
        }

        private void AddLabel(Canvas canvas, string text, Point location)
        {
            TextBlock label = new TextBlock
            {
                Text = text,
                FontSize = 12,
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(label, location.X + 5);
            Canvas.SetTop(label, location.Y + 5);
            canvas.Children.Add(label);
        }
    }
}
