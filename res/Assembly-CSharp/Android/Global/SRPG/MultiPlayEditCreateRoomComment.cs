// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayEditCreateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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

    public void OnEndEdit()
    {
      this.Comment.readOnly = true;
    }
  }
}
