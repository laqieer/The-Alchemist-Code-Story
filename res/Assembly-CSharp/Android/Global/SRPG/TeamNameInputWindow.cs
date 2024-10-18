// Decompiled with JetBrains decompiler
// Type: SRPG.TeamNameInputWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
