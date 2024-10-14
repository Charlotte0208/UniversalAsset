using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class TypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro; // Automatically get the TMP component
    private LocalizeStringEvent localizedStringEvent; // Localized text event
    [SerializeField] private float typingSpeed = 0.05f;   // Typing speed
    [SerializeField] private float minimumFontSize = 10f; // Minimum font size
    [SerializeField] private float maximumFontSize = 100f; // Maximum font size
    private string fullText;    // Full text content
    private bool hasTyped = false;   // Flag indicating if the typing effect is complete

    void Awake()
    {
        // Automatically get the TextMeshProUGUI component
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found!");
        }

        // Automatically get the LocalizeStringEvent component from the GameObject
        localizedStringEvent = GetComponent<LocalizeStringEvent>();
        if (localizedStringEvent == null)
        {
            Debug.LogError("LocalizeStringEvent component not found!");
        }
        else
        {
            // Subscribe to the localized text change event
            localizedStringEvent.OnUpdateString.AddListener(UpdateLocalizedText);
        }

        // Clear the text content initially
        textMeshPro.text = "";
    }

    // Remove the listener when the GameObject is destroyed
    void OnDestroy()
    {
        if (localizedStringEvent != null)
        {
            localizedStringEvent.OnUpdateString.RemoveListener(UpdateLocalizedText);
        }
    }

    // Handle localized text updates
    void UpdateLocalizedText(string localizedText)
    {
        // Get the new localized text
        fullText = localizedText;

        // Adjust the font size based on the new text
        AdjustFontSizeUsingAutoSize();

        // Reset typing status
        hasTyped = false;

        // Reset the text and start the typewriter effect
        textMeshPro.text = "";
        StartCoroutine(TypeText());
    }

    // Typewriter effect coroutine
    IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            textMeshPro.text += letter;  // Add each letter one by one
            yield return new WaitForSeconds(typingSpeed);  // Wait for a set time interval
        }

        // Mark the typing effect as complete
        hasTyped = true;
    }

    // Adjust the font size to simulate TMP's Auto Size feature
    private void AdjustFontSizeUsingAutoSize()
    {
        // Enable TMP's Auto Size feature
        textMeshPro.enableAutoSizing = true;

        // Set the minimum and maximum font size range
        textMeshPro.fontSizeMin = minimumFontSize;
        textMeshPro.fontSizeMax = maximumFontSize;

        // Force TMP to update the layout and calculate the font size
        textMeshPro.ForceMeshUpdate();

        // Get the calculated final font size
        float finalFontSize = textMeshPro.fontSize;

        // Disable Auto Size and lock in the final font size
        textMeshPro.enableAutoSizing = false;
        textMeshPro.fontSize = finalFontSize;
    }
}
