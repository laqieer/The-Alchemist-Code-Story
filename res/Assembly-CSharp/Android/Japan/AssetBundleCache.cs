// Decompiled with JetBrains decompiler
// Type: AssetBundleCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

  public void AddReferencer(int count)
  {
    this.NumReferencers += count;
  }

  public void RemoveReferencer(int count)
  {
    this.NumReferencers -= count;
  }

  public void Unload()
  {
    if (!((Object) this.AssetBundle != (Object) null))
      return;
    this.AssetBundle.Unload(false);
    this.AssetBundle = (AssetBundle) null;
  }
}
