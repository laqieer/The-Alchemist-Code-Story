// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog3
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
  [EventActionInfo("New/会話/表示3(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog3 : EventAction
  {
    private const float DialogPadding = 20f;
    private const float normalScale = 1f;
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string CharaID;
    [StringIsLocalUnitIDPopup]
    public string UnitID;
    [StringIsTextIDPopup(false)]
    public string TextID;
    public string Emotion = string.Empty;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<GameObject> fadeOutList = new List<GameObject>();
    private List<CanvasGroup> fadeInParticleList = new List<CanvasGroup>();
    private List<CanvasGroup> fadeOutParticleList = new List<CanvasGroup>();
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    [HideInInspector]
    public float FadeTime = 0.2f;
    private float fadingTime;
    private bool IsFading;
    private bool FoldOut;
    [HideInInspector]
    public bool ActorParticle;
    private UnitParam mUnit;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;
    private RectTransform bubbleTransform;
    [SerializeField]
    [HideInInspector]
    public string[] IgnoreFadeOut = new string[1];
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    private static readonly string AssetPath = "UI/DialogBubble2";
    private bool FoldOutShake;
    [HideInInspector]
    public float Duration;
    [HideInInspector]
    public float FrequencyX = 12.51327f;
    [HideInInspector]
    public float FrequencyY = 20.4651f;
    [HideInInspector]
    public float AmplitudeX = 0.1f;
    [HideInInspector]
    public float AmplitudeY = 0.1f;
    private float mSeedX;
    private float mSeedY;
    private float ShakingTime;
    private bool IsShaking;
    private Vector2 originalPvt;

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
      return (IEnumerator) new Event2dAction_Dialog3.\u003CPreloadAssets\u003Ec__Iterator0()
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
          string[] idPair = Event2dAction_Dialog3.GetIDPair(this.mVoiceID);
          if (idPair != null)
          {
            this.mBubble.VoiceSheetName = idPair[0];
            this.mBubble.VoiceCueName = idPair[1];
          }
        }
        ((Component) this.mBubble).transform.SetAsLastSibling();
        this.bubbleTransform = ((Component) this.mBubble).transform as RectTransform;
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count; ++index)
        {
          RectTransform transform = ((Component) EventDialogBubbleCustom.Instances[index]).transform as RectTransform;
          if (Object.op_Inequality((Object) this.bubbleTransform, (Object) transform))
          {
            Rect rect = this.bubbleTransform.rect;
            if (((Rect) ref rect).Overlaps(transform.rect))
              EventDialogBubbleCustom.Instances[index].Close();
          }
        }
        string empty = string.Empty;
        string name = this.mUnit == null ? "???" : this.mUnit.name;
        this.mBubble.SetName(name);
        this.mBubble.SetBody(this.mTextData);
        EventScript.AddBackLog(name, this.mBubble.ReplaceText(this.mTextData));
        this.mBubble.Open();
      }
      this.fadeInList.Clear();
      this.fadeOutList.Clear();
      this.fadeInParticleList.Clear();
      this.fadeOutParticleList.Clear();
      this.IsFading = false;
      if (EventStandCharaController2.Instances != null && EventStandCharaController2.Instances.Count > 0)
      {
        foreach (EventStandCharaController2 instance in EventStandCharaController2.Instances)
        {
          if (!instance.IsClose)
          {
            if (instance.CharaID == this.CharaID || this.ContainIgnoreFO(instance.CharaID))
            {
              Color white = Color.white;
              if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(instance.CharaID))
                white = Event2dAction_OperateStandChara.CharaColorDic[instance.CharaID];
              foreach (GameObject standChara in instance.StandCharaList)
              {
                if (Color.op_Inequality(((Graphic) standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color, white))
                {
                  this.fadeInList.Add(standChara);
                  this.IsFading = true;
                }
              }
              if (this.ActorParticle)
              {
                foreach (Component componentsInChild in ((Component) instance).gameObject.GetComponentsInChildren<GameObjectID>())
                {
                  CanvasGroup canvasGroup = componentsInChild.RequireComponent<CanvasGroup>();
                  if ((double) canvasGroup.alpha != 1.0)
                    this.fadeInParticleList.Add(canvasGroup);
                }
              }
              int num = ((Component) this.mBubble).transform.GetSiblingIndex() - 1;
              ((Component) instance).transform.SetSiblingIndex(num);
              ((Component) instance).transform.localScale = Vector3.op_Multiply(((Component) instance).transform.localScale, 1f);
              if (!string.IsNullOrEmpty(this.Emotion))
                instance.UpdateEmotion(this.Emotion);
            }
            else if (((Behaviour) instance).isActiveAndEnabled)
            {
              Color color = Color.white;
              if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(instance.CharaID))
                color = Color.op_Multiply(Event2dAction_OperateStandChara.CharaColorDic[instance.CharaID], Color.gray);
              foreach (GameObject standChara in instance.StandCharaList)
              {
                if (Color.op_Inequality(((Graphic) standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>()).color, color))
                {
                  this.fadeOutList.Add(standChara);
                  this.IsFading = true;
                }
              }
              if (this.ActorParticle)
              {
                foreach (Component componentsInChild in ((Component) instance).gameObject.GetComponentsInChildren<GameObjectID>())
                {
                  CanvasGroup canvasGroup = componentsInChild.RequireComponent<CanvasGroup>();
                  if ((double) canvasGroup.alpha != 0.0)
                    this.fadeOutParticleList.Add(canvasGroup);
                }
              }
            }
          }
        }
      }
      if (this.IsFading)
        this.fadingTime = this.FadeTime;
      this.IsShaking = false;
      if (Object.op_Inequality((Object) this.mBubble, (Object) null) && (double) this.Duration > 0.0)
      {
        this.IsShaking = true;
        this.originalPvt = new Vector2(0.5f, 0.0f);
        this.ShakingTime = this.Duration;
        this.mSeedX = Random.value;
        this.mSeedY = Random.value;
      }
      if (!this.Async)
        return;
      this.ActivateNext(true);
    }

    public override void Update()
    {
      if (this.IsFading)
      {
        this.fadingTime -= Time.deltaTime;
        if ((double) this.fadingTime <= 0.0)
        {
          this.fadingTime = 0.0f;
          this.IsFading = false;
        }
        this.FadeIn(this.fadingTime);
      }
      if (this.IsShaking)
      {
        this.ShakingTime -= Time.deltaTime;
        if ((double) this.ShakingTime <= 0.0)
        {
          this.ShakingTime = 0.0f;
          this.IsShaking = false;
        }
        this.Shake(this.ShakingTime);
      }
      if (this.Async && !this.IsFading && !this.IsShaking)
        this.enabled = false;
      if (this.IsFading || !EventScript.IsMessageAuto || !Object.op_Inequality((Object) this.mBubble, (Object) null) || !this.mBubble.Finished || this.mBubble.IsVoicePlaying || !EventScript.MessageAutoForward(Time.deltaTime))
        return;
      this.Forward();
    }

    private void FadeIn(float time)
    {
      float num1 = time / this.FadeTime;
      Color color1 = Color.Lerp(Color.white, Color.grey, num1);
      Color color2 = Color.Lerp(Color.grey, Color.white, num1);
      foreach (GameObject fadeIn in this.fadeInList)
      {
        EventStandChara2 component = fadeIn.GetComponent<EventStandChara2>();
        string charaId = fadeIn.GetComponentInParent<EventStandCharaController2>().CharaID;
        Color white = Color.white;
        if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
          white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
        Color color3 = Color.op_Multiply(white, color1);
        Color color4 = ((Graphic) component.BodyObject.GetComponent<RawImage>()).color;
        if ((double) ((Color) ref color4).maxColorComponent <= (double) ((Color) ref color3).maxColorComponent)
        {
          ((Graphic) component.FaceObject.GetComponent<RawImage>()).color = color3;
          ((Graphic) component.BodyObject.GetComponent<RawImage>()).color = color3;
        }
      }
      foreach (GameObject fadeOut in this.fadeOutList)
      {
        EventStandChara2 component = fadeOut.GetComponent<EventStandChara2>();
        string charaId = fadeOut.GetComponentInParent<EventStandCharaController2>().CharaID;
        Color white = Color.white;
        if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
          white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
        Color color5 = Color.op_Multiply(white, color2);
        Color color6 = ((Graphic) component.BodyObject.GetComponent<RawImage>()).color;
        if ((double) ((Color) ref color6).maxColorComponent >= (double) ((Color) ref color5).maxColorComponent)
        {
          ((Graphic) component.FaceObject.GetComponent<RawImage>()).color = color5;
          ((Graphic) component.BodyObject.GetComponent<RawImage>()).color = color5;
        }
      }
      float num2 = Mathf.Lerp(1f, 0.0f, num1);
      float num3 = Mathf.Lerp(0.0f, 1f, num1);
      foreach (CanvasGroup fadeInParticle in this.fadeInParticleList)
      {
        if ((double) fadeInParticle.alpha <= (double) num2)
          fadeInParticle.alpha = num2;
      }
      foreach (CanvasGroup fadeOutParticle in this.fadeOutParticleList)
      {
        if ((double) fadeOutParticle.alpha >= (double) num3)
          fadeOutParticle.alpha = num3;
      }
    }

    private void Shake(float time)
    {
      if ((double) time > 0.0)
      {
        float num1 = Mathf.Clamp01(time / this.Duration);
        float num2 = Mathf.Sin((float) (((double) Time.time + (double) this.mSeedX) * (double) this.FrequencyX * 3.1415927410125732)) * this.AmplitudeX * num1;
        float num3 = Mathf.Sin((float) (((double) Time.time + (double) this.mSeedY) * (double) this.FrequencyY * 3.1415927410125732)) * this.AmplitudeY * num1;
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(this.originalPvt.x + num2, this.originalPvt.y + num3);
        this.bubbleTransform.pivot = vector2;
      }
      else
        this.bubbleTransform.pivot = this.originalPvt;
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
      if (this.Async || !Object.op_Inequality((Object) this.mBubble, (Object) null))
        return false;
      if (this.mBubble.Finished)
      {
        if (this.IsFading)
          this.FadeIn(0.0f);
        if (this.IsShaking)
          this.Shake(0.0f);
        this.mBubble.StopVoice();
        this.mBubble.Forward();
        this.ActivateNext();
        return true;
      }
      if (this.mBubble.IsPrinting)
        this.mBubble.Skip();
      return false;
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (!string.IsNullOrEmpty(this.TextID))
      {
        this.LoadTextData();
        if (!string.IsNullOrEmpty(this.mVoiceID))
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog3.GetIDPair(this.mVoiceID));
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
