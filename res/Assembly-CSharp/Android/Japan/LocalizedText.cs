// Decompiled with JetBrains decompiler
// Type: LocalizedText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class LocalizedText
{
  private static bool s_UseAssetManager = true;
  private static List<LocalizedText.TextTable> mTables = new List<LocalizedText.TextTable>();
  private static string mLanguageCode = "japanese";
  public const int BYTE_SIZE_FOR_AUTO_UNLOAD = 307200;

  public static bool UseAssetManager
  {
    get
    {
      return LocalizedText.s_UseAssetManager;
    }
    set
    {
      LocalizedText.s_UseAssetManager = value;
    }
  }

  public static List<LocalizedText.TextTable> Tables
  {
    get
    {
      return LocalizedText.mTables;
    }
  }

  public static string LanguageCode
  {
    get
    {
      return LocalizedText.mLanguageCode;
    }
    set
    {
      LocalizedText.mLanguageCode = value;
    }
  }

  private static LocalizedText.TextTable FindTable(string tableID)
  {
    int hashCode = tableID.GetHashCode();
    for (int index = 0; index < LocalizedText.mTables.Count; ++index)
    {
      if (LocalizedText.mTables[index].HashCode == hashCode && LocalizedText.mTables[index].TableID == tableID)
      {
        LocalizedText.TextTable mTable = LocalizedText.mTables[index];
        if (!mTable.DonotUnload && index < LocalizedText.mTables.Count - 1)
        {
          LocalizedText.mTables.RemoveAt(index);
          LocalizedText.mTables.Add(mTable);
        }
        return mTable;
      }
    }
    return (LocalizedText.TextTable) null;
  }

  public static string ComposeTablePath(string tableID)
  {
    return "Loc/" + LocalizedText.mLanguageCode + "/" + tableID;
  }

  public static int GetAllTextByteSize()
  {
    int byteSize = 0;
    LocalizedText.mTables.ForEach((Action<LocalizedText.TextTable>) (table =>
    {
      if (table.DonotUnload)
        return;
      byteSize += table.ByteSize;
    }));
    return byteSize;
  }

  private static LocalizedText.TextTable InternalLoadTable(string tableID, LocalizedText.TextTable overwriteTable = null)
  {
    LocalizedText.TextTable textTable;
    if (overwriteTable == null)
    {
      textTable = new LocalizedText.TextTable(tableID);
      LocalizedText.mTables.Add(textTable);
    }
    else
    {
      textTable = overwriteTable;
      textTable.Items.Clear();
    }
    string path = LocalizedText.ComposeTablePath(tableID);
    string s;
    if (LocalizedText.UseAssetManager)
    {
      s = AssetManager.LoadTextData(path);
    }
    else
    {
      TextAsset textAsset = Resources.Load<TextAsset>(path);
      s = !((UnityEngine.Object) textAsset != (UnityEngine.Object) null) ? (string) null : textAsset.text;
    }
    if (s != null)
    {
      textTable.ByteSize = Encoding.UTF8.GetByteCount(s);
      char[] separator = new char[1]{ '\t' };
      using (StringReader stringReader = new StringReader(s))
      {
        string str;
        while ((str = stringReader.ReadLine()) != null)
        {
          if (!string.IsNullOrEmpty(str) && !str.StartsWith(";"))
          {
            string[] strArray = str.Split(separator, 2);
            if (strArray.Length >= 2 && !string.IsNullOrEmpty(strArray[0]) && !string.IsNullOrEmpty(strArray[1]))
              textTable.Items[strArray[0]] = strArray[1].Replace("<br>", "\n");
          }
        }
      }
    }
    else
    {
      Debug.LogError((object) ("Failed to load text '" + path + "'"));
      LocalizedText.mTables.Remove(textTable);
    }
    return textTable;
  }

  public static void UnloadAllTables()
  {
    LocalizedText.mTables.Clear();
  }

  public static void UnloadTable(string tableID)
  {
    LocalizedText.TextTable table = LocalizedText.FindTable(tableID);
    if (table == null)
      return;
    LocalizedText.mTables.Remove(table);
  }

  public static string[] GetTextIDs(string tableID)
  {
    LocalizedText.TextTable table = LocalizedText.FindTable(tableID);
    if (table != null)
      return new List<string>((IEnumerable<string>) table.Items.Keys).ToArray();
    return new string[0];
  }

  public static void LoadTable(string tableID, bool forceReload = false)
  {
    LocalizedText.TextTable overwriteTable = LocalizedText.FindTable(tableID);
    if (overwriteTable == null || forceReload)
      overwriteTable = LocalizedText.InternalLoadTable(tableID, overwriteTable);
    overwriteTable.DonotUnload = true;
  }

  public static string GetResourcePath(string key)
  {
    int length = key.IndexOf(".");
    if (length < 0)
      return (string) null;
    return LocalizedText.ComposeTablePath(key.Substring(0, length));
  }

  public static string Get(string formatKey, params object[] args)
  {
    return string.Format(LocalizedText.Get(formatKey), args);
  }

  public static string Get(string key)
  {
    bool success = false;
    return LocalizedText.Get(key, ref success);
  }

  public static string ReplaceTag(string text)
  {
    FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
    text = text.Replace("<fix:continue_cost>", fixParam.ContinueCoinCost.ToString());
    text = text.Replace("<fix:continue_cost_multi>", fixParam.ContinueCoinCostMulti.ToString());
    text = text.Replace("<fix:abilupcoin>", fixParam.AbilityRankUpCountCoin.ToString());
    PaymentManager.Product product = MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID);
    if (product != null)
    {
      if (!string.IsNullOrEmpty(product.name))
        text = text.Replace("<selected_product>", product.name);
      if (!string.IsNullOrEmpty(product.desc))
        text = text.Replace("<selecter_desc>", product.desc);
    }
    text = text.Replace("<before_coin>", GlobalVars.BeforeCoin.ToString());
    text = text.Replace("<after_coin>", GlobalVars.AfterCoin.ToString());
    string newValue = string.Format(LocalizedText.Get("sys.BIRTHDAY_FORMAT"), (object) GlobalVars.EditedYear, (object) GlobalVars.EditedMonth, (object) GlobalVars.EditedDay);
    text = text.Replace("<birthday>", newValue);
    if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
    {
      text = text.Replace("<photon_err>", SceneBattle.Instance.PhotonErrorString);
      text = text.Replace("<mp_first_contact>", SceneBattle.Instance.FirstContact.ToString());
    }
    else
    {
      text = text.Replace("<photon_err>", "0");
      text = text.Replace("<mp_first_contact>", "0");
    }
    return text;
  }

  public static string Get(string key, ref bool success)
  {
    if (string.IsNullOrEmpty(key))
      return key;
    int length = key.IndexOf(".");
    if (length < 0)
      return key;
    string tableID = key.Substring(0, length);
    string key1 = key.Substring(length + 1);
    LocalizedText.TextTable table = LocalizedText.FindTable(tableID);
    if (table == null)
    {
      table = LocalizedText.InternalLoadTable(tableID, (LocalizedText.TextTable) null);
      while (LocalizedText.GetAllTextByteSize() > 307200)
      {
        LocalizedText.TextTable textTable = LocalizedText.mTables.Find((Predicate<LocalizedText.TextTable>) (tbl => !tbl.DonotUnload && tbl != table));
        if (textTable == null)
        {
          Debug.LogWarning((object) ("It needs to extend to buffer size. : " + string.Format("{0:#,0} Bytes", (object) 307200)));
          break;
        }
        LocalizedText.UnloadTable(textTable.TableID);
        Debug.Log((object) ("Unload text table '" + textTable.TableID + "'"));
      }
    }
    string text = table.FindText(key1);
    success = !string.IsNullOrEmpty(text) && !text.Equals(key1);
    return text;
  }

  private class TableUnloader
  {
    ~TableUnloader()
    {
      for (int index = LocalizedText.mTables.Count - 1; index >= 0; --index)
      {
        if (!LocalizedText.mTables[index].DonotUnload)
          LocalizedText.mTables.RemoveAt(index--);
      }
    }
  }

  public class TextTable
  {
    public Dictionary<string, string> Items = new Dictionary<string, string>();
    public string TableID;
    public int HashCode;
    public bool DonotUnload;
    public int ByteSize;

    public TextTable(string tableID)
    {
      this.TableID = tableID;
      this.HashCode = tableID.GetHashCode();
      this.ByteSize = 0;
    }

    public string FindText(string key)
    {
      if (this.Items.ContainsKey(key))
        return this.Items[key];
      return key;
    }
  }
}
