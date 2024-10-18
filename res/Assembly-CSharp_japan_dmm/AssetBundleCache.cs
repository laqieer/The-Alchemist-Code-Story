// Decompiled with JetBrains decompiler
// Type: AssetBundleCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AssetBundleCache
{
  public string Name;
  public int HashCode;
  public AssetBundle AssetBundle;
  public bool Persistent;
  public AssetBundleCache[] Dependencies;
  public int NumReferencers;

  public AssetBundleCache(string name, AssetBundle ab)
  {
    this.Name = name;
    this.HashCode = name.GetHashCode();
    this.AssetBundle = ab;
  }

  public void AddReferencer(int count) => this.NumReferencers += count;

  public void RemoveReferencer(int count) => this.NumReferencers -= count;

  public void Unload()
  {
    if (!Object.op_Inequality((Object) this.AssetBundle, (Object) null))
      return;
    this.AssetBundle.Unload(false);
    this.AssetBundle = (AssetBundle) null;
  }
}
