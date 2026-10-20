[![CI](https://github.com/Delta3-Studio/ByteSize/actions/workflows/ci.yml/badge.svg)](https://github.com/Delta3-Studio/ByteSize/actions/workflows/ci.yml)
[![Nuget](https://img.shields.io/nuget/v/Delta3.ByteSize.svg?style=flat)](https://www.nuget.org/packages/Delta3.ByteSize)
![](https://raw.githubusercontent.com/Delta3-Studio/ByteSize/site/test_report_badge.svg)
![](https://raw.githubusercontent.com/Delta3-Studio/ByteSize/site/lines_badge.svg)

![](https://raw.githubusercontent.com/Delta3-Studio/ByteSize/site/dotnet_version_badge.svg)
![](https://img.shields.io/badge/Lang-C%23-green)
![https://editorconfig.org/](https://img.shields.io/badge/style-EditorConfig-black)

# ByteSize

This library makes byte quantities easier to work with by giving them a clear, unambiguous representation in code.

Supports both decimal (SI) and binary (IEC) units ([See this](https://en.wikipedia.org/wiki/Kilobyte#Definitions_and_usage)).
- **Decimal:** 1 kilobyte (KB) equals 1,000 bytes. Supported units include `KB`, `MB`, `GB`, `TB`, and `PB`.
- **Binary:** 1 kibibyte (KiB) equals 1,024 bytes. Supported units include `KiB`, `MiB`, `GiB`, `TiB`, and `PiB`.

It handles conversions and provides methods for creating and retrieving values for any of those units.

> Check the [API Docs](https://delta3-studio.github.io/ByteSize)

## Alternatives

- [omar/ByteSize](https://github.com/omar/ByteSize)
