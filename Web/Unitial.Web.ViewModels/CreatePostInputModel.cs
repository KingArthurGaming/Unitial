using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Unitial.Web.ViewModels
{
    public class CreatePostInputModel
    {
        public string Caption { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile UploadImage { get; set; }
    }
}
