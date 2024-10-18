// Decompiled with JetBrains decompiler
// Type: SRPG.ShopLineupWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ShopLineupWindow : MonoBehaviour
  {
    [SerializeField]
    private UnityEngine.UI.Text title;
    [SerializeField]
    private UnityEngine.UI.Text lineuplist;

    private void Start()
    {
      if ((UnityEngine.Object) this.lineuplist == (UnityEngine.Object) null || (UnityEngine.Object) this.title == (UnityEngine.Object) null)
        return;
      this.title.text = LocalizedText.Get("sys.TITLE_SHOP_LINEUP") + " (" + LocalizedText.Get(MonoSingleton<GameManager>.Instance.Player.GetShopName(GlobalVars.ShopType)) + ")";
    }

    public void SetItemInames(ShopLineupParam[] lineups)
    {
      if (lineups == null || lineups.Length <= 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < lineups.Length; ++index1)
      {
        string[] items = lineups[index1].items;
        for (int index2 = 0; index2 < items.Length; ++index2)
        {
          if (items[index2].StartsWith("AF_"))
          {
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(items[index2]);
            if (artifactParam != null)
              stringBuilder.Append("・" + artifactParam.name + "\n");
          }
          else
          {
            ItemParam itemParam = instance.GetItemParam(items[index2]);
            if (itemParam != null)
              stringBuilder.Append("・" + itemParam.name + "\n");
          }
        }
        stringBuilder.Append("\n");
      }
      stringBuilder.Append(LocalizedText.Get("sys.MSG_SHOP_LINEUP_CAUTION"));
      this.lineuplist.text = stringBuilder.ToString();
    }
  }
}
