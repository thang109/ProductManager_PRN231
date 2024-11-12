using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBusiness
{
    public class Books
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string? BookName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int?  Price {  get; set; }
        public int CategoryId { get; set; }
        public Categories? Category { get; set; }

        public ICollection<Shippings>? Shippings { get; set; }

    }
}
