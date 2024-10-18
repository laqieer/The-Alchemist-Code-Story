// Decompiled with JetBrains decompiler
// Type: SRPG.GachaOffline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GachaOffline
  {
    private List<KeyValuePair<int, string>> MakeDropTable(string tablePath)
    {
      List<KeyValuePair<int, string>> keyValuePairList = new List<KeyValuePair<int, string>>();
      TextAsset textAsset = Resources.Load<TextAsset>(tablePath);
      if ((UnityEngine.Object) textAsset == (UnityEngine.Object) null)
      {
        DebugUtility.LogWarning("ドロップテーブル '" + tablePath + "' の読み込みに失敗しました。");
        return new List<KeyValuePair<int, string>>();
      }
      string[] strArray = textAsset.text.Replace("\r\n", "\n").Split('\n');
      string str = (string) null;
      foreach (string s in strArray)
      {
        int key;
        try
        {
          key = int.Parse(s);
        }
        catch (Exception ex)
        {
          str = s;
          if (s.IndexOf("\r") != -1)
          {
            str = str.Replace("\r", string.Empty);
            continue;
          }
          continue;
        }
        keyValuePairList.Add(new KeyValuePair<int, string>(key, str));
      }
      string msg = string.Empty;
      foreach (KeyValuePair<int, string> keyValuePair in keyValuePairList)
        msg = msg + (object) keyValuePair.Key + ": " + keyValuePair.Value + "\n";
      DebugUtility.Log("# drop table #");
      DebugUtility.Log(msg);
      DebugUtility.Log("##############");
      return keyValuePairList;
    }

    public string ExecGacha(string fileName)
    {
      List<KeyValuePair<int, string>> keyValuePairList = this.MakeDropTable("LocalMaps/drop/" + fileName);
      int max = 0;
      foreach (KeyValuePair<int, string> keyValuePair in keyValuePairList)
        max += keyValuePair.Key;
      int num = Random.Range(0, max);
      foreach (KeyValuePair<int, string> keyValuePair in keyValuePairList)
      {
        num -= keyValuePair.Key;
        if (num < 0)
          return keyValuePair.Value;
      }
      return "none";
    }
  }
}
