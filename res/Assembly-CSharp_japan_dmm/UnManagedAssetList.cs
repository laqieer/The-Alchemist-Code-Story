// Decompiled with JetBrains decompiler
// Type: UnManagedAssetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.IO;

#nullable disable
public class UnManagedAssetList
{
  public Dictionary<string, UnManagedAssetList.Item> mAssets = new Dictionary<string, UnManagedAssetList.Item>();

  public void Setup(string path)
  {
    this.mAssets.Clear();
    using (StreamReader streamReader = new StreamReader(path))
    {
      while (!streamReader.EndOfStream)
      {
        string str = streamReader.ReadLine();
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split('\t');
          if (strArray.Length >= 2)
            this.mAssets.Add(strArray[0], new UnManagedAssetList.Item(strArray[0], int.Parse(strArray[1])));
        }
      }
    }
  }

  public int GetSize(string name)
  {
    UnManagedAssetList.Item obj = (UnManagedAssetList.Item) null;
    int size = 0;
    if (this.mAssets.TryGetValue(name, out obj))
      size = obj.Size;
    return size;
  }

  public UnManagedAssetList.Item FindByItemName(string name)
  {
    UnManagedAssetList.Item byItemName = (UnManagedAssetList.Item) null;
    this.mAssets.TryGetValue(name, out byItemName);
    return byItemName;
  }

  public class Item
  {
    private string m_Path;
    private int m_Size;

    public Item(string path, int size)
    {
      this.m_Path = path;
      this.m_Size = size;
    }

    public string Path => this.m_Path;

    public int Size => this.m_Size;

    public bool IsAssetDownloadApproved { get; private set; }

    public void SetDownloadApproved(bool value) => this.IsAssetDownloadApproved = value;

    public void ResetDownloadApproved() => this.IsAssetDownloadApproved = false;
  }
}
