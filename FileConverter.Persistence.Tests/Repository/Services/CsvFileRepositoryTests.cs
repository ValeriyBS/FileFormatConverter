using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using FileConverter.Persistence.Repository.Services;
using Xunit;

namespace FileConverter.Persistence.Tests.Repository.Services
{
    public class CsvFileRepositoryTests
    {
        private readonly IFileSystem? _mockFileSystem;

        public CsvFileRepositoryTests()
        {
            var mockInputFile =
                new
                    MockFileData($"name,address_line1,address_line2{Environment.NewLine}Dave,Street,Town");

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile("test.txt", mockInputFile);
            _mockFileSystem = mockFileSystem ?? throw new ArgumentNullException(nameof(mockFileSystem));
        }


        [Fact]
        public void GetAllRowsShouldReturnAllRowsFromFile()
        {
            //Arrange
            var expectedResult = new List<string>
            {
                "name,address_line1,address_line2",
                "Dave,Street,Town"
            };

            var sut = new CsvFileRepository("test.txt", _mockFileSystem!);

            //Act
            var result = sut.GetAllRows().ToList();


            //Assert
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public void GetRowShouldReturnCorrectRowFromFile()
        {
            //Arrange
            const string expectedResult = "Dave,Street,Town";

            var sut = new CsvFileRepository("test.txt", _mockFileSystem!);

            //Act
            var result = sut.GetRow(1).Trim();

            //Assert
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public void TestConstructorShouldThrowsFileNotFoundExceptionWhenInvalidFileName()
        {
            //arrange
            //act
            //assert
            Assert.Throws<FileNotFoundException>(() => new CsvFileRepository("NonExistingFileName.txt"));
        }
    }
}