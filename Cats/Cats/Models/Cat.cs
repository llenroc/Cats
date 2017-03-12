using Microsoft.WindowsAzure.MobileServices;

namespace Cats.Models
{
    [DataTable("Cats")]
    public class Cat
    {
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Nome { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string WebSite { get; set; }
        public string Image { get; set; }
    }
}
