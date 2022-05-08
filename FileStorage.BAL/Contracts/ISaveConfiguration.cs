using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.BAL.Contracts
{
    public interface ISaveConfiguration
    {
        string Path { get; }
        string FolderName { get; }
        string GenerateFileName(string fileName);
    }
}
