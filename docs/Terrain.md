# Terrain

Terrain files come in a few different versions/formats:

-   TR4 - V3
-   TR3 - V2 Exclusive, V3 cannot even read these
-   TR2 - V2
-   TER - V2 Exclusive

A tile is `844.3211` meters in the X, and `1026.0822` meters in the Z. Each tile consists of `625` chunks in a `25x25` grid. Each chunk measures `33.77` meters (844.3211/25) by `41.04` meters (1026.0822/25)

Lat per tile: `0.009253208`
Lon per tile: `0.009255224`
Start latitude: `35`
Start Longitude: `-119`

# TR2

TR2 Tiles are uncompressed, and only contain terrain textures and height values.

| Offset | Type    | Explaination |
| ------ | ------- | ------------ |
| 0      | String  |              |
| ...    | String  |              |
| ...    | String  |              |
| ...    | String  |              |
| ...    | Chunk[] | Chunk Data   |

# TR3

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | String |              |
| ...    | String |              |
| ...    | String |              |
| ...    | String |              |
| ...    | Int32  |              |

# TR4

TR4 tiles are compressed with Deflate, this version is an extension of TR2, as such, they can store scenery items, vegetation, decals, and more in them. TR4 is backwards compatible with TR2.

| Offset | Type           | Explaination          |
| ------ | -------------- | --------------------- |
| 0      | String         |                       |
| ...    | String         |                       |
| ...    | String         |                       |
| ...    | String         |                       |
| ...    | Chunk[]        | Chunk Data            |
| ...    | Float?         | Longitude East        |
| ...    | Float?         | Longitude West        |
| ...    | Float?         | Latitude North        |
| ...    | Float?         | Latitude South        |
| ...    | String?        | Procedural Vegetation |
| ...    | Int32          | Scenery Asset Count   |
| ...    | SceneryAsset[] | Scenery Assets        |
| ...    | Int32          | Vegetation Count      |
| ...    | Vector4[]      | Vegetation            |
| ...    | Int32          | Reserved              |

-   `?` indicates a field that may not be present, use a try catch block.

### Chunk

| Offset | Type    | Explaination     |
| ------ | ------- | ---------------- |
| 0      | Int32   | Hixel Count (HC) |
| 4      | Float[] | HCxHC Height Map |

### SceneryAsset

| Offset | Type    | Explaination            |
| ------ | ------- | ----------------------- |
| 0      | Int32   | Decal Count             |
| 4      | Decal[] | Decals                  |
| ...    | Boolean | Disregard Bounding Test |
| ...    | String  | Model Name              |
| ...    | Vector3 | Position                |
| ...    | Vector3 | Rotation Degrees        |
| ...    | Vector3 | Scale                   |
| ...    | TileXZ  | TileIndex               |

### Decal

| Offset | Type    | Explaination     |
| ------ | ------- | ---------------- |
| 0      | Vector3 | RGB              |
| 12     | Int32   | Digit Count      |
| 16     | Int32[] | Digits           |
| ...    | Vector3 | Offset           |
| ...    | Vector3 | Rotation Degrees |
| ...    | Float   | Size             |
| ...    | String  | Texture Name     |
