# 3D Model (.rn8)

Contains 3D Models

the first 4 bytes are used to determine the "special" content:
  - `-969697` - appears to only be used for locomotives. Can contain multiple meshes
  - `-969696` - not sure, seems to only be used for a few files in `Splendid Assets`, they all seem like tests. maybe its for unoptimized models? Can contain multiple meshes
  - everything else, reset stream. Can only contain a single mesh