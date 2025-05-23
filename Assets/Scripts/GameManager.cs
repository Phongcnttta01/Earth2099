using System;
using System.Buffers.Text;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 10;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject spawnEnemy;
    public static bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject weaponMenu;
    [SerializeField] private GameObject infoMenu;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CinemachineCamera cinemachineCamera;
     public static Transform spawnPoint;
     public static Transform spawnSwordPoint;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject sword;

    void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        MainMenu();
        audioManager.StopAudio();
        cinemachineCamera.Lens.OrthographicSize = 5f;
    }

    void Update()
    {

    }

    public void AddEnergy()
    {
        if (bossCalled == true) return;
        currentEnergy++;
        UpdateEnergyBar();
        if (currentEnergy >= energyThreshold)
        {
            CallBoss();
        }

    }

    private void CallBoss()
    {
        bossCalled = true;
        boss.SetActive(true);
        spawnEnemy.SetActive(false);
        gameUI.SetActive(false);
        audioManager.PlayBossSound();
        cinemachineCamera.Lens.OrthographicSize = 8f;
        
    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentEnergy / (float)energyThreshold);
            energyBar.fillAmount = fillAmount;
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        weaponMenu.SetActive(false);
        infoMenu.SetActive(false);
        sword.SetActive(false);
        gun.SetActive(false);
        currentEnergy = 0;
        UpdateEnergyBar();
        bossCalled = false;
        audioManager.PlayBasisSound();
        cinemachineCamera.Lens.OrthographicSize = 5f;
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        weaponMenu.SetActive(false);
        infoMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        weaponMenu.SetActive(false);
        infoMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        weaponMenu.SetActive(false);
        infoMenu.SetActive(false);
        Time.timeScale = 1f;
        audioManager.PlayBasisSound();
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        weaponMenu.SetActive(false);
        infoMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void WinGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(true);
        weaponMenu.SetActive(false);
        infoMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void WeaponGame()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        infoMenu.SetActive(false);
        weaponMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Info()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        infoMenu.SetActive(true);
        weaponMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ChooseGun()
    {
        gun.SetActive(true);
        sword.SetActive(false);

    }

    public void ChooseSword()
    {
        sword.SetActive(true);
        gun.SetActive(false);
    }
}