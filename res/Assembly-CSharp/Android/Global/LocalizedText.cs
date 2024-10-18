﻿// Decompiled with JetBrains decompiler
// Type: LocalizedText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalizedText
{
  public static bool UseAssetManager = true;
  private static List<LocalizedText.TextTable> mTables = new List<LocalizedText.TextTable>();
  private static string mLanguageCode = "english";

  public static string SGGet(string language, string tableID, string textID)
  {
    if (string.IsNullOrEmpty(textID))
      return textID;
    if (string.IsNullOrEmpty(tableID))
      return tableID;
    string str = (LocalizedText.FindTable(tableID) ?? LocalizedText.InternalLoadTable(language, tableID, (LocalizedText.TextTable) null)).FindText(textID);
    if (string.IsNullOrEmpty(str) || str.Equals(textID) || GameUtility.ShowEnumOnly)
      str = tableID + (object) '.' + textID;
    return str;
  }

  private static string ComposeTablePath(string language, string tableID)
  {
    return "Loc/" + language + "/" + tableID;
  }

  private static LocalizedText.TextTable InternalLoadTable(string language, string tableID, LocalizedText.TextTable overwriteTable = null)
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
    string path = LocalizedText.ComposeTablePath(language, tableID);
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
      Debug.LogWarning((object) ("SG: Loading text table '" + tableID + "'"));
      char[] separator = new char[1]{ '\t' };
      using (StringReader stringReader = new StringReader(s))
      {
        string end = stringReader.ReadToEnd();
        char[] chArray = new char[1]{ '\n' };
        foreach (string str in end.Split(chArray))
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

  public static void UnloadAll()
  {
    LocalizedText.mTables.Clear();
  }

  public static string LanguageCode
  {
    get
    {
      return GameUtility.Config_Language;
    }
    set
    {
      GameUtility.Config_Language = value;
    }
  }

  private static LocalizedText.TextTable FindTable(string tableID)
  {
    int hashCode = tableID.GetHashCode();
    for (int index = 0; index < LocalizedText.mTables.Count; ++index)
    {
      if (LocalizedText.mTables[index].HashCode == hashCode && LocalizedText.mTables[index].TableID == tableID)
        return LocalizedText.mTables[index];
    }
    return (LocalizedText.TextTable) null;
  }

  public static string ComposeTablePath(string tableID)
  {
    if (GameUtility.Config_Language == "None")
      return "Loc/english/" + tableID;
    return "Loc/" + LocalizedText.LanguageCode + "/" + tableID;
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
      DebugUtility.LogWarning("Failed to load text '" + path + "'");
      LocalizedText.mTables.Remove(textTable);
      textTable = (LocalizedText.TextTable) null;
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
    overwriteTable.AutoUnload = false;
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
    string format = LocalizedText.Get(formatKey);
    try
    {
      return string.Format(format, args);
    }
    catch (Exception ex)
    {
      return format;
    }
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
    string newValue1 = string.Format(LocalizedText.Get("sys.BIRTHDAY_FORMAT"), (object) GlobalVars.EditedYear, (object) GlobalVars.EditedMonth, (object) GlobalVars.EditedDay);
    text = text.Replace("<birthday>", newValue1);
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
    string newValue2 = string.Format(LocalizedText.Get("sys.FB_DATAFOUND_MESSAGE"), (object) GlobalVars.NewPlayerLevel, (object) GlobalVars.NewPlayerName);
    text = text.Replace("<fb_load>", newValue2);
    text = text.Replace("<unlock_level>", GlobalVars.EventQuestMinimumLevel.ToString());
    return text;
  }

  public static string Get(string key, ref bool success)
  {
    if (string.IsNullOrEmpty(key) || GameUtility.ShowEnumOnly)
      return key;
    int length = key.IndexOf(".");
    if (length < 0)
      return key;
    string tableID = key.Substring(0, length);
    string key1 = key.Substring(length + 1);
    LocalizedText.TextTable textTable = LocalizedText.FindTable(tableID);
    if (textTable == null)
    {
      textTable = LocalizedText.InternalLoadTable(tableID, (LocalizedText.TextTable) null);
      if (textTable == null)
        return key;
    }
    string text = textTable.FindText(key1);
    success = !string.IsNullOrEmpty(text) && !text.Equals(key1);
    return text;
  }

  private class TableUnloader
  {
    ~TableUnloader()
    {
      for (int index = LocalizedText.mTables.Count - 1; index >= 0; --index)
      {
        if (LocalizedText.mTables[index].AutoUnload)
          LocalizedText.mTables.RemoveAt(index--);
      }
    }
  }

  private class TextTable
  {
    public Dictionary<string, string> Items = new Dictionary<string, string>();
    public string TableID;
    public int HashCode;
    public bool AutoUnload;

    public TextTable(string tableID)
    {
      this.TableID = tableID;
      this.HashCode = tableID.GetHashCode();
    }

    public string FindText(string key)
    {
      if (this.Items.ContainsKey(key))
        return this.Items[key];
      return key;
    }
  }
}
