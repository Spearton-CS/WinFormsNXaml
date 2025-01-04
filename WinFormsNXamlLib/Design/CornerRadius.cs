using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WinFormsNXaml.Design
{
    public struct CornerRadius : IEqualityComparer<CornerRadius>,
        IEqualityOperators<CornerRadius, CornerRadius, bool>,
        IDivisionOperators<CornerRadius, float, CornerRadius>,
        IAdditionOperators<CornerRadius, float, CornerRadius>,
        ISubtractionOperators<CornerRadius, float, CornerRadius>,
        IMultiplyOperators<CornerRadius, float, CornerRadius>,
        IIncrementOperators<CornerRadius>,
        IDecrementOperators<CornerRadius>
    {
        public CornerRadius(float leftTop, float leftBottom = 0, float rightTop = 0, float rightBottom = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(leftTop, nameof(leftTop));
            ArgumentOutOfRangeException.ThrowIfNegative(leftBottom, nameof(leftBottom));
            ArgumentOutOfRangeException.ThrowIfNegative(rightTop, nameof(rightTop));
            ArgumentOutOfRangeException.ThrowIfNegative(rightBottom, nameof(rightBottom));
            LeftTop = leftTop;
            LeftBottom = leftBottom;
            RightTop = rightTop;
            RightBottom = rightBottom;
        }
        public float LeftTop { readonly get; set; }
        public float LeftBottom { readonly get; set; }
        public float RightTop { readonly get; set; }
        public float RightBottom { readonly get; set; }
        public readonly bool IsEmpty => LeftTop == 0 && LeftBottom == 0 && RightTop == 0 && RightBottom == 0;
        public static implicit operator Tuple<float, float, float, float>(CornerRadius thickness) => new(thickness.LeftTop, thickness.LeftBottom, thickness.RightTop, thickness.RightBottom);
        public static implicit operator CornerRadius(Tuple<float, float, float, float> thickness) => new(thickness.Item1, thickness.Item2, thickness.Item3, thickness.Item4);
        public static implicit operator CornerRadius(Tuple<uint, uint, uint, uint> thickness) => new(thickness.Item1, thickness.Item2, thickness.Item3, thickness.Item4);
        public static implicit operator CornerRadius(Tuple<int, int, int, int> thickness) => new(thickness.Item1, thickness.Item2, thickness.Item3, thickness.Item4);
        public static implicit operator CornerRadius(Tuple<short, short, short, short> thickness) => new(thickness.Item1, thickness.Item2, thickness.Item3, thickness.Item4);
        public static implicit operator CornerRadius(float value) => new(value, value, value, value);
        public static bool operator ==(CornerRadius left, CornerRadius right) => left.LeftTop == right.LeftTop && left.LeftBottom == right.LeftBottom && left.RightTop == right.RightTop && left.RightBottom == right.RightBottom;
        public static bool operator !=(CornerRadius left, CornerRadius right) => left.LeftTop != right.LeftTop || left.LeftBottom != right.LeftBottom || left.RightTop != right.RightTop || left.RightBottom != right.RightBottom;
        public static CornerRadius operator /(CornerRadius left, float right) => new(left.LeftTop / right, left.LeftBottom / right, left.RightTop / right, left.RightBottom / right);
        public static CornerRadius operator -(CornerRadius left, float right) => new(left.LeftTop - right, left.LeftBottom - right, left.RightTop - right, left.RightBottom - right);
        public static CornerRadius operator *(CornerRadius left, float right) => new(left.LeftTop * right, left.LeftBottom * right, left.RightTop * right, left.RightBottom * right);
        public static CornerRadius operator ++(CornerRadius value) => new(++value.LeftTop, ++value.LeftBottom, ++value.RightTop, ++value.RightBottom);
        public static CornerRadius operator +(CornerRadius left, float right) => new(left.LeftTop + right, left.LeftBottom + right, left.RightTop + right, left.RightBottom + right);
        public static CornerRadius operator --(CornerRadius value) => new(--value.LeftTop, --value.LeftBottom, --value.RightTop, --value.RightBottom);
        public bool Equals(CornerRadius x, CornerRadius y) => x == y;
        public override bool Equals([NotNullWhen(true)] object? obj) => obj is CornerRadius y && this == y;
        public int GetHashCode([DisallowNull] CornerRadius obj) => obj.GetHashCode();
    }
}