using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using AxWMPLib;

namespace SoundBox {
    public partial class Form1 : Form {

        private const string location = @"..\..\sounds\";
        private string fileName;

        public Form1() {
            InitializeComponent();

            KeyPreview = true; // キー入力をFormへ渡す
            messageLabel.Text = "音源を選択してください。";

            // ListBox Sounds へデータ追加
            string[] files = Directory.GetFiles(location, "*", SearchOption.AllDirectories);
            string[] filesName = new string[files.Count()];

            foreach (var f in files.Select((item, index) => new { item, index })) {
                filesName[f.index] = Path.GetFileName(f.item);
            }

            Sounds.Items.AddRange(filesName);
            createInstance();
        }

        WindowsMediaPlayer[] WMP;
        private void createInstance() {
            int elm = 30;
            WMP = new WindowsMediaPlayer[elm];
            for (int i = 0; i < elm; i++) {
                WMP[i] = new WindowsMediaPlayer();
            }
        }

        private void playSound() {
            int i = 0;
            int stoppedNumber = -1;
            foreach (WindowsMediaPlayer w in WMP) {
                if (w.playState != WMPPlayState.wmppsPlaying) { //stopped
                    stoppedNumber = i;
                    break;
                }
                i++;
            }

            if (stoppedNumber != -1) {
                WMP[stoppedNumber].URL = Path.GetFullPath(location) + fileName;
                WMP[stoppedNumber].controls.play();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            messageLabel.Text = e.KeyCode + "が押されました。";

            if (fileName != null) {
                playSound();
            }
        }

        private void SetSoundsBox_SelectedIndexChanged(object sender, EventArgs e) {
            ListBox ls = sender as ListBox;
            fileName = Sounds.Text;
            messageLabel.Text = location + fileName + "が選択されました。";
        }

        private void Sounds_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) return;

            e.SuppressKeyPress = true;
        }
    }
}
