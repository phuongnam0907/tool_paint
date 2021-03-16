using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class FormSetting : Form
    {
        private int permissionUser = 0;
        private bool isTest_tabPageTimeRunAuto = false;
        Keypad numKey = new Keypad();

        public FormSetting()
        {
            InitializeComponent();

            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            controlPermission();
        }

        private void buttonLoginFromSetting_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            formLogin.LoginEvent += setPermission;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            permissionUser = 0;
            numKey.Close();
            this.Close();
        }

        private void setPermission(int level)
        {
            permissionUser = level;
            string lv;
            switch (level)
            {
                case 1:
                    lv = "1";
                    break;
                case 2:
                    lv = "2";
                    break;
                case 3:
                    lv = "3";
                    break;
                case 4:
                    lv = "4";
                    break;
                case 0:
                default:
                    lv = "0";
                    break;
            }
            labelStatusUser.Text = "Người Dùng\n Mức " + lv;

            controlPermission();
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {

        }

        private void buttonStopMachine_Click(object sender, EventArgs e)
        {
            if (isTest_tabPageTimeRunAuto == true)
            {
                buttonStopMachine.Text = "BẬT";
                buttonStopMachine.BackColor = Color.Blue;
            }
            else
            {
                buttonStopMachine.Text = "TẮT";
                buttonStopMachine.BackColor = Color.Red;
            }
            isTest_tabPageTimeRunAuto = !isTest_tabPageTimeRunAuto;
        }

        private void textBoxEncoder_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxEncoder);
            numKey.Show();
        }

        private void buttonSetupConnect_Click(object sender, EventArgs e)
        {
            FormSettingSerial formSettingSeiral = new FormSettingSerial(permissionUser);
            formSettingSeiral.Show();
        }

        private void textBoxDKPully_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxDKPully);
            numKey.Show();
        }

        private void textBoxCDBuCat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxCDBuCat);
            numKey.Show();
        }

        private void textBoxCDBuSai_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxCDBuSai);
            numKey.Show();
        }

        private void textBoxDKSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxDKSat);
            numKey.Show();
        }

        private void textBoxKeoChamCuoi_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxKeoChamCuoi);
            numKey.Show();
        }

        private void textBoxCT_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxCT);
            numKey.Show();
        }

        private void textBoxCL_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxCL);
            numKey.Show();
        }

        private void textBoxTreCT_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreCT);
            numKey.Show();
        }

        private void textBoxTreBL_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreBL);
            numKey.Show();
        }

        private void textBoxTreDongTac_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreDongTac);
            numKey.Show();
        }

        private void textBoxTreBeToiSensor_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreBeToiSensor);
            numKey.Show();
        }

        private void textBoxThoiGianTreKeoCham_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxThoiGianTreKeoCham);
            numKey.Show();
        }

        private void textBoxVongLoXo_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxVongLoXo);
            numKey.Show();
        }

        private void textBoxThoiGianChayMay_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxThoiGianChayMay);
            numKey.Show();
        }

        private void textBoxThoiGianDungMay_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxThoiGianDungMay);
            numKey.Show();
        }

        private void textBoxThoiGianBaoHanh_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxThoiGianBaoHanh);
            numKey.Show();
        }

        private void textBoxChieuDaiBuCatSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxChieuDaiBuCatSat);
            numKey.Show();
        }

        private void textBoxChieuDaiBuCatInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxChieuDaiBuCatInox);
            numKey.Show();
        }

        private void textBoxChieuDaiBuSaiSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxChieuDaiBuSaiSat);
            numKey.Show();
        }

        private void textBoxChieuDaiBuSaiInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxChieuDaiBuSaiInox);
            numKey.Show();
        }

        private void textBoxKeoChamCuoiSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxKeoChamCuoiSat);
            numKey.Show();
        }

        private void textBoxKeoChamCuoiInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxKeoChamCuoiInox);
            numKey.Show();
        }

        private void textBoxTreCatToiSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreCatToiSat);
            numKey.Show();
        }

        private void textBoxTreCatToiInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreCatToiInox);
            numKey.Show();
        }

        private void textBoxTreBeLuiSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreBeLuiSat);
            numKey.Show();
        }

        private void textBoxTreBeLuiInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreBeLuiInox);
            numKey.Show();
        }

        private void textBoxTreDongTacSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreDongTacSat);
            numKey.Show();
        }

        private void textBoxTreDongTacInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxTreDongTacInox);
            numKey.Show();
        }

        private void textBoxThoiGianTreApSuatSat_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxThoiGianTreApSuatSat);
            numKey.Show();
        }

        private void textBoxThoiGianTreApSuatInox_MouseDown(object sender, MouseEventArgs e)
        {
            numKey.setTextBox(textBoxThoiGianTreApSuatInox);
            numKey.Show();
        }

        private void controlPermission()
        {
            if (permissionUser > 0)
            {
                tabControl.Enabled = true;
                buttonSaveSettings.Enabled = true;
            }
            else
            {
                tabControl.Enabled = false;
                buttonSaveSettings.Enabled = false;
            }
        }

    }
}
