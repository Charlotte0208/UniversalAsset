using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class VIDELocalization : MonoBehaviour
{
    void Start()
    {
        LogCurrentLocalization();
    }

    void LogCurrentLocalization()
    {
        // 获取当前语言环境
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        // Debug.Log 当前语言环境的名称
        Debug.Log("当前语言环境: " + currentLocale.LocaleName);
    }
}