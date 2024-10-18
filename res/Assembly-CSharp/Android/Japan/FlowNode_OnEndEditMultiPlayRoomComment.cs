// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditMultiPlayRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnEndEditMultiPlayRoomComment", 58751)]
[FlowNode.Pin(1, "Edited", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(2, "Error", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnEndEditMultiPlayRoomComment : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField Target;
  public bool CreateMode;

  private void Start()
  {
    if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
      return;
    string str = string.Empty;
    MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
    JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
    if (myPhotonRoomParam != null)
      str = myPhotonRoomParam.comment;
    if (this.CreateMode)
      str = this.GetComment();
    if (this.Target is InputFieldCensorship)
      ((InputFieldCensorship) this.Target).text = str;
    else
      this.Target.text = str;
    GlobalVars.EditMultiPlayRoomComment = str;
    this.Target.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnEndEdit(this.Target)));
    this.enabled = true;
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null) || this.Target.onEndEdit == null)
      return;
    this.Target.onEndEdit.RemoveAllListeners();
  }

  private void OnEndEdit(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    if (this.CreateMode)
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, field.text, false);
    DebugUtility.Log("OnEndEditRoomName:" + field.text);
    GlobalVars.EditMultiPlayRoomComment = field.text;
    this.Activate(1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }

  private string GetComment()
  {
    string name;
    if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY))
    {
      int multiRoomCommentMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.MultiRoomCommentMax;
      name = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
      if (!string.IsNullOrEmpty(name) && name.Length > multiRoomCommentMax)
        name = name.Substring(0, multiRoomCommentMax);
      if (string.IsNullOrEmpty(name))
        name = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
      if (!MyMsgInput.isLegal(name))
        name = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
    }
    else
      name = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
    return name;
  }
}
