using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapingWebPage.Models
{
    [Table("SubgroupDetails", Schema = "dbo")]
    public class SubgroupDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubgroupId { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped] 
        public string DetailLink { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public GroupDetailsModel GroupDetail { get; set; }

        public List<DetailModel> Details { get; set; }
    }
}
