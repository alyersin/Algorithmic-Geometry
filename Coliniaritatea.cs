using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComputationalGeometry
{
    internal class Coliniaritatea
    {
        public void ShowColiniaritateaPanel(StackPanel contentPanel)
        {
            contentPanel.Children.Clear();

            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5),
                Background = Brushes.CadetBlue,
                VerticalAlignment = VerticalAlignment.Stretch,  // Ensure it stretches vertically
                HorizontalAlignment = HorizontalAlignment.Stretch // Stretch horizontally as well
            };

            Label label1 = new Label
            {
                Content = "Introduceti numarul de elemente:",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,  // Align to the left
                Margin = new Thickness(5)
            };

            TextBox textBox1 = new TextBox
            {
                Width = 200,
                Height = 30,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left // Align to the left
            };

            Button button1 = new Button
            {
                Content = "Submit",
                Width = 100,
                Height = 30,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left // Align to the left
            };

            panel.Children.Add(label1);
            panel.Children.Add(textBox1);
            panel.Children.Add(button1);

            button1.Click += (sender, e) =>
            {
                string numarElemValue = textBox1.Text;

                if (int.TryParse(numarElemValue, out int numarElem))
                {
                    // Create a Grid layout for labels and textboxes
                    Grid grid = new Grid();
                    grid.Margin = new Thickness(5);

                    // Define two columns: one for labels, one for textboxes
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

                    // Define three rows for A, B, C
                    for (int i = 0; i < 3; i++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    }

                    string[] labels = { "A", "B", "C" };
                    TextBox[] textBoxes = new TextBox[3];

                    for (int i = 0; i < 3; i++)
                    {
                        // Create the label for A, B, C
                        Label label = new Label
                        {
                            Content = labels[i],
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = new Thickness(5)
                        };

                        // Add label to the grid
                        Grid.SetRow(label, i);
                        Grid.SetColumn(label, 0);
                        grid.Children.Add(label);

                        // Create the textbox for corresponding label
                        TextBox textBox = new TextBox
                        {
                            Width = 200,
                            Height = 30,
                            Margin = new Thickness(5),
                            HorizontalAlignment = HorizontalAlignment.Left
                        };

                        // Store references to the textboxes
                        textBoxes[i] = textBox;

                        // Add textbox to the grid
                        Grid.SetRow(textBox, i);
                        Grid.SetColumn(textBox, 1);
                        grid.Children.Add(textBox);
                    }

                    panel.Children.Add(grid);

                    // After adding the textboxes and labels, add a button to process the data
                    Button checkButton = new Button
                    {
                        Content = "Submit",
                        Width = 150,
                        Height = 30,
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    checkButton.Click += (s, ev) =>
                    {
                        try
                        {
                            // Parse input from textboxes (space-separated numbers)
                            int[] A = Array.ConvertAll(textBoxes[0].Text.Split(), int.Parse);
                            int[] B = Array.ConvertAll(textBoxes[1].Text.Split(), int.Parse);
                            int[] C = Array.ConvertAll(textBoxes[2].Text.Split(), int.Parse);

                            if (A.Length != numarElem || B.Length != numarElem || C.Length != numarElem)
                            {
                                MessageBox.Show("Numarul de elemente din A, B si C trebuie sa fie egal cu numarul de elemente introdus.");
                                return;
                            }

                            // Call logic function here to process the data
                            ProcessData(A, B, C, numarElem, Application.Current.MainWindow);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Introduceti numere intregi separate prin spatiu.");
                        }
                    };

                    panel.Children.Add(checkButton);
                }
                else
                {
                    // Handle invalid input (not a number)
                    MessageBox.Show("Introduceti un numar valid");
                }
            };

            // Add the fully stretched panel to the contentPanel
            contentPanel.Children.Add(panel);
        }

        //COLINIARITATE LOGIC
        public static void CitireVector(int[] v, string input)
        {
            string[] valori = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = int.Parse(valori[i]);
            }
        }

        public static void Quicksort(int[] v, int stg, int dr)
        {
            if (stg < dr)
            {
                int i = stg, j = dr, mutare = 0;
                int mijl = (stg + dr) / 2;
                Swap(v, stg, mijl);
                while (i < j)
                {
                    if (v[i] > v[j])
                    {
                        Swap(v, i, j);
                        mutare = 1 - mutare;
                    }
                    i += mutare;
                    j -= (1 - mutare);
                }
                Quicksort(v, stg, i - 1);
                Quicksort(v, i + 1, dr);
            }
        }

        public static void Swap(int[] v, int i, int j)
        {
            int temp = v[i];
            v[i] = v[j];
            v[j] = temp;
        }

        public static bool VerificareColiniaritate(int[] A, int[] B, int[] C, int n)
        {
            Quicksort(B, 0, n - 1);
            Quicksort(C, 0, n - 1);

            int[] D = new int[n];
            for (int i = 0; i < n; i++)
            {
                D[i] = 2 * B[i];
            }

            for (int i = 0; i < n; i++)
            {
                int ai = A[i];

                int[] E = new int[n];
                for (int j = 0; j < n; j++)
                {
                    E[j] = ai + C[j];
                }

                int jIndex = 0, kIndex = 0;
                while (jIndex < n && kIndex < n)
                {
                    if (D[jIndex] == E[kIndex])
                    {
                        return true;
                    }
                    if (D[jIndex] < E[kIndex])
                    {
                        jIndex++;
                    }
                    else
                    {
                        kIndex++;
                    }
                }
            }

            return false;
        }

        public static void ProcessData(int[] a, int[] b, int[] c, int n, Window fereastra)
        {
            bool rezultat = VerificareColiniaritate(a, b, c, n);

            if (rezultat)
            {
                MessageBox.Show(fereastra, "Exista coliniaritate!");
            }
            else
            {
                MessageBox.Show(fereastra, "Nu exista coliniaritate!");
            }
        }
    }
}
