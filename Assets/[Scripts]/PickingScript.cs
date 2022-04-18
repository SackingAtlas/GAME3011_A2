using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingScript : MonoBehaviour
{
    private int onPin = 0;
    private bool stillIn;
    private int rounded;
    private float handMove, handTwitch, twitchTimer;
    private float[] pinsArrayMax = new float [6];
    public AudioClip[] soundFX = new AudioClip[6];
    public GameObject pickTool;
    public float speed, sweetSpotRange, timeBetweenTwitch;
    public int pinsLockHas, pickHealth;
    public MiniGameManager MGM;
    public PlayerStats PS;


    private void Start()
    {
        MakePins();
    }
    private void Update()
    {
        if (MGM.gameOn)
        {
            SteadyHand();
            TiltPick();
            CheckPin(onPin);
        }
    }
    private void SteadyHand()
    {
        if (twitchTimer <= 0)
        {
            handTwitch = Random.Range(-PS.amountOfJitters, PS.amountOfJitters);
            twitchTimer = Random.Range(0.0f, timeBetweenTwitch);
        }
        else
            twitchTimer -= Time.deltaTime;
    }
    private void TiltPick()
    {
        if (Input.GetMouseButton(0))
        {
            handMove = Input.GetAxis("Mouse Y") + handTwitch;
            if (pickTool.transform.rotation.x >= -0.25 && pickTool.transform.rotation.x <= 0.25)
                pickTool.transform.Rotate(new Vector3(handMove, 0, 0) * Time.deltaTime * speed);
            if (pickTool.transform.rotation.x >= 0.25)
                pickTool.transform.rotation = Quaternion.Euler(25, 0, 0);
            if (pickTool.transform.rotation.x <= -0.25)
                pickTool.transform.rotation = Quaternion.Euler(-25, 0, 0);
        }
    }
    private void CheckPin(int pinNumber)
    {
        if (pickTool.transform.rotation.x >= pinsArrayMax[pinNumber] && pickTool.transform.rotation.x <= pinsArrayMax[pinNumber] + sweetSpotRange)
        {
            Debug.Log("hit it");
            if (!stillIn)
            {
                GetComponent<AudioSource>().PlayOneShot(soundFX[0]);
                stillIn = true;
            }          
        }
        else
        {
            if (stillIn)
            {
                GetComponent<AudioSource>().PlayOneShot(soundFX[1]);
            }
            stillIn = false;
            Debug.Log("nope");
        }

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Animator>().Play("TryLockAnim");
            if (stillIn)
                WinCondition();
            else
            {
                GetComponent<AudioSource>().PlayOneShot(soundFX[4]);
                DamagePick(Mathf.Abs(pickTool.transform.rotation.x - (pinsArrayMax[pinNumber] + (sweetSpotRange / 2))));
            }                
        }
    }
    private void DamagePick(float dmg)
    {
        pickHealth += Mathf.RoundToInt(dmg * 100);
        if (pickHealth >= 100)
        {
            GetComponent<Animator>().Play("BreakAnim");
            GetComponent<AudioSource>().PlayOneShot(soundFX[5]);
        }
        MGM.UpdatePickHP(pickHealth);
    }
    private void MakePins()
    {
        for (int i = 0; i < pinsArrayMax.Length; i++)
        {
            rounded = Random.Range(-22, 22);
            pinsArrayMax[i] = rounded * 0.01f;
        }
        Debug.Log(pinsArrayMax[onPin]);
    }
    private void WinCondition()
    {
        ++onPin;
        --MGM.pinsLeft;
        MGM.pinText.text = MGM.pinsLeft.ToString();
        if (onPin == pinsLockHas)
        {
            GetComponent<AudioSource>().PlayOneShot(soundFX[3]);
            GetComponent<Animator>().Play("OpenLockAnim");
            MGM.EndGame(1); 
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(soundFX[2]);

            Debug.Log("Next Pin");
        }
    }
}