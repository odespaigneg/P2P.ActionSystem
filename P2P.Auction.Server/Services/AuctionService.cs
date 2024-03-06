using Grpc.Core;
using P2P.Auction.Server.Model;
using P2P.Auction.Server.Persistence;

namespace P2P.Auction.Server.Services
{
    public class AuctionService : Auction.AuctionBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly AuctionDbContext _context;

        public AuctionService(ILogger<GreeterService> logger, AuctionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<ItemReply> CreateAuction(ItemRequest request, ServerCallContext context)
        {
            Item item = new()
            {
                BasePrice = request.BasePrice,
                Name = request.Name
            };
            _context.Add(item);
            _context.SaveChanges();

            return Task.FromResult(new ItemReply
            {
            });
        }

        public override Task<OfferReply> SendOffer(OfferRequest request, ServerCallContext context)
        {
            var item = _context.Items.FirstOrDefault(x => x.Name == request.ItemName);
            var user = _context.Users.FirstOrDefault(x => x.Name == request.UserName);

            Offer offer = new()
            {
                ItemId = item.Id,
                UserId = user.Id,
                Price = request.Price
            };

            _context.Add(offer);
            _context.SaveChanges();

            return Task.FromResult(new OfferReply
            {
            });
        }

        public override Task<UserReply> AddUser(UserRequest request, ServerCallContext context)
        {
            User user = new()
            {
                Email = request.Email,
                Name = request.Name
            };

            _context.Add(user);
            _context.SaveChanges();

            return Task.FromResult(new UserReply
            {
            });
        }
    }
}
