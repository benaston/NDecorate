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

namespace NDecorate
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NServiceLocator;

	public static class DecoratorHelpers
	{
		/// <summary>
		/// 	The idea being here that at service locator initialization-time, types are registered with reference to this method.
		/// </summary>
		public static TSharedInterface[] GetDecoratorsFor<TSharedInterface, TActivationContext>(
			IServiceLocator<TActivationContext> serviceLocator,
			IEnumerable<string> decoratorTypeNames)
			where TSharedInterface : IDecorateable<TSharedInterface>, IDecorator<TSharedInterface> {
			var decorators = new List<TSharedInterface>();

			foreach (var decoratorTypeName in decoratorTypeNames) {
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