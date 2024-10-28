namespace Backend.Data_Access_Layer
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Token { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public DateTime? Revoked { get; set; }

        public string CreatedByIp { get; set; }
        public string? RevokedByIp { get; set; }

        public bool IsExpired => ExpireDateTime <= DateTime.UtcNow;

        public bool IsActive => Revoked == null && !IsExpired;
    }
}
