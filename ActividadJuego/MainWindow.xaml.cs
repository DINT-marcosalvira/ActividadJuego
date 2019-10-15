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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActividadJuego
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButtonBaja.IsChecked == true)
            {
                InicioNivel(3);
            }
            else if(RadioButtonMedia.IsChecked == true)
            {
                InicioNivel(4);
            }
            else if(RadioButtonAlta.IsChecked == true)
            {
                InicioNivel(5);
            }
        }

        public void InicioNivel(int numeroFilas)
        {
            GridTabla.RowDefinitions.Clear();

            for (int i = 0; i < numeroFilas; i++)
            {
                GridTabla.RowDefinitions.Add(new RowDefinition());
            }

            for (int fila = 0; fila < numeroFilas; fila++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Border borde = new Border();
                    Viewbox vb = new Viewbox();
                    TextBlock tb = new TextBlock();
                    vb.Child = tb;
                    borde.Child = vb;
                    borde.BorderBrush = Brushes.Black;
                    borde.BorderThickness = new Thickness(3);
                    borde.Margin = new Thickness(3);
                    borde.CornerRadius = new CornerRadius(10);
                    tb.FontFamily = new FontFamily("Webdings");
                    tb.Text = "s";
                    borde.Background = LinearGradientBrush(); Brushes.Beige;
                    GridTabla.Children.Add(borde);
                    Grid.SetRow(borde, fila);
                    Grid.SetColumn(borde, z);
                }
                
            }
        }
    }
}
