using System;
using UD = UnityEngine.Debug;
public static class LogUtil
{
	private static string FormatMsg(string prefix, string msg)
	{
		return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{prefix}]: {msg}";
	}
	public static void Error(string msg)
	{
		UD.LogError(FormatMsg("Error", msg));
	}
	public static void Debug(string msg)
	{
		if(!Common.Debug) return;
		UD.LogWarning(FormatMsg("Debug", msg));
	}
}
