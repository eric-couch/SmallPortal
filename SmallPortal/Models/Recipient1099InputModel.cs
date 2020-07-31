using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmallPortal.Models
{
    [NotMapped]
    public class Recipient1099InputModel
    {
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Key]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
        //[RegularExpression("^\\d{3}-\\d{2}-\\d{4}$",
        //                ErrorMessage = "Invalid TaxId Number")]
        [Display(Name = "Tax ID Number")]
        public string TaxIDNumber { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Box 1")]
        [Column(TypeName = "money")]
        public decimal box1 { get; set; }
        [Display(Name = "Box 2")]
        [Column(TypeName = "money")]
        public decimal box2 { get; set; }
        [Display(Name = "Box 3")]
        [Column(TypeName = "money")]
        public decimal box3 { get; set; }
        [Display(Name = "Box 4")]
        [Column(TypeName = "money")]
        public decimal box4 { get; set; }
        [Display(Name = "Box 5")]
        [Column(TypeName = "money")]
        public decimal box5 { get; set; }
        [Display(Name = "Box 6")]
        [Column(TypeName = "money")]
        public decimal box6 { get; set; }
        [Display(Name = "Box 7")]
        public bool box7 { get; set; }
        [Display(Name = "Box 8")]
        [Column(TypeName = "money")]
        public decimal box8 { get; set; }
        [Display(Name = "Box 9")]
        [Column(TypeName = "money")]
        public decimal box9 { get; set; }
        [Display(Name = "Box 10")]
        [Column(TypeName = "money")]
        public decimal box10 { get; set; }
        [Display(Name = "Box 12")]
        [Column(TypeName = "money")]
        public decimal box12 { get; set; }
        [Display(Name = "Box 13")]
        [Column(TypeName = "money")]
        public decimal box13 { get; set; }
        [Display(Name = "Box 14")]
        [Column(TypeName = "money")]
        public decimal box14 { get; set; }
        [Display(Name = "Box 15")]
        [Column(TypeName = "money")]
        public decimal box15 { get; set; }
        [Display(Name = "Box 16")]
        //[Column(TypeName = "money")]
        public string box16 { get; set; }
        [Display(Name = "Box 17")]
        [Column(TypeName = "money")]
        public decimal box17 { get; set; }
    }

}
