// Decompiled with JetBrains decompiler
// Type: IntentHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public class IntentHandler
{
  private static IntentHandler instance = new IntentHandler();

  public static IntentHandler GetInstance()
  {
    return IntentHandler.instance;
  }

  public void ClearIntentHandlers()
  {
  }

  public void AddNoopIntentHandler()
  {
  }

  public void AddUrlIntentHandler()
  {
  }

  public void AddCustomIntentHandler(string gameObjectName, string methodName)
  {
  }
}
