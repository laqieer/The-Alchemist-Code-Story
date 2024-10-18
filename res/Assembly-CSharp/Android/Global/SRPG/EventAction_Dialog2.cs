// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Dialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/会話/表示", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592456, 5592490)]
  public class EventAction_Dialog2 : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble1";
    public EventDialogBubble.Anchors Position = EventDialogBubble.Anchors.BottomCenter;
    private const float DialogPadding = 20f;
    private const string ExtraEmotionDir = "ExtraEmotions/";
    [StringIsActorList]
    public string ActorID;
    [StringIsLocalUnitIDPopup]
    public string UnitID;
    [StringIsTextIDPopup(true)]
    public string TextID;
    private string mTextData;
    private string mVoiceID;
    private UnitParam mUnit;
    private EventDialogBubble mBubble;
    private LoadRequest mBubbleResource;
    private LoadRequest mPortraitResource;
    [Tooltip("非同期にするか？")]
    public bool Async;
    [HideInInspector]
    public PortraitSet.EmotionTypes Emotion;
    [HideInInspector]
    [StringIsResourcePath(typeof (Texture2D), "ExtraEmotions/")]
    public string CustomEmotion;

    private static string[] GetIDPair(string src)
    {
      string[] strArray = src.Split(new char[1]{ '.' }, 2);
      if (strArray.Length >= 2 && strArray[0].Length > 0 && strArray[1].Length > 0)
        return strArray;
      UnityEngine.Debug.LogError((object) ("Invalid Voice ID: " + src));
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
      return (IEnumerator) new EventAction_Dialog2.\u003CPreloadAssets\u003Ec__IteratorA3() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!((UnityEngine.Object) this.mBubble == (UnityEngine.Object) null))
        return;
      this.mBubble = EventDialogBubble.Find(this.ActorID);
      if (this.mBubbleResource != null && ((UnityEngine.Object) this.mBubble == (UnityEngine.Object) null || this.mBubble.Anchor != this.Position))
      {
        this.mBubble = UnityEngine.Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubble;
        this.mBubble.transform.SetParent(this.ActiveCanvas.transform, false);
        this.mBubble.BubbleID = this.ActorID;
        this.mBubble.gameObject.SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
      this.mBubble.Anchor = this.Position;
    }

    private void LoadTextData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        string[] strArray = LocalizedText.Get(this.TextID).Split('\t');
        this.mTextData = strArray[0];
        if (string.IsNullOrEmpty(this.mTextData))
          this.mTextData = this.TextID;
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
        for (int index = 0; index < EventDialogBubble.Instances.Count && (UnityEngine.Object) EventDialogBubble.Instances[index] != (UnityEngine.Object) this.mBubble; ++index)
        {
          if (EventDialogBubble.Instances[index].BubbleID == this.ActorID)
            EventDialogBubble.Instances[index].Close();
        }
        this.mBubble.gameObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null)
      {
        if (!string.IsNullOrEmpty(this.mVoiceID))
        {
          string[] idPair = EventAction_Dialog2.GetIDPair(this.mVoiceID);
          if (idPair != null)
          {
            this.mBubble.VoiceSheetName = idPair[0];
            this.mBubble.VoiceCueName = idPair[1];
          }
        }
        RectTransform transform1 = this.mBubble.transform as RectTransform;
        for (int index = 0; index < EventDialogBubble.Instances.Count; ++index)
        {
          RectTransform transform2 = EventDialogBubble.Instances[index].transform as RectTransform;
          if ((UnityEngine.Object) transform1 != (UnityEngine.Object) transform2 && transform1.rect.Overlaps(transform2.rect))
            EventDialogBubble.Instances[index].Close();
        }
        this.mBubble.SetName(this.mUnit == null ? "???" : this.mUnit.name);
        this.mBubble.SetBody(this.mTextData);
        if (this.mPortraitResource != null && this.mPortraitResource.isDone)
        {
          if (this.mPortraitResource.asset is PortraitSet)
          {
            this.mBubble.PortraitSet = (PortraitSet) this.mPortraitResource.asset;
            this.mBubble.CustomEmotion = (Texture2D) null;
          }
          else
            this.mBubble.CustomEmotion = (Texture2D) this.mPortraitResource.asset;
        }
        this.mBubble.Emotion = this.Emotion;
        this.mBubble.Open();
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
          this.mBubble.StopVoice();
          this.mBubble.Forward();
          this.OnFinish();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public override void SkipImmediate()
    {
      if (!((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null))
        return;
      this.mBubble.Close();
      this.OnFinish();
    }

    protected virtual void OnFinish()
    {
      this.ActivateNext();
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(EventAction_Dialog2.GetIDPair(this.mVoiceID), false);
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
