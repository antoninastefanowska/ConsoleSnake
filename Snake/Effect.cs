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
            Fast,
            Slow,
            Invicible,
            Shrink,
            Life,
        };

        public const int EffectNumber = 5;
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

        public void LowerDuration()
        {
            if (Variant != EffectVariant.None)
                Duration--;
        }

        public void ResetEffect()
        {
            Variant = EffectVariant.None;
            Duration = -1;
        }

        public override string ToString()
        {
            switch (Variant)
            {
                case EffectVariant.Invicible:
                    return "Nieśmiertelność";
                    break;
                case EffectVariant.Fast:
                    return "Przyśpieszenie";
                    break;
                case EffectVariant.Slow:
                    return "Spowolnienie";
                    break;
                case EffectVariant.Shrink:
                    return "Skurczenie";
                    break;
                case EffectVariant.Life:
                    return "Dodatkowe życie";
                    break;
                default:
                    return "Brak";
                    break;
            }
        }
    }
}
