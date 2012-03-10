namespace NDecorate.Test.Fast
{
	public class MyAQuery1 : IQueryTypeA
	{
		public string Execute() {
			return "hello";
		}

		public IQueryTypeA DecoratorTarget { get; set; }
	}
}