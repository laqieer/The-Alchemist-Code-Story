// Decompiled with JetBrains decompiler
// Type: GR.File
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.IO;
using System.Text;

#nullable disable
namespace GR
{
  public class File
  {
    public static void Write(string path, string data)
    {
      StreamWriter streamWriter = new StreamWriter(path, false, (Encoding) new UTF8Encoding(false));
      streamWriter.Write(data);
      streamWriter.Flush();
      streamWriter.Close();
      streamWriter.Dispose();
    }

    public static string Read(string path)
    {
      StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
      string end = streamReader.ReadToEnd();
      streamReader.Close();
      streamReader.Dispose();
      return end;
    }

    public static void WriteAllBytes(string path, byte[] bytes) => System.IO.File.WriteAllBytes(path, bytes);

    public static byte[] ReadAllBytes(string path) => System.IO.File.ReadAllBytes(path);
  }
}
