namespace NDecorate.Test.Fast
{
	public interface IQueryTypeA : IDecorator<IQueryTypeA>
	{
		string Execute();
	}
}