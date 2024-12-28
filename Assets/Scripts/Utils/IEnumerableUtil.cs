using System;
using System.Collections.Generic;

public static class IEnumerableUtil
{
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
		foreach (var item in enumerable)
		{
			action(item);
		}
	}
	public static Queue<T> EnqueueRange<T>(this Queue<T> q, IEnumerable<T> enumerable)
	{
		enumerable.ForEach(q.Enqueue);
		return q;
	}
	public static Queue<T> DequeueRange<T>(this Queue<T> q, Action<T> action)
	{
		q.ForEach(action);
		
		while(q.Count > 0)
		{
			action(q.Dequeue());
		}
		return q;
	}
}