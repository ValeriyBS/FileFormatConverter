using System.Collections.Generic;
using System.Linq;
using FileConverter.Application.ConvertStrategy.Services;
using Newtonsoft.Json.Linq;
using Xunit;

namespace FileConverter.Application.Tests.ConvertStrategy.Services
{
    public class CsvToJsonConverterTests
    {
        [Fact]
        public void TestConvertShouldReturnCorrectJson()
        {
            //Arrange
            var expectedResult = new JArray(
                new JObject(
                    new JProperty("name", "Dave"),
                    new JProperty("address",
                        new JObject(
                            new JProperty("line1", "Street"),
                            new JProperty("line2", "Town"))))).ToString();


            var testData = new List<string>
            {
                "name,address_line1,address_line2",
                "Dave,Street,Town"
            }.AsQueryable();

            var sut = new CsvToJsonConverter();

            //Act

            var result = sut.Convert(testData);


            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}