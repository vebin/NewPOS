﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPOS
{
    public partial class Adminsettings : Form
    {
        string [] stringArr;
        string adminpin = string.Empty;
        DataTable mycompany = new DataTable();
        summaryreportTableAdapters.tblCompanyDetailsTableAdapter summarytableadapter = new summaryreportTableAdapters.tblCompanyDetailsTableAdapter();
        
        public Adminsettings()
        {
            InitializeComponent();
        }

        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm rf1 = new RegisterForm();

            rf1.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int id = 0;
            adminpin = adminpass.Text;
            try
            {
                id = Convert.ToInt32(stringArr[0]);
            }
            catch (Exception io) { MessageBox.Show("No Record Exits!"); return; }
            int cashlesslmt  = Convert.ToInt32(cashlesspass.Text);
            summarytableadapter.UpdateQuery(adminpin,cashlesslmt,id);
            MessageBox.Show("Company Details Inserted Successfully");
            this.Close();
        }

        private void Adminsettings_Load(object sender, EventArgs e)
        {
            mycompany = summarytableadapter.GetData();
            try
            {
                stringArr = mycompany.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                cashlesspass.Text = stringArr[10];
                adminpass.Text = stringArr[9];
            }
            catch (Exception ioe) { }

            if (Properties.Settings.Default.Printer == "Yes")
            {
                print.Checked = true;
            }
            if (Properties.Settings.Default.Printer == "No")
            {
                print.Checked = false;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            companydetails pf1 = new companydetails();
            pf1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult mydialog = MessageBox.Show(this, "Do you want to reset the program to default settings?", "Program Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mydialog == DialogResult.Yes)
            {
                string curFile = Environment.CurrentDirectory + "\\FirstRun.ini";
                File.Delete(curFile);
                MessageBox.Show("POS Reset Complete");
            }
        }

        private void print_CheckedChanged(object sender, EventArgs e)
        {
            if (print.Checked == true)
            {
                Properties.Settings.Default.Printer = "Yes";
            }
            if (print.Checked == false)
            {
                Properties.Settings.Default.Printer = "No";
            }
        }

        private void acr1222llcdtxt_Click(object sender, EventArgs e)
        {
            LCDText lcd = new LCDText();
            lcd.ShowDialog();
        }
    }
}
