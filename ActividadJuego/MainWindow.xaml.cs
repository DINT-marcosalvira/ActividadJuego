﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
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
    public partial class MainWindow : Window
    {
        private string primerCaracter = null;
        private string segundoCaracter = null;
        private TextBlock textBlockExterno = null;
        private List<char> listaExterna = null;

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

        private void ButtonMostrar_Click(object sender, RoutedEventArgs e)
        {
            int numeroFilas = 0;
            if (RadioButtonBaja.IsChecked == true)
            {
                numeroFilas = 3;
            }
            else if (RadioButtonMedia.IsChecked == true)
            {
                numeroFilas = 4;
            }
            else if (RadioButtonAlta.IsChecked == true)
            {
                numeroFilas = 5;
            }

            for (int fila = 0, contadorLista = 0; fila < numeroFilas; fila++)
            {
                for (int z = 0; z < 4; contadorLista++, z++)
                {
                    Border borde = new Border();
                    Viewbox vb = new Viewbox();
                    TextBlock tb = new TextBlock();
                    vb.Child = tb;
                    borde.Child = vb;
                    borde.Background = Brushes.NavajoWhite; 
                    borde.Margin = new Thickness(3);
                    borde.CornerRadius = new CornerRadius(10);
                    tb.FontFamily = new FontFamily("Webdings");
                    tb.Tag = listaExterna[contadorLista];
                    tb.Text = tb.Tag.ToString();
                    GridTabla.Children.Add(borde);
                    Grid.SetRow(borde, fila);
                    Grid.SetColumn(borde, z);
                }
            }
            BarraProgreso.Value = 600;
            MessageBox.Show("¡Partida finalizada!");
        }

        public List<char> GeneraAleatorio(int numeroFilas)
        {
            List<char> listaLetras = new List<char>();

            if (numeroFilas == 3)
            {
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    char l;
                    int letra;
                    do
                    {
                        letra = r.Next(65, 91);
                        l = (char)letra;
                    } while (listaLetras.Contains(l));
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
                    char l;
                    int letra;
                    do
                    {
                        letra = r.Next(65, 91);
                        l = (char)letra;
                    } while (listaLetras.Contains(l));
                    listaLetras.Add(l);
                }

                for (int i = 0; i < 8; i++)
                {
                    listaLetras.Add(listaLetras[i]);
                }
            }
            else if(numeroFilas == 5)
            {
                Random r = new Random();
                for (int i = 0; i < 10; i++)
                {
                    char l;
                    int letra;
                    do
                    {
                        letra = r.Next(65, 91);
                        l = (char)letra;
                    } while (listaLetras.Contains(l));
                    listaLetras.Add(l);
                }

                for (int i = 0; i < 10; i++)
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
            BarraProgreso.Value = 0;
            for (int i = 0; i < numeroFilas; i++)
            {
                GridTabla.RowDefinitions.Add(new RowDefinition());
            }
            

            //Con la lista desordenada y un contador, añadimos al tag toda la lista.
            List<char> lista = GeneraAleatorio(numeroFilas);
            listaExterna = lista;
            for (int fila = 0, contadorLista = 0; fila < numeroFilas; fila++)
            {
                for (int z = 0; z < 4; contadorLista++ ,z++)
                {
                    Border borde = new Border();
                    Viewbox vb = new Viewbox();
                    TextBlock tb = new TextBlock();
                    vb.Child = tb;
                    borde.Child = vb;
                    borde.Margin = new Thickness(3);
                    borde.CornerRadius = new CornerRadius(10);
                    tb.FontFamily = new FontFamily("Webdings");
                    tb.Text = "s";
                    tb.Tag = lista[contadorLista];

                    borde.Background = Brushes.NavajoWhite;
                    GridTabla.Children.Add(borde);
                    Grid.SetRow(borde, fila);
                    Grid.SetColumn(borde, z);

                    borde.MouseLeftButtonDown += Borde_MouseLeftButtonDown1;
                    borde.MouseLeftButtonUp += Borde_MouseLeftButtonUp1;
                }
            }
        }

        private void Borde_MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            Border b = (Border)sender; 
            Viewbox vb = (Viewbox)b.Child;
            TextBlock tb = (TextBlock)vb.Child;
            if (primerCaracter == null)
	        {
                primerCaracter = tb.Tag.ToString();
	        }
            else if (segundoCaracter == null)
	        {
                segundoCaracter= tb.Tag.ToString();
            }

            if(textBlockExterno == null)
            {
                textBlockExterno = tb; 
            }

            if(primerCaracter != null && segundoCaracter != null)
            {
                if (segundoCaracter != primerCaracter)
	            {
                   System.Threading.Thread.Sleep(500);
                   tb.Text = "s";
                   textBlockExterno.Text = "s";
                   primerCaracter = null;
                   segundoCaracter = null;
                   textBlockExterno = null;
	            }
                else
                {
                   ControladorBarraProgreso();
                   primerCaracter = null;
                   segundoCaracter = null;
                   textBlockExterno = null;
                }
            }
            
        }

        public void ControladorBarraProgreso()
        {
            double progresoBaja = 0;
            double progresoMedia = 0;
            double progresoAlta = 0;

            if (RadioButtonBaja.IsChecked == true)
            {
                progresoBaja = BarraProgreso.Maximum / 6;
                BarraProgreso.Value += progresoBaja;
            }
            else if (RadioButtonMedia.IsChecked == true)
            {
                progresoMedia = BarraProgreso.Maximum / 8;
                BarraProgreso.Value += progresoMedia;
            }
            else if (RadioButtonAlta.IsChecked == true)
            {
                progresoAlta = BarraProgreso.Maximum / 10;
                BarraProgreso.Value += progresoAlta;
            }

            if (BarraProgreso.Value == 600)
            {
                MessageBox.Show("¡Partida finalizada!");
            }
        }

        private void Borde_MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            Border b = (Border)sender; 
            Viewbox vb = (Viewbox)b.Child;
            TextBlock tb = (TextBlock)vb.Child;
            if (tb.Text.Equals("s"))
            {
                tb.Text = tb.Tag.ToString();
            }
            else
            {
                b.MouseLeftButtonUp -= Borde_MouseLeftButtonUp1;
            }
            
        }
    }
}
