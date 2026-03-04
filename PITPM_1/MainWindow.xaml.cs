using ScottPlot;
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
using Colors = ScottPlot.Colors;

namespace PITPM_1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbStartValue.Text) && !string.IsNullOrWhiteSpace(tbStep.Text) && !string.IsNullOrWhiteSpace(tbCount.Text))
            {
                bool a = double.TryParse(tbStartValue.Text, out double start);
                bool b = double.TryParse(tbStep.Text, out double step);
                bool c = int.TryParse(tbCount.Text, out int count);

                if (a && b && c)
                {
                    double[] xValues = ScottPlot.Generate.Consecutive(count, 1, 1);
                    double[] yValues = new double[count];
                    bool isAccess = false;

                    switch (cbType.SelectedIndex)
                    {
                        case 0:
                            for (int i = 0; i < count; i++)
                            {
                                yValues[i] = start + i * step;
                            }
                            isAccess = true;
                            break;
                        case 1:
                            for (int i = 0; i < count; i++)
                            {
                                yValues[i] = start * Math.Pow(step,i);
                            }
                            isAccess = true;
                            break;
                        case -1:
                            MessageBox.Show("Выберите тип прогрессии", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                    
                    if (isAccess)
                    {
                        MyPlot.Plot.Clear();
                        var scatter = MyPlot.Plot.Add.Scatter(xValues, yValues);
                        scatter.MarkerSize = 10;
                        scatter.MarkerShape = ScottPlot.MarkerShape.OpenSquare;
                        scatter.LineWidth = 2;
                        scatter.LineColor = Colors.DodgerBlue;

                        MyPlot.Plot.Axes.AutoScale();

                        MyPlot.Refresh();
                    }                  
                }
                else
                {
                    MessageBox.Show("Укажите корректные значения", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все текстовые поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}