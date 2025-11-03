# change the env vars so that Windows Security exception applies
# for the pc in HLab
$env:GOCACHE = $(Resolve-Path "..\cache")
$env:GOTMPDIR = $(Resolve-Path "..\tmp")
go run .
