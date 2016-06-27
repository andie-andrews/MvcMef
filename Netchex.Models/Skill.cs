using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeOfMana.Models
{
    public class Skill : Entity
    {
        public Skill()
        {
            Achievements = new List<Achievement>();
        }

        public string Description { get; set; }

        public int Order { get; set; }

        public int Intellect { get; set; }

        public int Wisdom { get; set; }

        public int Charisma { get; set; }

        public int Fortitude { get; set; }

        public int SkillSetID { get; set; }

        public SkillSet SkillSet { get; set; }

        public IList<Achievement> Achievements { get; set; }
    }
}
