using System;
using System.Collections.Generic;

namespace ArchipelagoClient.Data
{
    public struct HeroKit : IEquatable<HeroKit>
    {
        public HeroKit(String name, String kit)
        {
            Name = name;
            Kit = kit;
        }
        
        public String Name;
        public String Kit;

        public override string ToString() => $"{Name} - {Kit}";

        public static HeroKit FromString(String s)
        {
            String[] vals = s.Split('-');
            return new HeroKit(vals[0].Trim(), vals[1].Trim());
        }

        public bool Equals(HeroKit other)
        {
            return Name == other.Name && Kit == other.Kit;
        }

        public override bool Equals(object obj)
        {
            return obj is HeroKit other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Kit != null ? Kit.GetHashCode() : 0);
            }
        }
    }

    public static class HeroKitLookup
    {
        public static readonly Dictionary<string, HeroKit> OSFEBeingIdToHeroKit = new Dictionary<string, HeroKit>()
        {
            { "Saffron", HeroKit.FromString("Saffron - Default") },
            { "SaffronAlt", HeroKit.FromString("Saffron - Chrono") },
            { "SaffronGen", HeroKit.FromString("Saffron - Solo") },
            { "Reva", HeroKit.FromString("Reva - Shield") },
            { "RevaAlt", HeroKit.FromString("Reva - Beat") },
            { "Gunner", HeroKit.FromString("Gunner - Manafire") },
            { "GunnerAlt", HeroKit.FromString("Gunner - Bullethell") },
            { "Selicy", HeroKit.FromString("Selicy - Snow") },
            { "SelicyAlt", HeroKit.FromString("Selicy - Invade") },
            { "Hazel", HeroKit.FromString("Hazel - Build") },
            { "HazelAlt", HeroKit.FromString("Hazel - Teardown") },
            { "Terra", HeroKit.FromString("Terra - Break") },
            { "TerraAlt", HeroKit.FromString("Terra - Pyro") },
            { "Shiso", HeroKit.FromString("Shiso - Execute") },
            { "ShisoAlt", HeroKit.FromString("Shiso - Kunai") },
            { "Violette", HeroKit.FromString("Violette - Doubletime") },
            { "VioletteAlt", HeroKit.FromString("Violette - Aria") },
            { "Shopkeeper", HeroKit.FromString("Shopkeeper - Money") },
        };
    }
}