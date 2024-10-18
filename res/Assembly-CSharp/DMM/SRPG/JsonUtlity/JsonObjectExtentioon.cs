// Decompiled with JetBrains decompiler
// Type: SRPG.JsonUtlity.JsonObjectExtentioon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace SRPG.JsonUtlity
{
  public static class JsonObjectExtentioon
  {
    public static bool TryGetValueAndCast<T>(
      this Dictionary<string, object> json,
      string key,
      out T val,
      bool isRestrict = false)
    {
      val = default (T);
      object obj1;
      if (!json.TryGetValue(key, out obj1))
      {
        JsonObjectExtentioon.OutputDebugLog("キー [" + key + "] が見つかりません。", isRestrict);
      }
      else
      {
        try
        {
          if (obj1 is T obj2)
          {
            val = obj2;
            return true;
          }
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
          val = (T) converter.ConvertTo(obj1, typeof (T));
          return true;
        }
        catch
        {
          JsonObjectExtentioon.OutputDebugLog("値 [" + obj1 + "] は" + obj1.GetType().Name + "です。" + typeof (T).Name + "にキャストできません。", isRestrict);
        }
      }
      return false;
    }

    private static void OutputDebugLog(string msg, bool isRestrict)
    {
      if (isRestrict)
        DebugUtility.LogError(msg);
      else
        DebugUtility.Log(msg);
    }
  }
}
