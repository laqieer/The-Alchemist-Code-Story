// Decompiled with JetBrains decompiler
// Type: SetupNetworkVersion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

  private void Start()
  {
    this.uiRoot.SetActive(false);
  }
}
