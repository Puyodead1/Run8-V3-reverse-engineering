@echo off

for %%f in (ksy\*.ksy) do (
    python generate_docs.py "%%f" "..\..\docs"
)