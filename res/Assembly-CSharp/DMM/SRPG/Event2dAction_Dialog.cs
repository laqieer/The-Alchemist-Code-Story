// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("会話/表示(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog : EventAction
  {
    private const float DialogPadding = 20f;
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string CharaID;
    [StringIsLocalUnitID]
    public string UnitID;
    [StringIsTextID(false)]
    public string TextID;
    public string Emotion = string.Empty;
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    private UnitParam mUnit;
    private string mPlayerName;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    private static readonly string AssetPath = "UI/DialogBubble2";

    private static string[] GetIDPair(string src)
    {
      string[] strArray = src.Split(new char[1]{ '.' }, 2);
      return strArray.Length >= 2 && strArray[0].Length > 0 && strArray[1].Length > 0 ? strArray : (string[]) null;
    }

    public override bool IsPreloadAssets => true;

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_Dialog.\u003CPreloadAssets\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBubble, (Object) null))
        return;
      if (!string.IsNullOrEmpty(this.UnitID))
        this.ActorID = this.UnitID;
      this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
      if (this.mBubbleResource != null && Object.op_Equality((Object) this.mBubble, (Object) null))
      {
        this.mBubble = Object.Instantiate(this.mBubbleResource.asset) as EventDialogBubbleCustom;
        ((Component) this.mBubble).transform.SetParent((Transform) this.EventRootTransform, false);
        this.mBubble.BubbleID = this.ActorID;
        ((Component) this.mBubble).transform.SetAsLastSibling();
        ((Component) this.mBubble).gameObject.SetActive(false);
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
      Vector2 vector2 = Vector2.op_Implicit(Camera.main.WorldToScreenPoint(position));
      vector2.x /= (float) Screen.width;
      vector2.y /= (float) Screen.height;
      return vector2;
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && !((Component) this.mBubble).gameObject.activeInHierarchy)
      {
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count && Object.op_Inequality((Object) EventDialogBubbleCustom.Instances[index], (Object) this.mBubble); ++index)
        {
          if (EventDialogBubbleCustom.Instances[index].BubbleID == this.ActorID)
            EventDialogBubbleCustom.Instances[index].Close();
        }
        ((Component) this.mBubble).gameObject.SetActive(true);
      }
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
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
        ((Component) this.mBubble).transform.SetAsLastSibling();
        RectTransform transform1 = ((Component) this.mBubble).transform as RectTransform;
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count; ++index)
        {
          RectTransform transform2 = ((Component) EventDialogBubbleCustom.Instances[index]).transform as RectTransform;
          if (Object.op_Inequality((Object) transform1, (Object) transform2))
          {
            Rect rect = transform1.rect;
            if (((Rect) ref rect).Overlaps(transform2.rect))
              EventDialogBubbleCustom.Instances[index].Close();
          }
        }
        string empty = string.Empty;
        string name = !string.IsNullOrEmpty(this.mPlayerName) ? this.mPlayerName : (this.mUnit == null ? "???" : this.mUnit.name);
        this.mBubble.SetName(name);
        this.mBubble.SetBody(this.mTextData);
        EventScript.AddBackLog(name, this.mBubble.ReplaceText(this.mTextData));
        this.mBubble.Open();
      }
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        foreach (EventStandCharaController2 instance in EventStandCharaController2.Instances)
        {
          if (instance.CharaID == this.CharaID)
          {
            if (!instance.IsClose)
            {
              ((Component) instance).transform.SetSiblingIndex(((Component) this.mBubble).transform.GetSiblingIndex() - 1);
              if (!string.IsNullOrEmpty(this.Emotion))
                instance.UpdateEmotion(this.Emotion);
            }
          }
          else if (!instance.IsClose)
            ;
        }
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

    public override void Update()
    {
      base.Update();
      if (!EventScript.IsMessageAuto || !Object.op_Inequality((Object) this.mBubble, (Object) null) || !this.mBubble.Finished || this.mBubble.IsVoicePlaying || !EventScript.MessageAutoForward(Time.deltaTime))
        return;
      this.Forward();
    }

    public override bool Forward()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
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

    public override bool OnEventSkip()
    {
      if (Object.op_Inequality((Object) this.mBubble, (Object) null))
        this.mBubble.Close();
      this.OnFinish();
      return true;
    }

    protected virtual void OnFinish() => this.ActivateNext();

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog.GetIDPair(this.mVoiceID));
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
