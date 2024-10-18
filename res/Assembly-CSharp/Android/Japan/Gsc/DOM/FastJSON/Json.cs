// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.FastJSON.Json
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
