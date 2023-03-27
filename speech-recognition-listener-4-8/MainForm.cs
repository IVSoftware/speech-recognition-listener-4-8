using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace speech_recognition_listener_4_8
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonShowSpeech.Click += onClickShowSpeech;
        }

        SpeechForm _speechForm = new SpeechForm();
        private void onClickShowSpeech(object sender, EventArgs e)
        {
            Task
                .Delay(TimeSpan.FromSeconds(5))
                .GetAwaiter()
                .OnCompleted(() => _speechForm.Close());
            _speechForm.ShowDialog();
        }
    }
}
