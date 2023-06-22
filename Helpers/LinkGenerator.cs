using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Create.Helpers
{
    public static class LinkGenerator
    {

        public static string GenerateLink()
        {
            StringBuilder link = new StringBuilder();
            // Use the current time to seed random so it's different every time we run it
            Random rand = new Random(DateTime.Now.ToString().GetHashCode());
            link.Append(australianPlaceNames[rand.Next(0, australianPlaceNames.Length)]);
            link.Append(australianSlangTerms[rand.Next(0, australianSlangTerms.Length)]);
            link.Append(australianAnimalNames[rand.Next(0, australianAnimalNames.Length)]);
            return link.ToString();
        }

        private static string[] australianPlaceNames = new string[]
{
    "Adelaide",
    "Ballarat",
    "Canberra",
    "Darwin",
    "Echuca",
    "Fremantle",
    "Geelong",
    "Hobart",
    "Ipswich",
    "Jindabyne",
    "Katoomba",
    "Launceston",
    "Mildura",
    "Newcastle",
    "Orange",
    "Perth",
    "Queensland",
    "Rockhampton",
    "Sydney",
    "Toowoomba",
    "Uluru",
    "Victoria",
    "Wollongong",
    "Xantippe",
    "Yamba",
    "Zeehan",
    "Albury",
    "Bendigo",
    "Cairns",
    "Dubbo",
    "Esperance",
    "Geraldton",
    "Hamilton",
    "Innisfail",
    "Kalgoorlie",
    "Lismore",
    "Mackay",
    "Nambour",
    "Oatlands",
    "Penrith",
    "Queanbeyan",
    "Rutherglen",
    "Shepparton",
    "Taree",
    "Ulladulla",
    "Vermont",
    "Warrnambool",
    "Yarram",
    "Zetland"
};

        private static string[] australianSlangTerms = new string[]
{
    "Arvo",
    "Barbie",
    "Bingle",
    "Bludger",
    "Chuck",
    "Crikey",
    "Dinkum",
    "Drongo",
    "Esky",
    "Gday",
    "NotPavlova",
    "Jackaroo",
    "Kip",
    "Lollies",
    "Mate",
    "Nuddy",
    "Ocker",
    "Pash",
    "Quokka",
    "Rort",
    "Sanger",
    "Thongs",
    "Ute",
    "Wombat",
    "Yabber",
    "Zak",
    "Avo",
    "Brolly",
    "Cark",
    "Dob",
    "Footy",
    "Galah",
    "Hoon",
    "Innie",
    "Jaffle",
    "Lamington",
    "Mozzie",
    "Nix",
    "Outback",
    "Prezzy",
    "Quoll",
    "Ripper",
    "Stubby",
    "Tucker",
    "Ugg",
    "Vinnies",
    "Woopwoop",
    "Yakka",
    "Bonza",
    "Cobber",
    "Dunny"
};

        private static string[] australianAnimalNames = new string[]
{
    "Kangaroo",
    "Wombat",
    "Dingo",
    "Platypus",
    "Koala",
    "Wallaby",
    "Possum",
    "Quokka",
    "Bilby",
    "Bandicoot",
    "Kookaburra",
    "Emu",
    "Cassowary",
    "Cockatoo",
    "Goanna",
    "Quoll",
    "TasmanianDevil",
    "Echidna",
    "Galah",
    "Numbat",
    "Lyrebird",
    "Pademelon",
    "Lorikeet",
    "Dugong",
    "SugarGlider",
    "SaltwaterCrocodile",
        "FreshwaterCrocodile",
    "WedgeTailedEagle",
    "FrilledLizard",
    "BlueTonguedSkink",
    "ThornyDevil",
    "SpottedQuoll",
    "Antechinus",
    "Yabby",
    "Mudcrab",
    "RedbackSpider",
    "FunnelWebSpider",
    "Stingray",
    "Bandicoot",
    "Kestrel",
    "Jabiru",
    "Cuscus",
    "Bettong",
    "ParmaWallaby",
    "BoxJellyfish",
    "Stonefish",
    "TigerSnake",
    "BrownSnake",
    "Taipan",
    "DeathAdder"
};
    }
}
