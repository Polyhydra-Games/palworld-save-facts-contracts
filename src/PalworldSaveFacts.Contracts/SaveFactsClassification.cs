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
    [property: JsonPropertyName("domainCounts"), FactClassification(FactSensitivity.Operator)] IReadOnlyDictionary<string, int> DomainCounts);

public sealed record SaveFactsDecodeManifestV1(
    [property: JsonPropertyName("schemaVersion"), FactClassification(FactSensitivity.Public)] string SchemaVersion,
    [property: JsonPropertyName("snapshotId"), FactClassification(FactSensitivity.Restricted)] string SnapshotId,
    [property: JsonPropertyName("sourceDigest"), FactClassification(FactSensitivity.Restricted)] string SourceDigest,
    [property: JsonPropertyName("observedAt"), FactClassification(FactSensitivity.Operator)] DateTimeOffset ObservedAt,
    [property: JsonPropertyName("provenance"), FactClassification(FactSensitivity.Operator)] SaveFactsDecoderProvenanceV1 Provenance,
    [property: JsonPropertyName("completeness"), FactClassification(FactSensitivity.Operator)] SaveFactsCompletenessState Completeness,
    [property: JsonPropertyName("warnings"), FactClassification(FactSensitivity.Operator)] IReadOnlyList<SaveFactsWarningV1> Warnings,
    [property: JsonPropertyName("rawArtifact"), FactClassification(FactSensitivity.Operator)] SaveFactsArtifactManifestV1 RawArtifact);
