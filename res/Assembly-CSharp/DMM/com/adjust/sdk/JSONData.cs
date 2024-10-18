// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.JSONData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.IO;

#nullable disable
namespace com.adjust.sdk
{
  public class JSONData : JSONNode
  {
    private string m_Data;

    public JSONData(string aData) => this.m_Data = aData;

    public JSONData(float aData) => this.AsFloat = aData;

    public JSONData(double aData) => this.AsDouble = aData;

    public JSONData(bool aData) => this.AsBool = aData;

    public JSONData(int aData) => this.AsInt = aData;

    public override string Value
    {
      get => this.m_Data;
      set => this.m_Data = value;
    }

    public override string ToString() => "\"" + JSONNode.Escape(this.m_Data) + "\"";

    public override string ToString(string aPrefix) => "\"" + JSONNode.Escape(this.m_Data) + "\"";

    public override void Serialize(BinaryWriter aWriter)
    {
      JSONData jsonData = new JSONData(string.Empty);
      jsonData.AsInt = this.AsInt;
      if (jsonData.m_Data == this.m_Data)
      {
        aWriter.Write((byte) 4);
        aWriter.Write(this.AsInt);
      }
      else
      {
        jsonData.AsFloat = this.AsFloat;
        if (jsonData.m_Data == this.m_Data)
        {
          aWriter.Write((byte) 7);
          aWriter.Write(this.AsFloat);
        }
        else
        {
          jsonData.AsDouble = this.AsDouble;
          if (jsonData.m_Data == this.m_Data)
          {
            aWriter.Write((byte) 5);
            aWriter.Write(this.AsDouble);
          }
          else
          {
            jsonData.AsBool = this.AsBool;
            if (jsonData.m_Data == this.m_Data)
            {
              aWriter.Write((byte) 6);
              aWriter.Write(this.AsBool);
            }
            else
            {
              aWriter.Write((byte) 3);
              aWriter.Write(this.m_Data);
            }
          }
        }
      }
    }
  }
}
