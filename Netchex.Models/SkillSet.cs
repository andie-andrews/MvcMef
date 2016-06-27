using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeOfMana.Models
{
    public class SkillSet : Entity
    {
        public SkillSet()
        {
            Skills = new List<Skill>();
        }

        public string Name { get; set; }

        public IList<Skill> Skills { get; set; }
    }
}
