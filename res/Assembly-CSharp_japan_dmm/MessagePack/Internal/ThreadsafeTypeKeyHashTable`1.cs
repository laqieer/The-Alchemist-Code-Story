// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ThreadsafeTypeKeyHashTable`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Threading;

#nullable disable
namespace MessagePack.Internal
{
  internal class ThreadsafeTypeKeyHashTable<TValue>
  {
    private ThreadsafeTypeKeyHashTable<TValue>.Entry[] buckets;
    private int size;
    private readonly object writerLock = new object();
    private readonly float loadFactor;

    public ThreadsafeTypeKeyHashTable(int capacity = 4, float loadFactor = 0.75f)
    {
      this.buckets = new ThreadsafeTypeKeyHashTable<TValue>.Entry[ThreadsafeTypeKeyHashTable<TValue>.CalculateCapacity(capacity, loadFactor)];
      this.loadFactor = loadFactor;
    }

    public bool TryAdd(Type key, TValue value)
    {
      return this.TryAdd(key, (Func<Type, TValue>) (_ => value));
    }

    public bool TryAdd(Type key, Func<Type, TValue> valueFactory)
    {
      return this.TryAddInternal(key, valueFactory, out TValue _);
    }

    private bool TryAddInternal(
      Type key,
      Func<Type, TValue> valueFactory,
      out TValue resultingValue)
    {
      lock (this.writerLock)
      {
        int capacity = ThreadsafeTypeKeyHashTable<TValue>.CalculateCapacity(this.size + 1, this.loadFactor);
        if (this.buckets.Length < capacity)
        {
          ThreadsafeTypeKeyHashTable<TValue>.Entry[] buckets1 = new ThreadsafeTypeKeyHashTable<TValue>.Entry[capacity];
          for (int index = 0; index < this.buckets.Length; ++index)
          {
            for (ThreadsafeTypeKeyHashTable<TValue>.Entry entry = this.buckets[index]; entry != null; entry = entry.Next)
            {
              ThreadsafeTypeKeyHashTable<TValue>.Entry newEntryOrNull = new ThreadsafeTypeKeyHashTable<TValue>.Entry()
              {
                Key = entry.Key,
                Value = entry.Value,
                Hash = entry.Hash
              };
              this.AddToBuckets(buckets1, key, newEntryOrNull, (Func<Type, TValue>) null, out resultingValue);
            }
          }
          bool buckets2 = this.AddToBuckets(buckets1, key, (ThreadsafeTypeKeyHashTable<TValue>.Entry) null, valueFactory, out resultingValue);
          ThreadsafeTypeKeyHashTable<TValue>.VolatileWrite(ref this.buckets, buckets1);
          if (buckets2)
            ++this.size;
          return buckets2;
        }
        bool buckets = this.AddToBuckets(this.buckets, key, (ThreadsafeTypeKeyHashTable<TValue>.Entry) null, valueFactory, out resultingValue);
        if (buckets)
          ++this.size;
        return buckets;
      }
    }

    private bool AddToBuckets(
      ThreadsafeTypeKeyHashTable<TValue>.Entry[] buckets,
      Type newKey,
      ThreadsafeTypeKeyHashTable<TValue>.Entry newEntryOrNull,
      Func<Type, TValue> valueFactory,
      out TValue resultingValue)
    {
      int num = newEntryOrNull == null ? newKey.GetHashCode() : newEntryOrNull.Hash;
      if (buckets[num & buckets.Length - 1] == null)
      {
        if (newEntryOrNull != null)
        {
          resultingValue = newEntryOrNull.Value;
          ThreadsafeTypeKeyHashTable<TValue>.VolatileWrite(ref buckets[num & buckets.Length - 1], newEntryOrNull);
        }
        else
        {
          resultingValue = valueFactory(newKey);
          ThreadsafeTypeKeyHashTable<TValue>.VolatileWrite(ref buckets[num & buckets.Length - 1], new ThreadsafeTypeKeyHashTable<TValue>.Entry()
          {
            Key = newKey,
            Value = resultingValue,
            Hash = num
          });
        }
      }
      else
      {
        ThreadsafeTypeKeyHashTable<TValue>.Entry entry;
        for (entry = buckets[num & buckets.Length - 1]; (object) entry.Key != (object) newKey; entry = entry.Next)
        {
          if (entry.Next == null)
          {
            if (newEntryOrNull != null)
            {
              resultingValue = newEntryOrNull.Value;
              ThreadsafeTypeKeyHashTable<TValue>.VolatileWrite(ref entry.Next, newEntryOrNull);
              goto label_12;
            }
            else
            {
              resultingValue = valueFactory(newKey);
              ThreadsafeTypeKeyHashTable<TValue>.VolatileWrite(ref entry.Next, new ThreadsafeTypeKeyHashTable<TValue>.Entry()
              {
                Key = newKey,
                Value = resultingValue,
                Hash = num
              });
              goto label_12;
            }
          }
        }
        resultingValue = entry.Value;
        return false;
      }
label_12:
      return true;
    }

    public bool TryGetValue(Type key, out TValue value)
    {
      ThreadsafeTypeKeyHashTable<TValue>.Entry[] buckets = this.buckets;
      int hashCode = key.GetHashCode();
      ThreadsafeTypeKeyHashTable<TValue>.Entry entry = buckets[hashCode & buckets.Length - 1];
      if (entry != null)
      {
        if ((object) entry.Key == (object) key)
        {
          value = entry.Value;
          return true;
        }
        for (ThreadsafeTypeKeyHashTable<TValue>.Entry next = entry.Next; next != null; next = next.Next)
        {
          if ((object) next.Key == (object) key)
          {
            value = next.Value;
            return true;
          }
        }
      }
      value = default (TValue);
      return false;
    }

    public TValue GetOrAdd(Type key, Func<Type, TValue> valueFactory)
    {
      TValue resultingValue;
      if (this.TryGetValue(key, out resultingValue))
        return resultingValue;
      this.TryAddInternal(key, valueFactory, out resultingValue);
      return resultingValue;
    }

    private static int CalculateCapacity(int collectionSize, float loadFactor)
    {
      int num1 = (int) ((double) collectionSize / (double) loadFactor);
      int num2 = 1;
      while (num2 < num1)
        num2 <<= 1;
      return num2 < 8 ? 8 : num2;
    }

    private static void VolatileWrite(
      ref ThreadsafeTypeKeyHashTable<TValue>.Entry location,
      ThreadsafeTypeKeyHashTable<TValue>.Entry value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    private static void VolatileWrite(
      ref ThreadsafeTypeKeyHashTable<TValue>.Entry[] location,
      ThreadsafeTypeKeyHashTable<TValue>.Entry[] value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    private class Entry
    {
      public Type Key;
      public TValue Value;
      public int Hash;
      public ThreadsafeTypeKeyHashTable<TValue>.Entry Next;

      public override string ToString() => this.Key.ToString() + "(" + (object) this.Count() + ")";

      private int Count()
      {
        int num = 1;
        for (ThreadsafeTypeKeyHashTable<TValue>.Entry entry = this; entry.Next != null; entry = entry.Next)
          ++num;
        return num;
      }
    }
  }
}
