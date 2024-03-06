using P2P.Auction.Server.Model.Enums;

namespace P2P.Auction.Server.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ConditionEnum Condition { get; set; } = ConditionEnum.ForSale;
        public double BasePrice { get; set; }
    }
}
