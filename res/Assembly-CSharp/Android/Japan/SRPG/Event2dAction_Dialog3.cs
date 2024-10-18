// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Dialog3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/会話/表示3(2D)", "会話の文章を表示し、プレイヤーの入力を待ちます。", 5592405, 4473992)]
  public class Event2dAction_Dialog3 : EventAction
  {
    private static readonly string AssetPath = "UI/DialogBubble2";
    [StringIsActorID]
    [HideInInspector]
    public string ActorID = "2DPlus";
    public string Emotion = string.Empty;
    private List<GameObject> fadeInList = new List<GameObject>();
    private List<GameObject> fadeOutList = new List<GameObject>();
    private List<CanvasGroup> fadeInParticleList = new List<CanvasGroup>();
    private List<CanvasGroup> fadeOutParticleList = new List<CanvasGroup>();
    [HideInInspector]
    public float FadeTime = 0.2f;
    [SerializeField]
    [HideInInspector]
    public string[] IgnoreFadeOut = new string[1];
    [HideInInspector]
    public float FrequencyX = 12.51327f;
    [HideInInspector]
    public float FrequencyY = 20.4651f;
    [HideInInspector]
    public float AmplitudeX = 0.1f;
    [HideInInspector]
    public float AmplitudeY = 0.1f;
    private const float DialogPadding = 20f;
    private const float normalScale = 1f;
    public string CharaID;
    [StringIsLocalUnitIDPopup]
    public string UnitID;
    [StringIsTextIDPopup(false)]
    public string TextID;
    public bool Async;
    private string mTextData;
    private string mVoiceID;
    private float fadingTime;
    private bool IsFading;
    private bool FoldOut;
    [HideInInspector]
    public bool ActorParticle;
    private UnitParam mUnit;
    private EventDialogBubbleCustom mBubble;
    private LoadRequest mBubbleResource;
    private RectTransform bubbleTransform;
    private const EventDialogBubbleCustom.Anchors AnchorPoint = EventDialogBubbleCustom.Anchors.BottomCenter;
    private bool FoldOutShake;
    [HideInInspector]
    public float Duration;
    private float mSeedX;
    private float mSeedY;
    private float ShakingTime;
    private bool IsShaking;
    private Vector2 originalPvt;

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
      return (IEnumerator) new Event2dAction_Dialog3.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
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
          string[] idPair = Event2dAction_Dialog3.GetIDPair(this.mVoiceID);
          if (idPair != null)
          {
            this.mBubble.VoiceSheetName = idPair[0];
            this.mBubble.VoiceCueName = idPair[1];
          }
        }
        this.mBubble.transform.SetAsLastSibling();
        this.bubbleTransform = this.mBubble.transform as RectTransform;
        for (int index = 0; index < EventDialogBubbleCustom.Instances.Count; ++index)
        {
          RectTransform transform = EventDialogBubbleCustom.Instances[index].transform as RectTransform;
          if ((UnityEngine.Object) this.bubbleTransform != (UnityEngine.Object) transform && this.bubbleTransform.rect.Overlaps(transform.rect))
            EventDialogBubbleCustom.Instances[index].Close();
        }
        string empty = string.Empty;
        string name = this.mUnit == null ? "???" : this.mUnit.name;
        this.mBubble.SetName(name);
        this.mBubble.SetBody(this.mTextData);
        EventScript.AddBackLog(name, this.mBubble.ReplaceText(this.mTextData, false));
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
                if (standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().color != white)
                {
                  this.fadeInList.Add(standChara);
                  this.IsFading = true;
                }
              }
              if (this.ActorParticle)
              {
                foreach (Component componentsInChild in instance.gameObject.GetComponentsInChildren<GameObjectID>())
                {
                  CanvasGroup canvasGroup = componentsInChild.RequireComponent<CanvasGroup>();
                  if ((double) canvasGroup.alpha != 1.0)
                    this.fadeInParticleList.Add(canvasGroup);
                }
              }
              int index = this.mBubble.transform.GetSiblingIndex() - 1;
              instance.transform.SetSiblingIndex(index);
              instance.transform.localScale = instance.transform.localScale * 1f;
              if (!string.IsNullOrEmpty(this.Emotion))
                instance.UpdateEmotion(this.Emotion);
            }
            else if (instance.isActiveAndEnabled)
            {
              Color color = Color.white;
              if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(instance.CharaID))
                color = Event2dAction_OperateStandChara.CharaColorDic[instance.CharaID] * Color.gray;
              foreach (GameObject standChara in instance.StandCharaList)
              {
                if (standChara.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().color != color)
                {
                  this.fadeOutList.Add(standChara);
                  this.IsFading = true;
                }
              }
              if (this.ActorParticle)
              {
                foreach (Component componentsInChild in instance.gameObject.GetComponentsInChildren<GameObjectID>())
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
      if ((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null && (double) this.Duration > 0.0)
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
      if (this.IsFading || !EventScript.IsMessageAuto || (!((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null) || !this.mBubble.Finished) || (this.mBubble.IsVoicePlaying || !EventScript.MessageAutoForward(Time.deltaTime)))
        return;
      this.Forward();
    }

    private void FadeIn(float time)
    {
      float t = time / this.FadeTime;
      Color color1 = Color.Lerp(Color.white, Color.grey, t);
      Color color2 = Color.Lerp(Color.grey, Color.white, t);
      foreach (GameObject fadeIn in this.fadeInList)
      {
        EventStandChara2 component = fadeIn.GetComponent<EventStandChara2>();
        string charaId = fadeIn.GetComponentInParent<EventStandCharaController2>().CharaID;
        Color white = Color.white;
        if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
          white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
        Color color3 = white * color1;
        if ((double) component.BodyObject.GetComponent<RawImage>().color.maxColorComponent <= (double) color3.maxColorComponent)
        {
          component.FaceObject.GetComponent<RawImage>().color = color3;
          component.BodyObject.GetComponent<RawImage>().color = color3;
        }
      }
      foreach (GameObject fadeOut in this.fadeOutList)
      {
        EventStandChara2 component = fadeOut.GetComponent<EventStandChara2>();
        string charaId = fadeOut.GetComponentInParent<EventStandCharaController2>().CharaID;
        Color white = Color.white;
        if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
          white = Event2dAction_OperateStandChara.CharaColorDic[charaId];
        Color color3 = white * color2;
        if ((double) component.BodyObject.GetComponent<RawImage>().color.maxColorComponent >= (double) color3.maxColorComponent)
        {
          component.FaceObject.GetComponent<RawImage>().color = color3;
          component.BodyObject.GetComponent<RawImage>().color = color3;
        }
      }
      float num1 = Mathf.Lerp(1f, 0.0f, t);
      float num2 = Mathf.Lerp(0.0f, 1f, t);
      foreach (CanvasGroup fadeInParticle in this.fadeInParticleList)
      {
        if ((double) fadeInParticle.alpha <= (double) num1)
          fadeInParticle.alpha = num1;
      }
      foreach (CanvasGroup fadeOutParticle in this.fadeOutParticleList)
      {
        if ((double) fadeOutParticle.alpha >= (double) num2)
          fadeOutParticle.alpha = num2;
      }
    }

    private void Shake(float time)
    {
      if ((double) time > 0.0)
      {
        float num = Mathf.Clamp01(time / this.Duration);
        this.bubbleTransform.pivot = new Vector2(this.originalPvt.x + Mathf.Sin((float) (((double) Time.time + (double) this.mSeedX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num, this.originalPvt.y + Mathf.Sin((float) (((double) Time.time + (double) this.mSeedY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num);
      }
      else
        this.bubbleTransform.pivot = this.originalPvt;
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
      if (this.Async || !((UnityEngine.Object) this.mBubble != (UnityEngine.Object) null))
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
          return EventAction.GetUnManagedStreamAssets(Event2dAction_Dialog3.GetIDPair(this.mVoiceID), false);
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
