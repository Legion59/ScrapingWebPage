using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapingWebPage.Models
{
    [Table("GroupDetails", Schema = "dbo")]
    public class GroupDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public string SubgroupLink { get; set; }

        public int ConfigurationId { get; set; }
        [ForeignKey("ConfigurationId")]
        public CarConfigurationModel CarConfiguration { get; set; }

        public List<SubgroupDetailsModel> Subgroups { get; set; }
    }
}
