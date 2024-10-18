// Decompiled with JetBrains decompiler
// Type: SRPG.KeyQuestBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class KeyQuestBanner : BaseIcon
  {
    public GameObject IconRoot;
    public RawImage Icon;
    public Image Frame;
    public Text UseNum;
    public Text Amount;
    public GameObject Locked;
    public QuestTimeLimit QuestTimer;

    public override void UpdateValue()
    {
      KeyItem dataOfClass1 = DataSource.FindDataOfClass<KeyItem>(this.gameObject, (KeyItem) null);
      if (dataOfClass1 != null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass1.iname);
        if (itemParam != null)
        {
          ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
          int num = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
          if ((UnityEngine.Object) this.Icon != (UnityEngine.Object) null)
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(itemParam));
          if ((UnityEngine.Object) this.Frame != (UnityEngine.Object) null)
            this.Frame.sprite = GameSettings.Instance.GetItemFrame(itemParam);
          if ((UnityEngine.Object) this.UseNum != (UnityEngine.Object) null)
            this.UseNum.text = dataOfClass1.num.ToString();
          if ((UnityEngine.Object) this.Amount != (UnityEngine.Object) null)
            this.Amount.text = num.ToString();
          if ((UnityEngine.Object) this.Locked != (UnityEngine.Object) null)
          {
            ChapterParam dataOfClass2 = DataSource.FindDataOfClass<ChapterParam>(this.gameObject, (ChapterParam) null);
            this.Locked.SetActive(dataOfClass2 == null || !dataOfClass2.IsKeyQuest() || !dataOfClass2.IsKeyUnlock(Network.GetServerTime()));
          }
        }
      }
      if ((UnityEngine.Object) this.QuestTimer != (UnityEngine.Object) null)
        this.QuestTimer.UpdateValue();
      if (!((UnityEngine.Object) this.IconRoot != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Locked != (UnityEngine.Object) null))
        return;
      this.IconRoot.SetActive(this.Locked.activeSelf);
    }
  }
}
