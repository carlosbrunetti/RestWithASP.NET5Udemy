using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASP.NET5Udemy.Business;
using RestWithASP.NET5Udemy.Data.VO;

namespace RestWithASP.NET5Udemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    [Authorize("Bearer")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer =  _fileBusiness.GetFile(fileName);
            if (buffer != null)
            {
                HttpContext.Response.ContentType =
                    $"application/{Path.GetExtension(fileName.Replace(".", string.Empty))}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }


        [HttpPost("UploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("UploadMultipleFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFile([FromForm] List<IFormFile> files)
        {
           List<FileDetailVO> detail = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(detail);
        }

    }
}
