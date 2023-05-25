﻿using OnionCrafter.Base.Commons;

namespace OnionCrafter.Service.Options.ServiceContainerOptions
{
    /// <summary>
    /// Provides options for configuring a service container, including whether to use a logger and logger options.
    /// </summary>
    /// <typeparam name="TLoggerOptions">The type of logger options to use.</typeparam>
    public interface IServiceContainerOptions<TLoggerOptions> : IBaseServiceContainerOptions, IUseLogger
        where TLoggerOptions : class, IServiceContainerLoggerOptions
    {
        /// <summary>
        /// Gets or sets the logger options to use.
        /// </summary>
        public TLoggerOptions LoggerOptions { get; set; }
    }
}