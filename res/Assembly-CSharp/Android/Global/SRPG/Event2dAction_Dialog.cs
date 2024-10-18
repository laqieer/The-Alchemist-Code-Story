// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("会話/表示(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble2";
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    [StringIsCharaEmo]
    public string Emotion = string.Empty;
    private const float DialogPadding = 20f;
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    [StringIsCharaType]
    public string CharaID;
    [StringIsLocalUnitID]
    public string UnitID;
    [StringIsTextID(false)]
    public string TextID;
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    private UnitParam mUnit;
    private string mPlayerName;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;

    private static string[] GetIDPair(string src)
    {
      string[] strArray = src.Split(new char[1]{ '.' }, 2);
      if (strArray.Length >= 2 && strArray[0].Length > 0 && strArray[1].Length > 0)
        return strArray;
      return (string[]) null;
    }

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_Dialog.\u003CPreloadAssets\u003Ec__IteratorB7() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mBubble == (UnityEngine.Object) null))
        return;
      if (!string.IsNullOrEmpty(this.UnitID))
        this.ActorID = this.UnitID;
      this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
      if (this.mBubbleResource != null && (UnityEngine.Object) this.mBubble == (UnityEngine.Object) null)
      {
        this.mBubble = UnityEngine.Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubbleCustom;
        this.mBubble.transform.SetParent(this.ActiveCanvas.transform, false);
        this.mBubble.BubbleID = this.ActorID;
        this.mBubble.transform.SetAsLastSibling();
        this.mBubble.gameObject.SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
      this.mBubble.Anchor = EventDialogBubbleCustom.Anchors.BottomCenter;
    }

    private void LoadTextData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        string[] strArray = LocalizedText.Get(this.TextID).Split('\t');
        this.mTextData = strArray[0];
        this.mVoiceID = strArray.Length <= 1 ? (string) null : strArray[1];
      }
      else
        this.mTextData = this.mVoiceID = (string) null;
    }

    private Vector2 CalcBubblePosition(Vector3 position)
    {
      Vector2 screenPoint = (Vector2) UnityEngine.Camera.main.WorldToScreenPoint(position);
      screenPoint.x /= (float) Screen.width;
      screenPoint.y /= (float) Screen.height;
      return screenPoint;
    }

    public override void OnActivate()
    {
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null && !this.mBubble.gameObject.activeInHierarchy)
      {
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count && (UnityEngine.Object) EventDialogBubbleCustom.Instances[index] != (UnityEngine.Object) this.mBubble; ++index)
        {
          if (EventDialogBubbleCustom.Instances[index].BubbleID == this.ActorID)
            EventDialogBubbleCustom.Instances[index].Close();
        }
        this.mBubble.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null)
      {
        if (!string.IsNullOrEmpty(this.mVoiceID))
        {
          string[] idPair = Event2dAction_Dialog.GetIDPair(this.mVoiceID);
          if (idPair != null)
          {
            this.mBubble.VoiceSheetName = idPair[0];
            this.mBubble.VoiceCueName = idPair[1];
          }
        }
        this.mBubble.transform.SetAsLastSibling();
        RectTransform transform1 = this.mBubble.transform as RectTransform;
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count; ++index)
        {
          RectTransform transform2 = EventDialogBubbleCustom.Instances[index].transform as RectTransform;
          if ((UnityEngine.Object) transform1 != (UnityEngine.Object) transform2 && transform1.rect.Overlaps(transform2.rect))
            EventDialogBubbleCustom.Instances[index].Close();
        }
        if (string.IsNullOrEmpty(this.mPlayerName))
          this.mBubble.SetName(this.mUnit == null ? "???" : this.mUnit.name);
        else
          this.mBubble.SetName(this.mPlayerName);
        this.mBubble.SetBody(this.mTextData);
        if (!GlobalVars.IsTutorialEnd)
          AnalyticsManager.TrackTutorialAnalyticsEvent(this.TextID);
        this.mBubble.Open();
      }
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        using (List<EventStandCharaController2>.Enumerator enumerator = EventStandCharaController2.Instances.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EventStandCharaController2 current = enumerator.Current;
            if (current.CharaID == this.CharaID)
            {
              if (!current.IsClose)
              {
                current.transform.SetSiblingIndex(this.mBubble.transform.GetSiblingIndex() - 1);
                if (!string.IsNullOrEmpty(this.Emotion))
                  current.UpdateEmotion(this.Emotion);
              }
            }
            else if (!current.IsClose)
              ;
          }
        }
      }
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    private string GetActorName(string actorID)
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
      {
        TacticsUnitController component = actor.GetComponent<TacticsUnitController>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          Unit unit = component.Unit;
          if (unit != null)
            return unit.UnitName;
        }
      }
      return actorID;
    }

    public override bool Forward()
    {
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null)
      {
        if (this.mBubble.Finished)
        {
          this.mBubble.Forward();
          this.ActivateNext();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog.GetIDPair(this.mVoiceID), false);
      }
      return (string[]) null;
    }

    public enum TextSpeedTypes
    {
      Normal,
      Slow,
      Fast,
    }
  }
}
