// Decompiled with JetBrains decompiler
// Type: SRPG.GachaOffline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GachaOffline
  {
    private List<KeyValuePair<int, string>> MakeDropTable(string tablePath)
    {
      List<KeyValuePair<int, string>> keyValuePairList = new List<KeyValuePair<int, string>>();
      TextAsset textAsset = Resources.Load<TextAsset>(tablePath);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) textAsset, (UnityEngine.Object) null))
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
      int num1 = 0;
      foreach (KeyValuePair<int, string> keyValuePair in keyValuePairList)
        num1 += keyValuePair.Key;
      int num2 = Random.Range(0, num1);
      foreach (KeyValuePair<int, string> keyValuePair in keyValuePairList)
      {
        num2 -= keyValuePair.Key;
        if (num2 < 0)
          return keyValuePair.Value;
      }
      return "none";
    }
  }
}
