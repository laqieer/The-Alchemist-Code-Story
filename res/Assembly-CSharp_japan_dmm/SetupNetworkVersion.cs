// Decompiled with JetBrains decompiler
// Type: SetupNetworkVersion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class SetupNetworkVersion : MonoBehaviour
{
  [SerializeField]
  private GameObject uiRoot;
  [SerializeField]
  private Toggle toggle;
  [SerializeField]
  private SRPG_InputField versionInputField;
  [SerializeField]
  private Dropdown serverNameDropdown;
  [SerializeField]
  private List<string> serverList;

  private void Start() => this.uiRoot.SetActive(false);
}
