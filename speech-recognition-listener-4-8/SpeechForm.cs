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

        protected async override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            await Task.Delay(TimeSpan.FromSeconds(5));
            Application.Exit();
        }

        private void onSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            BeginInvoke(new Action(() => textBox1.Text = e.Result.Text));
        }
        SpeechRecognitionEngine SRE { get; }
    }
}
