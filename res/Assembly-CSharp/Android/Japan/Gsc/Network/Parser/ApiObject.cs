// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Parser.ApiObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM.Generic;
using System.Collections.Generic;

namespace Gsc.Network.Parser
{
  public class ApiObject : Object
  {
    public void Add(string name, IList<bool> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<string> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<int> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<uint> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<long> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<ulong> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<float> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add(string name, IList<double> values)
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) values[index]);
      this.Add(name, (Value) array);
    }

    public void Add<T>(string name, IList<T> values) where T : Object
    {
      Array array = new Array();
      for (int index = 0; index < values.Count; ++index)
        array.Add((Value) ((Object) values[index]));
      this.Add(name, (Value) array);
    }
  }
}
