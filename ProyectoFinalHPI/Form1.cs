using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinalHPI
{
    public partial class Form1 : Form
    {
        //double t;
        string RutaDocu = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string Dia;
        DateTime Tiempo = DateTime.Now;
        string[] CarrosParqueados = new string[20];
        int[,] ValoresCarrosP = new int[20, 2];
        int[,] CantidadDias = new int[20, 2];
        int PosCarro = 0;
        int indice;
        int tiempo = 1000;

        public Form1()
        {

            InitializeComponent();
            RutaDocu += "\\pruebasParqueadero.txt";
            textBox1.MaxLength = 6;

            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (!VerificacionPlaca())
            {
                MessageBox.Show("Placa Incorrecta");
                textBox1.Text = "";

            }
            else if (VerificaionEspaciosYrepeticion() == 0)
            {
                if (PosCarro < CarrosParqueados.GetLength(0))
                {
                    //Lpruebas.Text = "";
                    CarrosParqueados[PosCarro] = textBox1.Text;
                    // Tiempos[PosCarro, 0] = Tiempo.Hour;
                    ///CarrosParqueados[PosCarro, 1] = $"{Dia} {Tiempo.Hour}:{Tiempo.Minute}";
                    if (Tiempo.Hour == 0) {
                        ValoresCarrosP[PosCarro, 0] = 60 * 12 + Tiempo.Minute;
                        //ValoresCarrosP[PosCarro, 0] +=Tiempo.Minute;

                    }
                    else {
                        ValoresCarrosP[PosCarro, 0] = 60 * Tiempo.Hour + Tiempo.Minute;
                        //ValoresCarrosP[PosCarro, 0] +=Tiempo.Minute;
                    }
                    checkedListBox1.Items.Add(textBox1.Text);
                    CantidadDias[PosCarro, 0] = Tiempo.Day;
                    //Tiempo.Day;
                    StreamWriter W2 = new StreamWriter(RutaDocu, true);
                    W2.WriteLine($"{Dia} {Tiempo.Hour}:{Tiempo.Minute}");
                    W2.Close();
                    StreamWriter W1 = new StreamWriter(RutaDocu, true);
                    W1.WriteLine(textBox1.Text);
                    W1.Close();
                    StreamWriter W3 = new StreamWriter(RutaDocu, true);
                    W3.WriteLine(" ");
                    W3.Close();
                    StreamWriter W4 = new StreamWriter(RutaDocu, true);
                    W4.WriteLine(" ");
                    W4.Close();
                    textBox1.Text = "";
                    //PosCarro++;
                }
                else { button1.Enabled = false; }



                //W3.Close();
                //string[] Arreglito = File.ReadAllLines(RutaDocu);


            }
            else
            {
                MessageBox.Show("Placa Duplicada o Parqueadero sin espacio");
                textBox1.Text = "";
            }




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            indice = checkedListBox1.SelectedIndex;
            ImprimirSalidatxt(checkedListBox1.Items[indice].ToString());
            // Lpruebas.Text = checkedListBox1.Items[indice].ToString();

            checkedListBox1.Items.Remove(checkedListBox1.Items[indice]);
            //CarrosParqueados[indice, 2] = $"{Dia}  {Tiempo.Hour}:{Tiempo.Minute}";


            //StreamWriter W1 = new StreamWriter(RutaDocu, true);
            //W1.WriteLine($"{Dia}  {Tiempo.Hour}:{Tiempo.Minute}");
            //W1.Close();
            //label1.Text = indice.ToString();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            indice = checkedListBox1.SelectedIndex;
            if (indice == -1)
            {
                button2.Enabled = false;
            }
            else { button2.Enabled = true; }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool VerificacionPlaca() {
            textBox1.Text += "      ";
            for (int i = 0; i <= 2; i++)
            {

                if (!char.IsLetter(textBox1.Text[i]))
                {
                    return false;

                }

                if (!char.IsNumber(textBox1.Text[3 + i]))
                {
                    return false;


                }

            }
            return true;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Lpruebas.Text = timer1.Interval.ToString();

            Tiempo = Tiempo.AddHours(0.25);
            Lhora.Text = Tiempo.Hour.ToString();
            Lhora.Text += ": " + Tiempo.Minute.ToString();
            Dia = Tiempo.DayOfWeek.ToString();
            Ldia.Text = Dia;

        }

        private void Ldia_Click(object sender, EventArgs e)
        {

        }

        private void Lhora_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void ImprimirSalidatxt(string CarroSalir) {
            int PosicionCarro = 0;
            double TotalPagar = 0;
            string[] Arreglito = File.ReadAllLines(RutaDocu);

            for (int i = 0; i < CarrosParqueados.Length; i++)
            {

                if (CarrosParqueados[i] == CarroSalir)
                {
                    CarrosParqueados[i] = null;
                    PosicionCarro = i;

                }

            }

            CantidadDias[PosicionCarro, 1] = Tiempo.Day;
            if (CantidadDias[PosicionCarro, 0] - CantidadDias[PosicionCarro, 1] > 1){
            
            TotalPagar += 1440*((CantidadDias[PosicionCarro, 0] - CantidadDias[PosicionCarro, 1])-1);
            
            }



            ValoresCarrosP[PosicionCarro, 1] = 60 * Tiempo.Hour + Tiempo.Minute;
            if (ValoresCarrosP[PosicionCarro, 1] < ValoresCarrosP[PosicionCarro, 0])
            {
                TotalPagar = 24 * 60 - ValoresCarrosP[PosicionCarro, 0];
                TotalPagar += ValoresCarrosP[PosicionCarro, 1];
                TotalPagar = TotalPagar * 83.3333334;



            }
            else
            {
                TotalPagar = ValoresCarrosP[PosicionCarro, 1] - ValoresCarrosP[PosicionCarro, 0];
                TotalPagar *= 83.3333334;


            }
           

            //ValoresCarrosP[PosCarro, 1] += ;
            for (int i = 0; i < Arreglito.Length; i++) {

                if (Arreglito[i] == CarroSalir) {

                    Arreglito[i+1] = $"{Dia}  {Tiempo.Hour}:{Tiempo.Minute}";
                    Arreglito[i + 2] = $"Total Pagado: {TotalPagar}";
                


                }
            }

            
                File.WriteAllLines(RutaDocu, Arreglito);


        }

        public int VerificaionEspaciosYrepeticion() {
            int ban = 0;
            for (int i = 0; i < CarrosParqueados.Length; i++) {
                //Lpruebas2.Text = i.ToString();

                if (CarrosParqueados[i] == null)
                {
                    // ban++;
                   Lpruebas.Text = "bueno"+i;
                    PosCarro = i;
                    ban = 0;
                    break;
                   

                }
                else { ban++; }
            }
            for (int o = 0; o < CarrosParqueados.Length; o++)
            {
                if (CarrosParqueados[o] == textBox1.Text)
                {
                    ban++;
                    return ban;

                }
            }
            return ban;
        
        
        }

        private void AcelerarT_Click(object sender, EventArgs e)
        {
          Tiempo= Tiempo.AddDays(1);
          Dia = Tiempo.DayOfWeek.ToString(); 
          Ldia.Text = Dia;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;

            }
            else { timer1.Enabled = true; }
        }

        private void Lpruebas2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
