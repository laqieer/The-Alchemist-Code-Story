// Decompiled with JetBrains decompiler
// Type: SRPG.TeamNameInputWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TeamNameInputWindow : MonoBehaviour
  {
    [SerializeField]
    private InputFieldCensorship inputField;

    public void SetInputName()
    {
      if (string.IsNullOrEmpty(this.inputField.text))
        return;
      string str = this.inputField.text;
      if (str.Length > this.inputField.characterLimit)
        str = str.Substring(0, this.inputField.characterLimit);
      GlobalVars.TeamName = str;
    }
  }
}
