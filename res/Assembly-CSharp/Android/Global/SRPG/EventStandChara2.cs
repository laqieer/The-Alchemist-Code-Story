// Decompiled with JetBrains decompiler
// Type: SRPG.EventStandChara2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventStandChara2 : MonoBehaviour
  {
    public static List<EventStandChara2> Instances = new List<EventStandChara2>();
    private float[] AnchorPostionX = new float[7]{ -1f, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f, 2f };
    public const float FADEIN_TIME = 0.3f;
    public const float FADEOUT_TIME = 0.5f;
    public string CharaID;
    [HideInInspector]
    public bool mClose;
    public GameObject FaceObject;
    public GameObject BodyObject;
    private float mFadeTime;
    private bool IsFading;
    private EventStandChara2.StateTypes mState;

    public bool IsClose
    {
      get
      {
        return this.mClose;
      }
    }

    public bool Fading
    {
      get
      {
        return this.IsFading;
      }
    }

    public EventStandChara2.StateTypes State
    {
      get
      {
        return this.mState;
      }
      set
      {
        this.mState = value;
      }
    }

    public static EventStandChara2 Find(string id)
    {
      for (int index = EventStandChara2.Instances.Count - 1; index >= 0; --index)
      {
        if (EventStandChara2.Instances[index].CharaID == id)
          return EventStandChara2.Instances[index];
      }
      return (EventStandChara2) null;
    }

    public static void DiscardAll()
    {
      for (int index = EventStandChara2.Instances.Count - 1; index >= 0; --index)
      {
        if (!EventStandChara2.Instances[index].gameObject.activeInHierarchy)
          UnityEngine.Object.Destroy((UnityEngine.Object) EventStandChara2.Instances[index].gameObject);
      }
      EventStandChara2.Instances.Clear();
    }

    public static string[] GetCharaIDs()
    {
      List<string> stringList = new List<string>();
      for (int index = EventStandChara2.Instances.Count - 1; index >= 0; --index)
        stringList.Add(EventStandChara2.Instances[index].CharaID);
      return stringList.ToArray();
    }

    private void Awake()
    {
      EventStandChara2.Instances.Add(this);
      this.mFadeTime = 0.0f;
      this.IsFading = false;
    }

    private void OnDestroy()
    {
      EventStandChara2.Instances.Remove(this);
      this.mState = EventStandChara2.StateTypes.Inactive;
    }

    public void Open()
    {
      if (!this.mClose)
        return;
      this.mClose = false;
      this.StartFadeIn();
    }

    public void Close()
    {
      if (this.mClose)
        return;
      this.mClose = true;
      this.StartFadeOut();
    }

    public void StartFadeIn()
    {
      this.IsFading = true;
      this.mFadeTime = 0.3f;
      this.mState = EventStandChara2.StateTypes.FadeIn;
    }

    public void StartFadeOut()
    {
      this.IsFading = true;
      this.mFadeTime = 0.5f;
      this.mState = EventStandChara2.StateTypes.FadeOut;
    }

    private void Update()
    {
      if (!this.IsFading)
        return;
      this.mFadeTime -= Time.deltaTime;
      if ((double) this.mFadeTime <= 0.0)
      {
        this.mFadeTime = 0.0f;
        this.IsFading = false;
      }
      else if (this.mState == EventStandChara2.StateTypes.FadeIn)
      {
        this.FadeIn(this.mFadeTime);
      }
      else
      {
        if (this.mState != EventStandChara2.StateTypes.FadeOut)
          return;
        this.FadeOut(this.mFadeTime);
      }
    }

    private void FadeIn(float time)
    {
      Color color1 = this.FaceObject.GetComponent<RawImage>().color;
      this.FaceObject.GetComponent<RawImage>().color = new Color(color1.r, color1.g, color1.b, Mathf.Lerp(1f, 0.0f, time));
      Color color2 = this.BodyObject.GetComponent<RawImage>().color;
      this.BodyObject.GetComponent<RawImage>().color = new Color(color2.r, color2.g, color2.b, Mathf.Lerp(1f, 0.0f, time));
    }

    private void FadeOut(float time)
    {
      Color color1 = this.FaceObject.GetComponent<RawImage>().color;
      this.FaceObject.GetComponent<RawImage>().color = new Color(color1.r, color1.g, color1.b, Mathf.Lerp(0.0f, 1f, time));
      Color color2 = this.BodyObject.GetComponent<RawImage>().color;
      this.BodyObject.GetComponent<RawImage>().color = new Color(color2.r, color2.g, color2.b, Mathf.Lerp(0.0f, 1f, time));
    }

    public float GetAnchorPostionX(int index)
    {
      if (index >= 0 && index < this.AnchorPostionX.Length)
        return this.AnchorPostionX[index];
      return 0.0f;
    }

    public enum PositionTypes
    {
      OverLeft,
      Left,
      HLeft,
      Center,
      HRight,
      Right,
      OverRight,
      None,
    }

    public enum StateTypes
    {
      FadeIn,
      Active,
      FadeOut,
      Inactive,
      None,
    }
  }
}
