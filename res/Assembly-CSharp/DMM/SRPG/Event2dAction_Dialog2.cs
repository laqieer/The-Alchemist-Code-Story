﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/会話/表示2(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog2 : EventAction
  {
    private const float DialogPadding = 20f;
    private const float normalScale = 1f;
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string CharaID;
    [StringIsLocalUnitID]
    public string UnitID;
    [StringIsTextID(false)]
    public string TextID;
    public string Emotion = string.Empty;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<GameObject> fadeOutList = new List<GameObject>();
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    [HideInInspector]
    public float FadeTime = 0.2f;
    public bool Fade;
    private bool IsFading;
    private UnitParam mUnit;
    private string mPlayerName;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;
    [SerializeField]
    [HideInInspector]
    public string[] IgnoreFadeOut = new string[1];
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
      return (IEnumerator) new Event2dAction_Dialog2.\u003CPreloadAssets\u003Ec__Iterator0()
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

    private bool ContainIgnoreFO(string charID)
    {
      for (int index = 0; index < this.IgnoreFadeOut.Length; ++index)
      {
        if (this.IgnoreFadeOut[index].Equals(charID))
          return true;
      }
      return false;
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
          string[] idPair = Event2dAction_Dialog2.GetIDPair(this.mVoiceID);
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
      this.fadeInList.Clear();
      this.fadeOutList.Clear();
      this.IsFading = false;
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        foreach (EventStandCharaController2 instance in EventStandCharaController2.Instances)
        {
          if (!instance.IsClose)
          {
            if (instance.CharaID == this.CharaID || this.ContainIgnoreFO(instance.CharaID))
            {
              foreach (GameObject standChara in instance.StandCharaList)
              {
                if (Color.op_Inequality(((Graphic) standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color, Color.white))
                {
                  this.fadeInList.Add(standChara);
                  this.IsFading = true;
                }
              }
              int num = ((Component) this.mBubble).transform.GetSiblingIndex() - 1;
              Debug.Log((object) ("set index:" + (object) num));
              ((Component) instance).transform.SetSiblingIndex(num);
              ((Component) instance).transform.localScale = Vector3.op_Multiply(Vector3.one, 1f);
              if (!string.IsNullOrEmpty(this.Emotion))
                instance.UpdateEmotion(this.Emotion);
            }
            else if (((Behaviour) instance).isActiveAndEnabled)
            {
              foreach (GameObject standChara in instance.StandCharaList)
              {
                if (Color.op_Inequality(((Graphic) standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color, Color.gray))
                {
                  this.fadeOutList.Add(standChara);
                  this.IsFading = true;
                }
              }
            }
          }
        }
      }
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    public override void Update()
    {
      if (!this.IsFading)
      {
        if (!EventScript.IsMessageAuto || !Object.op_Inequality((Object) this.mBubble, (Object) null) || !this.mBubble.Finished || this.mBubble.IsVoicePlaying || !EventScript.MessageAutoForward(Time.deltaTime))
          return;
        this.Forward();
      }
      else
      {
        this.FadeTime -= Time.deltaTime;
        if ((double) this.FadeTime <= 0.0)
        {
          this.FadeTime = 0.0f;
          this.IsFading = false;
        }
        else
          this.FadeIn(this.FadeTime);
      }
    }

    private void FadeIn(float time)
    {
      Color color1;
      // ISSUE: explicit constructor call
      ((Color) ref color1).\u002Ector(Mathf.Lerp(Color.white.r, Color.gray.r, time), Mathf.Lerp(Color.white.g, Color.gray.g, time), Mathf.Lerp(Color.white.b, Color.gray.b, time), 1f);
      Color color2;
      // ISSUE: explicit constructor call
      ((Color) ref color2).\u002Ector(Mathf.Lerp(Color.gray.r, Color.white.r, time), Mathf.Lerp(Color.gray.g, Color.white.g, time), Mathf.Lerp(Color.gray.b, Color.white.b, time), 1f);
      foreach (GameObject fadeIn in this.fadeInList)
      {
        if ((double) ((Graphic) fadeIn.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color.r <= (double) color1.r)
        {
          ((Graphic) fadeIn.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color = color1;
          ((Graphic) fadeIn.GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>()).color = color1;
        }
      }
      foreach (GameObject fadeOut in this.fadeOutList)
      {
        if ((double) ((Graphic) fadeOut.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color.r >= (double) color2.r)
        {
          ((Graphic) fadeOut.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color = color2;
          ((Graphic) fadeOut.GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>()).color = color2;
        }
      }
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
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog2.GetIDPair(this.mVoiceID));
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
