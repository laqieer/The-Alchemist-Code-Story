// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.MiniJSON.Json
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM.Json;
using System.Collections.Generic;

#nullable disable
namespace Gsc.DOM.MiniJSON
{
  public static class Json
  {
    public static object Deserialize(string text)
    {
      using (Document document = Document.Parse(text))
        return Gsc.DOM.MiniJSON.Json.Deserialize((IValue) document.Root);
    }

    public static object Deserialize(byte[] bytes)
    {
      using (Document document = Document.Parse(bytes))
        return Gsc.DOM.MiniJSON.Json.Deserialize((IValue) document.Root);
    }

    public static object Deserialize(IValue node)
    {
      if (node.IsObject())
      {
        IObject @object = node.GetObject();
        Dictionary<string, object> dictionary = new Dictionary<string, object>(@object.MemberCount);
        foreach (IMember member in (IEnumerable<IMember>) @object)
          dictionary.Add(member.Name, Gsc.DOM.MiniJSON.Json.Deserialize(member.Value));
        return (object) dictionary;
      }
      if (node.IsArray())
      {
        IArray array = node.GetArray();
        List<object> objectList = new List<object>(array.Length);
        foreach (IValue node1 in (IEnumerable<IValue>) array)
          objectList.Add(Gsc.DOM.MiniJSON.Json.Deserialize(node1));
        return (object) objectList;
      }
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
