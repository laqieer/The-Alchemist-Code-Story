// Decompiled with JetBrains decompiler
// Type: SRPG.ItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ItemIcon : BaseIcon
  {
    [Description("「？」アイコン⇒正規アイコン変更アニメ開始までの時間")]
    public float SecretWaitSec = 1f;
    [Description("「？」アイコン⇒正規アイコン変更アニメトリガー名")]
    public string SecretAnimName = string.Empty;
    [Description("「？」アイコン⇒正規アイコン変更アニメ開始後、アイコンを差し替えるまでの時間")]
    public float SecretAnimWaitSec = 0.2f;
    protected const string TooltipPath = "UI/ItemTooltip";
    protected const string ICON_NAME_UNKNOWN = "IT_UNKNOWN";
    [Space(10f)]
    public GameParameter.ItemInstanceTypes InstanceType;
    public int InstanceIndex;
    [Space(10f)]
    public RawImage Icon;
    public Image Frame;
    public Text Num;
    public Slider NumSlider;
    public bool Tooltip;
    public Text HaveNum;
    public bool IsSecret;
    private ItemParam mSecretItemParam;
    [Description("個数表記GameObjectへの参照")]
    public GameObject SecretAmount;
    [Description("装備可能なユニットが存在する場合に表示状態を変更するバッジへの参照")]
    public Image SecretBadge;
    private bool mReqExchgSecretIcon;

    public override bool HasTooltip
    {
      get
      {
        if (!this.Tooltip)
          return false;
        ItemParam itemParam;
        int itemNum;
        this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
        return itemParam != null;
      }
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      RectTransform transform = this.transform as RectTransform;
      SRPG.Tooltip original = AssetManager.Load<SRPG.Tooltip>("UI/ItemTooltip");
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      SRPG.Tooltip tooltip = UnityEngine.Object.Instantiate<SRPG.Tooltip>(original);
      LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip.Body);
      float width = tooltip.Body.rect.width;
      Vector2 vector2 = screen;
      float num1 = screen.x - width / 2f;
      if ((double) num1 < 0.0)
        vector2 += Vector2.right * -num1;
      RectTransform component = this.transform.GetComponentInParent<Canvas>().rootCanvas.GetComponent<RectTransform>();
      LayoutRebuilder.ForceRebuildLayoutImmediate(component);
      float num2 = component.rect.width - (screen.x + width / 2f);
      if ((double) num2 < 0.0)
        vector2 += Vector2.left * -num2;
      SRPG.Tooltip.TooltipPosition = vector2 + Vector2.up * transform.rect.height * 0.5f;
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
      DataSource.Bind<ItemParam>(tooltip.gameObject, itemParam);
    }

    public override void UpdateValue()
    {
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
      if (itemParam == null)
        return;
      this.mSecretItemParam = itemParam;
      if ((UnityEngine.Object) this.Icon != (UnityEngine.Object) null)
      {
        if (this.IsSecret)
        {
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon("IT_UNKNOWN"));
          if ((bool) ((UnityEngine.Object) this.SecretAmount))
            this.SecretAmount.SetActive(false);
          if ((bool) ((UnityEngine.Object) this.SecretBadge))
            this.SecretBadge.enabled = false;
        }
        else
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(itemParam));
      }
      if ((UnityEngine.Object) this.Frame != (UnityEngine.Object) null)
      {
        if (this.IsSecret)
        {
          if (GameSettings.Instance.ItemIcons.NormalFrames != null && GameSettings.Instance.ItemIcons.NormalFrames.Length != 0)
            this.Frame.sprite = GameSettings.Instance.ItemIcons.NormalFrames[0];
        }
        else
          this.Frame.sprite = GameSettings.Instance.GetItemFrame(itemParam);
      }
      if ((UnityEngine.Object) this.Num != (UnityEngine.Object) null)
        this.Num.text = itemNum.ToString();
      if ((UnityEngine.Object) this.NumSlider != (UnityEngine.Object) null)
        this.NumSlider.value = (float) itemNum / (float) (int) itemParam.cap;
      if (!((UnityEngine.Object) this.HaveNum != (UnityEngine.Object) null))
        return;
      int num = -1;
      if (itemParam.iname == "$COIN")
      {
        num = MonoSingleton<GameManager>.Instance.Player.Coin;
      }
      else
      {
        ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
        if (itemDataByItemParam != null)
          num = itemDataByItemParam.Num;
      }
      if (num < 0)
        return;
      this.HaveNum.text = LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", new object[1]
      {
        (object) num
      });
    }

    public void ExchgSecretIcon()
    {
      if (!this.IsSecret || this.mReqExchgSecretIcon || this.mSecretItemParam == null)
        return;
      this.mReqExchgSecretIcon = true;
      this.StartCoroutine(this.exchgSecretIcon());
    }

    [DebuggerHidden]
    private IEnumerator exchgSecretIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ItemIcon.\u003CexchgSecretIcon\u003Ec__IteratorF6() { \u003C\u003Ef__this = this };
    }
  }
}
