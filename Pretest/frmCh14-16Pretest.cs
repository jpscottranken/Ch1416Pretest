using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EmployeeLibrary;

namespace Pretest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //  Global Constants
        const int MINEMPNUMBER  = 1000;
        const int MAXEMPNUMBER  = 9999;

        const decimal MINHOURS  =  0.00m;
        const decimal MAXHOURS  = 84.00M;
        const decimal MINHRATE  =  0.00m;
        const decimal MAXHRATE  = 99.99m;
        const decimal MAXNONOT  = 40.00m;
        const decimal OTRATE    = 1.5m;

        const int MINPIECES     =   0;
        const int MAXPIECES     = 100;
        const decimal MINPPP    = 0.00m;
        const decimal MAXPPP    = 1.00m;

        //  Global variable
        decimal grossPay = 0.00m;

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            bool success;

            if ((!radHourlyEmployee.Checked) &&
                (!radPieceworkerEmployee.Checked))
            {
                showMessage("You Must Choose An Employee Type!!!",
                            "NO EMPLOYEE TYPE CHOSEN");
                return;
            }

            success = validateCommonFields();
            {
                if (!success)
                {
                    return;
                }

                if (radHourlyEmployee.Checked)
                {
                    success = validateHourlyEmployee();

                    if (success)
                    {
                        int empNum    = Convert.ToInt32(txtEmployeeNumber.Text);
                        decimal hours = Convert.ToDecimal(textBox5.Text);
                        decimal rate  = Convert.ToDecimal(textBox6.Text);

                        HourlyEmployee h = new HourlyEmployee(
                                    txtFirstName.Text,
                                    txtLastName.Text,
                                    empNum, hours, rate);

                        if (hours <= MAXNONOT)
                        {
                            grossPay = hours * rate;
                        }
                        else
                        {
                            grossPay = (MAXNONOT * rate)
                                       + ((hours - MAXNONOT) * rate * OTRATE);
                        }

                        txtResults.Text = "HOURLY EMPLOYEE STATS" +
                                            "\r\n" + h.displayText() +
                                            "\r\n" + "Union Status: " +
                                            chkUnionMember.Checked +
                                            "\r\n" + "Gross Pay: " +
                                            grossPay.ToString("c");
                    }
                }
                else if (radPieceworkerEmployee.Checked)
                {
                    success = validatePieceworkerEmployee();

                    if (success)
                    {
                        int empNum   = Convert.ToInt32(txtEmployeeNumber.Text);
                        int pieces   = Convert.ToInt32(textBox5.Text);
                        decimal ppp  = Convert.ToDecimal(textBox6.Text);

                        PieceworkerEmployee p = new PieceworkerEmployee(
                                    txtFirstName.Text,
                                    txtLastName.Text,
                                    empNum, pieces, ppp);

                        grossPay = pieces * ppp * MAXNONOT;

                        txtResults.Text = "PIECEWORKER EMPLOYEE STATS" +
                                          "\r\n" + p.displayText() +
                                          "\r\n" + "Union Status: " +
                                           chkUnionMember.Checked +
                                          "\r\n" + "Gross Pay: " + 
                                          grossPay.ToString("c");
                    }
                }
            }
        }

        private bool validateCommonFields()
        {
            bool success = true;
            string errorMessage = "";

            errorMessage += Validator.IsPresent(txtFirstName.Text, txtFirstName.Tag.ToString());
            errorMessage += Validator.IsPresent(txtLastName.Text,  txtLastName.Tag.ToString());
            errorMessage += Validator.IsInt32(txtEmployeeNumber.Text, txtEmployeeNumber.Tag.ToString());
            errorMessage += Validator.IsWithinRange(txtEmployeeNumber.Text,
                                                    txtEmployeeNumber.Tag.ToString(),
                                                    MINEMPNUMBER, MAXEMPNUMBER);

            if (errorMessage != "")
            {
                success = false;
                showMessage(errorMessage,
                            "THE FOLLOWING ERRORS HAVE BEEN DISCOVERED");
            }

            return success;
        }

        private bool validateHourlyEmployee()
        {
            bool success = true;
            string errorMessage = "";

            textBox5.Tag = "hourswored";
            textBox6.Tag = "hourlyrate";
            errorMessage += Validator.IsDecimal(textBox5.Text, textBox5.Tag.ToString());
            errorMessage += Validator.IsWithinRange(textBox5.Text, textBox5.Tag.ToString(), 
                                                    MINHOURS, MAXHOURS);
            errorMessage += Validator.IsDecimal(textBox6.Text, textBox6.Tag.ToString());
            errorMessage += Validator.IsWithinRange(textBox6.Text, textBox6.Tag.ToString(),
                                                    MINHRATE, MAXHRATE);

            if (errorMessage != "")
            {
                success = false;
                showMessage(errorMessage,
                            "THE FOLLOWING ERRORS HAVE BEEN DISCOVERED");
            }

            return success;
        }

        private bool validatePieceworkerEmployee()
        {
            bool success = true;
            string errorMessage = "";

            textBox5.Tag = "pieces";
            textBox6.Tag = "priceperpiece";
            errorMessage += Validator.IsWithinRange(textBox5.Text, textBox5.Tag.ToString(),
                                                    MINPIECES, MAXPIECES);
            errorMessage += Validator.IsWithinRange(textBox6.Text, textBox6.Tag.ToString(),
                                                    MINPPP, MAXPPP);

            if (errorMessage != "")
            {
                success = false;
                showMessage(errorMessage,
                            "THE FOLLOWING ERRORS HAVE BEEN DISCOVERED");
            }

            return success;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAndSetFocus();
        }

        private void clearAndSetFocus()
        {
            clearCommonInfo();
            makeTypeInfoInvisible();
            txtFirstName.Focus();
        }

        private void radHourlyEmployee_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Tag = "hourswored";
            textBox6.Tag = "hourlyrate";
            label5.Text = "Hours Worked:";
            label6.Text = "Hourly Rate:";
            makeTypeInfoVisible();
        }

        private void radPieceworkerEmployee_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Tag = "pieces";
            textBox6.Tag = "priceperpiece";
            label5.Text = "# Pieces Made:";
            label6.Text = "Price/Piece:";
            makeTypeInfoVisible();
        }

        private void clearCommonInfo()
        {
            txtFirstName.Text               = "";
            txtLastName.Text                = "";
            chkUnionMember.Checked          = false;
            txtEmployeeNumber.Text          = "";
            txtResults.Text                 = "";
            radHourlyEmployee.Checked       = false;
            radPieceworkerEmployee.Checked  = false;
        }

        private void makeTypeInfoVisible()
        {
            label5.Visible   = true;
            label6.Visible   = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
        }

        private void makeTypeInfoInvisible()
        {
            textBox5.Text    = "";
            textBox6.Text    = "";
            textBox5.Tag     = "";
            textBox6.Tag     = "";
            label5.Text      = "";
            label6.Text      = "";
            label5.Visible   = false;
            label6.Visible   = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            exitProgramOrNot();
        }

        private void exitProgramOrNot()
        {
            DialogResult dialog = MessageBox.Show(
                "Do You Really Want To Exit The Program?",
                "EXIT NOW?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            makeTypeInfoInvisible();
        }

        private void showMessage(string msg, string title)
        {
            MessageBox.Show(msg, title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
    }
}
