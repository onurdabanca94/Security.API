using System.ComponentModel.DataAnnotations.Schema;

namespace Security.WebUI.Models
{
    public partial class Product
    {
        [NotMapped]
        public string EncrypedId { get; set; }
    }
}
