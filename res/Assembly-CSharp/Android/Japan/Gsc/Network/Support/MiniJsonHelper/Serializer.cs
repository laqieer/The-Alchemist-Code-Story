// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Support.MiniJsonHelper.Serializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Network.Support.MiniJsonHelper
{
  public class Serializer
  {
    private Stack<Delegate> stack = new Stack<Delegate>();
    private List<Delegate> functions = new List<Delegate>();
    public static readonly Serializer Instance = new Serializer();

    private Serializer()
    {
    }

    public Serializer WithArray<T>()
    {
      this.Add<List<T>>(new Func<List<T>, object>(this.FromArray<T>));
      return this;
    }

    public Serializer WithArray<T>(Func<T, object> func)
    {
      this.Add<List<T>>(new Func<List<T>, object>(this.FromArray<T>));
      this.Add<T>(func);
      return this;
    }

    public Serializer WithDict<TKey, TValue>()
    {
      this.Add<Dictionary<TKey, TValue>>(new Func<Dictionary<TKey, TValue>, object>(this.FromDictionary<TKey, TValue>));
      return this;
    }

    public Serializer WithDict<TKey, TValue>(Func<TKey, object> keyFunc, Func<TValue, object> valueFunc)
    {
      this.Add<Dictionary<TKey, TValue>>(new Func<Dictionary<TKey, TValue>, object>(this.FromDictionary<TKey, TValue>));
      this.Add<TKey>(keyFunc);
      this.Add<TValue>(valueFunc);
      return this;
    }

    public Serializer Add<T>(Func<T, object> func)
    {
      this.functions.Add((Delegate) func);
      return this;
    }

    public object Serialize<T>(T source)
    {
      for (int index = this.functions.Count - 1; index >= 0; --index)
        this.stack.Push(this.functions[index]);
      this.functions.Clear();
      return ((Func<T, object>) this.stack.Pop())(source);
    }

    private object FromArray<T>(List<T> source)
    {
      int count = source.Count;
      object[] objArray = new object[count];
      Func<T, object> func = (Func<T, object>) this.stack.Pop();
      for (int index = 0; index < count; ++index)
        objArray[index] = func(source[index]);
      return (object) objArray;
    }

    private object FromDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
    {
      Func<TKey, object> keyFunc = (Func<TKey, object>) this.stack.Pop();
      Func<TValue, object> valueFunc = (Func<TValue, object>) this.stack.Pop();
      return (object) new Dictionary<string, object>() { { "keys", (object) source.Keys.Select<TKey, object>((Func<TKey, object>) (x => keyFunc(x))).ToArray<object>() }, { "values", (object) source.Values.Select<TValue, object>((Func<TValue, object>) (x => valueFunc(x))).ToArray<object>() } };
    }

    public static object FromObject<T>(T source) where T : IRequestObject
    {
      return (object) source.GetPayload();
    }

    public static object FromDateTime(DateTime source)
    {
      if (source == DateTime.MinValue)
        return (object) null;
      return (object) source.ToUniversalTime().ToString("u");
    }

    public static object From<T>(T source)
    {
      return (object) source;
    }
  }
}
