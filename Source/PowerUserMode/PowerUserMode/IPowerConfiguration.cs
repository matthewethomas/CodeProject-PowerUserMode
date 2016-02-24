﻿using System.ComponentModel;

namespace PowerUserMode
{
    public interface IPowerConfiguration : INotifyPropertyChanged
    {        
        /// <summary>
        /// Gets whether or not the user can see an expanded list of options
        /// </summary>
        bool ShowExpandedOptions { get; }
        
        /// <summary>
        /// Gets whether or not the application will automatically advance to the next screen when appropriate
        /// </summary>
        bool AutoNext { get; }

        /// <summary>
        /// Gets whether warning dialog boxes will be suppressed
        /// </summary>
        /// <remarks>
        /// This option will not suppress validation, rather, it only suppresses the warning message
        /// </remarks>
        bool SuppressWarnings { get; }
    }
}
