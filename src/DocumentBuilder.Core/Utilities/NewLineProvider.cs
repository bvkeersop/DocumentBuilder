namespace DocumentBuilder.Utilities;

public interface INewLineProvider
{
    string GetNewLine();
}

public class WindowsNewLineProvider : INewLineProvider
{
    public string GetNewLine()
    {
        return "\r\n";
    }
}

public class LinuxNewLineProvider : INewLineProvider
{
    public string GetNewLine()
    {
        return "\n";
    }
}

public class EnvironmentNewLineProvider : INewLineProvider
{
    public string GetNewLine()
    {
        return Environment.NewLine;
    }
}