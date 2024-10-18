// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Dialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/会話/表示", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592456, 5592490)]
  public class EventAction_Dialog2 : EventAction
  {
    private const float DialogPadding = 20f;
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
    private const string ExtraEmotionDir = "ExtraEmotions/";
    [HideInInspector]
    [StringIsResourcePath(typeof (Texture2D), "ExtraEmotions/")]
    public string CustomEmotion;
    public EventDialogBubble.Anchors Position = EventDialogBubble.Anchors.BottomCenter;
    private static readonly string AssetPath = "UI/DialogBubble1";

    private static string[] GetIDPair(string src)
    {
      string[] idPair = src.Split(new char[1]{ '.' }, 2);
      if (idPair.Length >= 2 && idPair[0].Length > 0 && idPair[1].Length > 0)
        return idPair;
      Debug.LogError((object) ("Invalid Voice ID: " + src));
      return (string[]) null;
    }

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_Dialog2.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble = EventDialogBubble.Find(this.ActorID);
      if (this.mBubbleResource != null && (Object.op_Equality((Object) this.mBubble, (Object) null) || this.mBubble.Anchor != this.Position))
      {
        this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubble;
        ((Component) this.mBubble).transform.SetParent((Transform) this.EventRootTransform, false);
        this.mBubble.BubbleID = this.ActorID;
        ((Component) this.mBubble).gameObject.SetActive(false);
      }
      this.mBubble.AdjustWidth(this.mTextData);
      this.mBubble.Anchor = this.Position;
    }

    public override void Update()
    {
      base.Update();
      if (!EventScript.IsMessageAuto || !Object.op_Inequality((Object) this.mBubble, (Object) null) || !this.mBubble.Finished || this.mBubble.IsVoicePlaying || !EventScript.MessageAutoForward(Time.deltaTime))
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
      Vector2 vector2 = Vector2.op_Implicit(Camera.main.WorldToScreenPoint(position));
      vector2.x /= (float) Screen.width;
      vector2.y /= (float) Screen.height;
      return vector2;
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && !((Component) this.mBubble).gameObject.activeInHierarchy)
      {
        for (int index = 0; index < EventDialogBubble.Instances.Count && Object.op_Inequality((Object) EventDialogBubble.Instances[index], (Object) this.mBubble); ++index)
        {
          if (EventDialogBubble.Instances[index].BubbleID == this.ActorID)
            EventDialogBubble.Instances[index].Close();
        }
        ((Component) this.mBubble).gameObject.SetActive(true);
      }
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
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
        RectTransform transform1 = ((Component) this.mBubble).transform as RectTransform;
        for (int index = 0; index < EventDialogBubble.Instances.Count; ++index)
        {
          RectTransform transform2 = ((Component) EventDialogBubble.Instances[index]).transform as RectTransform;
          if (Object.op_Inequality((Object) transform1, (Object) transform2))
          {
            Rect rect = transform1.rect;
            if (((Rect) ref rect).Overlaps(transform2.rect))
              EventDialogBubble.Instances[index].Close();
          }
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
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    private string GetActorName(string actorID)
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if (Object.op_Inequality((Object) actor, (Object) null))
      {
        TacticsUnitController component = actor.GetComponent<TacticsUnitController>();
        if (Object.op_Inequality((Object) component, (Object) null))
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
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
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

    public override void GoToEndState()
    {
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble.Close();
      this.enabled = false;
    }

    public override void SkipImmediate()
    {
      if (!Object.op_Inequality((Object) this.mBubble, (Object) null))
        return;
      this.mBubble.Close();
      this.OnFinish();
    }

    protected virtual void OnFinish() => this.ActivateNext();

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(EventAction_Dialog2.GetIDPair(this.mVoiceID));
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
