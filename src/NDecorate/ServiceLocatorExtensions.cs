// Copyright 2011, Ben Aston (ben@bj.ma).
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
// along with NDecorate.  If not, see <http://www.gnu.org/licenses/>.

namespace NDecorate
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NServiceLocator;

	public static class DecoratorHelpers
	{
		/// <summary>
		/// 	WIP
		/// </summary>
		public static TSharedInterface[] GetDecoratorsFor<TSharedInterface, TActivationContext>(
			IServiceLocator<TActivationContext> serviceLocator,
			string[] decoratorPrefixes,
			string implicitDecoratorSuffix)
			where TSharedInterface : IDecorateable<TSharedInterface>, IDecorator<TSharedInterface>
		{
			var decoratorTypeNames = decoratorPrefixes.Select(i => i + implicitDecoratorSuffix);
			var decorators = new List<TSharedInterface>();

			foreach (var decoratorTypeName in decoratorTypeNames)
			{
				decorators.Add((TSharedInterface) serviceLocator.Locate(Type.GetType(decoratorTypeName)));
			}

			return decorators.ToArray();
		}

		//public static string[] GetDecoratorPrefixesFor<TSharedInterface, TFeature>(Enum feature, 
		//                                                                           Enum decoratorSetting, 
		//                                                                           FeatureManifest<TFeature> featureManifest,
		//                                                                           string decoratorListDelimiter = ",")
		//    where TFeature : struct
		//{
		//    var decoratorPrefixes =
		//            feature.Setting(decoratorSetting, featureManifest).Split(
		//                new[] { decoratorListDelimiter },
		//                        StringSplitOptions.RemoveEmptyEntries);
		//}
	}
}