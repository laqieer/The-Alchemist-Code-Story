// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WeatherInfo : MonoBehaviour
  {
    public GameObject GoWeatherInfo;

    private void Start()
    {
      if (!Object.op_Implicit((Object) this.GoWeatherInfo))
        return;
      this.GoWeatherInfo.SetActive(false);
    }

    public void Refresh(WeatherData wd)
    {
      if (!Object.op_Implicit((Object) this.GoWeatherInfo))
        return;
      if (wd != null)
      {
        GameObject goWeatherInfo = this.GoWeatherInfo;
        DataSource component = goWeatherInfo.GetComponent<DataSource>();
        if (Object.op_Implicit((Object) component))
          component.Clear();
        DataSource.Bind<WeatherParam>(goWeatherInfo, wd.WeatherParam);
        GameParameter.UpdateAll(goWeatherInfo);
      }
      this.GoWeatherInfo.SetActive(wd != null);
    }
  }
}
