﻿using Nop.Core.Domain.Stores;

namespace Nop.Core
{
    /// <summary>
    /// Store context.
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// Gets or sets the   store.
        /// </summary>
        Store CurrentStore { get; }
    }
}
