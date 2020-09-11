using System.Xml.Linq;
using FileConverter.Helper.Extensions;
using Xunit;

namespace FileConverter.Helper.Tests.Extensions
{
    public class XmlExtensionsTests
    {
        [Fact]
        public void TestCsvRowToXmlShouldReturnCorrectXmlConversion()
        {
            //Arrange
            const string csvRow = "Dave,Street,Town";
            const string csvHeader = "name,address_line1,address_line2";

            var expectedResult = new XElement("Line1",
                new XElement("name","Dave"),
                new XElement("address",
                    new XElement("line1","Street"),
                    new XElement("line2","Town")
                    )
                ).ToString();

            var sut = new XElement("Line1");

            //Act
            var result= sut.CsvRowToXml(csvHeader, csvRow).ToString();

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
