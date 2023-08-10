using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Schema;

namespace Calculator
{
    public partial class Calculator : Form
    {
        string[] operators = {"+", "-", "*", "/"};

        public Calculator()
        {
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        private string[] combineStrings(string[] arrMain, string[] tempRes, int i)
        {
            int len = arrMain.Length;
            string[] arrLeft = new string[len - 2];
            int lenRight = len - i - 2;

            if(i == 1)// (1 * 1) + 1 + 1
            {
                Array.Copy(tempRes, 0, arrLeft, 0, tempRes.Length); //copy result to empty arrLeft
                Array.Copy(arrMain, i + 2, arrLeft, 1, lenRight); // copy what's left from arrMain to arrLeft
            }
            else if(i == len - 2)// 1 + 1 + (1 * 1)
            {
                Array.Copy(arrMain, 0, arrLeft, 0, i - 1); //copy arrMain minus result to arrLeft
                Array.Copy(tempRes, 0, arrLeft, arrLeft.Length - 1, 1); //copy result to arrLeft
            }
            else // 1 + (1 * 1) + 1
            {
                Array.Copy(arrMain, 0, arrLeft, 0, i - 1); //copy arrMain minus result and right half of arrMain to arrLeft
                Array.Copy(tempRes, 0, arrLeft, i - 1, 1); //copy result to arrLeft
                Array.Copy(arrMain, i + 2, arrLeft, i, lenRight);//copy right half of arrMain to arrLeft
            }

            return arrLeft;
        }

        private string checkForDouble(string txt, string sym)
        {
            if (txt.Length == 0 && !Char.IsDigit(sym, 0)) return txt; // avoids starting with +, -, *, /, .

            if (txt.Length == 0) //first input is always a number
            {
                return txt + sym;
            }
            else if(!Char.IsDigit(txt, txt.Length - 1) && !Char.IsDigit(sym, 0)) // if txt is + and sym is + DO NOT add sym
            {
                return txt;
            }
            else if(txt.Length == 1) 
            {
                if (txt[0].Equals('0') && Char.IsDigit(sym, 0)) return txt; // if txt is 0 and sym is 0 to 9 DO NOT add sym
                else return txt + sym;
            }
            else if(txt.Length >= 2) //if txt length is 2 or higher
            {
                if (txt.LastIndexOf(".") != -1) // find out if there's a . in txt
                {
                    for(int i = txt.LastIndexOf(".") + 1; i < txt.Length - 1; i++) // go through txt starting from 1 position past .
                    {
                        if (!Char.IsDigit(txt[i])) return txt + sym; // if find +, -, * or /  copy .
                        else if (i == txt.Length - 1) return txt;// found end of txt where last is number so no adding .
                    }
                }

                for(int i = txt.Length - 2; i >= 0; i--) //go through txt string, starting from the second last element
                {
                    if(!Char.IsDigit(txt, i) && txt[i + 1].Equals('0')) // if txt[i - 2] is + and txt[i + 1] is 0
                    {
                        return Char.IsDigit(sym, 0) ? txt + sym : txt; // if sym is 0 to 9 DO NOT add sym else add sym
                    }
                    else return txt + sym;
                }

               return txt + sym;
            }

           return txt + sym;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn9.Text);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btn0.Text);
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btnDot.Text);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btnPlus.Text);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btnMinus.Text);
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btnMultiply.Text);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            textBox.Text = checkForDouble(textBox.Text, btnDivide.Text);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(textBox.Text.Length != 0) textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox.Clear();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string pattern = $"({string.Join("|", operators.Select(Regex.Escape))})";
            string[] result = Regex.Split(textBox.Text, pattern);
            string[] tempRes = new string[1];
            string strOperator = "";
            
            while (result.Length != 1)
            {
                if (Array.IndexOf(result, btnMultiply.Text) != -1)
                {
                    strOperator = btnMultiply.Text;
                    tempRes[0] = (float.Parse(result[Array.IndexOf(result, strOperator) - 1])
                        * float.Parse(result[Array.IndexOf(result, strOperator) + 1])).ToString();
                }
                else if (Array.IndexOf(result, btnDivide.Text) != -1)
                {
                    strOperator = btnDivide.Text;
                    tempRes[0] = (float.Parse(result[Array.IndexOf(result, strOperator) - 1]) 
                        / float.Parse(result[Array.IndexOf(result, strOperator) + 1])).ToString();
                }
                else if (Array.IndexOf(result, btnPlus.Text) != -1)
                {
                    strOperator = btnPlus.Text;
                    tempRes[0] = (float.Parse(result[(Array.IndexOf(result, strOperator)) - 1]) 
                        + float.Parse(result[Array.IndexOf(result, strOperator) + 1])).ToString();
                }
                else if (Array.IndexOf(result, btnMinus.Text) != -1)
                {
                    strOperator = btnMinus.Text;
                    tempRes[0] = (float.Parse(result[(Array.IndexOf(result, strOperator)) - 1]) 
                        - float.Parse(result[Array.IndexOf(result, strOperator) + 1])).ToString();
                }

                if(result.Length != 1)
                {
                    result = combineStrings(result, tempRes, (Array.IndexOf(result, strOperator)));
                }  
            }
            textBox.Text = result[0];
        }
    }
}
