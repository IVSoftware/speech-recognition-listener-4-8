Based on your comment, I would suggest that the speech recognition form be contained by a main form. This would allow the speech form to be re-triggered the way you mentioned. This might not be the _exact_ behavior you want, but I hope it will serve as a starting point.

[![main form][1]][1]

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

**Main Form**

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

***
When the button is clicked, the SpeechForm will display for 5 seconds and then close. This process can be repeated any number of times.

[![speech flow][2]][2]

**Speech Form**

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

  [1]: https://i.stack.imgur.com/LTiMz.png
  [2]: https://i.stack.imgur.com/DBgdv.png