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
    public partial class MathQuiz : Form
    {
        
        public MathQuiz()
        {
            InitializeComponent();
            todaysDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }

        // Object to generate random numbers
        Random randomizer = new Random();

        // Integer variables for the addition problem
        int addend1;
        int addend2;

        // Integer variables for the subtraction problem
        int minuend;
        int subtrahend;

        // Integer variables for the multiplication problem
        int multiplicand;
        int multiplier;

        // Integer variables for the division problem
        int dividend;
        int divisor;

        // Integer to keep track of quiz time
        int timeLeft;

        public void StartTheQuiz()
        {
            // Generate two random numbers for addition problem.
            // Store values in addend1 and addend2
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert random numbers into strings for display in label controls
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // Make sure sum is clear before performing addition
            sum.Value = 0;

            // Fill in the subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer
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
            // If all answers are true stop timer and show message
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right! Good Job!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Display new time left by updating time left label
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If user runs out of time, stop timer, show message and fill in answers
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }

            // Turn timer red when at five seconds or less
            if (timeLeft <= 5)
            {
                timeLabel.BackColor = Color.Red;
            }
        }

        // Check answers
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            }
            else
                return false;
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in NumericUpDown control 
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
