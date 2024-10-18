// Decompiled with JetBrains decompiler
// Type: UnManagedAssetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

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
    int num = 0;
    if (this.mAssets.TryGetValue(name, out obj))
      num = obj.Size;
    return num;
  }

  public UnManagedAssetList.Item FindByItemName(string name)
  {
    UnManagedAssetList.Item obj = (UnManagedAssetList.Item) null;
    this.mAssets.TryGetValue(name, out obj);
    return obj;
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

    public string Path
    {
      get
      {
        return this.m_Path;
      }
    }

    public int Size
    {
      get
      {
        return this.m_Size;
      }
    }

    public bool IsAssetDownloadApproved { get; private set; }

    public void SetDownloadApproved(bool value)
    {
      this.IsAssetDownloadApproved = value;
    }

    public void ResetDownloadApproved()
    {
      this.IsAssetDownloadApproved = false;
    }
  }
}
