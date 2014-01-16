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
        Random ran = new Random();
        AdapterIdentifier ai = new AdapterIdentifier(3);
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
            //WMI: getting Network adapters on computer
            ManagementObjectSearcher mos = 
                new ManagementObjectSearcher("select * from Win32_NetworkAdapter " +
                                            "Where AdapterType='Ethernet 802.3'");
            
            ai.InializeAdapterList();
            for (int i = 0; i < 3; i++)
                adapterCB.Items.Add(ai.GetAdapterName(i));

                foreach (var mo in mos.Get())
                {
                    adapterCB.Items.Add(mo["Name"].ToString());
                    //mo[]
                }


            adapterCB.SelectedIndex = 0;

            /* string launchPath = "E:\\Users\\Public\\Sony Online Entertainment\\" +
                                "Installed Games\\PlanetSide 2\\LaunchPad.exe";
            FileStream LaunchPadMod = new FileStream(launchPath, FileMode.Open, FileAccess.ReadWrite);

            LaunchPadMod.Seek(192377, SeekOrigin.Begin);
            LaunchPadMod.WriteByte(235); //Change to JMP 0xE8
            LaunchPadMod.Close();


            Process.Start(launchPath);
            Process.Start("C:\\Users\\George\\Documents\\Visual Studio 2013" +
                          "\\Projects\\Vimba\\Debug\\Configurable_Injector.exe");*/
        }

        //-----------------------------------------------------------------------------------------

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------

        private void adapterCB_SelectedIndexChanged(object sender, EventArgs e)
        {

            macAdd_txt.Text = ai.GetAdapterMac(adapterCB.SelectedIndex);
            /*ManagementObjectSearcher mos = new ManagementObjectSearcher(
                            "select * from Win32_NetworkAdapter where Name='"
                            + adapterCB.SelectedItem.ToString() + "'");

            ManagementObjectCollection moc = mos.Get();
            macAdd_txt.Text = "";




            if (randomMaxChkBox.Checked == false)
            {
                if (moc.Count > 0)
                {
                    foreach (ManagementObject mo in moc)
                    {

                        macAdd_txt.Text = (string) mo["MACAddress"];

                    }
                    
                }

            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    macAdd_txt.Text += RandomHexNumber();
                    macAdd_txt.Text += RandomHexNumber();
                    macAdd_txt.Text += ' ';
                }

                
            }

            macAdd_txt.Text = macAdd_txt.Text.Replace(':', ' '); */
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
    }
}
