using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [Header("Gameplay HUD")]
    [SerializeField] TextMeshProUGUI scoreText;    
    [SerializeField] TextMeshProUGUI killsText;
    [Space(20)]

    [Header("Victory Screen HUD")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI challengesText;
    [SerializeField] TextMeshProUGUI totalKillsText;
    [Space(20)]

    [Header("Audio HUD")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [Space(20)]

    [Header("Player")]
    [SerializeField] private PlayerAttack playerAttack;

    private void OnEnable()
    {
        GameManager.onScoreChange.AddListener(UpdateScoreText);
        GameManager.onWinGame.AddListener(UpdateWinScreen);
        playerAttack.onPlayerKill.AddListener(UpdateKillsText);
    }

    private void UpdateScoreText(int points)
    {
        scoreText.text = $"Score:\n{points}";
    }

    private void UpdateKillsText(int kills)
    {
        killsText.text = $"Kills:\n{kills}";
    }

    private void UpdateWinScreen()
    {
        timeText.text = $"Game Time: {GameManager.gameTime.ToString("F0")}";
        challengesText.text = $"Challenges: {GameManager.passedObstaclesCount}";
        totalKillsText.text = $"Kills: {playerAttack.playerKills}";
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(sfxSlider.value);
    }

    private void OnDisable()
    {
        GameManager.onScoreChange.RemoveListener(UpdateScoreText);
        GameManager.onWinGame.RemoveListener(UpdateWinScreen);
        playerAttack.onPlayerKill.RemoveListener(UpdateKillsText);
    }


}
