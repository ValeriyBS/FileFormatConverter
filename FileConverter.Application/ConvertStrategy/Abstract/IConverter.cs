using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Application.ConvertStrategy.Abstract
{
    public interface IConverter
    {
        public string Convert(IQueryable<string> source);
    }
}