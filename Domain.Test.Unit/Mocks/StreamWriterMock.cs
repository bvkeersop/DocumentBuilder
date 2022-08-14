namespace NDocument.Domain.Test.Unit.Mocks
{
    internal class StreamWriterMock : StreamWriter
    {
        public IEnumerable<string> WriteStringValuesReceived { get; private set; } = Enumerable.Empty<string>();
        public IEnumerable<string> WriteLineStringValuesReceived { get; private set; } = Enumerable.Empty<string>();
        public IEnumerable<char> WriteCharValuesReceived { get; private set; } = Enumerable.Empty<char>();
        public int AmountOfFlushes { get; private set; } = 0;

        public StreamWriterMock() : base(new MemoryStream()) { }

        public override Task WriteAsync(string value)
        {
            WriteStringValuesReceived = WriteStringValuesReceived.Append(value);
            return Task.CompletedTask;
        }

        public override Task WriteAsync(char value)
        {
            WriteCharValuesReceived = WriteCharValuesReceived.Append(value);
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(string value)
        {
            WriteLineStringValuesReceived = WriteLineStringValuesReceived.Append(value);
            return Task.CompletedTask;
        }

        public override void Flush()
        {
            AmountOfFlushes += 1;
        }

        public bool HasReceivedWrite(string value)
        {
            return WriteStringValuesReceived.Contains(value);
        }

        public bool HasReceivedWrite(char value)
        {
            return WriteCharValuesReceived.Contains(value);
        }

        public bool HasReceivedWriteLine(string value)
        {
            return WriteLineStringValuesReceived.Contains(value);
        }
    }
}
