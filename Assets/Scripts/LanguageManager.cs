using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    void Start()
    {
        SetLanguage();
    }
    void Update()
    {
        
    }
    public void SetLanguage()
    {
        SystemLanguage systemLanguage = Application.systemLanguage;

        SetLanguage(systemLanguage);
    }
    void SetLanguage(SystemLanguage systemLanguage)
    {
        Locale locale = null;

        switch (systemLanguage)
        {
            case SystemLanguage.Turkish:
                locale = LocalizationSettings.AvailableLocales.GetLocale("tr");
                break;
            case SystemLanguage.English:
                locale = LocalizationSettings.AvailableLocales.GetLocale("en");
                break;
            default:
                locale = LocalizationSettings.AvailableLocales.GetLocale("en");
                break;
        }

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}
