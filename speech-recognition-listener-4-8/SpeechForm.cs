using System;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace speech_recognition_listener_4_8
{
    public partial class SpeechForm : Form
    {
        public SpeechForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;

            // https://learn.microsoft.com/en-us/dotnet/api/system.speech.recognition.speechrecognizer.loadgrammar?view=netframework-4.8.1
            SRE = new SpeechRecognitionEngine();
            SRE.SpeechRecognized += onSpeechRecognized;
            Grammar testGrammar =
              new Grammar(new GrammarBuilder("testing"));
            testGrammar.Name = "Test Grammar";
            SRE.LoadGrammar(testGrammar);
            SRE.SetInputToDefaultAudioDevice();
            SRE.RecognizeAsync(RecognizeMode.Multiple);
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if(Visible)
            {
                textBox1.Text = "Listening...";
                textBox1.Select(0, 0);
            }
        }

        private void onSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            BeginInvoke(new Action(() => textBox1.Text = e.Result.Text));
        }
        SpeechRecognitionEngine SRE { get; }
    }
}
