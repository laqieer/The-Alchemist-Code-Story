// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayEditCreateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MultiPlayEditCreateRoomComment : MonoBehaviour
  {
    public InputFieldCensorship Comment;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnClickEdit()
    {
      this.Comment.readOnly = false;
      this.Comment.ActivateInputField();
    }

    public void OnEndEdit() => this.Comment.readOnly = true;
  }
}
