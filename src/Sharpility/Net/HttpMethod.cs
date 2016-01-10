using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpility.Net
{
    /// <summary>
    /// Enum with http method types.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// GET method.
        /// </summary>
        Get,

        /// <summary>
        /// POST method.
        /// </summary>
        Post,

        /// <summary>
        /// DELETE method.
        /// </summary>
        Delete,

        /// <summary>
        /// PUT method.
        /// </summary>
        Put,

        /// <summary>
        /// PATCH method.
        /// </summary>
        Patch,

        /// <summary>
        /// OPTIONS method.
        /// </summary>
        Options
    }
}
