using DocumentBuilder.Shared.Enumerations;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Factories;

public static class NewLineProviderFactory
{
    public static INewLineProvider Create(LineEndings lineEndings) => lineEndings switch
    {
        LineEndings.Environment => new EnvironmentNewLineProvider(),
        LineEndings.Windows => new WindowsNewLineProvider(),
        LineEndings.Linux => new LinuxNewLineProvider(),
        _ => throw new NotSupportedException($"{lineEndings} is currently not supported")
    };
}
