
public class IDGenerateComponent
{
	private uint _nextID = 0;
	public uint GenID()
	{
		return _nextID++;
	}
}