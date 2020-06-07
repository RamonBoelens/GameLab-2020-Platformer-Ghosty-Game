using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnValueChanged : MonoBehaviour
{
    private GameSettings gameSettings;

    private void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();
    }

    public void OnValueChange()
    {
        gameSettings.OnValueChanged();
    }
}
