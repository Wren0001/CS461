#!/usr/bin/env python3
"""Copies the art this build needs out of the existing asset packs.
Nothing is redrawn or rescaled: it only picks files by name from the packs you already have.

  python3 Tools/copy_assets.py --pack16 path/to/assets_16x16 --pack32 path/to/assets_32x32 [--dry-run]

Reads Tools/asset_manifest.txt. Each line:   Destination | pattern pattern ... !exclude ...
  - patterns are filename globs; a pattern with a '/' is matched against the file's path inside the pack
  - !exclude patterns are matched against the path inside the pack
Files land in Assets/<ROOT>/Art/Sprites_16/<Destination>/ and Sprites_32/<Destination>/.
Unmatched patterns are listed at the end so you can fix names to match your pack.
"""
import argparse, fnmatch, os, shutil, sys
HERE = os.path.dirname(os.path.abspath(__file__))
PROJECT = os.path.dirname(HERE)
ROOT = "_Demo"
# Folders in the pack that hold backups, previews or other seasons (their names repeat the spring files).
GLOBAL_EXCLUDE = ("buildings_large_backup/", "clay_game_backup/", "previews/", "animation_previews/", "vegetation_fall/", "vegetation_winter/")

def load_manifest():
    rules = []
    for raw in open(os.path.join(HERE, "asset_manifest.txt"), encoding="utf-8"):
        line = raw.split("#", 1)[0].strip()
        if not line or "|" not in line: continue
        dest, pats = [p.strip() for p in line.split("|", 1)]
        toks = pats.split()
        rules.append((dest, [t for t in toks if not t.startswith("!")], [t[1:] for t in toks if t.startswith("!")]))
    return rules

def walk(pack):
    for d, _, files in os.walk(pack):
        for f in files:
            if f.lower().endswith((".png", ".gif", ".txt", ".json")):
                full = os.path.join(d, f)
                rel = os.path.relpath(full, pack).replace(os.sep, "/")
                if any(("/" + rel).find("/" + g) >= 0 for g in GLOBAL_EXCLUDE): continue
                yield rel, full

def run(pack, size, rules, dry):
    files = list(walk(pack))
    hit_patterns, copied, seen = set(), 0, {}
    for dest, pats, excl in rules:
        for rel, full in files:
            name = rel.rsplit("/", 1)[-1]
            if any(fnmatch.fnmatch(rel, "*" + e if not e.startswith("*") else e) for e in excl): continue
            matched = [p for p in pats if (fnmatch.fnmatch(rel, "*" + p) if "/" in p else fnmatch.fnmatch(name, p))]
            hit_patterns.update((dest, p) for p in matched)
            for p in matched[:1]:
                if True:
                    out = os.path.join(PROJECT, "Assets", ROOT, "Art", f"Sprites_{size}", dest, name)
                    if out in seen and seen[out] != rel:
                        print(f"[{size}] name clash, kept the first: {seen[out]}  (skipped {rel})"); break
                    seen[out] = rel
                    if not dry:
                        os.makedirs(os.path.dirname(out), exist_ok=True)
                        shutil.copy2(full, out)
                    copied += 1
                    break
    return hit_patterns, copied

def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--pack16"); ap.add_argument("--pack32"); ap.add_argument("--dry-run", action="store_true")
    a = ap.parse_args()
    if not (a.pack16 or a.pack32): ap.error("give --pack16 and/or --pack32")
    rules = load_manifest()
    all_pats = {(d, p) for d, ps, _ in rules for p in ps}
    for pack, size in ((a.pack16, "16"), (a.pack32, "32")):
        if not pack: continue
        hits, n = run(pack, size, rules, a.dry_run)
        print(f"[{size}] {'would copy' if a.dry_run else 'copied'} {n} files from {pack}")
        missing = sorted(all_pats - hits)
        if missing:
            print(f"[{size}] {len(missing)} patterns matched nothing (rename them to match your pack):")
            for d, p in missing: print(f"     {d:28s} {p}")
main()
