using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBusiness
{
    public class Shippings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingId { get; set; }
        public int BookId {  get; set; }
        public DateTime? DateBooking { get; set; }
        public DateTime? DateShip { get; set; }
        public string? LocationShip { get; set; }
        public int? UserOrderId {  get; set; }
        public int? UserApproveId {  get; set; }
        public string? Status { get; set; }
        [ForeignKey("UserOrderId")]
        public Users? UserOrder { get; set; }

        [ForeignKey("UserApproveId")]
        public Users? UserApprove { get; set; }
        [ForeignKey("BookId")]
        public Books? Books { get; set; }
 


    }
}
