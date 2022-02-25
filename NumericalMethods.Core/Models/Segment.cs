using System;

namespace NumericalMethods.Core.Models
{
    public struct Segment : IComparable<Segment>, IEquatable<Segment>
    {
        public double Start { get; }

        public double End { get; }

        public double Length => End - Start;

        public Segment(double start, double end)
        {
            Start = start;
            End = end;

            if (Start > End)
                (Start, End) = (End, Start);
        }

        public int CompareTo(Segment other)
            => Start == other.Start
                ? End.CompareTo(other.End)
                : Start.CompareTo(other.Start);

        public override bool Equals(object obj)
            => obj is Segment segment && Equals(segment);

        public bool Equals(Segment other)
            => Start == other.Start && End == other.End;

        public static bool operator ==(Segment left, Segment right)
            => left.Equals(right);

        public static bool operator !=(Segment left, Segment right)
            => !(left == right);

        public override int GetHashCode()
            => (Start, End).GetHashCode();
    }
}