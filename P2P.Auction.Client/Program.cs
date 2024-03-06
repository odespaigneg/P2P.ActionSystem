using Grpc.Net.Client;
using static P2P.Action.Client.Auction;

namespace P2P.Auction.Client
{
    class Program
    {
        public static string name = GenerateName();

        public static string GenerateName(int len = 5)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        public static string GenerateRandomEmail(string name)
        {
            return string.Format("{0}@{1}.com", name, GenerateRandomAlphabetString(10));
        }
        /// <summary>
        /// Gets a string from the English alphabet at random
        /// </summary>
        public static string GenerateRandomAlphabetString(int length)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rnd = SeedRandom();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rnd.Next(allowedChars.Length)];
            }

            return new string(chars);
        }

        private static Random SeedRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }

        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7190");
            var client = new AuctionClient(channel);

            Console.WriteLine($"Current user is {name}");

            string email = GenerateRandomEmail(name);

            client.AddUser(new Action.Client.UserRequest { Name = name, Email = email });

            Console.WriteLine("=========================");
            Console.WriteLine("1. Add auction");
            Console.WriteLine("2. Offer");
            Console.WriteLine("3. Exit");
            Console.WriteLine("=========================");

            int selection = 0;
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Item name");
                        string itemName = Console.ReadLine();
                        Console.WriteLine("Item price");
                        string itemPrice = Console.ReadLine();
                        client.CreateAuction(new Action.Client.ItemRequest { Name = itemName, BasePrice = Convert.ToDouble(itemPrice) });
                        break;
                    case 2:
                        Console.WriteLine("Item name");
                        string item = Console.ReadLine();
                        Console.WriteLine("Item price");
                        string price = Console.ReadLine();
                        client.SendOffer(new Action.Client.OfferRequest { UserName = name, Price = Convert.ToDouble(price), ItemName = item });
                        break;

                }

                Console.WriteLine("Please select an action");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }
        }
    }
}
