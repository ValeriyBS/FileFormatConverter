using System;
using FileConverter.Application.ConvertStrategy.Services;
using Moq;
using Xunit;

namespace FileConverter.Application.Tests.ConvertStrategy.Services
{
    public class ConverterFactoryTests
    {
        [Fact]
        public void TestConverterFactoryShouldReturnCsvToXmlConverter()
        {
            //Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();

            var sut = new ConverterFactory(mockServiceProvider.Object);

            //Act
            sut.GetConverter(ConverterType.CsvToXml);

            //Assert
            mockServiceProvider.Verify(s => s.GetService(typeof(CsvToXmlConverter)), Times.Once);
        }

        [Fact]
        public void TestConverterFactoryShouldReturnCsvToJsonConverter()
        {
            //Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();

            var sut = new ConverterFactory(mockServiceProvider.Object);

            //Act
            sut.GetConverter(ConverterType.CsvToJson);

            //Assert
            mockServiceProvider.Verify(s => s.GetService(typeof(CsvToJsonConverter)), Times.Once);
        }
    }
}