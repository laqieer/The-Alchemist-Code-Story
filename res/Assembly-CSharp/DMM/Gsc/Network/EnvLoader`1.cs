// Decompiled with JetBrains decompiler
// Type: Gsc.Network.EnvLoader`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM.Json;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Network
{
  public class EnvLoader<T> : Request<EnvLoader<T>, EnvLoader<T>.Response> where T : struct, Configuration.IEnvironment
  {
    private string url;

    public EnvLoader(string url) => this.url = url;

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary = parameters;
      Serializer instance = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (EnvLoader<T>.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        EnvLoader<T>.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = EnvLoader<T>.\u003C\u003Ef__mg\u0024cache0;
      object obj = instance.Add<string>(fMgCache0).Serialize<string>(SRPG.Network.Version);
      dictionary["ver"] = obj;
      return parameters;
    }

    public override string GetMethod() => "POST";

    public override string GetPath() => new Uri(this.url).AbsolutePath;

    public override string GetUrl() => this.url;

    public class Response : Gsc.Network.Response<EnvLoader<T>.Response>
    {
      public readonly Dictionary<string, string> VerRoute = new Dictionary<string, string>();
      public readonly Dictionary<string, Configuration.IEnvironment> Envs = new Dictionary<string, Configuration.IEnvironment>();

      public Response(WebInternalResponse response)
      {
        using (Document document = Document.Parse(response.Payload))
        {
          Gsc.DOM.Json.Object @object = document.Root.GetObject();
          T obj1 = new T();
          foreach (Member member in @object)
          {
            if (!(member.Name == "body"))
            {
              if (member.Value.IsLong())
                obj1.SetValue(member.Name, member.Value.ToLong().ToString());
              else
                obj1.SetValue(member.Name, member.Value.ToString());
            }
          }
          Value obj2;
          if (!@object.TryGetValue("body", out obj2))
          {
            Value obj3;
            Value obj4;
            if (@object.TryGetValue("stat", out obj3) && @object.TryGetValue("stat_code", out obj4) && obj3.IsInt() && 1.Equals(obj3.ToInt()) && obj4.IsString() && "unknown".Equals(obj4.ToString()))
              throw new MissingFieldException();
            this.Envs.Add("error", (Configuration.IEnvironment) obj1);
            this.VerRoute.Add("default", "error");
          }
          else
          {
            foreach (Member member1 in obj2.GetObject())
            {
              if (member1.Name == "environments")
              {
                using (IEnumerator<Member> enumerator = member1.Value.GetObject().GetEnumerator())
                {
                  if (enumerator.MoveNext())
                  {
                    Member current = enumerator.Current;
                    string name = current.Name;
                    foreach (Member member2 in current.Value.GetObject())
                    {
                      if (member2.Name == "env_name")
                        name = member2.Value.ToString();
                      else if (member2.Value.IsInt())
                        obj1.SetValue(member2.Name, member2.Value.ToInt().ToString());
                      else
                        obj1.SetValue(member2.Name, member2.Value.ToString());
                    }
                    this.Envs.Add(name, (Configuration.IEnvironment) obj1);
                    this.VerRoute.Add("default", name);
                    break;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
