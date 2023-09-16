using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Management.Automation;
using System.IO;

namespace CreateNewVM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string switch1;
        public void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string minram = numericUpDown3.Value.ToString();
            string maxram = numericUpDown1.Value.ToString();
            string core = numericUpDown2.Value.ToString();
            string gen = "2";

            if (radioButton1.Checked == true)
            {
                switch1 = radioButton1.Text;
            }
            else
            {
                switch1 = radioButton2.Text;

            }
            string disk1 = "robocopy \"d:\\VM\\AD02\\Virtual Hard Disks\" \"d:\\VM\\" + name + "\\Virtual Hard Disks\"";
            //string disk2 = "robocopy 'd:\\VM\\AD02\\Virtual Hard Disks\\Template-felles.vhdx' 'd:\\VM\\" + name + "\\Virtual Hard Disks\\'";
            //string disk3 = "robocopy 'd:\\VM\\AD02\\Virtual Hard Disks\\Template-programs.vhdx' 'd:\\VM\\" + name + "\\Virtual Hard Disks\\'";
            MessageBox.Show(disk1);
            string rename1 = "rename-item 'd:\\VM\\" + name + "\\Virtual Hard Disks\\Template.vhdx' " + name + ".vhdx";
            string rename2 = "rename-item 'd:\\VM\\" + name + "\\Virtual Hard Disks\\Template-Felles.vhdx' " + name + "-Felles.vhdx";
            string rename3 = "rename-item 'd:\\VM\\" + name + "\\Virtual Hard Disks\\Template-Programs.vhdx' " + name + "-Programs.vhdx";

            string addvm = "New-VM –Name " + name + " –MemoryStartupBytes " + minram + "MB -Generation " + gen + " -SwitchName \"" + switch1 + "\" -VHDPath \"d:\\VM\\" + name + "\\Virtual Hard Disks\\" + name + ".vhdx\"";

            string mem = "Set-VMMemory " + name + " -DynamicMemoryEnabled $true -MinimumBytes " + minram + "MB -MaximumBytes " + maxram + "MB ";

            string adddisk2 = "Add-VMHardDiskDrive -VMName " + name + " -Path \"d:\\VM\\" + name + "\\Virtual Hard Disks\\" + name + "-felles.vhdx\"";
            string adddisk3 = "Add-VMHardDiskDrive -VMName " + name + " -Path \"d:\\VM\\" + name + "\\Virtual Hard Disks\\" + name + "-programs.vhdx\"";

            string cores = "SET-VMProcessor -VMname " + name + " -count " + core;

            string shutd = "Set-VM " + name + " -AutomaticStopAction ShutDown";
            string start = "Start-VM -name " + name;
            MessageBox.Show(addvm);
            try
            {
                progressBar1.Visible = true;
                using (PowerShell powershelli1 = PowerShell.Create())
                {
                    powershelli1.AddScript(disk1);
                    powershelli1.Invoke();
                }
                progressBar1.Increment(1);
            }
            catch { MessageBox.Show("Error Copying the disks", "Error", MessageBoxButtons.OK); }
            try
            {
                using (PowerShell powershelli2 = PowerShell.Create())
                {
                    powershelli2.AddScript(rename1);
                    powershelli2.Invoke();
                }
                progressBar1.Increment(1);
            }
            catch { MessageBox.Show("Error renaming default disk", "Error", MessageBoxButtons.OK); }
            try
            {
                using (PowerShell powershelli1 = PowerShell.Create())
                {
                    powershelli1.AddScript(addvm);
                    powershelli1.Invoke();
                }
                progressBar1.Increment(1);
            }
            catch { MessageBox.Show("Error adding VM", "Error", MessageBoxButtons.OK); }
            try
            {
                using (PowerShell powershelli1 = PowerShell.Create())
                {
                    powershelli1.AddScript(mem);
                    powershelli1.Invoke();
                }
                progressBar1.Increment(1);
            }
            catch { MessageBox.Show("Error adding dynamic memory", "Error", MessageBoxButtons.OK); }
            try
            {
                if (checkBox1.Checked == true)
                {
                    try
                    {
                        using (PowerShell powershelli1 = PowerShell.Create())
                        {
                            powershelli1.AddScript(rename2);
                            powershelli1.Invoke();
                        }
                    }
                    catch { MessageBox.Show("Error renaming disk felles", "Error", MessageBoxButtons.OK); }
                    try
                    {
                        using (PowerShell powershelli1 = PowerShell.Create())
                        {
                            powershelli1.AddScript(adddisk2);
                            powershelli1.Invoke();
                        }
                    }
                    catch { MessageBox.Show("Error adding disk felles", "Error", MessageBoxButtons.OK); }
                    progressBar1.Increment(3);
                }
                else
                {
                    File.Delete("d:\\VM\\" + name + "\\Virtual Hard Disks\\Template-felles.vhdx");
                }
            }
            catch { MessageBox.Show("Error error on if state for adding disk felles", "Error", MessageBoxButtons.OK); }
            try
            {
                if (checkBox2.Checked == true)
                {
                    try
                    {
                        using (PowerShell powershelli1 = PowerShell.Create())
                        {
                            powershelli1.AddScript(rename3);
                            powershelli1.Invoke();
                        }
                    }
                    catch { MessageBox.Show("Error renaming disk programs", "Error", MessageBoxButtons.OK); }
                    try
                    {
                        using (PowerShell powershelli1 = PowerShell.Create())
                        {
                            powershelli1.AddScript(adddisk3);
                            powershelli1.Invoke();
                        }

                    }
                    catch { MessageBox.Show("Error adding disk programs", "Error", MessageBoxButtons.OK); }
                    progressBar1.Increment(3);
                }
                else
                {
                    File.Delete("d:\\VM\\" + name + "\\Virtual Hard Disks\\Template-programs.vhdx");
                }
            }
            catch { MessageBox.Show("Error running if statment for programs", "Error", MessageBoxButtons.OK); }
            try
            {
                using (PowerShell powershelli1 = PowerShell.Create())
                {
                    powershelli1.AddScript(cores);
                    powershelli1.Invoke();
                }
                progressBar1.Increment(1);
            }
            catch { MessageBox.Show("Error setting cores", "Error", MessageBoxButtons.OK); }
            try
            {
                using (PowerShell powershelli1 = PowerShell.Create())
                {
                    powershelli1.AddScript(shutd);
                    powershelli1.Invoke();
                }
                progressBar1.Increment(1);

            }
            catch { MessageBox.Show("Error setting autoshutdown", "Error", MessageBoxButtons.OK); }
            try
            {
                using (PowerShell powershelli1 = PowerShell.Create())
                {
                    powershelli1.AddScript(start);
                    powershelli1.Invoke();
                }
                progressBar1.Increment(1);
                button2.Visible = true;
                button3.Visible = true;
                button1.Visible = false;
                progressBar1.Visible = false;
                pictureBox1.BringToFront();
                pictureBox1.BackColor = Color.Transparent;
                pictureBox1.Visible = true;
            }
            catch { MessageBox.Show("Error running command = ", "Error", MessageBoxButtons.OK); }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            pictureBox1.Visible = false;
            progressBar1.Value = 0;
            textBox1.Text = "asp";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
