using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public enum Difficulty 
    { 
        Easy, 
        Med, 
        Hard 
    }

    public Difficulty difficulty;
    public PickingScript pickingData;
    public Transform targetPosition;
    public Sprite[] lockImages = new Sprite[3];
    public Image lockFace, HealthBar;
    public Slider slider;
    public Text timerText, mainText, winText, UIText, pinText;
    public float timeLeft = 60;
    public bool gameOn = false;
    public GameObject miniGameObject, miniGameInterface;
    public PlayerControl player;
    public int pinsLeft;

    private bool inRange = false;


    // Start is called before the first frame update
    void Start()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                EasyMode();
                break;
            case Difficulty.Med:
                MedMode();
                break;
            case Difficulty.Hard:
                HardMode();
                break;
        }
        miniGameInterface.SetActive(false);
        pinText.text = pinsLeft.ToString();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameOn && inRange)
        {
            player.ReturnToNormal();
            miniGameObject.SetActive(false);
        }
        if (timeLeft > 0 && gameOn)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "time: " + Mathf.Round(timeLeft);
        }
        if(timeLeft <= 0)
        {
            EndGame(2);
        }
        if (inRange)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                GameStart();
                winText.text = "";
                player.MiniGameState(targetPosition);
            }
        }
    }
    public void EasyMode()
    {
        pickingData.pinsLockHas = 2;
        lockFace.sprite = lockImages[0];
        mainText.text = "Level: Easy";
        pinsLeft = 2;
    }
    public void MedMode()
    {
        pickingData.pinsLockHas = 4;
        lockFace.sprite = lockImages[1];
        mainText.text = "Level: Medium";
        pinsLeft = 4;
    }
    public void HardMode()
    {
        pickingData.pinsLockHas = 6;
        lockFace.sprite = lockImages[2];
        mainText.text = "Level: Hard";
        pinsLeft = 6;
    }
    public void UpdatePickHP(int HP)
    {
        if (HP > 35 && HP < 70)
            HealthBar.color = Color.yellow;
        if (HP > 70)
            HealthBar.color = Color.red;
        if (HP >= 100)
            EndGame(3);

        slider.value = HP;
    }
    public void EndGame(int endState)
    {
        switch (endState)
        {
            case 1:
                mainText.text = " ";
                timerText.text = " ";
                HealthBar.enabled = false;
                winText.text = "Lock Open";
                break;
            case 2:
                mainText.text = "Out of time";
                break;
            case 3:
                mainText.text = "Tool Broke";
                break;
        }
        gameOn = false;
        UIText.text = "Press 'ESC' to Quit";
    }
    public void GameStart()
    {
        gameOn = true;
        miniGameInterface.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            winText.text = "Pick Lock?";
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
        inRange = false;
        winText.text = "";
        }
    }
}
