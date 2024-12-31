using UnityEngine;

public static class TransformUtil
{
	public static string PathFromRoot(this Transform t)
	{
		string path = t.name;
		while (t.parent != null)
		{
			t = t.parent;
			path = t.name + "/" + path;
		}
		return path;
	}
}