// Decompiled with JetBrains decompiler
// Type: SRPG.JsonUtlity.LoginBonusJsonParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG.JsonUtlity
{
  public static class LoginBonusJsonParser
  {
    public static Dictionary<string, object> Deserialize(string json)
    {
      if (string.IsNullOrEmpty(json))
        return (Dictionary<string, object>) null;
      Dictionary<string, object> dictionary1 = MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
      Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
      foreach (string key1 in dictionary1.Keys)
      {
        string[] strArray = key1.Split('~');
        Dictionary<string, object> dictionary3 = dictionary2;
        Dictionary<string, object> dictionary4 = (Dictionary<string, object>) null;
        string index = string.Empty;
        foreach (string key2 in strArray)
        {
          if (!dictionary3.ContainsKey(key2))
            dictionary3[key2] = (object) new Dictionary<string, object>();
          dictionary4 = dictionary3;
          index = key2;
          dictionary3 = dictionary3[key2] as Dictionary<string, object>;
        }
        dictionary4[index] = dictionary1[key1];
      }
      return dictionary2;
    }
  }
}
