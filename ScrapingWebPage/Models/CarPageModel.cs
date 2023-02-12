using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapingWebPage.Models
{
    [Table("CarModels", Schema = "dbo")]
    public class CarPageModel
    {
        [Key]
        public string CarId { get; set; }
        public string Name { get; set; }
        public string DateRange { get; set; }
        public string ModelCode { get; set; }

        [NotMapped]
        public string ConfigurationLink { get; set; }

        public List<CarConfigurationModel> CarConfigurations { get; set; }
    }
}
