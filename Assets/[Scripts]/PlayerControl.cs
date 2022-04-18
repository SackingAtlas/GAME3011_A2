using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject skillMenu;
    public Vector3 currentPos;
    public Quaternion currentRot;
    public int speed;
    private bool pausedControls;
    private float vert, horiz;

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            skillMenu.SetActive(true);
        }
        if (!pausedControls)
        {
            vert = Input.GetAxisRaw("Vertical");
            horiz = Input.GetAxisRaw("Horizontal");
            transform.Translate(new Vector3(horiz, 0, vert) * speed * Time.deltaTime);
        }
    }
    public void MiniGameState(Transform moveToHere)
    {
        pausedControls = true;
        currentPos = transform.position;
        currentRot = transform.rotation;
        transform.position = moveToHere.position;
        transform.rotation = moveToHere.rotation;
    }
    public void ReturnToNormal()
    {
        pausedControls = false;
        transform.position = currentPos;
        transform.rotation = currentRot;
    }
}
