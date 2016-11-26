using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Kalkulator
{
    public partial class Form1 : Form
    {
        Boolean bOn = false; //on/ off
        String sTmp = ""; //tymczasowa, jakby ktoś był zdziwiony
        double iVal1, iVal2;
        char cSign = ' '; //znak
        double dResult = 0.0; //

        Warning war = new Warning();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void buttonOne_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("1");
        }

        private void buttonTwo_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("2");
        }

        private void buttonThree_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("3");
        }

        private void buttonFour_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("4");
        }

        private void buttonFive_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("5");
        }

        private void buttonSix_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("6");
        }

        private void buttonSeven_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("7");
        }

        private void buttonEight_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("8");
        }

        private void buttonNine_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("9");
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            playButtonSound();
            actualizeResult("0");
        }

        private void buttonComa_Click(object sender, EventArgs e)
        { //trzeba ograniczenie, zeby ktos nie nawalił miliarda przecinków
            playButtonSound();
            if (sTmp != "")
            {
                if (sTmp.Contains(","))
                {
                    createNewWindow();
                }
                else
                {
                    actualizeResult(",");
                }
            }
        }

        private void buttonMul_Click(object sender, EventArgs e)
        { //objaśnię tylko dla jednego
            playButtonSound();
            if (!sTmp.Equals("")) //klikając cyferki aktualizuję textBox i temp dodając do obu cyferkę
            { //idiotoodpornosc, jak ktos chce napisac 56 + + to NIE POZWALAM. *byl pomysł wstawienia Gandalfa*               
                sTmp = sCheckComa(sTmp);
                if (cSign == ' ') //jak jeszcze nie bylo wcześniej jakiegoś działania, no to robiem.
                {
                    cSign = '*';
                    iVal1 = double.Parse(sTmp);
                    sTmp = "";
                    textBoxResult.Text += " * ";
                }
                else
                /*
                 * w innym przypadku oznacza, ze ktos chce wykonywac operacj jedna po drugiej, wiec
                 * wyliczam to, co mam teraz przez wywołanie "równa się", wyzerowanie sign
                 * i potem wracam do działania
                 */
                {
                    buttonEq_Click(sender, e);
                    cSign = ' ';
                    buttonMul_Click(sender, e);
                }
            }
        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            playButtonSound();
            if (!sTmp.Equals(""))
            {
                sTmp = sCheckComa(sTmp);
                if (cSign == ' ')
                {
                    cSign = '/';
                    iVal1 = double.Parse(sTmp);
                    sTmp = "";
                    textBoxResult.Text += " / ";
                }
                else
                {
                    buttonEq_Click(sender, e);
                    cSign = ' ';
                    buttonDiv_Click(sender, e);
                }
            }
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            playButtonSound();
            if (!sTmp.Equals(""))
            {
                sTmp = sCheckComa(sTmp);
                if (cSign == ' ')
                {
                    cSign = '-';
                    iVal1 = double.Parse(sTmp);
                    sTmp = "";
                    textBoxResult.Text += " - ";
                }
                else
                {
                    buttonEq_Click(sender, e);
                    cSign = ' ';
                    buttonSub_Click(sender, e);
                }
            }
        }

        private void buttonSum_Click(object sender, EventArgs e)
        {
            playButtonSound();
            if (!sTmp.Equals(""))
            {
                sTmp = sCheckComa(sTmp);
                if (cSign == ' ')
                {
                    cSign = '+';
                    iVal1 = double.Parse(sTmp);
                    sTmp = "";
                    textBoxResult.Text += " + ";
                }
                else
                {
                    buttonEq_Click(sender, e);
                    cSign = ' ';
                    buttonSum_Click(sender, e);
                }
            }
        }

        private void buttonEq_Click(object sender, EventArgs e)
        {
            playButtonSound();
            if (sTmp != "")
            {
                iVal2 = double.Parse(sTmp);
                if (cSign == '+')
                {
                    if (iVal1 + iVal2 < double.MaxValue)
                        textBoxResult.Text = (iVal1 + iVal2).ToString();
                    else
                    {
                        MessageBox.Show("Za duża liczba!");
                        textBoxResult.Text = double.MaxValue.ToString();
                    }
                }
                if (cSign == '-')
                {
                    if (iVal1 - iVal2 < double.MaxValue)
                        textBoxResult.Text = (iVal1 - iVal2).ToString();
                    else
                    {
                        MessageBox.Show("Za duża liczba!");
                        textBoxResult.Text = double.MaxValue.ToString();
                    }
                }
                if (cSign == '*')
                {
                    if (iVal1 * iVal2 < double.MaxValue)
                        textBoxResult.Text = (iVal1 * iVal2).ToString();
                    else
                    {
                        MessageBox.Show("Za duża liczba!");
                        textBoxResult.Text = double.MaxValue.ToString();
                    }
                }
                if (cSign == '/')
                {
                    if (iVal2 != 0)
                    {
                        if (iVal1 / iVal2 < double.MaxValue)
                            textBoxResult.Text = (iVal1 / iVal2).ToString();
                        else
                        {
                            MessageBox.Show("Za duża liczba!");
                            textBoxResult.Text = double.MaxValue.ToString();
                        }
                    }
                    else
                    {
                        createNewWindow();
                        clear();
                    }
                }
                cSign = ' ';
                sTmp = textBoxResult.Text; // pierwsza liczba to już wynik
                if (sTmp != "") iVal1 = double.Parse(sTmp);
                else iVal1 = 0;
                dResult = 0;
            }
        }

        private void buttonC_Click(object sender, EventArgs e) //czyści wsio
        {
            playButtonSound();
            clear();
        }

        private void buttonCE_Click(object sender, EventArgs e) //usun jeden znak
        {
            playButtonSound();
            if (textBoxResult.Text.Length > textBoxResult.Text.Length - sTmp.Length) //jak coś się wyświetla, to wywal ostatnie
            {
                textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);
            }
            if(sTmp.Length > 0) //i tak samo trzeba wyrzucić z tymczasowej
            {
                sTmp = sTmp.Substring(0, sTmp.Length - 1);
            }
            else 
            {
                sTmp = "";
            }
        }

        private void buttonOnOff_Click(object sender, EventArgs e)
        {
            if (bOn == true) //jeżeli jest włączony 
            {
                bOn = false;
                buttonOnOff.FlatAppearance.BorderColor = Color.MediumVioletRed;
            }
            else
            {
                bOn = true;
                buttonOnOff.FlatAppearance.BorderColor = Color.ForestGreen;
            }
            clear(); //bo w sumie czemu nie
        }
        

        private void actualizeResult(String str)
        {
            if (bOn == true) //aktualizuje wyświetlanie i zmienną tymczasowa
            {
                if (!sTmp.Contains("E"))
                {
                    sTmp += str;
                    textBoxResult.Text += str;
                }
            }
        }

        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void clear() //chyba nie trzeba tłumaczyć
        {
            sTmp = "";
            textBoxResult.Text = "";
            iVal1 = iVal2 = 0;
            cSign = ' ';
        }

        private void playButtonSound() //GRAJ BOOP!
        {
            if (bOn)
            {
                SoundPlayer buttonSound = new SoundPlayer(Properties.Resources.boop);
                buttonSound.Play();
            }
            else
            {
                MessageBox.Show("Musisz najpierw włączyc!");
            }
        }

        private void playDramaticSound()
        {
            if (bOn)
            {
                SoundPlayer buttonSound = new SoundPlayer(Properties.Resources.DUMDUMDUUUUUM);
                buttonSound.Play();
            }
            else
            {
                MessageBox.Show("Musisz najpierw włączyc!");
            }
        }
         
        private void createNewWindow()
        {
            playDramaticSound();
            war.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private String sCheckComa(String temp)
        {
            StringBuilder sb = new StringBuilder(temp);
            if (sb[temp.Length-1] == ',')
            {
                temp += "0";
                textBoxResult.Text += "0";
            }
            return temp;
        }
    }
}
