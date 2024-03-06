namespace P2P.Auction.Server.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
    }
}
