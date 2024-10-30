using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ComputationalGeometry
{
    public class LocalizareInPoligonConvex
    {
        private StackPanel contentPanel;
        private TextBox campNrPuncte;
        private TextBox[] campuriCoord;
        private TextBox campCoordX, campCoordY;
        private Label etRezultat;
        private int totalPuncte;

        public void ShowMainPanel(StackPanel panel)
        {
            contentPanel = panel;
            ShowFirstPage();
        }

        // Show the first page where the user enters the number of points
        // Show the first page where the user enters the number of points
        private void ShowFirstPage()
        {
            contentPanel.Children.Clear();

            var pag1 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.CadetBlue,
            };

            var etNrPuncte = new Label
            {
                Content = "Introduceti numarul de puncte al poligonului:",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };

            // Initialize the TextBox and make sure it is added to the panel
            campNrPuncte = new TextBox
            {
                Width = 200,
                Height = 30,
                MaxLength = 5,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5)
            };

            var btnUrmator = new Button
            {
                Content = "Submit",
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            btnUrmator.Click += (sender, e) => ShowSecondPage();

            pag1.Children.Add(etNrPuncte);
            pag1.Children.Add(campNrPuncte); // Ensure the TextBox is added
            pag1.Children.Add(btnUrmator);

            contentPanel.Children.Add(pag1);
        }


        // Show the second page where the user enters points of the polygon and point M
        private void ShowSecondPage()
        {
            if (!int.TryParse(campNrPuncte.Text, out totalPuncte) || totalPuncte <= 0)
            {
                MessageBox.Show("Introduceti un numar valid de puncte.");
                return;
            }

            contentPanel.Children.Clear();

                
            var pag2 = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(10), Background = Brushes.CadetBlue };
            var panouIntro = new StackPanel { Orientation = Orientation.Vertical };

            campCoordX = new TextBox { Width = 80, Margin = new Thickness(2, 0, 5, 2) };
            campCoordY = new TextBox { Width = 80, Margin = new Thickness(2, 0, 5, 2) };
            etRezultat = new Label { HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5), FontWeight = FontWeights.Bold };

            campuriCoord = new TextBox[totalPuncte * 2];

            for (int i = 0; i < totalPuncte; i++)
            {
                var horizontalPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5, 2, 5, 2) };

                var labelX = new Label
                {
                    Content = $"Punct {i + 1} - X:",
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 5, 0),
                };
                campuriCoord[i * 2] = new TextBox { Width = 80, Margin = new Thickness(0, 0, 5, 0) };

                var labelY = new Label
                {
                    Content = $"Punct {i + 1} - Y:",
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 5, 0)
                };
                campuriCoord[i * 2 + 1] = new TextBox { Width = 80, Margin = new Thickness(0, 0, 5, 0) };

                horizontalPanel.Children.Add(labelX);
                horizontalPanel.Children.Add(campuriCoord[i * 2]);
                horizontalPanel.Children.Add(labelY);
                horizontalPanel.Children.Add(campuriCoord[i * 2 + 1]);

                panouIntro.Children.Add(horizontalPanel);
            }

            // Applying the same style to point M inputs
            var mPointPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5, 2, 5, 2) };

            var mLabelX = new Label
            {
                Content = "Punct de cautat M - X:",
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 5, 0)
            };
            campCoordX = new TextBox { Width = 80, Margin = new Thickness(0, 0, 5, 0) };

            var mLabelY = new Label
            {
                Content = "Punct de cautat M - Y:",
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 5, 0)
            };
            campCoordY = new TextBox { Width = 80, Margin = new Thickness(0, 0, 5, 0) };

            mPointPanel.Children.Add(mLabelX);
            mPointPanel.Children.Add(campCoordX);
            mPointPanel.Children.Add(mLabelY);
            mPointPanel.Children.Add(campCoordY);

            var btnLocalizare = new Button
            {
                Content = "Localizeaza",
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            btnLocalizare.Click += (sender, e) => LocalizeazaPunctul();

            pag2.Children.Add(etRezultat);
            pag2.Children.Add(panouIntro);
            pag2.Children.Add(mPointPanel); // Adding M point panel
            pag2.Children.Add(btnLocalizare);

            contentPanel.Children.Add(pag2);
        }



        // Localize the point M
        private void LocalizeazaPunctul()
        {
            var puncteForm = new List<PunctForm>();

            try
            {
                for (int i = 0; i < totalPuncte; i++)
                {
                    int coordX = int.Parse(campuriCoord[i * 2].Text);
                    int coordY = int.Parse(campuriCoord[i * 2 + 1].Text);
                    puncteForm.Add(new PunctForm(coordX, coordY));
                }

                int coordXdeCautat = int.Parse(campCoordX.Text);
                int coordYdeCautat = int.Parse(campCoordY.Text);
                PunctForm punctM = new PunctForm(coordXdeCautat, coordYdeCautat);

                int centruX = CalculeazaXCentru(puncteForm[0].X, puncteForm[1].X, puncteForm[2].X);
                int centruY = CalculeazaYCentru(puncteForm[0].Y, puncteForm[1].Y, puncteForm[2].Y);
                for (int i = 0; i < puncteForm.Count; i++)
                {
                    var punctAjustat = new PunctForm(puncteForm[i].X - centruX, puncteForm[i].Y - centruY);
                    puncteForm[i] = punctAjustat;
                }
                punctM = new PunctForm(punctM.X - centruX, punctM.Y - centruY);

                CautareBinara(puncteForm, punctM);
            }
            catch (FormatException)
            {
                MessageBox.Show("Introduceti coordonate valide pentru toate punctele.");
            }
        }

        private void CautareBinara(List<PunctForm> puncteForm, PunctForm punctM)
        {
            int cadranM = DeterminaCadran(punctM.X, punctM.Y);
            int inceput = 0, sfarsit = puncteForm.Count - 1;
            while (inceput < sfarsit - 1)
            {
                int mijloc = (inceput + sfarsit) / 2;
                int cadranMijloc = DeterminaCadran(puncteForm[mijloc].X, puncteForm[mijloc].Y);
                if (cadranM > cadranMijloc || (cadranM == cadranMijloc && CalculeazaTriunghi(punctM.X, punctM.Y, 0, 0, puncteForm[mijloc].X, puncteForm[mijloc].Y) > 0))
                {
                    inceput = mijloc;
                }
                else
                {
                    sfarsit = mijloc;
                }
            }

            int rezultat = CalculeazaTriunghi(punctM.X, punctM.Y, puncteForm[inceput].X, puncteForm[inceput].Y, puncteForm[sfarsit].X, puncteForm[sfarsit].Y);
            if (rezultat > 0)
            {
                etRezultat.Content = "M se afla in interiorul poligonului";
            }
            else if (rezultat < 0)
            {
                etRezultat.Content = "M se afla in exteriorul poligonului";
            }
            else
            {
                etRezultat.Content = "M pe frontiera";
            }
        }

        static int CalculeazaXCentru(int x1, int x2, int x3) => (x1 + x2 + x3) / 3;
        static int CalculeazaYCentru(int y1, int y2, int y3) => (y1 + y2 + y3) / 3;

        static int DeterminaCadran(int x, int y)
        {
            if (x > 0 && y >= 0) return 1;
            if (x <= 0 && y > 0) return 2;
            if (x < 0 && y <= 0) return 3;
            if (x >= 0 && y < 0) return 4;
            return 0;
        }

        static int CalculeazaTriunghi(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            return (x1 * y2 + x2 * y3 + x3 * y1 - x3 * y2 - x1 * y3 - x2 * y1);
        }

        class PunctForm
        {
            public int X { get; }
            public int Y { get; }

            public PunctForm(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
