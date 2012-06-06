
using System.ComponentModel.DataAnnotations;

namespace SharpDummy.Web.Core.ViewModels
{
    public class LogOnViewModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CreatePersonViewModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
