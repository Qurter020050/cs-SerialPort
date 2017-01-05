using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;


namespace SerialPort
{
    
    public partial class Form1 : Form
    {
        private byte serialport_status;
        private long receivecount = 0;
        private long sendcount = 0;
        enum mode : byte {HEX, ASC};
        private mode sendmode =mode.HEX;
        private mode receivemode=mode.HEX;
        private string statetext;

        private void SetparaEnable()
        {
            l1_cmb_baudrate.Enabled = true;
            l1_cmb_checkbit.Enabled = true;
            l1_cmb_com.Enabled = true;
            l1_cmb_databit.Enabled = true;
            l1_cmb_stopbit.Enabled = true;
        }

        private void SetpataDisable()
        {
            l1_cmb_baudrate.Enabled = false;
            l1_cmb_checkbit.Enabled = false;
            l1_cmb_com.Enabled = false;
            l1_cmb_databit.Enabled = false;
            l1_cmb_stopbit.Enabled = false;
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

        private void TxtboxLoad(TextBox txt_box)
        {

        }

        private void TxtboxSave(TextBox txt_box)
        {
            if (txt_box.TextLength == 0)
            {
                DialogResult msgSave = MessageBox.Show("接收区未收到数据！\n是否保存？", "保存", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (msgSave == DialogResult.Cancel)
                    return;
            }
                SaveFileDialog digSave = new SaveFileDialog();
                digSave.Filter = "Text Files | *.txt";
                digSave.DefaultExt = "txt";
                if (digSave.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter txtsave = new System.IO.StreamWriter(digSave.FileName, true, Encoding.Default);

                    txtsave.Write(txt_box.Text);
                    txtsave.Close();

                }
        }

        private void GetSerialPorts()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();

            if (ports.Length != 0)
            {
                l1_cmb_com.Items.Clear();
                l1_cmb_com.Items.AddRange(ports);
                l1_cmb_com.SelectedIndex = 0;
                statetext = "检测到串口:" + ports;
            }
            else
            {
                l1_cmb_com.Items.Clear();
                l1_cmb_com.SelectedIndex = -1;
                statetext = "未检测到串口！";
            }
            DisplayUpdate();
        }
        
        private void InitEnviroments()
        {
            l1_cmb_checkbit.SelectedIndex = 0;
            l1_cmb_databit.SelectedIndex = 0;
            l1_cmb_stopbit.SelectedIndex = 0;
            l1_cmb_checkbit.SelectedIndex = 0;
            l1_cmb_baudrate.SelectedIndex = 6;
            l1_cmb_receivechannel.SelectedIndex = 0;

        }
        private void DisplayUpdate()
        {
            txt_reccount.Text = "(" + receivemode.ToString() + "):" + receivecount.ToString();
            txt_sendcount.Text = "(" + sendmode.ToString() + "):" + sendcount.ToString();
            txt_status.AppendText(statetext+Environment.NewLine);
            txt_status.ScrollToCaret();
            this.Text = "串口助手 Ver 0.1";
        }

        public Form1()
        {
            InitializeComponent();
            statetext = "串口助手 Ver 0.1";
            DisplayUpdate();
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

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (l2_rdo_sendlist.Checked == true)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            InitEnviroments();
            GetSerialPorts();

        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            GetSerialPorts();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void l1_combo_baud_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(l1_cmb_baudrate.SelectedItem);
        }

        private void check_topmost_CheckedChanged(object sender, EventArgs e)
        {
                this.TopMost = !this.TopMost;
        }

        private void l1_rdo_receivehex_CheckedChanged(object sender, EventArgs e)
        {
            if (l1_rdo_receivehex.Checked)
            {
                receivemode = mode.HEX;
                statetext = "按照HEX格式接收";
            }

            else
            {
                receivemode = mode.ASC;
                statetext = "按字符串格式接收，英文=单字节ASCII码，中文=双字节ANSI(GBK)码";
            }
            DisplayUpdate();
        }

        private void l1_chk_enableserial_CheckedChanged(object sender, EventArgs e)
        {
            if (this.l1_chk_enableserial.Checked == false)
            {
                this.l1_chk_enableserial.Text = "启动串行端口";
                serialPort1.Close();
                SetparaEnable();
            }
            else if ((this.l1_chk_enableserial.Checked == true) && (l1_cmb_com.SelectedIndex == -1))
            {
                MessageBox.Show("未检测到串口，检查连接！","错误",MessageBoxButtons.OK, MessageBoxIcon.Error);
                l1_chk_enableserial.Checked = false;
            }
            else
            {
                serialPort1.PortName = l1_cmb_com.SelectedItem.ToString();
                if (serialPort1.IsOpen)
                    MessageBox.Show("该COM口已被占用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    SetpataDisable();
                    this.l1_chk_enableserial.Text = "关闭串行端口";
                    serialPort1.BaudRate = Convert.ToInt32(l1_cmb_baudrate.SelectedItem.ToString());
                    serialPort1.DataBits = 8 - l1_cmb_databit.SelectedIndex;

                    switch (l1_cmb_stopbit.SelectedItem.ToString())
                    {
                        case "1.5bit":
                            serialPort1.StopBits = StopBits.OnePointFive;
                            break;
                        case "2 bit":
                            serialPort1.StopBits = StopBits.Two;
                            break;
                        case "1 bit":
                            serialPort1.StopBits = StopBits.One;
                            break;
                        default:
                            serialPort1.StopBits = StopBits.One;
                            break;
                    }

                    switch (l1_cmb_checkbit.SelectedItem.ToString())
                    {
                        case "None 无":
                            serialPort1.Parity = Parity.None;
                            break;
                        case "Odd 奇":
                            serialPort1.Parity = Parity.Odd;
                            break;
                        case "Even 偶":
                            serialPort1.Parity = Parity.Even;
                            break;
                        case "Mark 1":
                            serialPort1.Parity = Parity.Mark;
                            break;
                        case "Space 0":
                            serialPort1.Parity = Parity.Space;
                            break;
                        default:
                            serialPort1.Parity = Parity.None;
                            break;
                    }
                    serialPort1.Open();
                }
            }
        }

        private void btn_clrstate_Click(object sender, EventArgs e)
        {
            txt_status.Clear();
        }

        private void btn_clrcount_Click(object sender, EventArgs e)
        {
            receivecount = 0;
            sendcount = 0;
        }

        private void l1_btn_sendclear_Click(object sender, EventArgs e)
        {
            l1_txt_send.Clear();
        }

        private void l1_btn_send_Click(object sender, EventArgs e)
        {

        }

        private void l1_btn_receiveclear_Click(object sender, EventArgs e)
        {
            l1_txt_receive.Clear();
        }

        private void txt_reccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void l1_rdo_sendhex_CheckedChanged(object sender, EventArgs e)
        {
            if (l1_rdo_sendhex.Checked)
            {
                sendmode = mode.HEX;
                statetext = "按照HEX格式发送";
            }
            else
            {
                sendmode = mode.ASC;
                statetext = "按字符串格式发送（英文=单字节ASCII码，中文=双字节ANSI(GBK)码）";
            }

            DisplayUpdate();
        }

        private void l1_rdo_receivechar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void l1_btn_receivesave_Click(object sender, EventArgs e)
        {
            TxtboxSave(l1_txt_receive);
        }

        private void l1_btn_sendsave_Click(object sender, EventArgs e)
        {
            TxtboxSave(l1_txt_send);
        }
    }
}
