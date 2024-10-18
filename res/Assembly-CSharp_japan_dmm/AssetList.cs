// Decompiled with JetBrains decompiler
// Type: AssetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

#nullable disable
public class AssetList
{
  private int mRevision;
  private List<AssetList.Item> mItems = new List<AssetList.Item>();
  private Dictionary<string, AssetList.Item> mPath2Item = new Dictionary<string, AssetList.Item>();
  private Dictionary<uint, AssetList.Item> mID2Item = new Dictionary<uint, AssetList.Item>();
  private const string AssetsPrefix = "Assets/";
  private const string ResourcesPrefix = "Assets/Resources/";
  private const string StreamingAssetsPrefix = "Assets/StreamingAssets/";

  public AssetList()
  {
  }

  public AssetList(int revision) => this.mRevision = revision;

  public int Revision
  {
    get => this.mRevision;
    set => this.mRevision = value;
  }

  public AssetList.Item[] Assets => this.mItems.ToArray();

  public void SetPath(AssetList.Item item, string path)
  {
    if (!string.IsNullOrEmpty(item.Path))
      this.mPath2Item.Remove(item.Path);
    item.SetPath(path);
    if (string.IsNullOrEmpty(path))
      return;
    this.mPath2Item[path] = item;
  }

  public AssetList.Item AddItem(string IDStr)
  {
    return this.AddItem(uint.Parse(IDStr, NumberStyles.HexNumber));
  }

  public AssetList.Item AddItem(uint ID)
  {
    AssetList.Item itemById = this.FindItemByID(ID);
    if (itemById != null)
      return itemById;
    AssetList.Item obj = new AssetList.Item();
    obj.ID = ID;
    obj.IDStr = ID.ToString("X8").ToLower();
    this.mItems.Add(obj);
    if (!this.mID2Item.ContainsKey(obj.ID))
      this.mID2Item.Add(obj.ID, obj);
    return obj;
  }

  public AssetList.Item FindItemByID(string idstr) => this.FastFindItemByID(idstr);

  public AssetList.Item FindItemByID(uint id) => this.FastFindItemByID(id);

  public AssetList.Item GetItem(int idx)
  {
    return idx < 0 || this.mItems.Count < idx ? (AssetList.Item) null : this.mItems[idx];
  }

  public void UpdateIndex()
  {
    for (int index = 0; index < this.mItems.Count; ++index)
      this.mItems[index].Index = index;
  }

  public AssetList.Item FindItemByPath(string path)
  {
    try
    {
      return this.mPath2Item[path];
    }
    catch (Exception ex)
    {
    }
    return (AssetList.Item) null;
  }

  public AssetList.Item FastFindItemByID(string idstr)
  {
    return this.FastFindItemByID(uint.Parse(idstr, NumberStyles.HexNumber));
  }

  public AssetList.Item FastFindItemByID(uint id)
  {
    AssetList.Item itemById = (AssetList.Item) null;
    this.mID2Item.TryGetValue(id, out itemById);
    return itemById;
  }

  public void SetDependencies(uint ID, uint[] dependencies)
  {
    AssetList.Item obj = this.AddItem(ID);
    obj.Dependencies = new AssetList.Item[dependencies.Length];
    for (int index = 0; index < dependencies.Length; ++index)
      obj.Dependencies[index] = this.AddItem(dependencies[index]);
  }

  public List<AssetList.Item> GetAssetDependencies(string path)
  {
    Dictionary<uint, AssetList.Item> dependencies = new Dictionary<uint, AssetList.Item>();
    this.GetAssetDependencies(ref dependencies, path);
    return dependencies.Select<KeyValuePair<uint, AssetList.Item>, AssetList.Item>((Func<KeyValuePair<uint, AssetList.Item>, AssetList.Item>) (pair => pair.Value)).ToList<AssetList.Item>();
  }

  private void GetAssetDependencies(ref Dictionary<uint, AssetList.Item> dependencies, string path)
  {
    AssetList.Item itemByPath = this.FindItemByPath(path);
    if (itemByPath == null || dependencies.ContainsKey(itemByPath.ID))
      return;
    dependencies.Add(itemByPath.ID, itemByPath);
    for (int index = 0; index < itemByPath.Dependencies.Length; ++index)
    {
      AssetList.Item dependency = itemByPath.Dependencies[index];
      if (dependency != null)
        this.GetAssetDependencies(ref dependencies, dependency.Path);
    }
    for (int index = 0; index < itemByPath.AdditionalDependencies.Length; ++index)
    {
      AssetList.Item additionalDependency = itemByPath.AdditionalDependencies[index];
      if (additionalDependency != null)
        this.GetAssetDependencies(ref dependencies, additionalDependency.Path);
    }
    for (int index = 0; index < itemByPath.AdditionalStreamingAssets.Length; ++index)
    {
      AssetList.Item additionalStreamingAsset = itemByPath.AdditionalStreamingAssets[index];
      if (additionalStreamingAsset != null)
        this.GetAssetDependencies(ref dependencies, additionalStreamingAsset.Path);
    }
  }

  public void ReadFromMemory(string src)
  {
    try
    {
      StringReader stringReader1 = new StringReader(src);
      int num = 0;
      StringReader stringReader2 = new StringReader(src);
      this.mRevision = 0;
      string str1 = stringReader2.ReadLine();
      if (str1.StartsWith("Revision:"))
      {
        int startIndex = str1.IndexOf(':') + 1;
        int.TryParse(str1.Substring(startIndex), out this.mRevision);
        ++num;
      }
      string str2;
      while ((str2 = stringReader2.ReadLine()) != null)
      {
        ++num;
        string[] strArray1 = str2.Split('\t');
        AssetList.Item obj = this.AddItem(uint.Parse(strArray1[0], NumberStyles.HexNumber));
        this.SetPath(obj, strArray1[1]);
        obj.Size = int.Parse(strArray1[2]);
        obj.Hash = uint.Parse(strArray1[3], NumberStyles.HexNumber);
        obj.Flags = (AssetBundleFlags) int.Parse(strArray1[4]);
        obj.CompressedSize = int.Parse(strArray1[8]);
        if (!string.IsNullOrEmpty(strArray1[5]))
        {
          string[] strArray2 = strArray1[5].Split(',');
          obj.Dependencies = new AssetList.Item[strArray2.Length];
          for (int index = 0; index < strArray2.Length; ++index)
            obj.Dependencies[index] = this.AddItem(strArray2[index]);
        }
        else
          obj.Dependencies = new AssetList.Item[0];
        if (!string.IsNullOrEmpty(strArray1[6]))
        {
          string[] strArray3 = strArray1[6].Split(',');
          obj.AdditionalDependencies = new AssetList.Item[strArray3.Length];
          for (int index = 0; index < strArray3.Length; ++index)
            obj.AdditionalDependencies[index] = this.AddItem(strArray3[index]);
        }
        else
          obj.AdditionalDependencies = new AssetList.Item[0];
        if (strArray1.Length > 7 && !string.IsNullOrEmpty(strArray1[7]))
        {
          string[] strArray4 = strArray1[7].Split(',');
          obj.AdditionalStreamingAssets = new AssetList.Item[strArray4.Length];
          for (int index = 0; index < strArray4.Length; ++index)
            obj.AdditionalStreamingAssets[index] = this.AddItem(strArray4[index]);
        }
        else
          obj.AdditionalStreamingAssets = new AssetList.Item[0];
      }
    }
    catch (Exception ex)
    {
      Debug.LogException(ex);
    }
  }

  public void CreateAssetRevision(string path)
  {
    using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Create)))
    {
      binaryWriter.Write(this.mRevision.ToString());
      binaryWriter.Close();
    }
  }

  public void CreateAssetList(string path)
  {
    using (BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(path, FileMode.Create)))
    {
      this.UpdateIndex();
      binaryWriter.Write(this.mRevision);
      binaryWriter.Write(this.mItems.Count);
      for (int index1 = 0; index1 < this.mItems.Count; ++index1)
      {
        binaryWriter.Write(this.mItems[index1].ID);
        binaryWriter.Write(this.mItems[index1].Size);
        binaryWriter.Write(this.mItems[index1].CompressedSize);
        if (string.IsNullOrEmpty(this.mItems[index1].Path))
          this.mItems[index1].Path = string.Empty;
        binaryWriter.Write(this.mItems[index1].Path);
        binaryWriter.Write(this.mItems[index1].PathHash);
        binaryWriter.Write(this.mItems[index1].Hash);
        binaryWriter.Write((int) this.mItems[index1].Flags);
        if (this.mItems[index1].Dependencies != null)
        {
          binaryWriter.Write(this.mItems[index1].Dependencies.Length);
          for (int index2 = 0; index2 < this.mItems[index1].Dependencies.Length; ++index2)
            binaryWriter.Write(this.mItems[index1].Dependencies[index2].Index);
        }
        else
          binaryWriter.Write(0);
        if (this.mItems[index1].AdditionalDependencies != null)
        {
          binaryWriter.Write(this.mItems[index1].AdditionalDependencies.Length);
          for (int index3 = 0; index3 < this.mItems[index1].AdditionalDependencies.Length; ++index3)
            binaryWriter.Write(this.mItems[index1].AdditionalDependencies[index3].Index);
        }
        else
          binaryWriter.Write(0);
        if (this.mItems[index1].AdditionalStreamingAssets != null)
        {
          binaryWriter.Write(this.mItems[index1].AdditionalStreamingAssets.Length);
          for (int index4 = 0; index4 < this.mItems[index1].AdditionalStreamingAssets.Length; ++index4)
            binaryWriter.Write(this.mItems[index1].AdditionalStreamingAssets[index4].Index);
        }
        else
          binaryWriter.Write(0);
      }
      binaryWriter.Close();
    }
  }

  public void ReadAssetList(string path)
  {
    try
    {
      this.ReadAssetListProc(path, ref this.mItems);
      this.UpdateIndex();
    }
    catch (Exception ex)
    {
      Debug.Log((object) ex.ToString());
    }
  }

  public void ReadAssetListProc(string path, ref List<AssetList.Item> ItemList)
  {
    using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(path, FileMode.Open)))
    {
      this.mRevision = binaryReader.ReadInt32();
      int length1 = binaryReader.ReadInt32();
      AssetList.Item[] collection = new AssetList.Item[length1];
      ItemList = (List<AssetList.Item>) null;
      ItemList = new List<AssetList.Item>((IEnumerable<AssetList.Item>) collection);
      for (int index1 = 0; index1 < length1; ++index1)
      {
        if (ItemList[index1] == null)
          ItemList[index1] = new AssetList.Item();
        AssetList.Item obj = ItemList[index1];
        obj.ID = binaryReader.ReadUInt32();
        obj.IDStr = obj.ID.ToString("X8").ToLower();
        obj.Size = binaryReader.ReadInt32();
        obj.CompressedSize = binaryReader.ReadInt32();
        obj.Path = binaryReader.ReadString();
        obj.PathHash = binaryReader.ReadInt32();
        this.SetPath(obj, obj.Path);
        obj.Hash = binaryReader.ReadUInt32();
        obj.Flags = (AssetBundleFlags) binaryReader.ReadInt32();
        int length2 = binaryReader.ReadInt32();
        if (length2 != 0)
        {
          obj.Dependencies = length2 >= 0 ? new AssetList.Item[length2] : throw new Exception("Dependencies Paramater is Broken. cnt:" + (object) length1 + "/ hash:" + obj.IDStr);
          for (int index2 = 0; index2 < obj.Dependencies.Length; ++index2)
          {
            int index3 = binaryReader.ReadInt32();
            if (this.mItems[index3] == null)
              ItemList[index3] = new AssetList.Item();
            obj.Dependencies[index2] = ItemList[index3];
          }
        }
        else
          obj.Dependencies = new AssetList.Item[0];
        int length3 = binaryReader.ReadInt32();
        if (length3 != 0)
        {
          obj.AdditionalDependencies = length3 >= 0 ? new AssetList.Item[length3] : throw new Exception("AdditionalDependencies Paramater is Broken. cnt:" + (object) length1 + "/ hash:" + obj.IDStr);
          for (int index4 = 0; index4 < obj.AdditionalDependencies.Length; ++index4)
          {
            int index5 = binaryReader.ReadInt32();
            if (this.mItems[index5] == null)
              ItemList[index5] = new AssetList.Item();
            obj.AdditionalDependencies[index4] = ItemList[index5];
          }
        }
        else
          obj.AdditionalDependencies = new AssetList.Item[0];
        int length4 = binaryReader.ReadInt32();
        if (length4 != 0)
        {
          obj.AdditionalStreamingAssets = length4 >= 0 ? new AssetList.Item[length4] : throw new Exception("AdditionalStreamingAssets Paramater is Broken. cnt:" + (object) length1 + "/ hash:" + obj.IDStr);
          for (int index6 = 0; index6 < obj.AdditionalStreamingAssets.Length; ++index6)
          {
            int index7 = binaryReader.ReadInt32();
            if (this.mItems[index7] == null)
              ItemList[index7] = new AssetList.Item();
            obj.AdditionalStreamingAssets[index6] = ItemList[index7];
          }
        }
        else
          obj.AdditionalStreamingAssets = new AssetList.Item[0];
        if (!this.mID2Item.ContainsKey(obj.ID))
          this.mID2Item.Add(obj.ID, obj);
      }
    }
  }

  public void ReadSkipAssetList(string path, ref Dictionary<uint, AssetList.Item> ItemMap)
  {
    using (BinaryReader binaryReader = new BinaryReader((Stream) File.Open(path, FileMode.Open)))
    {
      binaryReader.ReadInt32();
      int num1 = binaryReader.ReadInt32();
      for (int index1 = 0; index1 < num1; ++index1)
      {
        AssetList.Item obj = new AssetList.Item()
        {
          ID = binaryReader.ReadUInt32()
        };
        obj.IDStr = obj.ID.ToString("X8").ToLower();
        obj.Size = binaryReader.ReadInt32();
        obj.CompressedSize = binaryReader.ReadInt32();
        obj.Path = binaryReader.ReadString();
        obj.PathHash = binaryReader.ReadInt32();
        obj.Hash = binaryReader.ReadUInt32();
        obj.Flags = (AssetBundleFlags) binaryReader.ReadInt32();
        for (int index2 = 0; index2 < 3; ++index2)
        {
          int num2 = binaryReader.ReadInt32();
          if (num2 < 0)
            throw new Exception("Paramater is Broken. cnt:" + (object) num1 + "/ hash:" + obj.IDStr);
          if (num2 != 0)
            binaryReader.ReadBytes(num2 * 4);
        }
        if (!ItemMap.ContainsKey(obj.ID))
          ItemMap.Add(obj.ID, obj);
      }
    }
  }

  public void WriteTo(string path)
  {
    this.ConvertAssetsPath();
    using (StreamWriter streamWriter = new StreamWriter(path))
    {
      streamWriter.WriteLine("Revision:" + (object) this.mRevision);
      for (int index1 = 0; index1 < this.mItems.Count; ++index1)
      {
        string str1 = string.Empty;
        List<string> stringList1 = new List<string>();
        if (this.mItems[index1].Dependencies != null)
        {
          for (int index2 = 0; index2 < this.mItems[index1].Dependencies.Length; ++index2)
          {
            if (this.mItems[index1].Dependencies[index2] != null && this.mItems[index1].Dependencies[index2].ID != 0U)
              stringList1.Add(this.mItems[index1].Dependencies[index2].IDStr);
          }
          stringList1.Sort();
          str1 = string.Join(",", stringList1.ToArray());
        }
        string str2 = string.Empty;
        List<string> stringList2 = new List<string>();
        if (this.mItems[index1].AdditionalDependencies != null)
        {
          for (int index3 = 0; index3 < this.mItems[index1].AdditionalDependencies.Length; ++index3)
          {
            if (this.mItems[index1].AdditionalDependencies[index3] != null && this.mItems[index1].AdditionalDependencies[index3].ID != 0U)
              stringList2.Add(this.mItems[index1].AdditionalDependencies[index3].IDStr);
          }
          stringList2.Sort();
          str2 = string.Join(",", stringList2.ToArray());
        }
        string str3 = string.Empty;
        List<string> stringList3 = new List<string>();
        if (this.mItems[index1].AdditionalStreamingAssets != null)
        {
          for (int index4 = 0; index4 < this.mItems[index1].AdditionalStreamingAssets.Length; ++index4)
          {
            if (this.mItems[index1].AdditionalStreamingAssets[index4] != null && this.mItems[index1].AdditionalStreamingAssets[index4].ID != 0U)
              stringList3.Add(this.mItems[index1].AdditionalStreamingAssets[index4].IDStr);
          }
          stringList3.Sort();
          str3 = string.Join(",", stringList3.ToArray());
        }
        streamWriter.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", (object) this.mItems[index1].IDStr, (object) this.mItems[index1].Path, (object) this.mItems[index1].Size, (object) this.mItems[index1].Hash.ToString("X8"), (object) (int) this.mItems[index1].Flags, (object) str1, (object) str2, (object) str3, (object) this.mItems[index1].CompressedSize);
      }
    }
  }

  public void ConvertAssetsPath()
  {
    for (int index = 0; index < this.mItems.Count; ++index)
    {
      string empty = string.Empty;
      string key;
      if (!string.IsNullOrEmpty(this.mItems[index].Path))
      {
        if (this.mItems[index].Path.StartsWith("Assets/Resources/"))
        {
          string path = this.mItems[index].Path.Substring("Assets/Resources/".Length);
          string directoryName = Path.GetDirectoryName(path);
          string withoutExtension = Path.GetFileNameWithoutExtension(path);
          key = string.IsNullOrEmpty(directoryName) ? withoutExtension : directoryName + "/" + withoutExtension;
        }
        else
          key = !this.mItems[index].Path.StartsWith("Assets/StreamingAssets/") ? ((this.mItems[index].Flags & AssetBundleFlags.Scene) == (AssetBundleFlags) 0 ? this.mItems[index].Path : Path.GetFileNameWithoutExtension(this.mItems[index].Path)) : this.mItems[index].Path.Substring("Assets/".Length);
      }
      else
        key = string.Empty;
      this.mItems[index].Path = key;
      if (!this.mPath2Item.ContainsKey(key))
        this.mPath2Item.Add(key, this.mItems[index]);
      else
        this.mPath2Item[key] = this.mItems[index];
    }
  }

  public class Item
  {
    public uint ID;
    public string IDStr;
    public int Size;
    public int CompressedSize;
    public string Path;
    public int PathHash;
    public uint Hash;
    public AssetBundleFlags Flags;
    public AssetList.Item[] Dependencies;
    public AssetList.Item[] AdditionalDependencies;
    public AssetList.Item[] AdditionalStreamingAssets;
    public bool Exist;
    public int Index;
    public StackTrace DownloadCaller;

    public void SetPath(string _path)
    {
      this.Path = _path;
      this.PathHash = _path.GetHashCode();
    }

    public void SetDownloadCaller() => this.DownloadCaller = new StackTrace(1, true);

    public bool IsAssetDownloadApproved { get; private set; }

    public void SetDownloadApproved(bool value) => this.IsAssetDownloadApproved = value;

    public void ResetDownloadApproved() => this.IsAssetDownloadApproved = false;
  }
}
