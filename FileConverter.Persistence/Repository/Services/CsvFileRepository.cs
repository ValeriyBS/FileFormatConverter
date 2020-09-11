using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using FileConverter.Application.Interfaces.Persistence;

namespace FileConverter.Persistence.Repository.Services
{
    public class CsvFileRepository : ICsvFileRepository
    {
        private readonly string[] _fileData;

        public CsvFileRepository(string filename) : this(filename, new FileSystem())
        {
        }

        public CsvFileRepository(string filename, IFileSystem fileSystem)
        {
            //Can be done also with CsvHelper
            var nonNullFileName = filename ?? throw new ArgumentNullException();

            try
            {
                _fileData = fileSystem.File.ReadAllLines(nonNullFileName);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{nonNullFileName} not found! {e}");

                throw;
            }
        }

        public IQueryable<string> GetAllRows()
        {
            return _fileData.AsQueryable();
        }

        public string GetRow(int id)
        {
            return _fileData.ToArray()[id];
        }
    }
}