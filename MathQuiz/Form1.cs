using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //create a random object called randomizer
        //to generate random numbers
        Random rnd = new Random();

        //these integer variables store the numbers
        //for the addition problem
        int addend1;
        int addend2;

        //these integer variables store the numbers
        //for the subtraction problem
        int minuend;
        int subtrahend;

        //these integer variables store the numbers
        //for the addition problem
        int multiplicand;
        int multiplier;

        //these integer variables store the numbers
        //for the addition problem
        int dividend;
        int divisor;

        //this integer variable keeps track of the 
        //remaining time
        int timeLeft;



        ///<summary>
        /// Start the quiz by filling in all of the problems
        /// and starting the timer
        /// </summary>
        /// 
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 ==sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartTheQuiz()
        {
            //fill in the addition problem
            //generate two random numbers to add.
            //store the values in the variables 'addend1' and 'addend2'
            addend1 = rnd.Next(51);
            addend2 = rnd.Next(51);

            //convert the two randomly generated numbers
            //into strings so that they can be displayed
            //in the label controls
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            //'sum' is the name of the numericupdown control
            //this step makes sure its value is zero before
            //adding any values to it.
            sum.Value = 0;

            //Fill in the subtraction problem
            minuend = rnd.Next(1, 101);
            subtrahend = rnd.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            //Fill in the multiplication problem
            multiplicand = rnd.Next(2, 11);
            multiplier = rnd.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            //Fill in the division problem
            divisor = rnd.Next(2, 11);
            int temporaryQuotient = rnd.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
                timeLabel.BackColor = Color.White;
            }
            else if(timeLeft > 0)
            {
                // If CheckTheAnswer() returns false, keep counting
                // down. Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
                if (timeLeft >= 20)
                {
                    timeLabel.BackColor = Color.Green;
                }
                else if (timeLeft < 20 && timeLeft >= 10)
                {
                    timeLabel.BackColor = Color.Yellow;
                }
                else if (timeLeft < 10)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = Color.White;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            //select the whole answer in the numericupdown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if(answerBox != null )
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
