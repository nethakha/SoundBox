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

        public const string location = @"..\..\sounds\";
        public string fileName;
        private Sound s = new Sound();

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
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            messageLabel.Text = e.KeyCode + "が押されました。";

            if (fileName != null) {
                s.fileName = fileName;
                s.playSound();
            }
        }

        private void SetSoundsBox_SelectedIndexChanged(object sender, EventArgs e) {
            fileName = Sounds.Text;
        }

        private void Sounds_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) return;
            e.SuppressKeyPress = true;
        }
    }
}
