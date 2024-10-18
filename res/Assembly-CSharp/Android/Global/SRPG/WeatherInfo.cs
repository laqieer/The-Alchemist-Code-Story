// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class WeatherInfo : MonoBehaviour
  {
    public GameObject GoWeatherInfo;

    private void Start()
    {
      if (!(bool) ((UnityEngine.Object) this.GoWeatherInfo))
        return;
      this.GoWeatherInfo.SetActive(false);
    }

    public void Refresh(WeatherData wd)
    {
      if (!(bool) ((UnityEngine.Object) this.GoWeatherInfo))
        return;
      if (wd != null)
      {
        GameObject goWeatherInfo = this.GoWeatherInfo;
        DataSource component = goWeatherInfo.GetComponent<DataSource>();
        if ((bool) ((UnityEngine.Object) component))
          component.Clear();
        DataSource.Bind<WeatherParam>(goWeatherInfo, wd.WeatherParam);
        GameParameter.UpdateAll(goWeatherInfo);
      }
      this.GoWeatherInfo.SetActive(wd != null);
    }
  }
}
