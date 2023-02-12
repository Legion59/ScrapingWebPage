using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingWebPage.Models
{
    public class CarConfigurationSpecModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecId { get; set; }
        public string SpecValue { get; set; }

        public int CarConfigurationId { get; set; }
        [ForeignKey("CarConfigurationId")]
        public CarConfigurationModel CarConfiguration { get; set; }
    }
}
