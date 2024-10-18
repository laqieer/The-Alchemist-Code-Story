// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayEditCreateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
