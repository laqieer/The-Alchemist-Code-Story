// Decompiled with JetBrains decompiler
// Type: SRPG.JsonUtlity.LoginBonusJsonParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
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
        string key2 = string.Empty;
        foreach (string key3 in strArray)
        {
          if (!dictionary3.ContainsKey(key3))
            dictionary3[key3] = (object) new Dictionary<string, object>();
          dictionary4 = dictionary3;
          key2 = key3;
          dictionary3 = dictionary3[key3] as Dictionary<string, object>;
        }
        dictionary4[key2] = dictionary1[key1];
      }
      return dictionary2;
    }
  }
}
