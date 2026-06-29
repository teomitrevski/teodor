using UnityEngine;
using TMPro; // Задолжително за TextMeshPro

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName; // Во Инспектор пиши: soundVolume или musicVolume
    [SerializeField] private string textIntro;  // Во Инспектор пиши: "Sound: " или "Music: "

    private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        if (txt == null) return;

        // Земаме од PlayerPrefs (ако нема ништо, почнува од 1)
        float volumeValue = PlayerPrefs.GetFloat(volumeName, 1f) * 100;

        // Mathf.RoundToInt ги средува дециматите за да пишува чисти бројки (пр. 40%, 60%)
        txt.text = textIntro + Mathf.RoundToInt(volumeValue).ToString() + "%";
    }
}