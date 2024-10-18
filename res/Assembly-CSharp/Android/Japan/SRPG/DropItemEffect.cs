// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

namespace SRPG
{
  public class DropItemEffect : MonoBehaviour
  {
    private DropItemEffect.State m_State = DropItemEffect.State.SETUP;
    public float Acceleration = 1f;
    public float Delay = 1f;
    public float OpenWait = 0.2f;
    public float PopSpeed = 1f;
    private float m_DeleteDelay = 1f;
    private const string TREASURE_GAMEOBJECT_NAME = "UI_TREASURE";
    private RectTransform m_TargetRect;
    private ItemIcon m_ItemIcon;
    private Unit m_DropOwner;
    private Unit.DropItem m_DropItem;
    private float m_Speed;
    private Animator m_EndAnimator;
    private float m_PopSpeed;
    private float m_ScaleSpeed;

    public RectTransform TargetRect
    {
      get
      {
        return this.m_TargetRect;
      }
    }

    public Unit DropOwner
    {
      set
      {
        this.m_DropOwner = value;
      }
      get
      {
        return this.m_DropOwner;
      }
    }

    public Unit.DropItem DropItem
    {
      set
      {
        this.m_DropItem = value;
      }
    }

    private void Start()
    {
      this.Hide();
    }

    private void Update()
    {
      switch (this.m_State)
      {
        case DropItemEffect.State.SETUP:
          this.State_Setup();
          break;
        case DropItemEffect.State.OPEN:
          this.State_Open();
          break;
        case DropItemEffect.State.POPUP:
          this.State_Popup();
          break;
        case DropItemEffect.State.MOVE:
          this.State_Move();
          break;
        case DropItemEffect.State.END:
          this.State_End();
          break;
        case DropItemEffect.State.DELETE:
          this.State_Delete();
          break;
      }
    }

    public void SetItem(Unit.DropItem item)
    {
      this.m_DropItem = item;
    }

    private void Hide()
    {
      IEnumerator enumerator = this.gameObject.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).gameObject.SetActive(false);
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
    }

    private void Show()
    {
      IEnumerator enumerator = this.gameObject.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).gameObject.SetActive(true);
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
      ItemIcon component = this.gameObject.GetComponent<ItemIcon>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null) || !component.IsSecret || !(bool) ((UnityEngine.Object) component.SecretAmount))
        return;
      component.SecretAmount.SetActive(false);
    }

    private void State_Setup()
    {
      GameObject gameObject = GameObjectID.FindGameObject("UI_TREASURE");
      if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
        Debug.LogError((object) "UI_TREASUREが見つかりませんでした。");
      else
        this.m_TargetRect = gameObject.transform as RectTransform;
      if (this.m_DropItem.isItem)
        DataSource.Bind<ItemParam>(this.gameObject, this.m_DropItem.itemParam, false);
      else if (this.m_DropItem.isConceptCard)
        DataSource.Bind<ConceptCardParam>(this.gameObject, this.m_DropItem.conceptCardParam, false);
      if ((bool) this.m_DropItem.is_secret)
      {
        ItemIcon component = (ItemIcon) this.gameObject.GetComponent<DropItemIcon>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.IsSecret = true;
      }
      GameParameter.UpdateAll(this.gameObject);
      this.m_ItemIcon = (ItemIcon) this.gameObject.GetComponent<DropItemIcon>();
      if ((UnityEngine.Object) this.m_ItemIcon != (UnityEngine.Object) null)
        this.m_ItemIcon.Num.text = this.m_DropItem.num.ToString();
      this.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 25f, this.transform.position.z);
      this.m_EndAnimator = gameObject.GetComponent<Animator>();
      this.m_State = DropItemEffect.State.OPEN;
    }

    private void State_Open()
    {
      if ((UnityEngine.Object) this.m_TargetRect == (UnityEngine.Object) null)
      {
        SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", (object) this);
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
      else
      {
        this.OpenWait -= Time.deltaTime;
        if ((double) this.OpenWait > 0.0)
          return;
        this.Show();
        this.m_State = DropItemEffect.State.POPUP;
      }
    }

    private void State_Popup()
    {
      this.Delay -= Time.deltaTime;
      this.m_PopSpeed += this.PopSpeed * Time.deltaTime;
      if (1.0 > (double) this.transform.localScale.x + (double) this.m_PopSpeed)
      {
        Vector3 localScale = this.transform.localScale;
        this.transform.localScale = new Vector3(localScale.x + this.m_PopSpeed, localScale.y + this.m_PopSpeed, localScale.z);
        float num = this.m_PopSpeed * 100f;
        if ((double) num > 25.0)
          num = 25f;
        Vector3 localPosition = this.transform.localPosition;
        this.transform.localPosition = new Vector3(localPosition.x, localPosition.y + num, localPosition.z);
      }
      else
      {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        if ((double) this.Delay >= 0.0)
          return;
        this.m_State = DropItemEffect.State.MOVE;
      }
    }

    private void State_Move()
    {
      this.m_Speed += this.Acceleration * Time.deltaTime;
      Vector3 position = this.m_TargetRect.position;
      position.x -= this.m_TargetRect.sizeDelta.x * 0.8f;
      position.y += this.m_TargetRect.sizeDelta.y * 0.5f;
      Vector3 vector3_1 = position - this.transform.position;
      Vector3 vector3_2 = vector3_1.normalized * this.m_Speed;
      if ((double) vector3_2.sqrMagnitude < (double) vector3_1.sqrMagnitude)
      {
        this.transform.position += vector3_2;
        this.m_ScaleSpeed += 1f * Time.deltaTime;
        if ((double) this.transform.localScale.x - (double) this.m_ScaleSpeed <= 0.5)
          return;
        this.transform.localScale = new Vector3(this.transform.localScale.x - this.m_ScaleSpeed, this.transform.localScale.y - this.m_ScaleSpeed, 1f);
      }
      else
      {
        this.transform.position = position;
        this.Hide();
        this.m_State = DropItemEffect.State.END;
      }
    }

    private void State_End()
    {
      SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", (object) this);
      if ((UnityEngine.Object) this.m_EndAnimator != (UnityEngine.Object) null)
      {
        if (!this.m_EndAnimator.GetBool("open"))
          this.m_EndAnimator.SetBool("open", true);
        else
          this.m_EndAnimator.Play("open", 0, 0.0f);
      }
      this.m_DeleteDelay = 0.1f;
      this.m_State = DropItemEffect.State.DELETE;
    }

    private void State_Delete()
    {
      this.m_DeleteDelay -= Time.deltaTime;
      if ((double) this.m_DeleteDelay >= 0.0)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      this.m_State = DropItemEffect.State.NONE;
    }

    private enum State
    {
      NONE,
      SETUP,
      OPEN,
      POPUP,
      MOVE,
      END,
      DELETE,
    }
  }
}
