// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Dialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("会話/表示", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592456, 5592490)]
  public class EventAction_Dialog : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble1";
    private const float DialogPadding = 20f;
    [StringIsActorID]
    public string ActorID;
    [StringIsUnitID]
    public string UnitID;
    [StringIsTextID(true)]
    public string TextID;
    private string mTextData;
    private string mVoiceID;
    private UnitParam mUnit;
    private EventDialogBubble mBubble;
    private LoadRequest mBubbleResource;
    private LoadRequest mPortraitResource;
    [HideInInspector]
    public PortraitSet.EmotionTypes Emotion;
    private const string ExtraEmotionDir = "ExtraEmotions/";
    [HideInInspector]
    [StringIsResourcePath(typeof (Texture2D), "ExtraEmotions/")]
    public string CustomEmotion;
    public EventDialogBubble.Anchors Position;

    private static string[] GetIDPair(string src)
    {
      string[] strArray = src.Split(new char[1]{ '.' }, 2);
      if (strArray.Length >= 2 && strArray[0].Length > 0 && strArray[1].Length > 0)
        return strArray;
      Debug.LogError((object) ("Invalid Voice ID: " + src));
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
      return (IEnumerator) new EventAction_Dialog.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
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

    public override void Update()
    {
      base.Update();
      if (!EventScript.IsMessageAuto || !((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null) || (!this.mBubble.Finished || this.mBubble.IsVoicePlaying) || !EventScript.MessageAutoForward(Time.deltaTime))
        return;
      this.Forward();
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
        for (int index = 0; index < EventDialogBubble.Instances.Count && (UnityEngine.Object) EventDialogBubble.Instances[index] != (UnityEngine.Object) this.mBubble; ++index)
        {
          if (EventDialogBubble.Instances[index].BubbleID == this.ActorID)
            EventDialogBubble.Instances[index].Close();
        }
        this.mBubble.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null))
        return;
      if (!string.IsNullOrEmpty(this.mVoiceID))
      {
        string[] idPair = EventAction_Dialog.GetIDPair(this.mVoiceID);
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
      string name = this.mUnit == null ? "???" : this.mUnit.name;
      this.mBubble.SetName(name);
      this.mBubble.SetBody(this.mTextData);
      EventScript.AddBackLog(name, this.mBubble.ReplaceText(this.mTextData));
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
          this.OnFinish();
          return true;
        }
        if (this.mBubble.IsPrinting)
          this.mBubble.Skip();
      }
      return false;
    }

    public override void GoToEndState()
    {
      if (!((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null))
        return;
      this.mBubble.Close();
      this.enabled = false;
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
          return EventAction.GetUnManagedStreamAssets(EventAction_Dialog.GetIDPair(this.mVoiceID), false);
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
