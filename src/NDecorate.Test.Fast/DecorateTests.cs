// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NBasicExtensionMethod.
// 
// NBasicExtensionMethod is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NBasicExtensionMethod is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NBasicExtensionMethod. If not, see <http://www.gnu.org/licenses/>.

namespace NDecorate.Test.Fast
{
	using NUnit.Framework;

	[TestFixture]
	public class Decorate_HappyPath_DecoratesType
	{
		[Test]
		public void Decorate_WhenSuppliedWithMultipleDecorators_ModifiesBehaviorCorrectly() {
			var query = new MyAQuery1();
			Assert.That(query.Execute() == "hello");
			var decoratedQuery = query.Decorate(new IQueryTypeA[] {
				new WorldAdderDecorator(),
				new ExclamationAdderDecorator(),
			});
			Assert.That(decoratedQuery.Execute() == "hello world!");
		}
	}

	public class WorldAdderDecorator : IQueryTypeA
	{
		public IQueryTypeA DecoratorTarget { get; set; }

		public string Execute() {
			return DecoratorTarget.Execute() + " world";
		}
	}

	public class ExclamationAdderDecorator : IQueryTypeA
	{
		public IQueryTypeA DecoratorTarget { get; set; }

		public string Execute() {
			return DecoratorTarget.Execute() + "!";
		}
	}
}

// ReSharper restore InconsistentNaming