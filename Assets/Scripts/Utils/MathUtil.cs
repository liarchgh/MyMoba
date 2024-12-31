
public static class MathUtil
{
	public static uint Min(uint a, uint b)
	{
		return (a < b) ? a : b;
	}
    public static uint Max(uint a, uint b)
    {
        return (a > b) ? a : b;
    }
    public static uint Clamp(uint value, uint min, uint max)
    {
        if (value < min)
        {
            value = min;
        }
        else if (value > max)
        {
            value = max;
        }

        return value;
    }
}