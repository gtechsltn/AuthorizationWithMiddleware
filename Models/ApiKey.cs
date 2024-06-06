using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationWithMiddleware.Models
{
    public class ApiKey
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
