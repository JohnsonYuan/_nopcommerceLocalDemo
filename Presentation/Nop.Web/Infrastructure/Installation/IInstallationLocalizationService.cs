using System.Collections.Generic;

namespace Nop.Web.Infrastructure.Installation
{
    /// <summary>
    /// Localization service for installation process
    /// </summary>
    public partial interface IInstallationLocalizationService
    {
        /// <summary>
        /// Get locale resource value
        /// </summary>
        /// <param name="resouceName">Resource name</param>
        /// <returns>Resouce value</returns>
        string GetResource(string resouceName);

        /// <summary>
        /// Get current language for the installation page
        /// </summary>
        /// <returns>Current language</returns>
        InstallationLanguage GetCurrentLanguage();

        /// <summary>
        /// Save a language for the installation page
        /// </summary>
        /// <param name="languageCode"></param>
        void SaveCurrentLanguage(string languageCode);

        /// <summary>
        /// Get a list of available languages
        /// </summary>
        /// <returns>Available installation languages</returns>
        IList<InstallationLanguage> GetAvailableLanguages();
    }
}
