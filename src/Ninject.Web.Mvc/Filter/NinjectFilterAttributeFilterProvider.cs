// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectFilterAttributeFilterProvider.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG. All rights reserved.
//   Copyright (c) 2010-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.Filter
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Filter provider that gets the filters form the attributes of the actions.
    /// </summary>
    public class NinjectFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        /// <summary>
        /// The ninject kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectFilterAttributeFilterProvider"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectFilterAttributeFilterProvider(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Gets the controller attributes.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The filters defined by attributes</returns>
        protected override IEnumerable<FilterAttribute> GetControllerAttributes(
            ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            foreach (var attribute in attributes)
            {
                this.kernel.Inject(attribute);
            }

            return attributes;
        }

        /// <summary>
        /// Gets the action attributes.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The filters defined by attributes.</returns>
        protected override IEnumerable<FilterAttribute> GetActionAttributes(
            ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            foreach (var attribute in attributes)
            {
                this.kernel.Inject(attribute);
            }

            return attributes;
        }
    }
}