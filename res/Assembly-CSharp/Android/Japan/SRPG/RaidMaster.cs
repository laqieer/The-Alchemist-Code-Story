// Decompiled with JetBrains decompiler
// Type: SRPG.RaidMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class RaidMaster
  {
    public static void Deserialize<T, U>(ref List<T> obj, U[] json) where T : RaidMasterParam<U>, new() where U : JSON_RaidMasterParam
    {
      if (json == null)
        return;
      if (obj == null)
        obj = new List<T>();
      for (int index = 0; index < json.Length; ++index)
      {
        if ((object) json[index] != null)
        {
          T instance = Activator.CreateInstance<T>();
          if (instance.Deserialize(json[index]))
            obj.Add(instance);
        }
      }
    }
  }
}
