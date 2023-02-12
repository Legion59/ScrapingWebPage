using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapingWebPage.Models
{
    [Table("CarConfigurations", Schema = "dbo")]
    public class CarConfigurationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationId { get; init; }
        public string ConfigurationName { get; set; }
        public string DateRange { get; set; }
        public List<CarConfigurationSpecModel> Specs { get; set; } = new List<CarConfigurationSpecModel>();

        [NotMapped]
        public string GroupDetailsLink { get; set; }

        public string CarId { get; set; }
        [ForeignKey("CarId")]
        public CarPageModel CarModel { get; set; }

        public List<GroupDetailsModel> GroupDetails { get; set; }
    }
}
