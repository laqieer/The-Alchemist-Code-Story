// Decompiled with JetBrains decompiler
// Type: SRPG.RaidMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RaidMaster
  {
    public static void Deserialize<T, U>(ref List<T> obj, U[] json)
      where T : RaidMasterParam<U>, new()
      where U : JSON_RaidMasterParam
    {
      if (json == null)
        return;
      if (obj == null)
        obj = new List<T>();
      for (int index = 0; index < json.Length; ++index)
      {
        if ((object) json[index] != null)
        {
          T obj1 = new T();
          if (obj1.Deserialize(json[index]))
            obj.Add(obj1);
        }
      }
    }
  }
}
