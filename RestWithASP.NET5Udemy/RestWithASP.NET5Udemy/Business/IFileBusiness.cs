using Microsoft.AspNetCore.Http;
using RestWithASP.NET5Udemy.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP.NET5Udemy.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
