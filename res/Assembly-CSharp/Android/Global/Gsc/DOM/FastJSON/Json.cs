// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.Json
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.DOM.FastJSON
{
  public static class Json
  {
    public static object Deserialize(IValue node)
    {
      if (node.IsObject())
        return (object) new Dictionary(node.GetObject());
      if (node.IsArray())
        return (object) new List(node.GetArray());
      if (node.IsString())
        return (object) node.ToString();
      if (node.IsLong())
        return (object) node.ToLong();
      if (node.IsDouble())
        return (object) node.ToDouble();
      if (node.IsBool())
        return (object) node.ToBool();
      return (object) null;
    }
  }
}
