using System.Text.Json.Serialization;

namespace PolyhydraGames.PalworldSaveFacts.Contracts;

public enum FactSensitivity
{
    Public,
    Operator,
    Restricted,
    RawOnly
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class FactClassificationAttribute(FactSensitivity sensitivity) : Attribute
{
    public FactSensitivity Sensitivity { get; } = sensitivity;
}

public enum SaveFactsCompletenessState
{
    Complete,
    Partial,
    Incomplete,
    Unknown
}

public enum SaveFactsSourceFieldState
{
    Present,
    Absent,
    Unknown
}

public sealed record SaveFactsStringFieldV1(
    [property: JsonPropertyName("state"), FactClassification(FactSensitivity.Operator)] SaveFactsSourceFieldState State,
    [property: JsonPropertyName("value"), FactClassification(FactSensitivity.Restricted)] string? Value);

public sealed record SaveFactsIntegerFieldV1(
    [property: JsonPropertyName("state"), FactClassification(FactSensitivity.Operator)] SaveFactsSourceFieldState State,
    [property: JsonPropertyName("value"), FactClassification(FactSensitivity.Operator)] long? Value);

public sealed record SaveFactsTimestampFieldV1(
    [property: JsonPropertyName("state"), FactClassification(FactSensitivity.Operator)] SaveFactsSourceFieldState State,
    [property: JsonPropertyName("value"), FactClassification(FactSensitivity.Restricted)] DateTimeOffset? Value);

public sealed record SaveFactsStringListFieldV1(
    [property: JsonPropertyName("state"), FactClassification(FactSensitivity.Operator)] SaveFactsSourceFieldState State,
    [property: JsonPropertyName("values"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<string> Values);

public sealed record SaveFactsSnapshotReferenceV1(
    [property: JsonPropertyName("snapshotLocalId"), FactClassification(FactSensitivity.Operator)] string SnapshotLocalId);

public sealed record SaveFactsPositionV1(
    [property: JsonPropertyName("x"), FactClassification(FactSensitivity.Restricted)] double X,
    [property: JsonPropertyName("y"), FactClassification(FactSensitivity.Restricted)] double Y,
    [property: JsonPropertyName("z"), FactClassification(FactSensitivity.Restricted)] double Z);

public sealed record SaveFactsPositionFieldV1(
    [property: JsonPropertyName("state"), FactClassification(FactSensitivity.Operator)] SaveFactsSourceFieldState State,
    [property: JsonPropertyName("value"), FactClassification(FactSensitivity.Restricted)] SaveFactsPositionV1? Value);

public sealed record SaveFactsPlayerV2(
    [property: JsonPropertyName("snapshotLocalId"), FactClassification(FactSensitivity.Operator)] string SnapshotLocalId,
    [property: JsonPropertyName("nativeId"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 NativeId,
    [property: JsonPropertyName("displayName"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 DisplayName,
    [property: JsonPropertyName("guild"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 Guild,
    [property: JsonPropertyName("level"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Level,
    [property: JsonPropertyName("experience"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Experience,
    [property: JsonPropertyName("points"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Points,
    [property: JsonPropertyName("technology"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 Technology,
    [property: JsonPropertyName("recipes"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 Recipes,
    [property: JsonPropertyName("quests"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 Quests,
    [property: JsonPropertyName("lastOnline"), FactClassification(FactSensitivity.Restricted)] SaveFactsTimestampFieldV1 LastOnline,
    [property: JsonPropertyName("inventoryReferences"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsSnapshotReferenceV1> InventoryReferences,
    [property: JsonPropertyName("equipmentReferences"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsSnapshotReferenceV1> EquipmentReferences,
    [property: JsonPropertyName("position"), FactClassification(FactSensitivity.Restricted)] SaveFactsPositionFieldV1 Position,
    [property: JsonPropertyName("state"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 State);

public sealed record SaveFactsPalVitalsV1(
    [property: JsonPropertyName("health"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Health,
    [property: JsonPropertyName("sanity"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Sanity,
    [property: JsonPropertyName("hunger"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Hunger,
    [property: JsonPropertyName("friendship"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Friendship);

public sealed record SaveFactsPalV2(
    [property: JsonPropertyName("snapshotLocalId"), FactClassification(FactSensitivity.Operator)] string SnapshotLocalId,
    [property: JsonPropertyName("nativeId"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 NativeId,
    [property: JsonPropertyName("species"), FactClassification(FactSensitivity.Operator)] SaveFactsStringFieldV1 Species,
    [property: JsonPropertyName("nickname"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 Nickname,
    [property: JsonPropertyName("owner"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 Owner,
    [property: JsonPropertyName("ownershipObservedAt"), FactClassification(FactSensitivity.Restricted)] SaveFactsTimestampFieldV1 OwnershipObservedAt,
    [property: JsonPropertyName("firstObservedAt"), FactClassification(FactSensitivity.Operator)] SaveFactsTimestampFieldV1 FirstObservedAt,
    [property: JsonPropertyName("level"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Level,
    [property: JsonPropertyName("experience"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Experience,
    [property: JsonPropertyName("rank"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Rank,
    [property: JsonPropertyName("gender"), FactClassification(FactSensitivity.Operator)] SaveFactsStringFieldV1 Gender,
    [property: JsonPropertyName("traits"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 Traits,
    [property: JsonPropertyName("ivStats"), FactClassification(FactSensitivity.Operator)] IReadOnlyDictionary<string, SaveFactsIntegerFieldV1> IvStats,
    [property: JsonPropertyName("souls"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Souls,
    [property: JsonPropertyName("passiveSkills"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 PassiveSkills,
    [property: JsonPropertyName("activeSkills"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 ActiveSkills,
    [property: JsonPropertyName("vitals"), FactClassification(FactSensitivity.Operator)] SaveFactsPalVitalsV1 Vitals,
    [property: JsonPropertyName("workSuitability"), FactClassification(FactSensitivity.Operator)] SaveFactsStringListFieldV1 WorkSuitability,
    [property: JsonPropertyName("container"), FactClassification(FactSensitivity.Operator)] SaveFactsStringFieldV1 Container,
    [property: JsonPropertyName("slot"), FactClassification(FactSensitivity.Operator)] SaveFactsIntegerFieldV1 Slot,
    [property: JsonPropertyName("party"), FactClassification(FactSensitivity.Operator)] SaveFactsStringFieldV1 Party,
    [property: JsonPropertyName("palbox"), FactClassification(FactSensitivity.Operator)] SaveFactsStringFieldV1 Palbox,
    [property: JsonPropertyName("base"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 Base,
    [property: JsonPropertyName("guild"), FactClassification(FactSensitivity.Restricted)] SaveFactsStringFieldV1 Guild);

public sealed record SaveFactsWarningV1(
    [property: JsonPropertyName("code"), FactClassification(FactSensitivity.Operator)] string Code,
    [property: JsonPropertyName("message"), FactClassification(FactSensitivity.Operator)] string Message);

public sealed record SaveFactsDecoderProvenanceV1(
    [property: JsonPropertyName("parserVersion"), FactClassification(FactSensitivity.Operator)] string ParserVersion,
    [property: JsonPropertyName("decoderVersion"), FactClassification(FactSensitivity.Operator)] string DecoderVersion,
    [property: JsonPropertyName("gameVersion"), FactClassification(FactSensitivity.Operator)] string? GameVersion);

public sealed record SaveFactsArtifactManifestV1(
    [property: JsonPropertyName("path"), FactClassification(FactSensitivity.Operator)] string Path,
    [property: JsonPropertyName("byteCount"), FactClassification(FactSensitivity.Operator)] long ByteCount,
    [property: JsonPropertyName("sha256"), FactClassification(FactSensitivity.Restricted)] string Sha256,
    [property: JsonPropertyName("compression"), FactClassification(FactSensitivity.Operator)] string Compression,
    [property: JsonPropertyName("contentType"), FactClassification(FactSensitivity.Operator)] string ContentType);

public sealed record SaveFactsDocumentV2(
    [property: JsonPropertyName("schemaVersion"), FactClassification(FactSensitivity.Public)] string SchemaVersion,
    [property: JsonPropertyName("snapshotId"), FactClassification(FactSensitivity.Restricted)] string SnapshotId,
    [property: JsonPropertyName("sourceDigest"), FactClassification(FactSensitivity.Restricted)] string SourceDigest,
    [property: JsonPropertyName("observedAt"), FactClassification(FactSensitivity.Operator)] DateTimeOffset ObservedAt,
    [property: JsonPropertyName("provenance"), FactClassification(FactSensitivity.Operator)] SaveFactsDecoderProvenanceV1 Provenance,
    [property: JsonPropertyName("completeness"), FactClassification(FactSensitivity.Operator)] SaveFactsCompletenessState Completeness,
    [property: JsonPropertyName("warnings"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsWarningV1> Warnings,
    [property: JsonPropertyName("domainCounts"), FactClassification(FactSensitivity.Operator)] IReadOnlyDictionary<string, int> DomainCounts,
    [property: JsonPropertyName("players"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsPlayerV2> Players,
    [property: JsonPropertyName("pals"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsPalV2> Pals);

public sealed record SaveFactsDecodeManifestV1(
    [property: JsonPropertyName("schemaVersion"), FactClassification(FactSensitivity.Public)] string SchemaVersion,
    [property: JsonPropertyName("snapshotId"), FactClassification(FactSensitivity.Restricted)] string SnapshotId,
    [property: JsonPropertyName("sourceDigest"), FactClassification(FactSensitivity.Restricted)] string SourceDigest,
    [property: JsonPropertyName("observedAt"), FactClassification(FactSensitivity.Operator)] DateTimeOffset ObservedAt,
    [property: JsonPropertyName("provenance"), FactClassification(FactSensitivity.Operator)] SaveFactsDecoderProvenanceV1 Provenance,
    [property: JsonPropertyName("completeness"), FactClassification(FactSensitivity.Operator)] SaveFactsCompletenessState Completeness,
    [property: JsonPropertyName("warnings"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsWarningV1> Warnings,
    [property: JsonPropertyName("rawArtifact"), FactClassification(FactSensitivity.Operator)] SaveFactsArtifactManifestV1 RawArtifact);
