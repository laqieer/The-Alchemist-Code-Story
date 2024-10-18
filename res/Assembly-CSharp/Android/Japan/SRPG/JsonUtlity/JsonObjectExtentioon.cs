// Decompiled with JetBrains decompiler
// Type: SRPG.JsonUtlity.JsonObjectExtentioon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.ComponentModel;

namespace SRPG.JsonUtlity
{
  public static class JsonObjectExtentioon
  {
    public static bool TryGetValueAndCast<T>(this Dictionary<string, object> json, string key, out T val, bool isRestrict = false)
    {
      val = default (T);
      object obj;
      if (!json.TryGetValue(key, out obj))
      {
        JsonObjectExtentioon.OutputDebugLog("キー [" + key + "] が見つかりません。", isRestrict);
      }
      else
      {
        try
        {
          if (obj is T)
          {
            val = (T) obj;
            return true;
          }
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
          val = (T) converter.ConvertTo(obj, typeof (T));
          return true;
        }
        catch
        {
          JsonObjectExtentioon.OutputDebugLog("値 [" + obj + "] は" + obj.GetType().Name + "です。" + typeof (T).Name + "にキャストできません。", isRestrict);
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
