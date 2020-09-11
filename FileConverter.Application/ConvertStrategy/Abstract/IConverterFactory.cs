using FileConverter.Application.ConvertStrategy.Services;

namespace FileConverter.Application.ConvertStrategy.Abstract
{
    public interface IConverterFactory
    {
        IConverter GetConverter(ConverterType type);
    }
}