﻿// Decompiled with JetBrains decompiler
// Type: UnManagedAssetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

public class UnManagedAssetList
{
  public Dictionary<string, int> mAssets = new Dictionary<string, int>();

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
            this.mAssets.Add(strArray[0], int.Parse(strArray[1]));
        }
      }
    }
  }

  public int GetSize(string name)
  {
    int num = 0;
    this.mAssets.TryGetValue(name, out num);
    return num;
  }
}
