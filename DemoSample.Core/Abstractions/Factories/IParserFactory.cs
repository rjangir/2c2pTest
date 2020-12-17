using DemoSample.Core.Abstractions.FileParsers;

namespace DemoSample.Core.Abstractions.Factories
{
    public interface IParserFactory
    {
        IParser GetParser(string format);
    }
}
