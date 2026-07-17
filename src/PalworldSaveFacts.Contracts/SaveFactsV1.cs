using System.Text.Json.Serialization;

namespace PolyhydraGames.PalworldSaveFacts.Contracts;

public static class SaveFactsSchema
{
    public const string V1 = "palworld-save-facts/v1";
}

public sealed record SaveFactsDocumentV1(
    [property: JsonPropertyName("schemaVersion")] string SchemaVersion,
    [property: JsonPropertyName("observedAt")] DateTimeOffset ObservedAt,
    [property: JsonPropertyName("players")] IReadOnlyList<SaveFactsPlayerV1> Players,
    [property: JsonPropertyName("guildCount")] int GuildCount,
    [property: JsonPropertyName("baseCount")] int BaseCount,
    [property: JsonPropertyName("palCount")] int PalCount);

public sealed record SaveFactsPlayerV1(
    [property: JsonPropertyName("nativeId")] string NativeId,
    [property: JsonPropertyName("level")] int Level,
    [property: JsonPropertyName("recipes")] IReadOnlyList<string> Recipes,
    [property: JsonPropertyName("completedQuests")] IReadOnlyList<string> CompletedQuests,
    [property: JsonPropertyName("technologyPoints")] int TechnologyPoints,
    [property: JsonPropertyName("guildId")] string? GuildId);
