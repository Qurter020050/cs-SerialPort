using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_topmost_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void l2_rdo_sendlst_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void l2_rdo_sendfile_CheckedChanged(object sender, EventArgs e)
        {
            if ((l2_grp_sendfile.Visible == true))
            {
                DisableGroupbox(l2_grp_sendfile);
                EnableGroupbox(l2_grp_sendlist);
            }
            else
            {
                DisableGroupbox(l2_grp_sendlist);
                EnableGroupbox(l2_grp_sendfile);
            }

        }

        private void EnableGroupbox(GroupBox grpname)
        {

            grpname.Enabled = true;
            foreach (var ctr in grpname.Controls)
            {
                (ctr as Control).Enabled = true;
                (ctr as Control).Visible = true;

            }
            grpname.Visible = true;
        }
        private void DisableGroupbox(GroupBox grpname)
        {
            grpname.Visible = false;
            foreach (var ctr in grpname.Controls)
            {
                (ctr as Control).Visible = false;
                (ctr as Control).Enabled = false;   
            }
            grpname.Enabled = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (l2_grp_sendfile.Visible == true)
                DisableGroupbox(l2_grp_sendfile);
            else
                EnableGroupbox(l2_grp_sendfile);
        }

        private void txt_status_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk_loadlasttime_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
