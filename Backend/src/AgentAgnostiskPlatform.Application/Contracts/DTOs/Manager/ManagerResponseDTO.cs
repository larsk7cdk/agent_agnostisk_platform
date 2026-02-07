namespace AgentAgnostiskPlatform.Application.Contracts.DTOs.Manager;

public class ManagerResponseDTO
{
    /// <summary>
    ///     User ID
    /// </summary>
    public required string UserID { get; init; }

    /// <summary>
    /// Password name
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Username
    /// </summary>
    public required string Username { get; init; }
    /// <summary>
    /// Password
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Encrypted password
    /// </summary>
    public required string EncryptedPassword { get; init; }
}
