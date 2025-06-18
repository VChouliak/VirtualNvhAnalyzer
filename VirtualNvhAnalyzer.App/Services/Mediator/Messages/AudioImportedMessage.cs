namespace VirtualNvhAnalyzer.App.Services.Mediator.Messages
{
    public class AudioImportedMessage
    {
        public AudioImportedMessage(string filename)
        {
            _fileName = filename;
        }

        private string _fileName;

        public string FileName
        {
            get => _fileName;
        }

    }
}
