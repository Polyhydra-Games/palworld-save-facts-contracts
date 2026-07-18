using System.Text.Json.Serialization;

namespace PolyhydraGames.PalworldSaveFacts.Contracts;

public static class SaveFactsSchema
{
    public const string V1 = "palworld-save-facts/v1";
    public const string V2 = "palworld-save-facts/v2";
    public const string DecodeManifestV1 = "palworld-save-decode-manifest/v1";
}

public sealed record SaveFactsDocumentV1(
    [property: JsonPropertyName("schemaVersion"), FactClassification(FactSensitivity.Public)] string SchemaVersion,
    [property: JsonPropertyName("observedAt"), FactClassification(FactSensitivity.Operator)] DateTimeOffset ObservedAt,
    [property: JsonPropertyName("players"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsPlayerV1> Players,
    [property: JsonPropertyName("guildCount"), FactClassification(FactSensitivity.Operator)] int GuildCount,
    [property: JsonPropertyName("baseCount"), FactClassification(FactSensitivity.Operator)] int BaseCount,
    [property: JsonPropertyName("palCount"), FactClassification(FactSensitivity.Operator)] int PalCount);

public sealed record SaveFactsPlayerV1(
    [property: JsonPropertyName("nativeId"), FactClassification(FactSensitivity.Restricted)] string NativeId,
    [property: JsonPropertyName("level"), FactClassification(FactSensitivity.Operator)] int Level,
    [property: JsonPropertyName("recipes"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<string> Recipes,
    [property: JsonPropertyName("completedQuests"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<string> CompletedQuests,
    [property: JsonPropertyName("technologyPoints"), FactClassification(FactSensitivity.Operator)] int TechnologyPoints,
    [property: JsonPropertyName("guildId"), FactClassification(FactSensitivity.Restricted)] string? GuildId);
