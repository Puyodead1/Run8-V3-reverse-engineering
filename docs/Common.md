# Common Structures

## LightLoader

| Type           | Explaination                   | Default |
| -------------- | ------------------------------ | ------- |
| Bool           | Billboard Glare                |         |
| Int32          | Light Range                    |         |
| Float          | Light Width                    |         |
| Float          | Light Intensity                |         |
| Float          | Decay Exponent                 | 1f      |
| Vector3        | Light Offset                   |         |
| Bool           | Is Spot Light                  |         |
| Vector4        | Color (W is constant at 1f)    |         |
| Vector3        | Light Direction Deg            |         |
| Bool           | Flashing                       |         |
| Float          | Flash Time Random Variation    |         |
| Double         | Flash Timer Seconds            |         |
| Bool           | Has Day Night Sensor           |         |
| Float          | Day Night Sensor Ambient Level |         |
| Vector3[]      | Glare List                     |         |
| Float          | Light Glare Radius Meters      | 0.35f   |
| Bool           | Is Hep Powered                 |         |
| Bool           | Is Marker Light                |         |
| Bool           | Is Numberboard Light           |         |
| Bool           | Is Limited Yard Light          |         |
| Bool           | Render Glare Only              | true    |
| Bool           | Is Incandescent                |         |
| Float          | Glow Scalar                    |         |

## Tile Index

| Type  | Explaination | Default |
| ----- | ------------ | ------- |
| Int32 | X            |         |
| Int32 | Y            |         |

## Vector3

| Type  | Explaination | Default |
| ----- | ------------ | ------- |
| Float | X            |         |
| Float | Y            |         |
| Float | Z            |         |

## Vector2

| Type  | Explaination | Default |
| ----- | ------------ | ------- |
| Float | X            |         |
| Float | Y            |         |

## Rectangle

| Type  | Explaination | Default |
| ----- | ------------ | ------- |
| Int32 | X            |         |
| Int32 | Y            |         |
| Int32 | Width        |         |
| Int32 | Height       |         |

## R8String
Run8 Encoded Strings (UTF-16?)

| Type  | Explaination        | Default |
| ----- | ------------------- | ------- |
| Int32 | Size of string data |         |
| Bytes | String data         |         |

## Encoding Strings

```c#
string s = "1ST COAST RECYCLING";
byte[] bytes = Encoding.UTF8.GetBytes(s);
byte[] encoded = new byte[bytes.Length * 2];
int num = 0;
for (int i = 0; i < bytes.Length; i++)
{
	encoded[num++] = (byte)(bytes[i] >> 4);
	encoded[num++] = (byte)(bytes[i] << 4);
}
```

## Decoding Strings

```c#
byte[] encoded = <string data>;
byte[] decodedBytes = new byte[encoded.Length / 2];
int num = 0;
for (int i = 0; i < decodedBytes.Length; i++)
{
	decodedBytes[i] |= (byte)(encoded[num++] << 4);
	decodedBytes[i] |= (byte)(encoded[num++] >> 4);
}

string decodedString = Encoding.UTF8.GetString(decodedBytes);
```


## VertexStruct

| Type  | Explaination    | Operation |
| ----- | --------------- | --------- |
| Float | Reserved        |           |
| Float | SVPosition X    | \* 63.7f  |
| Float | Normal Y        | / -1.732f |
| Float | SVPosition Z    | / 16f     |
| Float | TextureCoord X  | / 4.8f    |
| Float | Normal X        | / 10.962f |
| Float | Reserved        |           |
| Float | Normal Z        | / 11.432f |
| Float | TextureCoord Y  | / 9.6f    |
| Float | SVPosition Y    | \* 6f     |
| Float | BlendIndicies W |           |
| Float | BlendWeight Z   |           |
| Float | BlendIndicies X |           |
| Float | BlendWeight Y   |           |
| Float | BlendIndicies Y |           |
| Float | BlendWeight W   |           |
| Float | BlendIndicies Z |           |
| Float | BlendWeight X   |           |

- SVPosition is a Vector3
- Normal is a Vector3
- TextureCoord is a Vector2
- BlendIndicies is an Int4
- BlendWeight is a Vector4 
