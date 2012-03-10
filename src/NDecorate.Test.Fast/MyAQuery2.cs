namespace NDecorate.Test.Fast
{
	public class MyAQuery2 : IQueryTypeA
	{
		public string Execute() {
			return "goodbye";
		}

		public IQueryTypeA DecoratorTarget { get; set; }
	}
}