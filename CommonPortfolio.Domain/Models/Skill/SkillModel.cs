using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPortfolio.Domain.Models.Skill
{
    public class SkillModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SkillTypeId { get; set; }
        public required string Title { get; set; }
        public string? IconClass { get; set; }

    }
}
