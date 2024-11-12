using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBusiness
{
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {  get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public ICollection<Shippings>? Shippings { get; set; }

    }
}
