// Decompiled with JetBrains decompiler
// Type: SRPG.QuestArchiveOpenPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/QuestArchiveOpenPopup", 32741)]
  [FlowNode.Pin(1, "決定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "キャンセル", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "開放", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "戻る", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "アイテムが足りない", FlowNode.PinTypes.Output, 102)]
  public class QuestArchiveOpenPopup : MonoBehaviour, IFlowInterface
  {
    public Text Message;
    public SRPG_Button BtnDecide;
    public SRPG_Button BtnCancel;
    private ArchiveParam mArchive;
    private ChapterParam mChapter;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!MonoSingleton<GameManager>.Instance.Player.CanOpenArchiveForFree && MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mArchive.keys[0].iname) < this.mArchive.keys[0].num)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
      }
    }

    private void Start()
    {
      this.mArchive = MonoSingleton<GameManager>.Instance.FindArchive(GlobalVars.SelectedArchiveID);
      if (this.mArchive != null && !string.IsNullOrEmpty(this.mArchive.iname))
      {
        string iname = string.Empty;
        if (!string.IsNullOrEmpty(this.mArchive.area_iname))
          iname = this.mArchive.area_iname;
        else if (!string.IsNullOrEmpty(this.mArchive.area_iname_multi))
          iname = this.mArchive.area_iname_multi;
        this.mChapter = MonoSingleton<GameManager>.Instance.FindArea(iname);
      }
      if (this.mArchive == null || this.mChapter == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Message, (UnityEngine.Object) null))
          return;
        string str;
        if (MonoSingleton<GameManager>.Instance.Player.CanOpenArchiveForFree)
        {
          if (this.mArchive.keytime != 0)
          {
            TimeSpan timeSpan = TimeSpan.FromSeconds((double) this.mArchive.keytime);
            double totalHours = timeSpan.TotalHours;
            if (totalHours >= 1.0)
              str = LocalizedText.Get("sys.QUEST_ARCHIVE_OPEN_FREE_POPUP_MESSAGE_H", (object) Math.Round(totalHours));
            else
              str = LocalizedText.Get("sys.QUEST_ARCHIVE_OPEN_FREE_POPUP_MESSAGE_M", (object) Math.Round(timeSpan.TotalMinutes));
          }
          else
          {
            DebugUtility.LogError(this.mArchive.iname + "の開放時間情報がない");
            return;
          }
        }
        else if (this.mArchive.keys.Count > 0)
        {
          KeyItem key = this.mArchive.keys[0];
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(key.iname);
          int num = key.num;
          if (num < 0)
          {
            DebugUtility.LogError(this.mArchive.iname + "の消費するアイテムの個数情報がない");
            return;
          }
          if (this.mArchive.keytime != 0 && itemParam != null)
          {
            TimeSpan timeSpan = TimeSpan.FromSeconds((double) this.mArchive.keytime);
            double totalHours = timeSpan.TotalHours;
            if (totalHours >= 1.0)
            {
              str = LocalizedText.Get("sys.QUEST_ARCHIVE_OPEN_POPUP_MESSAGE_H", (object) itemParam.name, (object) num, (object) Math.Round(totalHours));
            }
            else
            {
              double totalMinutes = timeSpan.TotalMinutes;
              str = LocalizedText.Get("sys.QUEST_ARCHIVE_OPEN_POPUP_MESSAGE_M", (object) itemParam.name, (object) num, (object) Math.Round(totalMinutes));
            }
          }
          else
          {
            DebugUtility.LogError(this.mArchive.iname + "の開放時間情報がない");
            return;
          }
        }
        else
        {
          DebugUtility.LogError(this.mArchive.iname + "の消費するアイテム情報がない");
          return;
        }
        this.Message.text = str;
      }
    }
  }
}
