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

        public List<char> GeneraAleatorio(int numeroFilas)
        {
            List<char> listaLetras = new List<char>();

            if (numeroFilas == 3)
            {
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    int letra = r.Next(65,91);
                    char l = (char)letra;
                    listaLetras.Add(l);
                }
                //Añade la pareja de cada letra
                for (int i = 0; i < 6; i++)
                {
                    listaLetras.Add(listaLetras[i]);
                }
            }
            else if(numeroFilas == 4)
            {
                Random r = new Random();
                for (int i = 0; i < 8; i++)
                {
                    int letra = r.Next(65, 91);
                    char l = (char)letra;
                    listaLetras.Add(l);
                }

                for (int i = 0; i < 6; i++)
                {
                    listaLetras.Add(listaLetras[i]);
                }
            }
            else if(numeroFilas == 5)
            {
                Random r = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int letra = r.Next(65, 91);
                    char l = (char)letra;
                    listaLetras.Add(l);
                }

                for (int i = 0; i < 6; i++)
                {
                    listaLetras.Add(listaLetras[i]);
                }
            }
            listaLetras = DesordenarList(listaLetras);
            return listaLetras;
        }

        public List<char> DesordenarList(List<char> lista)
        {
            //Desordenar la lista y posiciones
            List<char> arr = lista;
            List<char> arrDes = new List<char>();

            Random randNum = new Random();
            while (arr.Count > 0)
            {
                int val = randNum.Next(0, arr.Count - 1);
                arrDes.Add(arr[val]);
                arr.RemoveAt(val);
            }

            return arrDes;
        }

        public void InicioNivel(int numeroFilas)
        {
            GridTabla.RowDefinitions.Clear();

            for (int i = 0; i < numeroFilas; i++)
            {
                GridTabla.RowDefinitions.Add(new RowDefinition());
            }
            //Con la lista desordenada y un contador, añadimos al tag toda la lista.
            List<char> lista = GeneraAleatorio(numeroFilas);

            for (int fila = 0, contadorLista = 0; fila < numeroFilas; fila++)
            {
                for (int z = 0; z < 4; contadorLista++ ,z++)
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
                    tb.Tag = lista[contadorLista];
                    borde.Background = Brushes.Beige;
                    GridTabla.Children.Add(borde);
                    Grid.SetRow(borde, fila);
                    Grid.SetColumn(borde, z);
                }
            }
        }
    }
}
