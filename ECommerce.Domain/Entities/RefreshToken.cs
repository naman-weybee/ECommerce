using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class RefreshToken : Base
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime ExpiredDate { get; set; }

        public DateTime? RevokedDate { get; set; }

        public virtual User User { get; set; }

        public void CreateRefreshToken(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = Guid.NewGuid().ToString();
            ExpiredDate = DateTime.UtcNow.AddDays(7);
        }

        public void UpdateRefreshToken(Guid id, Guid userId, string token, bool isRevoked, DateTime expiredDate)
        {
            Id = id;
            UserId = userId;
            Token = token;
            IsRevoked = isRevoked;
            ExpiredDate = expiredDate;

            StatusUpdated();
        }

        public void RevokeToken()
        {
            IsRevoked = true;
            RevokedDate = DateTime.UtcNow;

            StatusUpdated();
        }
    }
}