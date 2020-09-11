using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FileConverter.Application.ConvertStrategy.Services;
using Xunit;

namespace FileConverter.Application.Tests.ConvertStrategy.Services
{
    public class CsvToXmlConverterTests
    {
        [Fact]
        public void TestConvertShouldReturnCorrectXml()
        {
            //Arrange
            var expectedResult = new XElement("Root",
                new XElement("Line1",
                    new XElement("name", "Dave"),
                    new XElement("address",
                         new XElement("line1", "Street"),
                         new XElement("line2", "Town")
                )
            )).ToString();

            var testData = new List<string>
            {
                "name,address_line1,address_line2",
                "Dave,Street,Town"
            }.AsQueryable();

            var sut = new CsvToXmlConverter();

            //Act
            var result = sut.Convert(testData).ToString();

            //Assert
            Assert.Equal(expectedResult,result);
        }
    }
}