@../AGENTS.md

## Agent Skills (Claude Code only)

Project-level skills are committed under [../.agents/skills/](../.agents/skills/).
Claude Code only discovers skills under `.claude/skills/`, so on
Windows create a one-time directory junction so both tools see the same set:

```powershell
# Run from the repository root, once per clone.
if (Test-Path .claude\skills) { Remove-Item -Recurse -Force .claude\skills }
New-Item -ItemType Junction -Path .claude\skills -Target (Resolve-Path .agents\skills).Path | Out-Null
```

The junction itself is ignored by Git (the entire `.claude/` directory is in
[../.gitignore](../.gitignore)), so only `.agents/skills/` is committed.
