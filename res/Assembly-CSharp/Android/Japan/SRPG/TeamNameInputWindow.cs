// Decompiled with JetBrains decompiler
// Type: SRPG.TeamNameInputWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
