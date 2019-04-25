using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float displayImageDuration = 1f;
    public float fadeDuration = 1f;

    public AudioSource exitAudio;
    public CanvasGroup exitBackgroundImageCanvasGroup;

    public AudioSource caughtAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    public GameObject player;

    bool m_HasAudioPlayed;
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    private void OnTriggerEnter (Collider other)
    {
        m_IsPlayerAtExit |= other.gameObject == player;
    }

    private void Update ()
    {
        if (m_IsPlayerAtExit) 
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            } 
            else
            {
                Application.Quit();
            }
        }
    }
}