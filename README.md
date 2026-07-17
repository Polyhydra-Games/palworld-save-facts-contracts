# Palworld Save Facts Contracts

MIT-licensed, versioned wire contracts for privacy-safe facts emitted by the
[`palworld-save-facts`](https://github.com/Polyhydra-Games/palworld-save-facts)
GPL command-line decoder.

This repository intentionally contains no save decoder, game data, or GPL
dependency. Applications can reference these types to validate and consume
the decoder's JSON output without linking the decoder into their process.

## Contract

`palworld-save-facts/v1` is a single JSON document written to standard output
for one completed save snapshot. It includes only the raw, private-boundary
facts needed by an operator-owned projector:

- native player IDs, levels, unlocked recipes and quests, technology points,
  and optional guild IDs;
- aggregate guild, base, and Pal counts; and
- the operator-supplied observation time.

Consumers must treat every value as private input. Pseudonymization,
allowlisting, retention, and public projections belong outside the decoder.

## Development

```sh
dotnet test
dotnet pack -c Release
```

The authoritative machine-readable contract is
[`schema/palworld-save-facts.v1.schema.json`](schema/palworld-save-facts.v1.schema.json).
Permissive wire contracts and JSON schemas for privacy-safe Palworld save facts
