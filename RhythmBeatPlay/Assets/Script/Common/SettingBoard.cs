using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingBoard : MonoBehaviour
{
    [SerializeField]
    GameObject parent;

    [SerializeField]
    Slider musicVolumeSlider;

    [SerializeField]
    Slider bgsVolumeSlider;

    [SerializeField]
    Text musicVolumeValue;

    [SerializeField]
    Text bgsVolumeValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        musicVolumeSlider.value = DataManager.Instance.music_volume;
        bgsVolumeSlider.value = DataManager.Instance.bgs_volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCloseButtonClicked()
    {
        DataManager.Instance.music_volume_event.Invoke();
        BGSPlayer.Instance.playBGS("buttonOFF");
        Destroy(parent);
    }

    // function for button

    public void musicVolumeUp()
    {
        musicVolumeSlider.value += 0.01f;
    }

    public void musicVolumeDown()
    {
        musicVolumeSlider.value -= 0.01f;
    }

    public void bgsVolumeUp()
    {
        bgsVolumeSlider.value += 0.01f;
    }

    public void bgsVolumeDown()
    {
        bgsVolumeSlider.value -= 0.01f;
    }


    // function for slider
    public void onSliderPointExit()
    {
        DataManager.Instance.music_volume_event.Invoke();
        Debug.Log("point down invoked");
    }

    public void musicSliderValueChanged()
    {
        DataManager.Instance.music_volume = (musicVolumeSlider.value < 0.01f) ? 0 : musicVolumeSlider.value;
        musicVolumeValue.text = ((int)(musicVolumeSlider.value * 100)).ToString();
    }

    public void bgsSliderValueChanged()
    {
        DataManager.Instance.bgs_volume = (bgsVolumeSlider.value < 0.01f) ? 0 : bgsVolumeSlider.value;
        bgsVolumeValue.text = ((int)(bgsVolumeSlider.value * 100)).ToString();
    }
}
