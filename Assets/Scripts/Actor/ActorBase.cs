public class ActorBase: ICommonManger
{
	public uint ID { get; set; }
	public virtual void CommonStart() { }
	public virtual void CommonUpdate() { }
	public virtual void CommonFixedUpdate() { }
	protected ActorBase()
	{
	}
}
