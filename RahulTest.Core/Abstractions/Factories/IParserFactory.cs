using RahulTest.Core.Abstractions.FileParsers;

namespace RahulTest.Core.Abstractions.Factories
{
    public interface IParserFactory
    {
        IParser GetParser(string format);
    }
}
