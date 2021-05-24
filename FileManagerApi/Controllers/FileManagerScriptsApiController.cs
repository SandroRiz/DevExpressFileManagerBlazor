using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Controllers
{
   
    [ApiController]
    public class FileManagerScriptsApiController : ControllerBase
    {

       
        public FileManagerScriptsApiController()
        {
            
        }

        [Route("api/file-manager-file-system", Name = "FileManagementFileSystemApi")]
        public object FileSystem(FileSystemCommand command, string arguments)
        {
            var config = new FileSystemConfiguration
            {
                Request = Request,
                FileSystemProvider = new PhysicalFileSystemProvider(@"C:\temp"),
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
            object  res = result.GetClientCommandResult();
            return res;
        }
    }
}
