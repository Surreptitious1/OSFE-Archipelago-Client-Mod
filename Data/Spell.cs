using System.Collections.Generic;
using System.Threading;
using UnityEngine.Experimental.UIElements.StyleEnums;

namespace ArchipelagoClient.Data
{
    public static class SpellLookup
    {
        public static readonly Dictionary<string, string> APSpelltoOSFESpell = new Dictionary<string, string>()
        {
            // Anima
            { "Spell Card: Meltdown", "Meltdown" },
            { "Spell Card: Firewall", "Firewall" },
            { "Spell Card: Thunder", "Thunder" },
            { "Spell Card: Cold Medicine", "ColdMedicine" },
            { "Spell Card: Pekay Fire", "PekayFire" },
            { "Spell Card: Frost Barrage", "FrostBarrage" },
            { "Spell Card: Frost Bolt", "Frostbolt" },
            { "Spell Card: Mini Thunder", "ThunderLight" },
            { "Spell Card: Thunderstorm", "StormThunder" },
            { "Spell Card: Ice Spikes", "IceSpikes" },
            { "Spell Card: Cold Snap", "ColdSnap" },
            { "Spell Card: Back Burner", "BackBurner" },
            { "Spell Card: Twinferno", "Twinferno" },
            { "Spell Card: Combust", "Combust" },
            { "Spell Card: Frostbite", "Frostbite" },
            { "Spell Card: Ember", "Ember" },
            { "Spell Card: Cryokinesis", "Cryokinesis" },
            { "Spell Card: Salamander", "Salamander" },
            { "Spell Card: Ring of Fire", "RingOfFire" },
            { "Spell Card: Brushfire", "Brushfire" },
            { "Spell Card: Wildfire", "Wildfire" },
            { "Spell Card: Hailstorm", "StormIce" },
            { "Spell Card: Ice Hockey", "Hockey" },
            { "Spell Card: Rage", "Rage" },
            { "Spell Card: Pyroblast", "Pyroblast" },
            { "Spell Card: Carpet Bomb", "CarpetBomb" },
            { "Spell Card: Ice Needle", "IceNeedle" },
            { "Spell Card: Firestorm", "StormFire" },
            { "Spell Card: Sunder", "Sunder" },
            { "Spell Card: Blizzard", "StormBlizzard" },
            { "Spell Card: Flamberge", "Flamberge" },
            { "Spell Card: Fimbulveter", "Fimbulveter" },
            { "Spell Card: EXPLOSION!", "Explosion" },

            // Convergence
            { "Spell Card: Mana Potion", "ManaCatch" },
            { "Spell Card: Mana Steal", "ManaSteal" },
            { "Spell Card: Boomerang", "Boomerang" },
            { "Spell Card: Hired Gun", "HiredGun" },
            { "Spell Card: Tri Shot", "TriShot" },
            { "Spell Card: Meditate", "Meditate" },
            { "Spell Card: Focus", "Focus" },
            { "Spell Card: Tri Rag", "TriRag" },
            { "Spell Card: Whirl", "Whirl" },
            { "Spell Card: Sequencer", "Sequencer" },
            { "Spell Card: Incline", "Incline" },
            { "Spell Card: Amalgam", "Amalgam" },
            { "Spell Card: Mana Fusion", "ManaFusion" },
            { "Spell Card: Sapphire Ring", "SapphireRing" },
            { "Spell Card: Ping", "Ping" },
            { "Spell Card: Limit Break", "LimitBreak" },
            { "Spell Card: Innervate", "Innervate" },
            { "Spell Card: Empower Ring", "EmpowerRing" },
            { "Spell Card: Charge Ring", "ChargeRing" },
            { "Spell Card: Tri Force", "TriForce" },
            { "Spell Card: Sera Cannon", "SeraCannon" },
            { "Spell Card: Overload", "Overload" },
            { "Spell Card: Power Saws", "PowerSaws" },

            // Doublelift
            { "Spell Card: Ramjet", "Ramjet" },
            { "Spell Card: Sleight", "Sleight" },
            { "Spell Card: Skewer", "LaserSolo" },
            { "Spell Card: Ambient Burst", "AmbientBurst" },
            { "Spell Card: Jam Slam", "JamSlam" },
            { "Spell Card: Deck Slam", "DeckSlam" },
            { "Spell Card: Cataclysm", "Cataclysm" },
            { "Spell Card: Railgun", "LaserTriple" },
            { "Spell Card: Jam Cannon", "JamCannon" },
            { "Spell Card: Time Slow", "TimeSlow" },
            { "Spell Card: Switchbait", "Switchbait" },
            { "Spell Card: Viruspell", "Viruspell" },
            { "Spell Card: Fling", "Fling" },
            { "Spell Card: Echo", "Echo" },
            { "Spell Card: Gambit", "Gambit" },
            { "Spell Card: Unleash", "Unleash" },
            { "Spell Card: Sustain", "Sustain" },
            { "Spell Card: Time Stop", "TimeStop" },
            { "Spell Card: Preload", "Preload" },
            { "Spell Card: Trisect", "Trisect" },
            { "Spell Card: Chrono Sphere", "ChronoSphere" },
            { "Spell Card: Wonder", "Wonder" },
            { "Spell Card: Salvo", "Salvo" },
            { "Spell Card: Midnight", "Midnight" },

            // Glimmer
            { "Spell Card: Crossfire", "Crossfire" },
            { "Spell Card: Shattersaw", "Shattersaw" },
            { "Spell Card: ZigZag", "ZigZag" },
            { "Spell Card: Paragon", "Paragon" },
            { "Spell Card: Soul Fire", "SoulFire" },
            { "Spell Card: Breakout", "Breakout" },
            { "Spell Card: Diag Beam", "DiagBeam" },
            { "Spell Card: Energizer", "Energizer" },
            { "Spell Card: Zenith", "Zenith" },
            { "Spell Card: Glaive", "Glaive" },
            { "Spell Card: Shine", "Shine" },
            { "Spell Card: Circuit", "Circuit" },
            { "Spell Card: Solar Charge", "SolarCharge" },
            { "Spell Card: Eternity Cannon", "EternityCannon" },
            { "Spell Card: Infinity Beam", "InfinityBeam" },
            { "Spell Card: Spirit Sword", "SpiritSword" },
            { "Spell Card: Glitterbomb", "Glitterbomb" },
            { "Spell Card: Glitterstorm", "StormGlitter" },
            { "Spell Card: Sunshine", "Sunshine" },
            { "Spell Card: Sawstorm", "StormSaw" },

            // Hearth
            { "Spell Card: Sword Row", "SwordLine" },
            { "Spell Card: Earthen Armor", "EarthenArmor" },
            { "Spell Card: Rock Cycle", "RockCycle" },
            { "Spell Card: Ion Cannon", "IonCannon" },
            { "Spell Card: Tile Fire", "TileFire" },
            { "Spell Card: Tremor", "Tremor" },
            { "Spell Card: Coldstone", "Coldstone" },
            { "Spell Card: Fracture", "Fracture" },
            { "Spell Card: Orbital Beam", "OrbitalBeam" },
            { "Spell Card: Entangle", "Entangle" },
            { "Spell Card: Barrier", "Barrier" },
            { "Spell Card: Wreath", "Wreath" },
            { "Spell Card: Missiletoe", "MissileTow" },
            { "Spell Card: Earth Prayer", "EarthPrayer" },
            { "Spell Card: Flat Earth", "FlatEarth" },
            { "Spell Card: Fissure", "Fissure" },
            { "Spell Card: Flint Shot", "FlintShot" },
            { "Spell Card: Earth Wyrm", "EarthWyrm" },
            { "Spell Card: Excavate", "Excavate" },
            { "Spell Card: Waterfall", "Waterfall" },
            { "Spell Card: Prophecy", "Prophecy" },
            { "Spell Card: Swords of Light", "StormSword" },
            { "Spell Card: Rock Tomb", "RockTomb" },
            { "Spell Card: Jackhammer", "Jackhammer" },
            { "Spell Card: Ambush", "Ambush" },
            { "Spell Card: Weed Wacker", "WeedWacker" },
            { "Spell Card: Holy Ground", "HolyGround" },

            // Hexawan
            { "Spell Card: Minefield", "Minefield" },
            { "Spell Card: Shotgun", "Shotgun" },
            { "Spell Card: Minnie Gun", "Minigun" },
            { "Spell Card: Volley", "Volley" },
            { "Spell Card: Shard Toss", "ShardToss" },
            { "Spell Card: Gun Turret", "SumTurretgun" },
            { "Spell Card: Sunbeamer", "SumSunbeamer" },
            { "Spell Card: Bombard", "Bombard" },
            { "Spell Card: Shieldgen", "SumShieldgen" },
            { "Spell Card: Bomb Toss", "BombToss" },
            { "Spell Card: Blue Bullets", "BlueBullets" },
            { "Spell Card: Mana Lattice", "ManaLattice" },
            { "Spell Card: Beacon", "SumBeacon" },
            { "Spell Card: Sweeper", "Sweeper" },
            { "Spell Card: Wall", "SumWall" },
            { "Spell Card: Salvage", "Salvage" },
            { "Spell Card: Beam Crystals", "BeamCrystals" },
            { "Spell Card: Blast Crystals", "Blastcrystals" },
            { "Spell Card: Turret SD", "SumTurretSD" },
            { "Spell Card: Mine", "Mine" },
            { "Spell Card: Laser Turret", "SumTurretLaser" },
            { "Spell Card: Resonate", "Resonate" },
            { "Spell Card: Cross Turret", "CrossturretToss" },
            { "Spell Card: Sidewinder", "Sidewinder" },
            { "Spell Card: Monument", "SumMonument" },
            { "Spell Card: Castle", "Castle" },
            { "Spell Card: Super Minnie Gun", "SuperMiniGun" },
            { "Spell Card: Grail", "SumGrail" },
            { "Spell Card: Silo", "SumSilomini" },

            // Kinesys
            { "Spell Card: Undertow", "Undertow" },
            { "Spell Card: River", "River" },
            { "Spell Card: Kinetic Wave", "KineticWave" },
            { "Spell Card: Air Slash", "AirSlash" },
            { "Spell Card: Upwind", "Upwind" },
            { "Spell Card: Align", "Align" },
            { "Spell Card: Tractor Beam", "TractorBeam" },
            { "Spell Card: Flurry", "Flurry" },
            { "Spell Card: Pull", "Pull" },
            { "Spell Card: Spike Strip", "SpikeStrip" },
            { "Spell Card: Caltrops", "Caltrops" },
            { "Spell Card: Fadeaway", "Fadeaway" },
            { "Spell Card: Knock Down", "ForcePushDown" },
            { "Spell Card: Wobble", "Wobble" },
            { "Spell Card: Push Up", "ForcePushUp" },
            { "Spell Card: Claw Traps", "ClawTraps" },
            { "Spell Card: Warp Rays", "WarpRays" },
            { "Spell Card: Blink", "Blink" },
            { "Spell Card: Bow Snipe", "BowSnipe" },
            { "Spell Card: Skipper", "Skipper" },
            { "Spell Card: Northwind", "Northwind" },
            { "Spell Card: Tsunami", "Tsunami" },
            { "Spell Card: Inverter", "Inverter" },
            { "Spell Card: Hyper Beam", "HyperBeam" },
            { "Spell Card: Monsoon", "Monsoon" },

            // Miseri
            { "Spell Card: Glassify", "Glassify" },
            { "Spell Card: Anubis", "Anubis" },
            { "Spell Card: Revenge", "Revenge" },
            { "Spell Card: Poison Dart", "PoisonDart" },
            { "Spell Card: Booster Shot", "BoosterShot" },
            { "Spell Card: Poison Tails", "PoisonTail" },
            { "Spell Card: Soul Link", "SoulLink" },
            { "Spell Card: Pinch", "Pinch" },
            { "Spell Card: Twoxin", "Twoxin" },
            { "Spell Card: Detox", "Detox" },
            { "Spell Card: Showdown", "Showdown" },
            { "Spell Card: Rest", "Rest" },
            { "Spell Card: Leech", "Leech" },
            { "Spell Card: Sludge Bomb", "SludgeBomb" },
            { "Spell Card: Backstab", "Backstab" },
            { "Spell Card: Transfuse", "Transfuse" },
            { "Spell Card: Acid Rain", "AcidRain" },
            { "Spell Card: Smog", "Smog" },
            { "Spell Card: Pandemic", "Pandemic" },
            { "Spell Card: Corset", "Corset" },
            { "Spell Card: Devour", "Devour" },
            { "Spell Card: Health Potion", "HealthCatch" },
            { "Spell Card: Venoshock", "Venoshock" },
            { "Spell Card: Double Tap", "DoubleTap" },
            { "Spell Card: Hellfire", "Hellfire" },
            { "Spell Card: Blood Shield", "BloodShield" },
            { "Spell Card: Cynet", "Cynet" },

            // Phalanx
            { "Spell Card: Absorb", "Absorb" },
            { "Spell Card: Jam Shield", "JamShield" },
            { "Spell Card: Haven", "Haven" },
            { "Spell Card: Deck Shield", "DeckShield" },
            { "Spell Card: Shield Catch", "ShieldCatch" },
            { "Spell Card: Shields Up", "ShieldsUp" },
            { "Spell Card: Fate Shield", "FateShield" },
            { "Spell Card: Counter Strike", "CounterStrike" },
            { "Spell Card: Shield Throw", "ShieldThrow" },
            { "Spell Card: Reflector", "Reflector" },
            { "Spell Card: Shield Beam", "ShieldBeam" },
            { "Spell Card: Entrench", "Entrench" },
            { "Spell Card: Miss-Me Shield", "MissMeShield" },
            { "Spell Card: Steel Skin", "SteelSkin" },
            { "Spell Card: Iron Will", "IronWill" },
            { "Spell Card: Forte", "Forte" },
            { "Spell Card: Diamond Ring", "DiamondRing" },
            { "Spell Card: Spell Shield", "SpellShield" },
            { "Spell Card: Blessing of Susano'o", "BlessingOfSusano" },
            { "Spell Card: Ice Shield", "IceShield" },
            { "Spell Card: Stasis", "Stasis" },
            { "Spell Card: Downfall", "Downfall" },
            { "Spell Card: Stinger", "Stinger" },

            // Slashfik
            { "Spell Card: Kunai", "Kunai" },
            { "Spell Card: Slice", "Slice" },
            { "Spell Card: Doubletake", "MatKunais" },
            { "Spell Card: Magic Claw", "MagicClaw" },
            { "Spell Card: Swipe", "Swipe" },
            { "Spell Card: Shadow Shift", "ShadowShift" },
            { "Spell Card: Step Pierce", "StepPierce" },
            { "Spell Card: Knife", "Knife" },
            { "Spell Card: Bouncing Blade", "BladeThrow" },
            { "Spell Card: Step Slash", "StepSlash" },
            { "Spell Card: Blade Bay", "BladeBayToss" },
            { "Spell Card: Blade Rain", "BladeRain" },
            { "Spell Card: Last Letter", "LastLetter" },
            { "Spell Card: Collect Ring", "CollectRing" },
            { "Spell Card: Vivisection", "Vivisection" },
            { "Spell Card: Blackout", "Blackout" },
            { "Spell Card: Ragnarok", "Ragnarok" },
            { "Spell Card: Guillotine", "Guillotine" },
            { "Spell Card: Lifesword", "LifeSword" },
            { "Spell Card: Excalibur", "Excalibur" },
            { "Spell Card: Fury", "StepFury" },
            { "Spell Card: Scavenge", "Scavenge" },
            { "Spell Card: Warpath", "Warpath" },
            { "Spell Card: Bladeskrieg", "Bladeskrieg" },
        };

        
        private static readonly List<string> Anima = new List<string>()
        {
            "Spell Card: Meltdown",
            "Spell Card: Firewall",
            "Spell Card: Thunder",
            "Spell Card: Cold Medicine",
            "Spell Card: Pekay Fire",
            "Spell Card: Frost Barrage",
            "Spell Card: Frost Bolt",
            "Spell Card: Mini Thunder",
            "Spell Card: Thunderstorm",
            "Spell Card: Ice Spikes",
            "Spell Card: Cold Snap",
            "Spell Card: Back Burner",
            "Spell Card: Twinferno",
            "Spell Card: Combust",
            "Spell Card: Frostbite",
            "Spell Card: Ember",
            "Spell Card: Cryokinesis",
            "Spell Card: Salamander",
            "Spell Card: Ring of Fire",
            "Spell Card: Brushfire",
            "Spell Card: Wildfire",
            "Spell Card: Hailstorm",
            "Spell Card: Ice Hockey",
            "Spell Card: Rage",
            "Spell Card: Pyroblast",
            "Spell Card: Carpet Bomb",
            "Spell Card: Ice Needle",
            "Spell Card: Firestorm",
            "Spell Card: Sunder",
            "Spell Card: Blizzard",
            "Spell Card: Flamberge",
            "Spell Card: Fimbulveter",
            "Spell Card: EXPLOSION!",
        };

        private static readonly List<string> Convergence = new List<string>()
        {
            "Spell Card: Mana Potion",
            "Spell Card: Mana Steal",
            "Spell Card: Boomerang",
            "Spell Card: Hired Gun",
            "Spell Card: Tri Shot",
            "Spell Card: Meditate",
            "Spell Card: Focus",
            "Spell Card: Tri Rag",
            "Spell Card: Whirl",
            "Spell Card: Sequencer",
            "Spell Card: Incline",
            "Spell Card: Amalgam",
            "Spell Card: Mana Fusion",
            "Spell Card: Sapphire Ring",
            "Spell Card: Ping",
            "Spell Card: Limit Break",
            "Spell Card: Innervate",
            "Spell Card: Empower Ring",
            "Spell Card: Charge Ring",
            "Spell Card: Tri Force",
            "Spell Card: Sera Cannon",
            "Spell Card: Overload",
            "Spell Card: Power Saws",
        };

        private static readonly List<string> Doublelift = new List<string>()
        {
            "Spell Card: Ramjet",
            "Spell Card: Sleight",
            "Spell Card: Skewer",
            "Spell Card: Ambient Burst",
            "Spell Card: Jam Slam",
            "Spell Card: Deck Slam",
            "Spell Card: Cataclysm",
            "Spell Card: Railgun",
            "Spell Card: Jam Cannon",
            "Spell Card: Time Slow",
            "Spell Card: Switchbait",
            "Spell Card: Viruspell",
            "Spell Card: Fling",
            "Spell Card: Echo",
            "Spell Card: Gambit",
            "Spell Card: Unleash",
            "Spell Card: Sustain",
            "Spell Card: Time Stop",
            "Spell Card: Preload",
            "Spell Card: Trisect",
            "Spell Card: Chrono Sphere",
            "Spell Card: Wonder",
            "Spell Card: Salvo",
            "Spell Card: Midnight",
        };

        private static readonly List<string> Glimmer = new List<string>()
        {
            "Spell Card: Crossfire",
            "Spell Card: Shattersaw",
            "Spell Card: ZigZag",
            "Spell Card: Paragon",
            "Spell Card: Soul Fire",
            "Spell Card: Breakout",
            "Spell Card: Diag Beam",
            "Spell Card: Energizer",
            "Spell Card: Zenith",
            "Spell Card: Glaive",
            "Spell Card: Shine",
            "Spell Card: Circuit",
            "Spell Card: Solar Charge",
            "Spell Card: Eternity Cannon",
            "Spell Card: Infinity Beam",
            "Spell Card: Spirit Sword",
            "Spell Card: Glitterbomb",
            "Spell Card: Glitterstorm",
            "Spell Card: Sunshine",
            "Spell Card: Sawstorm",
        };

        private static readonly List<string> Hearth = new List<string>()
        {
            "Spell Card: Sword Row",
            "Spell Card: Earthen Armor",
            "Spell Card: Rock Cycle",
            "Spell Card: Ion Cannon",
            "Spell Card: Tile Fire",
            "Spell Card: Tremor",
            "Spell Card: Coldstone",
            "Spell Card: Fracture",
            "Spell Card: Orbital Beam",
            "Spell Card: Entangle",
            "Spell Card: Barrier",
            "Spell Card: Wreath",
            "Spell Card: Missiletoe",
            "Spell Card: Earth Prayer",
            "Spell Card: Flat Earth",
            "Spell Card: Fissure",
            "Spell Card: Flint Shot",
            "Spell Card: Earth Wyrm",
            "Spell Card: Excavate",
            "Spell Card: Waterfall",
            "Spell Card: Prophecy",
            "Spell Card: Swords of Light",
            "Spell Card: Rock Tomb",
            "Spell Card: Jackhammer",
            "Spell Card: Ambush",
            "Spell Card: Weed Wacker",
            "Spell Card: Holy Ground",
        };

        private static readonly List<string> Hexawan = new List<string>()
        {
            "Spell Card: Minefield",
            "Spell Card: Shotgun",
            "Spell Card: Minnie Gun",
            "Spell Card: Volley",
            "Spell Card: Shard Toss",
            "Spell Card: Gun Turret",
            "Spell Card: Sunbeamer",
            "Spell Card: Bombard",
            "Spell Card: Shieldgen",
            "Spell Card: Bomb Toss",
            "Spell Card: Blue Bullets",
            "Spell Card: Mana Lattice",
            "Spell Card: Beacon",
            "Spell Card: Sweeper",
            "Spell Card: Wall",
            "Spell Card: Salvage",
            "Spell Card: Beam Crystals",
            "Spell Card: Blast Crystals",
            "Spell Card: Turret SD",
            "Spell Card: Mine",
            "Spell Card: Laser Turret",
            "Spell Card: Resonate",
            "Spell Card: Cross Turret",
            "Spell Card: Sidewinder",
            "Spell Card: Monument",
            "Spell Card: Castle",
            "Spell Card: Super Minnie Gun",
            "Spell Card: Grail",
            "Spell Card: Silo",
        };

        private static readonly List<string> Kinesys = new List<string>()
        {
            "Spell Card: Undertow",
            "Spell Card: River",
            "Spell Card: Kinetic Wave",
            "Spell Card: Air Slash",
            "Spell Card: Upwind",
            "Spell Card: Align",
            "Spell Card: Tractor Beam",
            "Spell Card: Flurry",
            "Spell Card: Pull",
            "Spell Card: Spike Strip",
            "Spell Card: Caltrops",
            "Spell Card: Fadeaway",
            "Spell Card: Knock Down",
            "Spell Card: Wobble",
            "Spell Card: Push Up",
            "Spell Card: Claw Traps",
            "Spell Card: Warp Rays",
            "Spell Card: Blink",
            "Spell Card: Bow Snipe",
            "Spell Card: Skipper",
            "Spell Card: Northwind",
            "Spell Card: Tsunami",
            "Spell Card: Inverter",
            "Spell Card: Hyper Beam",
            "Spell Card: Monsoon",
        };

        private static readonly List<string> Miseri = new List<string>()
        {
            "Spell Card: Glassify",
            "Spell Card: Anubis",
            "Spell Card: Revenge",
            "Spell Card: Poison Dart",
            "Spell Card: Booster Shot",
            "Spell Card: Poison Tails",
            "Spell Card: Soul Link",
            "Spell Card: Pinch",
            "Spell Card: Twoxin",
            "Spell Card: Detox",
            "Spell Card: Showdown",
            "Spell Card: Rest",
            "Spell Card: Leech",
            "Spell Card: Sludge Bomb",
            "Spell Card: Backstab",
            "Spell Card: Transfuse",
            "Spell Card: Acid Rain",
            "Spell Card: Smog",
            "Spell Card: Pandemic",
            "Spell Card: Corset",
            "Spell Card: Devour",
            "Spell Card: Health Potion",
            "Spell Card: Venoshock",
            "Spell Card: Double Tap",
            "Spell Card: Hellfire",
            "Spell Card: Blood Shield",
            "Spell Card: Cynet",
        };

        private static readonly List<string> Phalanx = new List<string>()
        {
            "Spell Card: Absorb",
            "Spell Card: Jam Shield",
            "Spell Card: Haven",
            "Spell Card: Deck Shield",
            "Spell Card: Shield Catch",
            "Spell Card: Shields Up",
            "Spell Card: Fate Shield",
            "Spell Card: Counter Strike",
            "Spell Card: Shield Throw",
            "Spell Card: Reflector",
            "Spell Card: Shield Beam",
            "Spell Card: Entrench",
            "Spell Card: Miss-Me Shield",
            "Spell Card: Steel Skin",
            "Spell Card: Iron Will",
            "Spell Card: Forte",
            "Spell Card: Diamond Ring",
            "Spell Card: Spell Shield",
            "Spell Card: Blessing of Susano'o",
            "Spell Card: Ice Shield",
            "Spell Card: Stasis",
            "Spell Card: Downfall",
            "Spell Card: Stinger",
        };

        private static readonly List<string> Slashfik = new List<string>()
        {
            "Spell Card: Kunai",
            "Spell Card: Slice",
            "Spell Card: Doubletake",
            "Spell Card: Magic Claw",
            "Spell Card: Swipe",
            "Spell Card: Shadow Shift",
            "Spell Card: Step Pierce",
            "Spell Card: Knife",
            "Spell Card: Bouncing Blade",
            "Spell Card: Step Slash",
            "Spell Card: Blade Bay",
            "Spell Card: Blade Rain",
            "Spell Card: Last Letter",
            "Spell Card: Collect Ring",
            "Spell Card: Vivisection",
            "Spell Card: Blackout",
            "Spell Card: Ragnarok",
            "Spell Card: Guillotine",
            "Spell Card: Lifesword",
            "Spell Card: Excalibur",
            "Spell Card: Fury",
            "Spell Card: Scavenge",
            "Spell Card: Warpath",
            "Spell Card: Bladeskrieg",
        };

        public static readonly Dictionary<string, List<string>> APBrandtoAPSpells = new Dictionary<string, List<string>>()
        {
            { "Brand Unlock: Anima", Anima },
            { "Brand Unlock: Convergence", Convergence },
            { "Brand Unlock: Doublelift", Doublelift },
            { "Brand Unlock: Glimmer", Glimmer },
            { "Brand Unlock: Hearth", Hearth },
            { "Brand Unlock: Hexawan", Hexawan },
            { "Brand Unlock: Kinesys", Kinesys },
            { "Brand Unlock: Miseri", Miseri },
            { "Brand Unlock: Phalanx", Phalanx },
            { "Brand Unlock: Slashfik", Slashfik },
        };


        private static readonly List<string> AoE = new List<string>()
        {
            "Spell Card: EXPLOSION!",
            "Spell Card: Pyroblast",
            "Spell Card: Shotgun",
            "Spell Card: Twinferno",
        };
        
        private static readonly List<string> Beam = new List<string>()
        {
            "Spell Card: Hyper Beam",
            "Spell Card: Infinity Beam",
            "Spell Card: Shield Beam",
            "Spell Card: Tractor Beam",
        };
        
        private static readonly List<string> Blade = new List<string>()
        {
            "Spell Card: Bouncing Blade",
            "Spell Card: Blackout",
            "Spell Card: Blade Rain",
            "Spell Card: Bladeskrieg",
            "Spell Card: Excalibur",
            "Spell Card: Kunai",
            "Spell Card: Lifesword",
            "Spell Card: Spirit Sword",
            "Spell Card: Vivisection",
        };
        
        private static readonly List<string> Chrono = new List<string>()
        {
            "Spell Card: Time Slow",
            "Spell Card: Time Stop",
            "Spell Card: Chrono Sphere",
        };
        
        private static readonly List<string> Crack = new List<string>()
        {
            "Spell Card: Ambush",
            "Spell Card: Cataclysm",
            "Spell Card: Earth Wyrm",
            "Spell Card: Excavate",
            "Spell Card: Fissure",
            "Spell Card: Holy Ground",
            "Spell Card: Ion Cannon",
            "Spell Card: Jackhammer",
            "Spell Card: Orbital Beam",
            "Spell Card: Prophecy",
            "Spell Card: Ramjet",
            "Spell Card: Tile Fire",
        };
        
        private static readonly List<string> BigDeck = new List<string>()
        {
            "Spell Card: Deck Slam",
            "Spell Card: Deck Shield",
            "Spell Card: Gambit",
            "Spell Card: Midnight",
        };
        
        private static readonly List<string> Bouncy = new List<string>()
        {
            "Spell Card: Diag Beam",
            "Spell Card: Ice Hockey",
            "Spell Card: ZigZag",
        };
        
        private static readonly List<string> Flame = new List<string>()
        {
            "Spell Card: Back Burner",
            "Spell Card: Brushfire",
            "Spell Card: Carpet Bomb",
            "Spell Card: Combust",
            "Spell Card: Ember",
            "Spell Card: Firestorm",
            "Spell Card: Firewall",
            "Spell Card: Flamberge",
            "Spell Card: Pekay Fire",
            "Spell Card: Rage",
            "Spell Card: Ring of Fire",
            "Spell Card: Salamander",
            "Spell Card: Wildfire",
        };
        
        private static readonly List<string> Fling = new List<string>()
        {
            "Spell Card: Echo",
            "Spell Card: Fling",
            "Spell Card: Spell Shield",
            "Spell Card: Switchbait",
        };
        
        private static readonly List<string> Flow = new List<string>()
        {
            "Spell Card: Barrier",
            "Spell Card: Coldstone",
            "Spell Card: Earth Prayer",
            "Spell Card: Earthen Armor",
            "Spell Card: Eternity Cannon",
            "Spell Card: Flint Shot",
            "Spell Card: Fracture",
            "Spell Card: Kinetic Wave",
            "Spell Card: River",
            "Spell Card: Rock Cycle",
            "Spell Card: Rock Tomb",
            "Spell Card: Tremor",
            "Spell Card: Tsunami",
            "Spell Card: Undertow",
            "Spell Card: Waterfall",
        };
        
        private static readonly List<string> Fragile = new List<string>()
        {
            "Spell Card: Entrench",
            "Spell Card: Glassify",
            "Spell Card: Ice Shield",
            "Spell Card: Knife",
            "Spell Card: Limit Break",
            "Spell Card: Magic Claw",
            "Spell Card: Showdown",
        };
        
        private static readonly List<string> Frost = new List<string>()
        {
            "Spell Card: Blizzard",
            "Spell Card: Cold Medicine",
            "Spell Card: Cold Snap",
            "Spell Card: Cryokinesis",
            "Spell Card: Fimbulveter",
            "Spell Card: Frost Barrage",
            "Spell Card: Frostbite",
            "Spell Card: Frost Bolt",
            "Spell Card: Hailstorm",
            "Spell Card: Ice Needle",
            "Spell Card: Ice Spikes",
            "Spell Card: Meltdown",
            "Spell Card: Sunder",
        };
        
        private static readonly List<string> Health = new List<string>()
        {
            "Spell Card: Cynet",
            "Spell Card: Devour",
            "Spell Card: Health Potion",
            "Spell Card: Leech",
            "Spell Card: Pinch",
            "Spell Card: Rest",
            "Spell Card: Revenge",
            "Spell Card: Soul Link",
        };
        
        private static readonly List<string> Indirect = new List<string>()
        {
            "Spell Card: Glitterbomb",
            "Spell Card: Glitterstorm",
            "Spell Card: Guillotine",
            "Spell Card: Hellfire",
            "Spell Card: Last Letter",
            "Spell Card: Mini Thunder",
            "Spell Card: Paragon",
            "Spell Card: Ragnarok",
            "Spell Card: Shine",
            "Spell Card: Thunder",
            "Spell Card: Thunderstorm",
            "Spell Card: Whirl",
        };
        
        private static readonly List<string> Invade = new List<string>()
        {
            "Spell Card: Breakout",
            "Spell Card: Circuit",
            "Spell Card: Crossfire",
            "Spell Card: Fury",
            "Spell Card: Step Pierce",
            "Spell Card: Step Slash",
            "Spell Card: Warpath",
            "Spell Card: Zenith",
        };
        
        private static readonly List<string> Jam = new List<string>()
        {
            "Spell Card: Innervate",
            "Spell Card: Jam Cannon",
            "Spell Card: Jam Shield",
            "Spell Card: Jam Slam",
            "Spell Card: Railgun",
            "Spell Card: Salvo",
            "Spell Card: Skewer",
        };
        
        private static readonly List<string> Kunai = new List<string>()
        {
            "Spell Card: Counter Strike",
            "Spell Card: Doubletake",
            "Spell Card: Kunai",
            "Spell Card: Scavenge",
            "Spell Card: Shadow Shift",
            "Spell Card: Swipe",
        };
        
        private static readonly List<string> Mana = new List<string>()
        {
            "Spell Card: Blue Bullets",
            "Spell Card: Boomerang",
            "Spell Card: Focus",
            "Spell Card: Mana Fusion",
            "Spell Card: Mana Potion",
            "Spell Card: Overload",
            "Spell Card: Transfuse",
            "Spell Card: Unleash",
        };
        
        private static readonly List<string> Mine = new List<string>()
        {
            "Spell Card: Bomb Toss",
            "Spell Card: Caltrops",
            "Spell Card: Mine",
            "Spell Card: Minefield",
            "Spell Card: Spike Strip",
            "Spell Card: Sweeper",
        };
        
        private static readonly List<string> Poison = new List<string>()
        {
            "Spell Card: Acid Rain",
            "Spell Card: Anubis",
            "Spell Card: Backstab",
            "Spell Card: Blood Shield",
            "Spell Card: Booster Shot",
            "Spell Card: Detox",
            "Spell Card: Pandemic",
            "Spell Card: Poison Dart",
            "Spell Card: Poison Tails",
            "Spell Card: Slice",
            "Spell Card: Sludge Bomb",
            "Spell Card: Smog",
            "Spell Card: Twoxin",
            "Spell Card: Venoshock",
        };
        
        private static readonly List<string> Projectile = new List<string>()
        {
            "Spell Card: Amalgam",
            "Spell Card: Bow Snipe",
            "Spell Card: Double Tap",
            "Spell Card: Hired Gun",
            "Spell Card: Incline",
            "Spell Card: Ping",
            "Spell Card: Skipper",
            "Spell Card: Super Minnie Gun",
        };
        
        private static readonly List<string> Relocation = new List<string>()
        {
            "Spell Card: Air Slash",
            "Spell Card: Align",
            "Spell Card: Fadeaway",
            "Spell Card: Flurry",
            "Spell Card: Inverter",
            "Spell Card: Knock Down",
            "Spell Card: Monsoon",
            "Spell Card: Northwind",
            "Spell Card: Pull",
            "Spell Card: Push Up",
            "Spell Card: Upwind",
            "Spell Card: Warp Rays",
            "Spell Card: Wobble",
        };
        
        private static readonly List<string> Ring = new List<string>()
        {
            "Spell Card: Charge Ring",
            "Spell Card: Collect Ring",
            "Spell Card: Diamond Ring",
            "Spell Card: Empower Ring",
            "Spell Card: Sapphire Ring",
        };
        
        private static readonly List<string> Root = new List<string>()
        {
            "Spell Card: Claw Traps",
            "Spell Card: Entangle",
            "Spell Card: Missiletoe",
            "Spell Card: Sword Row",
            "Spell Card: Swords of Light",
            "Spell Card: Weed Wacker",
            "Spell Card: Wreath",
        };
        
        private static readonly List<string> Sawblade = new List<string>()
        {
            "Spell Card: Glaive",
            "Spell Card: Power Saws",
            "Spell Card: Sawstorm",
            "Spell Card: Shattersaw",
        };
        
        private static readonly List<string> Scaling = new List<string>()
        {
            "Spell Card: Ambient Burst",
            "Spell Card: Energizer",
            "Spell Card: Sera Cannon",
            "Spell Card: Soul Fire",
            "Spell Card: Viruspell",
        };
        
        private static readonly List<string> Shield = new List<string>()
        {
            "Spell Card: Absorb",
            "Spell Card: Blessing of Susano'o",
            "Spell Card: Blink",
            "Spell Card: Corset",
            "Spell Card: Downfall",
            "Spell Card: Fate Shield",
            "Spell Card: Forte",
            "Spell Card: Haven",
            "Spell Card: Iron Will",
            "Spell Card: Miss-Me Shield",
            "Spell Card: Reflector",
            "Spell Card: Shield Catch",
            "Spell Card: Shields Up",
            "Spell Card: Shield Throw",
            "Spell Card: Stasis",
            "Spell Card: Steel Skin",
            "Spell Card: Stinger",
        };
        
        private static readonly List<string> Shuffle = new List<string>()
        {
            "Spell Card: Blade Bay",
            "Spell Card: Preload",
            "Spell Card: Sleight",
        };
        
        private static readonly List<string> Solar = new List<string>()
        {
            "Spell Card: Solar Charge",
            "Spell Card: Sunshine",
            "Spell Card: Sunbeamer",
        };
        
        private static readonly List<string> Summon = new List<string>()
        {
            "Spell Card: Beacon",
            "Spell Card: Beam Crystals",
            "Spell Card: Blast Crystals",
            "Spell Card: Bombard",
            "Spell Card: Castle",
            "Spell Card: Cross Turret",
            "Spell Card: Grail",
            "Spell Card: Gun Turret",
            "Spell Card: Laser Turret",
            "Spell Card: Mana Lattice",
            "Spell Card: Monument",
            "Spell Card: Resonate",
            "Spell Card: Salvage",
            "Spell Card: Shard Toss",
            "Spell Card: Shieldgen",
            "Spell Card: Sidewinder",
            "Spell Card: Silo",
            "Spell Card: Turret SD",
            "Spell Card: Volley",
            "Spell Card: Wall",
        };
        
        private static readonly List<string> Trinity = new List<string>()
        {
            "Spell Card: Mana Steal",
            "Spell Card: Meditate",
            "Spell Card: Minnie Gun",
            "Spell Card: Sequencer",
            "Spell Card: Sustain",
            "Spell Card: Tri Force",
            "Spell Card: Tri Rag",
            "Spell Card: Tri Shot",
            "Spell Card: Trisect",
        };
        
        private static readonly List<string> Wonder = new List<string>()
        {
            "Spell Card: Wonder",
        };
        
        public static readonly Dictionary<string, List<string>> APPacktoAPSpells = new Dictionary<string, List<string>>()
        {
            { "Spell Booster Pack: AoE", AoE },
            { "Spell Booster Pack: Beam", Beam },
            { "Spell Booster Pack: Blade", Blade },
            { "Spell Booster Pack: Chrono", Chrono },
            { "Spell Booster Pack: Crack", Crack },
            { "Spell Booster Pack: BigDeck", BigDeck },
            { "Spell Booster Pack: Bouncy", Bouncy },
            { "Spell Booster Pack: Flame", Flame },
            { "Spell Booster Pack: Fling", Fling },
            { "Spell Booster Pack: Flow", Flow },
            { "Spell Booster Pack: Fragile", Fragile },
            { "Spell Booster Pack: Frost", Frost },
            { "Spell Booster Pack: Health", Health },
            { "Spell Booster Pack: Indirect", Indirect },
            { "Spell Booster Pack: Invade", Invade },
            { "Spell Booster Pack: Jam", Jam },
            { "Spell Booster Pack: Kunai", Kunai },
            { "Spell Booster Pack: Mana", Mana },
            { "Spell Booster Pack: Mine", Mine },
            { "Spell Booster Pack: Poison", Poison },
            { "Spell Booster Pack: Projectile", Projectile },
            { "Spell Booster Pack: Relocation", Relocation },
            { "Spell Booster Pack: Ring", Ring },
            { "Spell Booster Pack: Root", Root },
            { "Spell Booster Pack: Sawblade", Sawblade },
            { "Spell Booster Pack: Scaling", Scaling },
            { "Spell Booster Pack: Shield", Shield },
            { "Spell Booster Pack: Shuffle", Shuffle },
            { "Spell Booster Pack: Solar", Solar },
            { "Spell Booster Pack: Summon", Summon },
            { "Spell Booster Pack: Trinity", Trinity },
            { "Spell Booster Pack: Wonder", Wonder },
        };
    }
}