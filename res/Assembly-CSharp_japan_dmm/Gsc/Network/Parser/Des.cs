// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Parser.Des
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Core;
using Gsc.DOM;
using Gsc.DOM.Generic;
using Gsc.Network.Data;
using Gsc.Support.Obfuscation;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Network.Parser
{
  public class Des
  {
    public static readonly Des Ins = new Des();
    private static readonly Dictionary<Type, Delegate> defaultConverters = new Dictionary<Type, Delegate>();
    private Stack<Delegate> stack = new Stack<Delegate>();
    private List<Delegate> functions = new List<Delegate>();

    static Des()
    {
      Dictionary<Type, Delegate> defaultConverters1 = Des.defaultConverters;
      Type key1 = typeof (DateTime);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache0 = new Func<IValue, DateTime>(Des.ToDateTime);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, DateTime> fMgCache0 = Des.\u003C\u003Ef__mg\u0024cache0;
      defaultConverters1.Add(key1, (Delegate) fMgCache0);
      Dictionary<Type, Delegate> defaultConverters2 = Des.defaultConverters;
      Type key2 = typeof (string);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache1 = new Func<IValue, string>(Des.ToString);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, string> fMgCache1 = Des.\u003C\u003Ef__mg\u0024cache1;
      defaultConverters2.Add(key2, (Delegate) fMgCache1);
      Dictionary<Type, Delegate> defaultConverters3 = Des.defaultConverters;
      Type key3 = typeof (bool);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache2 = new Func<IValue, bool>(Des.ToBool);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, bool> fMgCache2 = Des.\u003C\u003Ef__mg\u0024cache2;
      defaultConverters3.Add(key3, (Delegate) fMgCache2);
      Dictionary<Type, Delegate> defaultConverters4 = Des.defaultConverters;
      Type key4 = typeof (sbyte);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache3 = new Func<IValue, sbyte>(Des.ToSByte);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, sbyte> fMgCache3 = Des.\u003C\u003Ef__mg\u0024cache3;
      defaultConverters4.Add(key4, (Delegate) fMgCache3);
      Dictionary<Type, Delegate> defaultConverters5 = Des.defaultConverters;
      Type key5 = typeof (byte);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache4 = new Func<IValue, byte>(Des.ToByte);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, byte> fMgCache4 = Des.\u003C\u003Ef__mg\u0024cache4;
      defaultConverters5.Add(key5, (Delegate) fMgCache4);
      Dictionary<Type, Delegate> defaultConverters6 = Des.defaultConverters;
      Type key6 = typeof (short);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache5 = new Func<IValue, short>(Des.ToShort);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, short> fMgCache5 = Des.\u003C\u003Ef__mg\u0024cache5;
      defaultConverters6.Add(key6, (Delegate) fMgCache5);
      Dictionary<Type, Delegate> defaultConverters7 = Des.defaultConverters;
      Type key7 = typeof (ushort);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache6 = new Func<IValue, ushort>(Des.ToUShort);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ushort> fMgCache6 = Des.\u003C\u003Ef__mg\u0024cache6;
      defaultConverters7.Add(key7, (Delegate) fMgCache6);
      Dictionary<Type, Delegate> defaultConverters8 = Des.defaultConverters;
      Type key8 = typeof (int);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache7 = new Func<IValue, int>(Des.ToInt);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, int> fMgCache7 = Des.\u003C\u003Ef__mg\u0024cache7;
      defaultConverters8.Add(key8, (Delegate) fMgCache7);
      Dictionary<Type, Delegate> defaultConverters9 = Des.defaultConverters;
      Type key9 = typeof (uint);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache8 = new Func<IValue, uint>(Des.ToUInt);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, uint> fMgCache8 = Des.\u003C\u003Ef__mg\u0024cache8;
      defaultConverters9.Add(key9, (Delegate) fMgCache8);
      Dictionary<Type, Delegate> defaultConverters10 = Des.defaultConverters;
      Type key10 = typeof (long);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache9 = new Func<IValue, long>(Des.ToLong);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, long> fMgCache9 = Des.\u003C\u003Ef__mg\u0024cache9;
      defaultConverters10.Add(key10, (Delegate) fMgCache9);
      Dictionary<Type, Delegate> defaultConverters11 = Des.defaultConverters;
      Type key11 = typeof (ulong);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cacheA == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cacheA = new Func<IValue, ulong>(Des.ToULong);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ulong> fMgCacheA = Des.\u003C\u003Ef__mg\u0024cacheA;
      defaultConverters11.Add(key11, (Delegate) fMgCacheA);
      Dictionary<Type, Delegate> defaultConverters12 = Des.defaultConverters;
      Type key12 = typeof (float);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cacheB == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cacheB = new Func<IValue, float>(Des.ToFloat);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, float> fMgCacheB = Des.\u003C\u003Ef__mg\u0024cacheB;
      defaultConverters12.Add(key12, (Delegate) fMgCacheB);
      Dictionary<Type, Delegate> defaultConverters13 = Des.defaultConverters;
      Type key13 = typeof (double);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cacheC == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cacheC = new Func<IValue, double>(Des.ToDouble);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, double> fMgCacheC = Des.\u003C\u003Ef__mg\u0024cacheC;
      defaultConverters13.Add(key13, (Delegate) fMgCacheC);
      Dictionary<Type, Delegate> defaultConverters14 = Des.defaultConverters;
      Type key14 = typeof (ObfuscatedBool);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cacheD == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cacheD = new Func<IValue, ObfuscatedBool>(Des.ToObfuscatedType.boolean);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedBool> fMgCacheD = Des.\u003C\u003Ef__mg\u0024cacheD;
      defaultConverters14.Add(key14, (Delegate) fMgCacheD);
      Dictionary<Type, Delegate> defaultConverters15 = Des.defaultConverters;
      Type key15 = typeof (ObfuscatedSByte);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cacheE == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cacheE = new Func<IValue, ObfuscatedSByte>(Des.ToObfuscatedType.int8);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedSByte> fMgCacheE = Des.\u003C\u003Ef__mg\u0024cacheE;
      defaultConverters15.Add(key15, (Delegate) fMgCacheE);
      Dictionary<Type, Delegate> defaultConverters16 = Des.defaultConverters;
      Type key16 = typeof (ObfuscatedByte);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cacheF == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cacheF = new Func<IValue, ObfuscatedByte>(Des.ToObfuscatedType.uint8);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedByte> fMgCacheF = Des.\u003C\u003Ef__mg\u0024cacheF;
      defaultConverters16.Add(key16, (Delegate) fMgCacheF);
      Dictionary<Type, Delegate> defaultConverters17 = Des.defaultConverters;
      Type key17 = typeof (ObfuscatedShort);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache10 = new Func<IValue, ObfuscatedShort>(Des.ToObfuscatedType.int16);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedShort> fMgCache10 = Des.\u003C\u003Ef__mg\u0024cache10;
      defaultConverters17.Add(key17, (Delegate) fMgCache10);
      Dictionary<Type, Delegate> defaultConverters18 = Des.defaultConverters;
      Type key18 = typeof (ObfuscatedUShort);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache11 = new Func<IValue, ObfuscatedUShort>(Des.ToObfuscatedType.uint16);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedUShort> fMgCache11 = Des.\u003C\u003Ef__mg\u0024cache11;
      defaultConverters18.Add(key18, (Delegate) fMgCache11);
      Dictionary<Type, Delegate> defaultConverters19 = Des.defaultConverters;
      Type key19 = typeof (ObfuscatedInt);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache12 = new Func<IValue, ObfuscatedInt>(Des.ToObfuscatedType.int32);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedInt> fMgCache12 = Des.\u003C\u003Ef__mg\u0024cache12;
      defaultConverters19.Add(key19, (Delegate) fMgCache12);
      Dictionary<Type, Delegate> defaultConverters20 = Des.defaultConverters;
      Type key20 = typeof (ObfuscatedUInt);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache13 = new Func<IValue, ObfuscatedUInt>(Des.ToObfuscatedType.uint32);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedUInt> fMgCache13 = Des.\u003C\u003Ef__mg\u0024cache13;
      defaultConverters20.Add(key20, (Delegate) fMgCache13);
      Dictionary<Type, Delegate> defaultConverters21 = Des.defaultConverters;
      Type key21 = typeof (ObfuscatedLong);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache14 = new Func<IValue, ObfuscatedLong>(Des.ToObfuscatedType.int64);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedLong> fMgCache14 = Des.\u003C\u003Ef__mg\u0024cache14;
      defaultConverters21.Add(key21, (Delegate) fMgCache14);
      Dictionary<Type, Delegate> defaultConverters22 = Des.defaultConverters;
      Type key22 = typeof (ObfuscatedULong);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache15 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache15 = new Func<IValue, ObfuscatedULong>(Des.ToObfuscatedType.uint64);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedULong> fMgCache15 = Des.\u003C\u003Ef__mg\u0024cache15;
      defaultConverters22.Add(key22, (Delegate) fMgCache15);
      Dictionary<Type, Delegate> defaultConverters23 = Des.defaultConverters;
      Type key23 = typeof (ObfuscatedFloat);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache16 = new Func<IValue, ObfuscatedFloat>(Des.ToObfuscatedType.float32);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedFloat> fMgCache16 = Des.\u003C\u003Ef__mg\u0024cache16;
      defaultConverters23.Add(key23, (Delegate) fMgCache16);
      Dictionary<Type, Delegate> defaultConverters24 = Des.defaultConverters;
      Type key24 = typeof (ObfuscatedDouble);
      // ISSUE: reference to a compiler-generated field
      if (Des.\u003C\u003Ef__mg\u0024cache17 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Des.\u003C\u003Ef__mg\u0024cache17 = new Func<IValue, ObfuscatedDouble>(Des.ToObfuscatedType.float64);
      }
      // ISSUE: reference to a compiler-generated field
      Func<IValue, ObfuscatedDouble> fMgCache17 = Des.\u003C\u003Ef__mg\u0024cache17;
      defaultConverters24.Add(key24, (Delegate) fMgCache17);
    }

    private Des()
    {
    }

    public T Deserialize<T>(IValue source) => this.Deserialize<T, IValue>(source);

    private T Deserialize<T, TSource>(TSource source)
    {
      for (int index = this.functions.Count - 1; index >= 0; --index)
        this.stack.Push(this.functions[index]);
      this.functions.Clear();
      return ((Func<TSource, T>) this.stack.Pop())(source);
    }

    public Des Add<T>(Func<IValue, T> func)
    {
      this.functions.Add((Delegate) func);
      return this;
    }

    public Des Arr<T>()
    {
      this.Add<T[]>(new Func<IValue, T[]>(this.ToArray<T>));
      return this;
    }

    public static T To<T>(IValue v) => ((Func<IValue, T>) Des.defaultConverters[typeof (T)])(v);

    public static T ToEntity<T>(IValue v) where T : Entity<T>
    {
      if (v.IsNull())
        return (T) null;
      return v.IsLong() ? EntityRepository.Get<T>(v.ToLong().ToString()) : EntityRepository.Get<T>(v.ToString());
    }

    public static T ToEntity<T>(object v) where T : Entity<T>
    {
      return v == null ? (T) null : EntityRepository.Get<T>(v.ToString());
    }

    public static T ToObject<T>(IValue v) where T : class, IResponseObject
    {
      if (v.IsNull())
        return (T) null;
      return AssemblySupport.CreateInstance<T>((object) v.GetObject());
    }

    public static DateTime ToDateTime(IValue v)
    {
      return v.IsNull() ? DateTime.MinValue : DateTime.Parse(v.ToString());
    }

    public static sbyte ToSByte(IValue v) => (sbyte) v.ToInt();

    public static byte ToByte(IValue v) => (byte) v.ToInt();

    public static short ToShort(IValue v) => (short) (sbyte) v.ToInt();

    public static ushort ToUShort(IValue v) => (ushort) v.ToInt();

    public static int ToInt(IValue v) => v.ToInt();

    public static uint ToUInt(IValue v) => v.ToUInt();

    public static long ToLong(IValue v) => v.ToLong();

    public static ulong ToULong(IValue v) => v.ToULong();

    public static float ToFloat(IValue v) => v.ToFloat();

    public static double ToDouble(IValue v) => v.ToDouble();

    public static string ToString(IValue v) => v.ToString();

    public static bool ToBool(IValue v) => v.ToBool();

    private T[] ToArray<T>(IValue source)
    {
      IArray array1 = source.GetArray();
      T[] array2 = new T[array1.Length];
      Func<IValue, T> func = (Func<IValue, T>) this.stack.Pop();
      for (int index = 0; index < array1.Length; ++index)
        array2[index] = func(array1[index]);
      return array2;
    }

    public static Value ToStringTree(IValue v)
    {
      return !v.IsObject() ? (!v.IsArray() ? (Value) v.ToString() : Des.ToStringTree(v.GetArray())) : Des.ToStringTree(v.GetObject());
    }

    private static Value ToStringTree(Gsc.DOM.IObject v)
    {
      Gsc.DOM.Generic.Object stringTree = new Gsc.DOM.Generic.Object();
      foreach (IMember member in (IEnumerable<IMember>) v)
        stringTree.Add(member.Name, Des.ToStringTree(member.Value));
      return (Value) stringTree;
    }

    private static Value ToStringTree(IArray v)
    {
      Gsc.DOM.Generic.Array stringTree = new Gsc.DOM.Generic.Array();
      foreach (IValue v1 in (IEnumerable<IValue>) v)
        stringTree.Add(Des.ToStringTree(v1));
      return (Value) stringTree;
    }

    public static class ToObfuscatedType
    {
      public static ObfuscatedBool boolean(IValue source)
      {
        return ObfuscatedBool.op_Implicit(source.ToBool());
      }

      public static ObfuscatedSByte int8(IValue source)
      {
        return ObfuscatedSByte.op_Implicit((sbyte) source.ToInt());
      }

      public static ObfuscatedShort int16(IValue source)
      {
        return ObfuscatedShort.op_Implicit((short) source.ToInt());
      }

      public static ObfuscatedInt int32(IValue source) => ObfuscatedInt.op_Implicit(source.ToInt());

      public static ObfuscatedLong int64(IValue source)
      {
        return ObfuscatedLong.op_Implicit(source.ToLong());
      }

      public static ObfuscatedByte uint8(IValue source)
      {
        return ObfuscatedByte.op_Implicit((byte) source.ToInt());
      }

      public static ObfuscatedUShort uint16(IValue source)
      {
        return ObfuscatedUShort.op_Implicit((ushort) source.ToInt());
      }

      public static ObfuscatedUInt uint32(IValue source)
      {
        return ObfuscatedUInt.op_Implicit(source.ToUInt());
      }

      public static ObfuscatedULong uint64(IValue source)
      {
        return ObfuscatedULong.op_Implicit(source.ToULong());
      }

      public static ObfuscatedFloat float32(IValue source)
      {
        return ObfuscatedFloat.op_Implicit(source.ToFloat());
      }

      public static ObfuscatedDouble float64(IValue source)
      {
        return ObfuscatedDouble.op_Implicit(source.ToDouble());
      }
    }
  }
}
