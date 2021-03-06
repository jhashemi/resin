﻿using System;
using System.Diagnostics;

namespace Resin.IO
{
    [DebuggerDisplay("{Position} {Length}")]
    public struct BlockInfo : IEquatable<BlockInfo>
    {
        public long Position;
        public int Length;

        public BlockInfo(long position, int length)
        {
            Position = position;
            Length = length;
        }

        public static BlockInfo MinValue { get { return new BlockInfo(0, 0);} }

        public bool Equals(BlockInfo other)
        {
            return other.Position == Position && other.Length == Length;
        }

        public override bool Equals(object obj)
        {
            return Equals((BlockInfo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Length.GetHashCode();
                return hashCode;
            }
        }
    }
}