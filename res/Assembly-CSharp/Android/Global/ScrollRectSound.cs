// Decompiled with JetBrains decompiler
// Type: ScrollRectSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

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
    ScrollRect component = this.gameObject.GetComponent<ScrollRect>();
    if ((UnityEngine.Object) component == (UnityEngine.Object) null)
      return;
    RectTransform content = component.content;
    if ((UnityEngine.Object) content == (UnityEngine.Object) null)
      return;
    int index1 = -1;
    for (int index2 = 0; index2 < content.childCount; ++index2)
    {
      Transform child = content.GetChild(index2);
      if (child.gameObject.GetActive())
      {
        if (!((UnityEngine.Object) child.gameObject.GetComponent<LayoutElement>() == (UnityEngine.Object) null))
          ;
        index1 = index2;
        break;
      }
    }
    if (index1 < 0)
      return;
    Transform child1 = content.GetChild(index1);
    Vector2 vector2_1 = new Vector2(child1.transform.position.x, child1.transform.position.y);
    Vector2 vector2_2 = new Vector2(vector2_1.x, vector2_1.y);
    for (int index2 = 0; index2 < content.childCount; ++index2)
    {
      if (index2 != index1)
      {
        Transform child2 = content.GetChild(index2);
        if (child2.gameObject.GetActive())
        {
          if (!((UnityEngine.Object) child2.gameObject.GetComponent<LayoutElement>() == (UnityEngine.Object) null))
            ;
          Vector2 vector2_3 = new Vector2(child2.transform.position.x, child2.transform.position.y);
          vector2_1.x = Math.Min(vector2_1.x, vector2_3.x);
          vector2_1.y = Math.Min(vector2_1.y, vector2_3.y);
          vector2_2.x = Math.Max(vector2_2.x, vector2_3.x);
          vector2_2.y = Math.Max(vector2_2.y, vector2_3.y);
        }
      }
    }
    Vector2 vector2_4 = new Vector2(Math.Abs(vector2_2.x - vector2_1.x), Math.Abs(vector2_2.y - vector2_1.y));
    if ((double) Math.Abs(vector2_4.x - this.mPosDif.x) >= 1.0 || (double) Math.Abs(vector2_4.y - this.mPosDif.y) >= 1.0 || !this.mInitPos)
    {
      this.mPosDif = vector2_4;
      this.mInitPos = false;
    }
    if (!this.mInitPos)
    {
      Transform child2 = content.GetChild(index1);
      this.mPos = new Vector2(child2.transform.position.x, child2.transform.position.y);
      this.mInitPos = true;
    }
    else
    {
      Vector2 vector2_3 = new Vector2(-1f, -1f);
      IntVector2 mPosId = this.mPosID;
      for (int index2 = 0; index2 < content.childCount; ++index2)
      {
        Transform child2 = content.GetChild(index2);
        if (child2.gameObject.GetActive())
        {
          if (!((UnityEngine.Object) child2.gameObject.GetComponent<LayoutElement>() == (UnityEngine.Object) null))
            ;
          Vector2 vector2_5 = new Vector2(child2.transform.position.x, child2.transform.position.y);
          Vector2 vector2_6 = new Vector2(Math.Abs(vector2_5.x - this.mPos.x), Math.Abs(vector2_5.y - this.mPos.y));
          if ((double) vector2_3.x < 0.0 || (double) vector2_6.x < (double) vector2_3.x)
          {
            vector2_3.x = vector2_6.x;
            mPosId.x = index2;
          }
          if ((double) vector2_3.y < 0.0 || (double) vector2_6.y < (double) vector2_3.y)
          {
            vector2_3.y = vector2_6.y;
            mPosId.y = index2;
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

  public void Play()
  {
    MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
  }

  public void Reset()
  {
    this.mWait = 0.0f;
    this.mInitPos = false;
    this.mPos = Vector2.zero;
    this.mPosDif = Vector2.zero;
    this.mPosID = new IntVector2(0, 0);
  }
}
