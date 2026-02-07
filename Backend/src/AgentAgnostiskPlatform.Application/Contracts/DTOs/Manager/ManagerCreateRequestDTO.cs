namespace AgentAgnostiskPlatform.Application.Contracts.DTOs.Manager;

/// <summary>
///     Create a new password
/// </summary>
public class ManagerCreateRequestDTO
{
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
}
