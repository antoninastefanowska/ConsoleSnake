using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Effect
    {
        public enum EffectVariant
        {
            None,
            Fly,
            Fast,
            Slow,
            Invicible,
        };

        public EffectVariant Variant { get; set; }
        public int Duration { get; set; }

        public Effect()
        {
            Variant = EffectVariant.None;
            Duration = -1;
        }

        public Effect(EffectVariant variant, int duration)
        {
            Variant = variant;
            Duration = duration;
        }
    }
}
