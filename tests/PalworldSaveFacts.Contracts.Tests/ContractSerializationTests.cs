using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using PolyhydraGames.PalworldSaveFacts.Contracts;
using Xunit;

namespace PalworldSaveFacts.Contracts.Tests;

public sealed class ContractSerializationTests
{
    [Fact]
    public void V1_document_serializes_with_expected_shape()
    {
        var document = new SaveFactsDocumentV1(
            SchemaVersion: SaveFactsSchema.V1,
            ObservedAt: DateTimeOffset.Parse("2026-07-18T12:30:00Z"),
            Players:
            [
                new SaveFactsPlayerV1(
                    NativeId: "player-1",
                    Level: 42,
                    Recipes: ["recipe-a", "recipe-b"],
                    CompletedQuests: ["quest-1"],
                    TechnologyPoints: 7,
                    GuildId: "guild-9")
            ],
            GuildCount: 3,
            BaseCount: 2,
            PalCount: 18);

        var json = JsonSerializer.Serialize(document, SaveFactsJson.SerializerOptions);

        Assert.Equal("""
            {"schemaVersion":"palworld-save-facts/v1","observedAt":"2026-07-18T12:30:00+00:00","players":[{"nativeId":"player-1","level":42,"recipes":["recipe-a","recipe-b"],"completedQuests":["quest-1"],"technologyPoints":7,"guildId":"guild-9"}],"guildCount":3,"baseCount":2,"palCount":18}
            """, json);
    }

    [Fact]
    public void V2_snapshot_metadata_serializes_with_expected_shape()
    {
        var snapshot = new SaveFactsDocumentV2(
            SchemaVersion: SaveFactsSchema.V2,
            SnapshotId: "snapshot-20260718-001",
            SourceDigest: "sha256:abc123",
            ObservedAt: DateTimeOffset.Parse("2026-07-18T12:30:00Z"),
            Provenance: new SaveFactsDecoderProvenanceV1(
                ParserVersion: "1.2.3",
                DecoderVersion: "4.5.6",
                GameVersion: "0.4.2.0"),
            Completeness: SaveFactsCompletenessState.Partial,
            Warnings:
            [
                new SaveFactsWarningV1("missing-dungeon", "Dungeon family was not present in the source snapshot.")
            ],
            DomainCounts: new Dictionary<string, int>
            {
                ["players"] = 1,
                ["guilds"] = 3,
                ["pals"] = 18
            },
            Players:
            [
                new SaveFactsPlayerV2(
                    SnapshotLocalId: "player:1",
                    NativeId: new(SaveFactsSourceFieldState.Present, "native-1"),
                    DisplayName: new(SaveFactsSourceFieldState.Present, "Example"),
                    Guild: new(SaveFactsSourceFieldState.Absent, null),
                    Level: new(SaveFactsSourceFieldState.Present, 42),
                    Experience: new(SaveFactsSourceFieldState.Unknown, null),
                    Points: new(SaveFactsSourceFieldState.Present, 7),
                    Technology: new(SaveFactsSourceFieldState.Present, ["TechA"]),
                    Recipes: new(SaveFactsSourceFieldState.Present, ["RecipeA"]),
                    Quests: new(SaveFactsSourceFieldState.Absent, []),
                    LastOnline: new(SaveFactsSourceFieldState.Unknown, null),
                    InventoryReferences: [],
                    EquipmentReferences: [],
                    Position: new(SaveFactsSourceFieldState.Absent, null),
                    State: new(SaveFactsSourceFieldState.Present, "Alive"))
            ]);

        var json = JsonSerializer.Serialize(snapshot, SaveFactsJson.SerializerOptions);

        Assert.Equal("""
            {"schemaVersion":"palworld-save-facts/v2","snapshotId":"snapshot-20260718-001","sourceDigest":"sha256:abc123","observedAt":"2026-07-18T12:30:00+00:00","provenance":{"parserVersion":"1.2.3","decoderVersion":"4.5.6","gameVersion":"0.4.2.0"},"completeness":"partial","warnings":[{"code":"missing-dungeon","message":"Dungeon family was not present in the source snapshot."}],"domainCounts":{"players":1,"guilds":3,"pals":18},"players":[{"snapshotLocalId":"player:1","nativeId":{"state":"present","value":"native-1"},"displayName":{"state":"present","value":"Example"},"guild":{"state":"absent","value":null},"level":{"state":"present","value":42},"experience":{"state":"unknown","value":null},"points":{"state":"present","value":7},"technology":{"state":"present","values":["TechA"]},"recipes":{"state":"present","values":["RecipeA"]},"quests":{"state":"absent","values":[]},"lastOnline":{"state":"unknown","value":null},"inventoryReferences":[],"equipmentReferences":[],"position":{"state":"absent","value":null},"state":{"state":"present","value":"Alive"}}]}
            """, json);
    }

    [Fact]
    public void Decode_manifest_serializes_with_expected_shape()
    {
        var manifest = new SaveFactsDecodeManifestV1(
            SchemaVersion: SaveFactsSchema.DecodeManifestV1,
            SnapshotId: "snapshot-20260718-001",
            SourceDigest: "sha256:abc123",
            ObservedAt: DateTimeOffset.Parse("2026-07-18T12:30:00Z"),
            Provenance: new SaveFactsDecoderProvenanceV1(
                ParserVersion: "1.2.3",
                DecoderVersion: "4.5.6",
                GameVersion: "0.4.2.0"),
            Completeness: SaveFactsCompletenessState.Partial,
            Warnings:
            [
                new SaveFactsWarningV1("missing-dungeon", "Dungeon family was not present in the source snapshot.")
            ],
            RawArtifact: new SaveFactsArtifactManifestV1(
                Path: "raw.json.zst",
                ByteCount: 4096,
                Sha256: "sha256:def456",
                Compression: "zstd",
                ContentType: "application/json"));

        var json = JsonSerializer.Serialize(manifest, SaveFactsJson.SerializerOptions);

        Assert.Equal("""
            {"schemaVersion":"palworld-save-decode-manifest/v1","snapshotId":"snapshot-20260718-001","sourceDigest":"sha256:abc123","observedAt":"2026-07-18T12:30:00+00:00","provenance":{"parserVersion":"1.2.3","decoderVersion":"4.5.6","gameVersion":"0.4.2.0"},"completeness":"partial","warnings":[{"code":"missing-dungeon","message":"Dungeon family was not present in the source snapshot."}],"rawArtifact":{"path":"raw.json.zst","byteCount":4096,"sha256":"sha256:def456","compression":"zstd","contentType":"application/json"}}
            """, json);
    }

    [Fact]
    public void Every_json_property_has_a_classification()
    {
        var contractTypes = typeof(SaveFactsDocumentV1).Assembly
            .GetTypes()
            .Where(type => type.IsPublic && !type.IsEnum && !type.IsAbstract && !type.IsSubclassOf(typeof(Attribute)))
            .Where(type => type.Namespace == typeof(SaveFactsDocumentV1).Namespace)
            .ToArray();

        foreach (var type in contractTypes)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.GetCustomAttribute<JsonPropertyNameAttribute>() is null)
                {
                    continue;
                }

                var classification = property.GetCustomAttribute<FactClassificationAttribute>();
                Assert.NotNull(classification);
            }
        }
    }

    [Fact]
    public void No_normalized_v2_property_is_raw_only()
    {
        var rawOnlyProperties = FindRawOnlyProperties(typeof(SaveFactsDocumentV2));

        Assert.Empty(rawOnlyProperties);
    }

    private static IReadOnlyList<string> FindRawOnlyProperties(Type rootType)
    {
        var contractNamespace = typeof(SaveFactsDocumentV2).Namespace;
        var pending = new Stack<Type>();
        var visited = new HashSet<Type>();
        var rawOnlyProperties = new List<string>();
        pending.Push(rootType);

        while (pending.TryPop(out var type))
        {
            type = UnwrapContractType(type);
            if (!visited.Add(type) || type.Namespace != contractNamespace)
            {
                continue;
            }

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.GetCustomAttribute<JsonPropertyNameAttribute>() is null)
                {
                    continue;
                }

                if (property.GetCustomAttribute<FactClassificationAttribute>()?.Sensitivity == FactSensitivity.RawOnly)
                {
                    rawOnlyProperties.Add($"{type.Name}.{property.Name}");
                }

                pending.Push(property.PropertyType);
            }
        }

        return rawOnlyProperties;
    }

    private static Type UnwrapContractType(Type type)
    {
        if (Nullable.GetUnderlyingType(type) is { } nullableType)
        {
            return nullableType;
        }

        if (type.IsArray)
        {
            return type.GetElementType()!;
        }

        if (type.IsGenericType)
        {
            var contractType = type.GetGenericArguments()
                .FirstOrDefault(candidate => candidate.Namespace == typeof(SaveFactsDocumentV2).Namespace);
            if (contractType is not null)
            {
                return contractType;
            }
        }

        return type;
    }
}
