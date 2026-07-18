# Palworld Save Facts Contracts

MIT-licensed, versioned wire contracts for privacy-safe facts emitted by the
[`palworld-save-facts`](https://github.com/Polyhydra-Games/palworld-save-facts)
GPL command-line decoder.

This repository intentionally contains no save decoder, game data, or GPL
dependency. Applications can reference these types to validate and consume
the decoder's JSON output without linking the decoder into their process.

## Contract

`palworld-save-facts/v1` remains the compact stdout snapshot contract for the
legacy projector path.

`palworld-save-facts/v2` is the normalized snapshot envelope. It carries the
metadata needed to retain, classify, and correlate snapshots without baking the
decoder into consumers:

- snapshot ID and source digest;
- observation time;
- parser, decoder, and game versions;
- completeness state and warnings; and
- per-domain counts for the retained snapshot.

`palworld-save-decode-manifest/v1` is the private raw-artifact manifest. It
records the raw output path, size, hash, compression, and decoder provenance so
the sidecar can retain and audit the raw decode without exposing it as the
normal output.

Consumers must treat every value as private input unless the contract marks it
public. Pseudonymization, allowlisting, retention, and public projections
belong outside the decoder.

## Development

```sh
dotnet test
dotnet pack -c Release
```

The authoritative machine-readable contract is
[`schema/palworld-save-facts.v1.schema.json`](schema/palworld-save-facts.v1.schema.json),
[`schema/palworld-save-facts.v2.schema.json`](schema/palworld-save-facts.v2.schema.json),
and [`schema/palworld-save-decode-manifest.v1.schema.json`](schema/palworld-save-decode-manifest.v1.schema.json).
Permissive wire contracts and JSON schemas for privacy-safe Palworld save facts.
