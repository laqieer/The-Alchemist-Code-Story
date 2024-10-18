// Decompiled with JetBrains decompiler
// Type: SRPG.LocalVariable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [DisallowMultipleComponent]
  public class LocalVariable : MonoBehaviour
  {
    private Dictionary<string, string> mVariables = new Dictionary<string, string>();

    public bool Exists(string key) => this.mVariables.ContainsKey(key);

    public void Set(string key, string val)
    {
      if (!this.Exists(key))
        this.mVariables.Add(key, val);
      else
        this.mVariables[key] = val;
    }

    public bool Equal(string key, string val) => this.Exists(key) && this.mVariables[key] == val;
  }
}
