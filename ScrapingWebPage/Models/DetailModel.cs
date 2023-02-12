using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapingWebPage.Models
{
    [Table("Details", Schema = "dbo")]
    public class DetailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TreeName { get; set; }
        public string TreeCode { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public string DateRange { get; set; }
        public string Info { get; set; }

        public int SubgroupId { get; set; }
        [ForeignKey("SubgroupId")]
        public SubgroupDetailsModel SubGroup { get; set; }
    }
}
