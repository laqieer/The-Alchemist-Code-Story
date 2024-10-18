// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditMultiPlayRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
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
    // ISSUE: method pointer
    ((UnityEvent<string>) this.Target.onEndEdit).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__0)));
    ((Behaviour) this).enabled = true;
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || this.Target.onEndEdit == null)
      return;
    ((UnityEventBase) this.Target.onEndEdit).RemoveAllListeners();
  }

  private void OnEndEdit(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    if (this.CreateMode)
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, field.text);
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
