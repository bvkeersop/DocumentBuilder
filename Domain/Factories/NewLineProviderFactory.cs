using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Utilities;

namespace DocumentBuilder.Domain.Factories
{
    internal static class NewLineProviderFactory
    {
        public static INewLineProvider Create(LineEndings lineEndings)
        {
            return lineEndings switch
            {
                LineEndings.Environment => new EnvironmentNewLineProvider(),
                LineEndings.Windows => new WindowsNewLineProvider(),
                LineEndings.Linux => new LinuxNewLineProvider(),
                _ => throw new NotSupportedException($"{lineEndings} is currently not supported")
            };
        }
    }
}
