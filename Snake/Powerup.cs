using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Powerup : Entity
    {
        public Effect Effect { get; set; }
        public int SpawnDuration { get; set; }

        public Powerup(Point newPosition, Effect.EffectVariant effectVariant, int effectDuration, int spawnDuration) : base(newPosition)
        {
            Effect = new Effect(effectVariant, effectDuration);
            SpawnDuration = spawnDuration;
        }
    }
}
