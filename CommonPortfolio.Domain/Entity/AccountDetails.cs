using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPortfolio.Domain.Entity
{
    public class AccountDetails
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Position { get; set; }
        public string? SubName { get; set; }
        public string? ProfilePictureLink { get; set; }
        public string? BannerPictureLink { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailedDescription { get; set;}

    }
}
