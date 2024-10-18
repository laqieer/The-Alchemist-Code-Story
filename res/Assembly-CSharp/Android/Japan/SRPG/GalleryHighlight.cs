// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryHighlight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Next", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Fade out Finished", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Base Fade out Finished", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Reset Text", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "Exit", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Start Fade out", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Start Fade in", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Start Base Fade in", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "Wait First Image", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(15, "Start Fisrt Image Fade in", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(100, "Replay", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "Close Gift", FlowNode.PinTypes.Input, 101)]
  public class GalleryHighlight : MonoBehaviour, IFlowInterface
  {
    [Space(10f)]
    [SerializeField]
    [Tooltip("ユーザーがタップ操作を行なわずにメッセージの表示が完了した後、何秒後に次のページへ遷移するか。負の値を設定するとオートスキップが無効になります。")]
    private float AutoSkipTime = 3f;
    [SerializeField]
    [Tooltip("ユーザーがタップ操作を行ってメッセージを早送りした後、何秒後に次のページへ遷移するか。負の値を設定するとオートスキップが無効になります。")]
    private float AutoSkipTimeShort = 1.5f;
    private const int PIN_IN_NEXT = 0;
    private const int PIN_IN_FADEOUT_FINISHED = 1;
    private const int PIN_IN_BASE_FADEOUT_FINISHED = 2;
    private const int PIN_IN_BASE_RESET_TEXT = 3;
    private const int PIN_OUT_EXIT = 10;
    private const int PIN_OUT_IMAGE_FADEOUT_START = 11;
    private const int PIN_OUT_IMAGE_FADEIN_START = 12;
    private const int PIN_OUT_BASE_FADEIN_START = 13;
    private const int PIN_OUT_WAIT_FIRST_IMAGE = 14;
    private const int PIN_OUT_FIRST_IMAGE_FADEIN_START = 15;
    private const int PIN_IN_REPLAY = 100;
    private const int PIN_IN_CLOSE_GIFT = 101;
    [SerializeField]
    private ScriptScreen ScriptScreen;
    [SerializeField]
    private GameObject HighlightHolder;
    [SerializeField]
    private GameObject HighlightBase;
    [SerializeField]
    private GalleryHighlightGiftWindow GiftWindow;
    private FlowNode_ReqGalleryHighlight.Json_Response mHighlightInfo;
    private HighlightParam mHighlightParam;
    private int mCurrentIndex;
    private ScriptScreen.TextParameter mHilightMessage;
    private bool mAlreadyRewarded;
    private bool mHilightFinished;
    private bool mFadeoutFinished;
    private bool mIsFirstHighLight;
    private LoadRequest mLoadReq;
    private GameObject mCurrentImage;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.ShowNextHighlight();
          break;
        case 1:
          this.mFadeoutFinished = true;
          break;
        case 2:
          this.ShowGifts();
          break;
        case 3:
          this.ResetText();
          break;
        case 100:
          this.Replay();
          break;
        case 101:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
      }
    }

    private void Awake()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
      else
      {
        this.mHighlightInfo = currentValue.GetObject<FlowNode_ReqGalleryHighlight.Json_Response>("highlight");
        if (this.mHighlightInfo == null)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        }
        else
        {
          HighlightParam[] highlightParam = MonoSingleton<GameManager>.Instance.MasterParam.HighlightParam;
          if (highlightParam == null || highlightParam.Length < 1)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          }
          else
          {
            this.mHighlightParam = ((IEnumerable<HighlightParam>) highlightParam).FirstOrDefault<HighlightParam>((Func<HighlightParam, bool>) (p => p.iname == this.mHighlightInfo.highlight_iname));
            if (this.mHighlightParam != null && this.mHighlightParam.resources != null && this.mHighlightParam.resources.Length >= 1)
              return;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          }
        }
      }
    }

    private void Update()
    {
      if (this.mLoadReq == null || !this.mFadeoutFinished || !this.mLoadReq.isDone)
        return;
      if ((UnityEngine.Object) this.ScriptScreen != (UnityEngine.Object) null)
      {
        this.ScriptScreen.SetBody(this.mHilightMessage);
        this.StartCoroutine(this.AutoSkipMessage(this.AutoSkipTime));
      }
      this.RefreshHilight(this.mLoadReq.asset as GameObject);
      this.mLoadReq = (LoadRequest) null;
      this.mFadeoutFinished = false;
      if (this.mIsFirstHighLight)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 15);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
    }

    private void ResetText()
    {
      if (!((UnityEngine.Object) this.ScriptScreen != (UnityEngine.Object) null))
        return;
      this.ScriptScreen.SetBody((ScriptScreen.TextParameter) null);
      this.ScriptScreen.Reset();
    }

    private void RefreshHilight(GameObject obj)
    {
      if ((UnityEngine.Object) this.mCurrentImage != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mCurrentImage);
      if (!((UnityEngine.Object) obj != (UnityEngine.Object) null))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(obj);
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      this.mCurrentImage = gameObject;
      if (!((UnityEngine.Object) this.HighlightHolder != (UnityEngine.Object) null))
        return;
      gameObject.transform.SetParent(this.HighlightHolder.transform, false);
    }

    [DebuggerHidden]
    private IEnumerator AutoSkipMessage(float waitTime)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GalleryHighlight.\u003CAutoSkipMessage\u003Ec__Iterator0() { waitTime = waitTime, \u0024this = this };
    }

    private void ShowNextHighlight()
    {
      this.StopAllCoroutines();
      if (this.mHilightFinished)
        return;
      if ((UnityEngine.Object) this.ScriptScreen != (UnityEngine.Object) null)
      {
        if (this.ScriptScreen.IsPrinting)
        {
          this.ScriptScreen.Skip();
          this.StartCoroutine(this.AutoSkipMessage(this.AutoSkipTimeShort));
          return;
        }
        if (!this.ScriptScreen.Finished)
        {
          this.StartCoroutine(this.AutoSkipMessage(this.AutoSkipTimeShort));
          return;
        }
      }
      if (this.mLoadReq != null)
        return;
      if (this.mCurrentIndex >= this.mHighlightParam.resources.Length)
      {
        this.mHilightFinished = true;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
      }
      else
      {
        this.mIsFirstHighLight = this.mCurrentIndex == 0;
        HighlightResource resource;
        string message;
        object[] args;
        if (this.FindNextHighlightResource(this.mHighlightInfo.highlight_info, out resource, out message, out args))
        {
          this.mHilightMessage = new ScriptScreen.TextParameter(message, args);
          if (this.mIsFirstHighLight)
          {
            this.mFadeoutFinished = false;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
          }
          else
          {
            this.mFadeoutFinished = false;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          }
          this.mLoadReq = AssetManager.LoadAsync<GameObject>(AssetPath.Highlight(resource.path));
        }
        else
        {
          this.mHilightFinished = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
        }
      }
    }

    private void Replay()
    {
      this.mHilightFinished = false;
      this.mCurrentIndex = 0;
      this.mLoadReq = (LoadRequest) null;
      this.mFadeoutFinished = false;
      this.mHilightMessage = (ScriptScreen.TextParameter) null;
      this.mIsFirstHighLight = false;
      this.ResetText();
      this.ShowNextHighlight();
    }

    private bool FindNextHighlightResource(FlowNode_ReqGalleryHighlight.JSON_HighlightInfo info, out HighlightResource resource, out string message, out object[] args)
    {
      resource = (HighlightResource) null;
      message = (string) null;
      args = (object[]) null;
      bool flag = false;
      while (this.mCurrentIndex < this.mHighlightParam.resources.Length)
      {
        HighlightResource resource1 = this.mHighlightParam.resources[this.mCurrentIndex];
        ++this.mCurrentIndex;
        switch (resource1.type)
        {
          case HighlightType.Message:
            message = LocalizedText.Get("sys." + resource1.message);
            flag = true;
            break;
          case HighlightType.Start:
            if (info.player != null)
            {
              DateTime dateTime = TimeManager.FromUnixTime(info.player.game_start);
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[4]
              {
                (object) info.player.name,
                (object) dateTime.Year,
                (object) dateTime.Month,
                (object) dateTime.Day
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Days:
            if (info.player != null)
            {
              TimeSpan timeSpan = TimeManager.ServerTime - TimeManager.FromUnixTime(info.player.game_start);
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) (int) Math.Ceiling(timeSpan.TotalDays)
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Quests:
            if (info.quest != null && info.quest.clear_count >= 1)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.quest.clear_count
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Units:
            if (info.gallery != null && info.gallery.unit_count >= 1)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.gallery.unit_count
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Artifact:
            if (info.gallery != null && info.gallery.artifact_count >= 1)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.gallery.artifact_count
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.ConceptCard:
            if (info.gallery != null && info.gallery.concept_card_count >= 1)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.gallery.concept_card_count
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Arena:
            if (info.arena != null && info.arena.rank_best > 0)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.arena.rank_best
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Tower:
            if (info.tower != null && info.tower.veda_clear_floor > 0)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.tower.veda_clear_floor
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.MultiTower:
            if (info.tower != null && info.tower.mebius_clear_floor > 0)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.tower.mebius_clear_floor
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.AlchemyPort:
            if (info.guild != null)
            {
              string guildName = info.guild.guild_name;
              string str1 = LocalizedText.Get("sys.HIGHLIGHT_ALCHMYPORT_MEMBER");
              if (info.guild.role_id == 1)
                str1 = LocalizedText.Get("sys.HIGHLIGHT_ALCHMYPORT_MASTER");
              else if (info.guild.role_id == 2)
                str1 = LocalizedText.Get("sys.HIGHLIGHT_ALCHMYPORT_MEMBER");
              else if (info.guild.role_id == 3)
                str1 = LocalizedText.Get("sys.HIGHLIGHT_ALCHMYPORT_SUBMASTER");
              string str2 = info.guild.join_days.ToString();
              string str3 = info.guild.member_count.ToString();
              string str4 = info.guild.level.ToString();
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[5]
              {
                (object) guildName,
                (object) str1,
                (object) str2,
                (object) str3,
                (object) str4
              };
              flag = true;
              break;
            }
            continue;
          case HighlightType.Friend:
            if (info.friend != null && info.friend.count >= 1)
            {
              message = LocalizedText.Get("sys." + resource1.message);
              args = new object[1]
              {
                (object) info.friend.count
              };
              flag = true;
              break;
            }
            continue;
        }
        if (flag)
        {
          resource = resource1;
          break;
        }
      }
      return flag;
    }

    private void ShowGifts()
    {
      if ((UnityEngine.Object) this.GiftWindow != (UnityEngine.Object) null)
        this.GiftWindow.gameObject.SetActive(true);
      bool isReward = !this.mAlreadyRewarded && this.mHighlightInfo.is_mail_reward != 0;
      this.mAlreadyRewarded = true;
      this.GiftWindow.Refresh(this.mHighlightParam, isReward);
    }
  }
}
