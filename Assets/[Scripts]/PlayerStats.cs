using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float amountOfJitters = 0.1f;
    public GameObject skillMenu;
    public Text amountText;
    private int calmHands = 0;

    private void Start()
    {
        skillMenu.SetActive(false);
    }
    public void PlusPressed()
    {
        if(calmHands != 10)
        {
            ++calmHands;
            amountText.text = calmHands + "/10 ";
            amountOfJitters -= 0.01f;
        }
    }
    public void minusPressed()
    {
        if (calmHands != 0)
        {
            --calmHands;
            amountOfJitters += 0.01f;
            amountText.text = calmHands + "/10 ";
        }
    }
}
