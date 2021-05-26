using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Controllers
{
   
    [ApiController]
    public class FileManagerScriptsApiController : ControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileManagerScriptsApiController> _log;

        public FileManagerScriptsApiController(IWebHostEnvironment webHostEnvironment, ILogger<FileManagerScriptsApiController> log )
        {
            _webHostEnvironment = webHostEnvironment;
            _log = log;
        }

      

       
        [Route("api/file-manager-file-system", Name = "FileManagementFileSystemApi")]
        [HttpPost, HttpGet]
        public object FileSystem(FileSystemCommand command, string arguments)
        {
            try
            {
                _log.LogInformation("Api Called with {command} and {arguments}", command.ToString(), arguments);
                var config = new FileSystemConfiguration
                {
                    Request = Request,
                    FileSystemProvider = new PhysicalFileSystemProvider(_webHostEnvironment.ContentRootPath + "/wwwroot/files"),
                    //uncomment the code below to enable file/folder management
                    AllowCopy = true,
                    AllowCreate = true,
                    AllowMove = true,
                    AllowDelete = true,
                    AllowRename = true,
                    AllowUpload = true,
                    AllowDownload = true
                    //AllowedFileExtensions = new[] { ".js", ".json", ".css" }
                };
                var processor = new FileSystemCommandProcessor(config);
                var result = processor.Execute(command, arguments);
                object res = result.GetClientCommandResult();
                return res;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }
            
        }
    }
}
