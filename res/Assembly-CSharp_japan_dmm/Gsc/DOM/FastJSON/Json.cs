// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.Json
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
      return node.IsBool() ? (object) node.ToBool() : (object) null;
    }
  }
}
