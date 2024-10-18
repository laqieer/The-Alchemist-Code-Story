// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DropItemEffect : MonoBehaviour
  {
    private const string TREASURE_GAMEOBJECT_NAME = "UI_TREASURE";
    private DropItemEffect.State m_State = DropItemEffect.State.SETUP;
    private RectTransform m_TargetRect;
    private ItemIcon m_ItemIcon;
    private Unit m_DropOwner;
    private Unit.DropItem m_DropItem;
    public float Acceleration = 1f;
    public float Delay = 1f;
    private float m_Speed;
    private Animator m_EndAnimator;
    public float OpenWait = 0.2f;
    public float PopSpeed = 1f;
    private float m_PopSpeed;
    private float m_ScaleSpeed;
    private float m_DeleteDelay = 1f;

    public RectTransform TargetRect => this.m_TargetRect;

    public Unit DropOwner
    {
      set => this.m_DropOwner = value;
      get => this.m_DropOwner;
    }

    public Unit.DropItem DropItem
    {
      set => this.m_DropItem = value;
    }

    private void Start() => this.Hide();

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

    public void SetItem(Unit.DropItem item) => this.m_DropItem = item;

    private void Hide()
    {
      foreach (Component component in ((Component) this).gameObject.transform)
        component.gameObject.SetActive(false);
    }

    private void Show()
    {
      foreach (Component component in ((Component) this).gameObject.transform)
        component.gameObject.SetActive(true);
      ItemIcon component1 = ((Component) this).gameObject.GetComponent<ItemIcon>();
      if (!Object.op_Inequality((Object) component1, (Object) null) || !component1.IsSecret || !Object.op_Implicit((Object) component1.SecretAmount))
        return;
      component1.SecretAmount.SetActive(false);
    }

    private void State_Setup()
    {
      GameObject gameObject = GameObjectID.FindGameObject("UI_TREASURE");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        Debug.LogError((object) "UI_TREASUREが見つかりませんでした。");
      else
        this.m_TargetRect = gameObject.transform as RectTransform;
      if (this.m_DropItem.isItem)
        DataSource.Bind<ItemParam>(((Component) this).gameObject, this.m_DropItem.itemParam);
      else if (this.m_DropItem.isConceptCard)
        DataSource.Bind<ConceptCardParam>(((Component) this).gameObject, this.m_DropItem.conceptCardParam);
      if ((bool) this.m_DropItem.is_secret)
      {
        ItemIcon component = (ItemIcon) ((Component) this).gameObject.GetComponent<DropItemIcon>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.IsSecret = true;
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.m_ItemIcon = (ItemIcon) ((Component) this).gameObject.GetComponent<DropItemIcon>();
      if (Object.op_Inequality((Object) this.m_ItemIcon, (Object) null))
        this.m_ItemIcon.Num.text = this.m_DropItem.num.ToString();
      ((Component) this).transform.localScale = new Vector3(0.3f, 0.3f, 1f);
      ((Component) this).transform.position = new Vector3(((Component) this).transform.position.x, ((Component) this).transform.position.y + 25f, ((Component) this).transform.position.z);
      this.m_EndAnimator = gameObject.GetComponent<Animator>();
      this.m_State = DropItemEffect.State.OPEN;
    }

    private void State_Open()
    {
      if (Object.op_Equality((Object) this.m_TargetRect, (Object) null))
      {
        SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", (object) this);
        Object.Destroy((Object) ((Component) this).gameObject);
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
      if (1.0 > (double) ((Component) this).transform.localScale.x + (double) this.m_PopSpeed)
      {
        Vector3 localScale = ((Component) this).transform.localScale;
        ((Component) this).transform.localScale = new Vector3(localScale.x + this.m_PopSpeed, localScale.y + this.m_PopSpeed, localScale.z);
        float num = this.m_PopSpeed * 100f;
        if ((double) num > 25.0)
          num = 25f;
        Vector3 localPosition = ((Component) this).transform.localPosition;
        ((Component) this).transform.localPosition = new Vector3(localPosition.x, localPosition.y + num, localPosition.z);
      }
      else
      {
        ((Component) this).transform.localScale = new Vector3(1f, 1f, 1f);
        if ((double) this.Delay >= 0.0)
          return;
        this.m_State = DropItemEffect.State.MOVE;
      }
    }

    private void State_Move()
    {
      this.m_Speed += this.Acceleration * Time.deltaTime;
      Vector3 position = ((Transform) this.m_TargetRect).position;
      position.x -= this.m_TargetRect.sizeDelta.x * 0.8f;
      position.y += this.m_TargetRect.sizeDelta.y * 0.5f;
      Vector3 vector3_1 = Vector3.op_Subtraction(position, ((Component) this).transform.position);
      Vector3 vector3_2 = Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, this.m_Speed);
      if ((double) ((Vector3) ref vector3_2).sqrMagnitude < (double) ((Vector3) ref vector3_1).sqrMagnitude)
      {
        Transform transform = ((Component) this).transform;
        transform.position = Vector3.op_Addition(transform.position, vector3_2);
        this.m_ScaleSpeed += 1f * Time.deltaTime;
        if ((double) ((Component) this).transform.localScale.x - (double) this.m_ScaleSpeed <= 0.5)
          return;
        ((Component) this).transform.localScale = new Vector3(((Component) this).transform.localScale.x - this.m_ScaleSpeed, ((Component) this).transform.localScale.y - this.m_ScaleSpeed, 1f);
      }
      else
      {
        ((Component) this).transform.position = position;
        this.Hide();
        this.m_State = DropItemEffect.State.END;
      }
    }

    private void State_End()
    {
      SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", (object) this);
      if (Object.op_Inequality((Object) this.m_EndAnimator, (Object) null))
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
      Object.Destroy((Object) ((Component) this).gameObject);
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
