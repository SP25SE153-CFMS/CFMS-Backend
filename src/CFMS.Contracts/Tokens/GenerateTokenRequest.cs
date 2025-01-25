using System.Text.Json.Serialization;

namespace CFMS.Contracts.Tokens
{
    public record GenerateTokenRequest(
        [property: JsonIgnore]
        Guid? Id,
        string FirstName,
        string LastName,
        string Email,
        List<string> Roles);
}
