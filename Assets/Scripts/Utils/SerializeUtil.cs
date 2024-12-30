using Newtonsoft.Json;

public static class SerializeUtil
{
	public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings(){ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.Auto };
	public static string Serialize(this object obj)
	{
		return JsonConvert.SerializeObject(obj, JsonSettings);
	}
	public static T Deserialize<T>(this string str)
	{
		return JsonConvert.DeserializeObject<T>(str, JsonSettings);
	}
}