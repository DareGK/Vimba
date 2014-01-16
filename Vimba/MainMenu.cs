using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkInterface;

namespace Vimba
{
    public partial class MainMenu : Form
    {
        private Random ran = new Random();
        private AdapterInformation ai = new AdapterInformation();
        private string[] adNames;
        private string[] adMacs;

        public MainMenu()
        {
            ran.Next(15);
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------

        private void MainMenu_Load(object sender, EventArgs e)
        {

            FirstTime ft = new FirstTime();
            ft.ShowDialog();
            string path = ft.PathInfo;

            ai.GetAdapters();
            adNames = ai.GetAdaptersNames();
            adMacs = ai.GetAdaptersMacs();
            adapterCB.Items.AddRange(adNames);
            adapterCB.SelectedIndex = 0;
        }

        //-----------------------------------------------------------------------------------------

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------

        private void adapterCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            macAdd_txt.Text = string.Empty;

            if (randomMaxChkBox.Checked == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    macAdd_txt.Text += RandomHexNumber();
                    macAdd_txt.Text += RandomHexNumber();
                    macAdd_txt.Text += '-';
                }
                macAdd_txt.Text = macAdd_txt.Text.Remove(macAdd_txt.Text.Length-1);
            }
            else
            {
                macAdd_txt.Text = adMacs[adapterCB.SelectedIndex]; 
            }      
        }

        //-----------------------------------------------------------------------------------------

        /// <summary>
        /// Generates a random hex value number
        /// </summary>
        /// <returns></returns>
        private char RandomHexNumber()
        {
            string HexAplha = "ABCDEF0123456789";
            return HexAplha[ran.Next(15)];
        }

        //-----------------------------------------------------------------------------------------

        private void randomMaxChkBox_CheckedChanged(object sender, EventArgs e)
        {
            adapterCB_SelectedIndexChanged(sender, e);
        }

        private void pathSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FirstTime FT = new FirstTime();
            FT.ShowDialog();
        }
    }
}
