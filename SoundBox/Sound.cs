using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace SoundBox {

    public class Sound {

        public const string location = Form1.location;
        public string fileName;

        WindowsMediaPlayer[] WMP;

        public Sound() {
            createInstance();
        }

        private void createInstance() {
            int elm = 30;
            WMP = new WindowsMediaPlayer[elm];
            for (int i = 0; i < elm; i++) {
                WMP[i] = new WindowsMediaPlayer();
            }
        }

        public void playSound() {
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
    }
}
