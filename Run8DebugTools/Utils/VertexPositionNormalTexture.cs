using System.Numerics;

namespace Run8Utils
{
    public struct VertexPositionNormalTexture : IEquatable<VertexPositionNormalTexture>
    {
        public VertexPositionNormalTexture(Vector3 position, Vector3 normal, Vector2 textureCoordinate)
        {
            this = default;
            this.svPosition = position;
            this.normal = normal;
            this.texcoord0 = textureCoordinate;
        }

        public bool Equals(VertexPositionNormalTexture other)
        {
            return this.svPosition.Equals(other.svPosition) && this.normal.Equals(other.normal) && this.texcoord0.Equals(other.texcoord0);
        }

        // Token: 0x060004A6 RID: 1190 RVA: 0x00017F88 File Offset: 0x00016188
        public override bool Equals(object obj)
        {
            return !object.ReferenceEquals(null, obj) && obj is VertexPositionNormalTexture && this.Equals((VertexPositionNormalTexture)obj);
        }

        // Token: 0x060004A7 RID: 1191 RVA: 0x00017FAC File Offset: 0x000161AC
        public override int GetHashCode()
        {
            int num = this.svPosition.GetHashCode();
            num = (num * 397) ^ this.normal.GetHashCode();
            return (num * 397) ^ this.texcoord0.GetHashCode();
        }

        public static bool operator ==(VertexPositionNormalTexture left, VertexPositionNormalTexture right)
        {
            return left.Equals(right);
        }

        // Token: 0x060004A9 RID: 1193 RVA: 0x0001800C File Offset: 0x0001620C
        public static bool operator !=(VertexPositionNormalTexture left, VertexPositionNormalTexture right)
        {
            return !left.Equals(right);
        }

        // Token: 0x060004AA RID: 1194 RVA: 0x0001801C File Offset: 0x0001621C
        public override string ToString()
        {
            return string.Format("svPosition: {0}, Normal: {1}, Texcoord: {2}", this.svPosition, this.normal, this.texcoord0);
        }

        // Token: 0x060004AB RID: 1195 RVA: 0x0001804C File Offset: 0x0001624C
        // Note: this type is marked as 'beforefieldinit'.
        static VertexPositionNormalTexture()
        {
        }

        public Vector3 svPosition;

        public Vector3 normal;

        public Vector2 texcoord0;

        public Vector3 tangent;

        public Vector3 binormal;
    }
}
