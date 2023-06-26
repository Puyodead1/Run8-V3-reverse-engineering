# Shader File (.tkb)

These files are from SharpDX, they are compiled shaders/effects. I am not going to write a dedicated tool for these as they're a pain in the ass (for me at least) and the compiler already has the ability to dump the bytecode.

## Dump Bytecode
You will need to get `tkfxc.exe` from SharpDX, rather then having to compile it, you can just get it precompiled from Nuget: https://www.nuget.org/api/v2/package/SharpDX/2.6.3. This will download `sharpdx.2.6.3.nupkg` which is just a zip file, you can either change the file extension from `.nupkg` to `.zip` or just open it with 7zip (or any other archive program really). The binary can be found in at `sharpdx.2.6.3\Bin\DirectX11_2-net40\tkfxc.exe`.

Now you can just run:
- `tkfxc.exe <path to .tkb file> > bytecode.txt`