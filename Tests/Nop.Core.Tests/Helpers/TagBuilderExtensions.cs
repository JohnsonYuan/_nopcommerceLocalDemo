// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Web.Mvc;

namespace System.Web.Helpers.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="renderMode"></param>
        /// <returns></returns>
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            Debug.Assert(tagBuilder != null);
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }
    }
}
