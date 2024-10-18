// Decompiled with JetBrains decompiler
// Type: SRPG.EventStandChara2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventStandChara2 : MonoBehaviour
  {
    public static List<EventStandChara2> Instances = new List<EventStandChara2>();
    public string CharaID;
    [HideInInspector]
    public bool mClose;
    public GameObject FaceObject;
    public GameObject BodyObject;
    private float[] AnchorPostionX = new float[7]
    {
      -1f,
      0.2f,
      0.35f,
      0.5f,
      0.65f,
      0.8f,
      2f
    };
    public const float FADEIN_TIME = 0.3f;
    public const float FADEOUT_TIME = 0.5f;
    private float mFadeTime;
    private bool IsFading;
    private EventStandChara2.StateTypes mState;

    public bool IsClose => this.mClose;

    public bool Fading => this.IsFading;

    public EventStandChara2.StateTypes State
    {
      get => this.mState;
      set => this.mState = value;
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
        if (!((Component) EventStandChara2.Instances[index]).gameObject.activeInHierarchy)
          Object.Destroy((Object) ((Component) EventStandChara2.Instances[index]).gameObject);
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
      if (Object.op_Inequality((Object) this.FaceObject, (Object) null))
      {
        RawImage component = this.FaceObject.GetComponent<RawImage>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.texture = (Texture) null;
          Object.Destroy((Object) component);
        }
      }
      if (Object.op_Inequality((Object) this.BodyObject, (Object) null))
      {
        RawImage component = this.BodyObject.GetComponent<RawImage>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.texture = (Texture) null;
          Object.Destroy((Object) component);
        }
      }
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
      Color color1 = ((Graphic) this.FaceObject.GetComponent<RawImage>()).color;
      ((Graphic) this.FaceObject.GetComponent<RawImage>()).color = new Color(color1.r, color1.g, color1.b, Mathf.Lerp(1f, 0.0f, time));
      Color color2 = ((Graphic) this.BodyObject.GetComponent<RawImage>()).color;
      ((Graphic) this.BodyObject.GetComponent<RawImage>()).color = new Color(color2.r, color2.g, color2.b, Mathf.Lerp(1f, 0.0f, time));
    }

    private void FadeOut(float time)
    {
      Color color1 = ((Graphic) this.FaceObject.GetComponent<RawImage>()).color;
      ((Graphic) this.FaceObject.GetComponent<RawImage>()).color = new Color(color1.r, color1.g, color1.b, Mathf.Lerp(0.0f, 1f, time));
      Color color2 = ((Graphic) this.BodyObject.GetComponent<RawImage>()).color;
      ((Graphic) this.BodyObject.GetComponent<RawImage>()).color = new Color(color2.r, color2.g, color2.b, Mathf.Lerp(0.0f, 1f, time));
    }

    public float GetAnchorPostionX(int index)
    {
      return index >= 0 && index < this.AnchorPostionX.Length ? this.AnchorPostionX[index] : 0.0f;
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
