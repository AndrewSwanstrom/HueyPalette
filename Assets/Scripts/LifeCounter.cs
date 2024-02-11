using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Toggle easyModeToggle;      // Toggle for Easy Mode
    public Toggle hardModeToggle;      // Toggle for Hard Mode
    public HueyController hueyController;
    public Image Easymodeicon;
    public Image Hardmodeicon;
    public Image Normalmodeicon;

    private int defaultHealth;

    private void Start()
    {
        // HealthValues
        defaultHealth = hueyController.health;

        // Set the default alpha of the Easymodeicon to 0
        ChangeAlpha(Easymodeicon, 0.0f);

        // Set the default alpha of the Hardmodeicon to 0
        ChangeAlpha(Hardmodeicon, 0.0f);

        // Set the default alpha of the NormalModeIcon to 1
        ChangeAlpha(Normalmodeicon, 1.0f);

        // Checks if toggled
        easyModeToggle.onValueChanged.AddListener(OnEasyModeToggleValueChanged);
        hardModeToggle.onValueChanged.AddListener(OnHardModeToggleValueChanged);
    }

    private void OnEasyModeToggleValueChanged(bool isEasyMode)
    {
        if (isEasyMode)
        {
            SetLives(99);
            // Set alpha to 0 when easy mode is turned on
            ChangeAlpha(Easymodeicon, 1.0f);
            ChangeAlpha(Hardmodeicon, 0.0f);
            ChangeAlpha(Normalmodeicon, 0.0f);
        }
        else if (hardModeToggle.isOn)
        {
            // If easy mode is off and hard mode is on, set alpha of Hardmodeicon to 1
            ChangeAlpha(Easymodeicon, 0.0f);
            ChangeAlpha(Hardmodeicon, 1.0f);
            ChangeAlpha(Normalmodeicon, 0.0f);
        }
        else
        {
            // If both easy mode and hard mode are off, set alpha of Normalmodeicon to 1
            SetLives(defaultHealth);
            ChangeAlpha(Easymodeicon, 0.0f);
            ChangeAlpha(Hardmodeicon, 0.0f);
            ChangeAlpha(Normalmodeicon, 1.0f);
        }
    }

    private void OnHardModeToggleValueChanged(bool isHardMode)
    {
        if (isHardMode)
        {
            SetLives(1);
            // Set alpha to 0 when hard mode is turned on
            ChangeAlpha(Hardmodeicon, 1.0f);
            ChangeAlpha(Easymodeicon, 0.0f);
            ChangeAlpha(Normalmodeicon, 0.0f);
        }
        else if (easyModeToggle.isOn)
        {
            // If hard mode is off and easy mode is on, set alpha of Easymodeicon to 1
            ChangeAlpha(Easymodeicon, 1.0f);
            ChangeAlpha(Hardmodeicon, 0.0f);
            ChangeAlpha(Normalmodeicon, 0.0f);
        }
        else
        {
            // If both hard mode and easy mode are off, set alpha of Normalmodeicon to 1
            SetLives(defaultHealth);
            ChangeAlpha(Easymodeicon, 0.0f);
            ChangeAlpha(Hardmodeicon, 0.0f);
            ChangeAlpha(Normalmodeicon, 1.0f);
        }
    }

    private void SetLives(int newLives)
    {
        // Debug
        Debug.Log("Setting lives to: " + newLives);

        // Update the health variable in HueyController
        hueyController.health = newLives;
    }

    private void ChangeAlpha(Image icon, float alphaValue)
    {
        // Set the alpha value
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, alphaValue);
    }
}
