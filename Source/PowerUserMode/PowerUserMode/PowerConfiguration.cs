﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PowerUserMode
{
    /// <summary>
    /// A collection of settings for power user mode
    /// </summary>
    public class PowerConfiguration : IPowerConfiguration, IPowerConfigurationEditor
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IAppSettings appSettings;

        public PowerConfiguration(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        private bool isEnabled;
        
        /// <summary>
        /// Gets and sets whether or not the collection of subscribed <see cref="PowerSetting"/>s are enabled
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if(isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged();

                    var allSettings = Enum.GetValues(typeof(PowerSetting)).Cast<PowerSetting>();
                    foreach(var setting in allSettings)
                    {
                        OnPropertyChanged(setting.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Gets whether or not the user can see an expanded list of options
        /// </summary>
        public bool ShowExpandedOptions
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.ShowExtendedOptions);
            }           
        }

        /// <summary>
        /// Gets whether or not the application will automatically advance to the next screen when appropriate
        /// </summary>
        public bool AutoNext
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.AutoNext);
            }            
        }

        /// <summary>
        /// Gets whether or not to suppress the warning dialog box when a value is changed
        /// </summary>
        public bool SuppressValueChangedWarnings
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.SuppressValueChangedWarnings);
            }
        }

        /// <summary>
        /// Gets whether validation warning dialog boxes will be suppressed
        /// </summary>
        /// <remarks>
        /// This option will not suppress validation, rather, it only suppresses the warning message
        /// </remarks>
        public bool SuppressValidationWarnings
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.SuppressValidationWarnings);
            }           
        }

        /// <summary>
        /// Determines if the user is subscribed to a particular <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>Whether or not the user is subscribed to the <see cref="PowerSetting"/></returns>
        public bool IsSubscribed(PowerSetting setting)
        {
            return appSettings.GetIsSubscribed(setting);
        }

        /// <summary>
        /// Subscribes to a <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting to opt-in to</param>
        public void Subscribe(PowerSetting setting)
        {
            appSettings.SetIsSubscribed(setting, true);
            OnPropertyChanged(setting.ToString());
        }

        /// <summary>
        /// Unsubscribes from a <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting to opt-out of</param>
        public void Unsubscribe(PowerSetting setting)
        {
            appSettings.SetIsSubscribed(setting, false);
            OnPropertyChanged(setting.ToString());
        }

        /// <summary>
        /// Gets a <see cref="PowerSetting"/>'s enabled state, provided that it is currently being subscribed to
        /// and power user mode is enabled
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>Whether the <see cref="PowerSetting"/> is currently enabled</returns>
        private bool GetPropertyEnabled(PowerSetting setting)
        {
            //if the user is subscribing to the setting, then that setting is on if power user mode is on.
            //otherwise, that setting is considered off.
            if (appSettings.GetIsSubscribed(setting))
            {
                return IsEnabled;
            }
            else
            {
                return false;
            }
        }
        

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event, if there are subscribers
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
