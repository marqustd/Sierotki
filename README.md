# Sierotki

[![Master](https://github.com/marqustd/Sierotki/actions/workflows/build.yml/badge.svg)](https://github.com/marqustd/Sierotki/actions/workflows/build.yml)

The program replaces spaces with non-breaking space characters `'~'` in .tex files before one-character words. One-character word at the end of a line is considered a typographical mistake in Poland. This mistake is called “sierotka” (pl. _orphan_).

## Usage
```
Usage:
  SierotkiCore [options]

Options:
  --file-path <file-path>      Path to a single .tex file. [default: ]
  --folder-path <folder-path>  Path to a folder with several .tex files. Folders can be nested. [default: ]
  --length <length>            Maximum length of a word which should be fixed. [default: 1]
  --words <words>              List of words which should be considered a short word. [default: ]
  --version                    Show version information
  -?, -h, --help               Show help and usage information
  ```