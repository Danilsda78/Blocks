using System;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    static public Action EClick;
    static public Action EBlob;
    static public Action ELose;
    [SerializeField] private AudioSource soundClick;
    [SerializeField] private AudioSource soundMusic;
    [SerializeField] private AudioSource soundBlob;
    [SerializeField] private AudioSource soundLose;
    [SerializeField] private Sprite Off;
    [SerializeField] private Sprite On;
    [SerializeField] private Image btnSound;

    private void OnEnable()
    {
        EClick += soundClick.Play;
        EBlob += soundBlob.Play;
        ELose += soundLose.Play;
    }

    private void OnDisable()
    {
        EClick -= soundClick.Play;
        EBlob -= soundBlob.Play;
        ELose -= soundLose.Play;
    }

    public void OnClickBtn()
    {
        EClick?.Invoke();
    }

    public void Swich()
    {
        if (soundMusic.mute == true)
        {
            btnSound.sprite = On;

            soundClick.mute = false;
            soundMusic.mute = false;
            soundBlob.mute = false;
            soundLose.mute = false;
        }
        else
        {
            btnSound.sprite = Off;

            soundClick.mute = true;
            soundMusic.mute = true;
            soundBlob.mute = true;
            soundLose.mute = true;
        }
    }
}
