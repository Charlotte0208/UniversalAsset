using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    private void Awake()
    {
        // 确保该脚本在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);
    }

    public void SetLanguageToChinese()
    {
        SetLanguage("zh");
    }

    public void SetLanguageToEnglish()
    {
        SetLanguage("en");
    }

    private void SetLanguage(string localeCode)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(locale => locale.Identifier.Code == localeCode);
    }
}