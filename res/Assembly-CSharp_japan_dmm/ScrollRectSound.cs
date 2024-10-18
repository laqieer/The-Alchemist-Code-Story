// Decompiled with JetBrains decompiler
// Type: ScrollRectSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("Audio/ScrollRect Sound")]
public class ScrollRectSound : MonoBehaviour
{
  public string cueID = "0008";
  private Vector2 mPos;
  private Vector2 mPosDif;
  private float mWait;
  private bool mInitPos;
  private IntVector2 mPosID;

  private void Awake()
  {
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    if ((double) this.mWait <= 0.0)
      return;
    this.mWait -= Time.unscaledDeltaTime;
  }

  public void OnValueChanged()
  {
    if ((double) this.mWait > 0.0)
      return;
    this.mWait = 0.1f;
    ScrollRect component = ((Component) this).gameObject.GetComponent<ScrollRect>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    RectTransform content = component.content;
    if (Object.op_Equality((Object) content, (Object) null))
      return;
    int num = -1;
    for (int index = 0; index < ((Transform) content).childCount; ++index)
    {
      Transform child = ((Transform) content).GetChild(index);
      if (((Component) child).gameObject.GetActive())
      {
        if (!Object.op_Equality((Object) ((Component) child).gameObject.GetComponent<LayoutElement>(), (Object) null))
          ;
        num = index;
        break;
      }
    }
    if (num < 0)
      return;
    Transform child1 = ((Transform) content).GetChild(num);
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector(((Component) child1).transform.position.x, ((Component) child1).transform.position.y);
    Vector2 vector2_2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_2).\u002Ector(vector2_1.x, vector2_1.y);
    for (int index = 0; index < ((Transform) content).childCount; ++index)
    {
      if (index != num)
      {
        Transform child2 = ((Transform) content).GetChild(index);
        if (((Component) child2).gameObject.GetActive())
        {
          if (!Object.op_Equality((Object) ((Component) child2).gameObject.GetComponent<LayoutElement>(), (Object) null))
            ;
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector(((Component) child2).transform.position.x, ((Component) child2).transform.position.y);
          vector2_1.x = Math.Min(vector2_1.x, vector2_3.x);
          vector2_1.y = Math.Min(vector2_1.y, vector2_3.y);
          vector2_2.x = Math.Max(vector2_2.x, vector2_3.x);
          vector2_2.y = Math.Max(vector2_2.y, vector2_3.y);
        }
      }
    }
    Vector2 vector2_4;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_4).\u002Ector(Math.Abs(vector2_2.x - vector2_1.x), Math.Abs(vector2_2.y - vector2_1.y));
    if ((double) Math.Abs(vector2_4.x - this.mPosDif.x) >= 1.0 || (double) Math.Abs(vector2_4.y - this.mPosDif.y) >= 1.0 || !this.mInitPos)
    {
      this.mPosDif = vector2_4;
      this.mInitPos = false;
    }
    if (!this.mInitPos)
    {
      Transform child3 = ((Transform) content).GetChild(num);
      this.mPos = new Vector2(((Component) child3).transform.position.x, ((Component) child3).transform.position.y);
      this.mInitPos = true;
    }
    else
    {
      Vector2 vector2_5;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_5).\u002Ector(-1f, -1f);
      IntVector2 mPosId = this.mPosID;
      for (int index = 0; index < ((Transform) content).childCount; ++index)
      {
        Transform child4 = ((Transform) content).GetChild(index);
        if (((Component) child4).gameObject.GetActive())
        {
          if (!Object.op_Equality((Object) ((Component) child4).gameObject.GetComponent<LayoutElement>(), (Object) null))
            ;
          Vector2 vector2_6;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_6).\u002Ector(((Component) child4).transform.position.x, ((Component) child4).transform.position.y);
          Vector2 vector2_7;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_7).\u002Ector(Math.Abs(vector2_6.x - this.mPos.x), Math.Abs(vector2_6.y - this.mPos.y));
          if ((double) vector2_5.x < 0.0 || (double) vector2_7.x < (double) vector2_5.x)
          {
            vector2_5.x = vector2_7.x;
            mPosId.x = index;
          }
          if ((double) vector2_5.y < 0.0 || (double) vector2_7.y < (double) vector2_5.y)
          {
            vector2_5.y = vector2_7.y;
            mPosId.y = index;
          }
        }
      }
      if (mPosId.x != this.mPosID.x && this.mPosID.x > 0 && component.horizontal)
        this.Play();
      else if (mPosId.y != this.mPosID.y && this.mPosID.y > 0 && component.vertical)
        this.Play();
      this.mPosID = mPosId;
    }
  }

  public void Play() => MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID);

  public void Reset()
  {
    this.mWait = 0.0f;
    this.mInitPos = false;
    this.mPos = Vector2.zero;
    this.mPosDif = Vector2.zero;
    this.mPosID = new IntVector2(0, 0);
  }
}
