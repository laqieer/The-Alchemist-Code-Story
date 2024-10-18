// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ByteArrayStringHashTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace MessagePack.Internal
{
  public class ByteArrayStringHashTable : IEnumerable<KeyValuePair<string, int>>, IEnumerable
  {
    private readonly ByteArrayStringHashTable.Entry[][] buckets;
    private readonly ulong indexFor;

    public ByteArrayStringHashTable(int capacity)
      : this(capacity, 0.42f)
    {
    }

    public ByteArrayStringHashTable(int capacity, float loadFactor)
    {
      this.buckets = new ByteArrayStringHashTable.Entry[ByteArrayStringHashTable.CalculateCapacity(capacity, loadFactor)][];
      this.indexFor = (ulong) this.buckets.Length - 1UL;
    }

    public void Add(string key, int value)
    {
      if (!this.TryAddInternal(Encoding.UTF8.GetBytes(key), value))
        throw new ArgumentException("Key was already exists. Key:" + key);
    }

    public void Add(byte[] key, int value)
    {
      if (!this.TryAddInternal(key, value))
        throw new ArgumentException("Key was already exists. Key:" + (object) key);
    }

    private bool TryAddInternal(byte[] key, int value)
    {
      ulong hashCode = ByteArrayStringHashTable.ByteArrayGetHashCode(key, 0, key.Length);
      ByteArrayStringHashTable.Entry entry = new ByteArrayStringHashTable.Entry()
      {
        Key = key,
        Value = value
      };
      ByteArrayStringHashTable.Entry[] bucket = this.buckets[checked ((ulong) (unchecked ((long) hashCode) & unchecked ((long) this.indexFor)))];
      if (bucket == null)
      {
        this.buckets[(IntPtr) checked ((ulong) (unchecked ((long) hashCode) & unchecked ((long) this.indexFor)))] = new ByteArrayStringHashTable.Entry[1]
        {
          entry
        };
      }
      else
      {
        for (int index = 0; index < bucket.Length; ++index)
        {
          byte[] key1 = bucket[index].Key;
          if (ByteArrayComparer.Equals(key, 0, key.Length, key1))
            return false;
        }
        ByteArrayStringHashTable.Entry[] destinationArray = new ByteArrayStringHashTable.Entry[bucket.Length + 1];
        Array.Copy((Array) bucket, (Array) destinationArray, bucket.Length);
        ByteArrayStringHashTable.Entry[] entryArray = destinationArray;
        entryArray[entryArray.Length - 1] = entry;
        this.buckets[checked ((ulong) (unchecked ((long) hashCode) & unchecked ((long) this.indexFor)))] = entryArray;
      }
      return true;
    }

    public bool TryGetValue(ArraySegment<byte> key, out int value)
    {
      ByteArrayStringHashTable.Entry[] bucket = this.buckets[checked ((ulong) (unchecked ((long) ByteArrayStringHashTable.ByteArrayGetHashCode(key.Array, key.Offset, key.Count)) & unchecked ((long) this.indexFor)))];
      if (bucket != null)
      {
        ByteArrayStringHashTable.Entry entry1 = bucket[0];
        if (ByteArrayComparer.Equals(key.Array, key.Offset, key.Count, entry1.Key))
        {
          value = entry1.Value;
          return true;
        }
        for (int index = 1; index < bucket.Length; ++index)
        {
          ByteArrayStringHashTable.Entry entry2 = bucket[index];
          if (ByteArrayComparer.Equals(key.Array, key.Offset, key.Count, entry2.Key))
          {
            value = entry2.Value;
            return true;
          }
        }
      }
      value = 0;
      return false;
    }

    private static ulong ByteArrayGetHashCode(byte[] x, int offset, int count)
    {
      uint hashCode = 0;
      if (x != null)
      {
        int num = offset + count;
        hashCode = 2166136261U;
        for (int index = offset; index < num; ++index)
          hashCode = (uint) (((int) x[index] ^ (int) hashCode) * 16777619);
      }
      return (ulong) hashCode;
    }

    private static int CalculateCapacity(int collectionSize, float loadFactor)
    {
      int num1 = (int) ((double) collectionSize / (double) loadFactor);
      int num2 = 1;
      while (num2 < num1)
        num2 <<= 1;
      return num2 < 8 ? 8 : num2;
    }

    [DebuggerHidden]
    public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<KeyValuePair<string, int>>) new ByteArrayStringHashTable.\u003CGetEnumerator\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    private struct Entry
    {
      public byte[] Key;
      public int Value;

      public override string ToString()
      {
        return "(" + Encoding.UTF8.GetString(this.Key) + ", " + (object) this.Value + ")";
      }
    }
  }
}
