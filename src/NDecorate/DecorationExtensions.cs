// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NDecorate.
// 
// NDecorate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NDecorate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NDecorate. If not, see <http://www.gnu.org/licenses/>.

namespace NDecorate
{
	using System;

	public static class IDecoratableExtensions
	{
		public static TSharedInterface
			Decorate<TSharedInterface>(this TSharedInterface instanceToDecorate,
			                           TSharedInterface[] decoratorList) //to be supplied via service locator
			where TSharedInterface : IDecorateable<TSharedInterface>, IDecorator<TSharedInterface> {
			for (var x = 0; x <= decoratorList.Length - 1; x++) {
				var decorator = decoratorList[x];
				var targetDecorateableInstance = x == 0
				                                 	? instanceToDecorate
				                                 	: decoratorList[x - 1];

				decorator.DecoratorTarget = targetDecorateableInstance;

				if (x == decoratorList.Length - 1) {
					return decorator;
				}
			}

			throw new Exception("Problem decorating type.");
		}
	}
}