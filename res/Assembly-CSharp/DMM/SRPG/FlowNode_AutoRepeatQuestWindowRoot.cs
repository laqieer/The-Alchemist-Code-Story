// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AutoRepeatQuestWindowRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("AutoRepeatQuest/ProgressWindow", 32741)]
  [FlowNode.Pin(10, "自動周回中かチェック", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "OK", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "NG", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_AutoRepeatQuestWindowRoot : FlowNode
  {
    private const int PIN_INPUT_CHECK = 10;
    private const int PIN_OUTPUT_OK = 100;
    private const int PIN_OUTPUT_NG = 110;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string mPrefabPath = "UI/AutoRepeatQuest/AutoRepeatQuestWindowRoot";
    [SerializeField]
    private bool mConfirm = true;
    [SerializeField]
    private bool mExit;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.IsExistRecord)
      {
        if (this.mConfirm)
          UIUtility.ConfirmBox(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_PROGRESS_CONFIRM"), (string) null, (UIUtility.DialogResultEvent) (go1 => this.CreateProgressWindow()), (UIUtility.DialogResultEvent) (go2 => this.ActivateOutputLinks(110)));
        else
          this.CreateProgressWindow();
      }
      else
        this.ActivateOutputLinks(100);
    }

    private void CreateProgressWindow()
    {
      GameObject gameObject = AssetManager.Load<GameObject>(this.mPrefabPath);
      if (Object.op_Equality((Object) gameObject, (Object) null))
      {
        DebugUtility.LogError("Failed to load '" + this.mPrefabPath + "'");
      }
      else
      {
        // ISSUE: method pointer
        Object.Instantiate<GameObject>(gameObject).RequireComponent<GameObjectCallBack>().onDestroy.AddListener(new UnityAction((object) this, __methodptr(\u003CCreateProgressWindow\u003Em__2)));
      }
    }
  }
}
