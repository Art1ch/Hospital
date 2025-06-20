namespace AuthAPI.Core.Entities;

public class RefreshTokenEntity 
{
    public int Id { get; set; }
    public string Token { get; set; }
    public Guid AccountId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public AccountEntity Account { get; set; }
}
