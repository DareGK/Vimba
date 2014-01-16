using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vimba
{
    public partial class FirstTime : Form
    {
        public string PathInfo {get; set;}

        public FirstTime()
        {
            InitializeComponent();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openFileBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pathDialog = new FolderBrowserDialog();

            if(pathDialog.ShowDialog() == DialogResult.OK)
            {
                filePathTxt.Text = pathDialog.SelectedPath;
                PathInfo = filePathTxt.Text;
            }
            
        }

        private void FirstTime_Load(object sender, EventArgs e)
        {

        }
    }
}
