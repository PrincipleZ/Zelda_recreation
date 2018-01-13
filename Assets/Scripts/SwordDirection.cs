using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDirection : MonoBehaviour {

    public GameObject magicSwordPrefab;
    public Collider swordCollider;
    public int startingDirectionNESW = 2;
    public float hitboxDuration = .1f;
    public float magicSwordSpeed = 10;
    public int directionFacingNESW;
    public bool isSwingingSword = false;

    bool canSwing = true;
    bool magicSwordActive = false;
    GameObject activeSword;
    

    private void Awake()
    {
        directionFacingNESW = startingDirectionNESW;
        swordCollider.enabled = false;
        swordCollider.transform.localPosition = new Vector3(.025f, -0.5f, 0);
        swordCollider.transform.localScale = new Vector3(.25f, .69f, 1);
    }

    // Update is called once per frame
    void Update () {

        //Which direction is Link facing
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            directionFacingNESW = 0;
            swordCollider.transform.localPosition = new Vector3(-.093f, 0.53f, 0);
            swordCollider.transform.localScale = new Vector3(.25f, .69f, 1);
        } else if (Input.GetAxisRaw("Vertical") == -1)
        {
            directionFacingNESW = 2;
            swordCollider.transform.localPosition = new Vector3(.025f, -0.5f, 0);
            swordCollider.transform.localScale = new Vector3(.25f, .69f, 1);
        } else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            directionFacingNESW = 1;
            swordCollider.transform.localPosition = new Vector3(.5f, -0.05f, 0);
            swordCollider.transform.localScale = new Vector3(.75f, .2f, 1);
        } else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            directionFacingNESW = 3;
            swordCollider.transform.localPosition = new Vector3(-.5f, -0.05f, 0);
            swordCollider.transform.localScale = new Vector3(.75f, .2f, 1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //add sword animation
            if (canSwing)
            {
                StartCoroutine(HitTime(hitboxDuration));
            }
        }
	}

    IEnumerator HitTime(float duration)
    {
        isSwingingSword = true;
        canSwing = false;
        swordCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        isSwingingSword = false;
        canSwing = true;
        swordCollider.enabled = false;

        //after
        if (GetComponent<Player>().currentHealth == GetComponent<Player>().maxHealth)
        {
            if (!magicSwordActive)
            {
                activeSword = Instantiate(magicSwordPrefab);

                if (directionFacingNESW == 0)
                {
                    activeSword.transform.Rotate(new Vector3(0, 0, 90f));
                    activeSword.transform.position = swordCollider.transform.position;
                    activeSword.GetComponent<Rigidbody>().velocity = new Vector3(0, magicSwordSpeed, 0);
                }
                else if(directionFacingNESW == 1)
                {
                    activeSword.transform.position = swordCollider.transform.position;
                    activeSword.GetComponent<Rigidbody>().velocity = new Vector3(magicSwordSpeed, 0f, 0);
                }else if(directionFacingNESW == 2)
                {
                    activeSword.transform.Rotate(new Vector3(0, 0, 270f));
                    activeSword.transform.position = swordCollider.transform.position;
                    activeSword.GetComponent<Rigidbody>().velocity = new Vector3(0, -magicSwordSpeed, 0);
                }
                else
                {
                    activeSword.transform.Rotate(new Vector3(0, 0, 180f));
                    activeSword.transform.position = swordCollider.transform.position;
                    activeSword.GetComponent<Rigidbody>().velocity = new Vector3(-magicSwordSpeed, 0f, 0);
                }
            }
        }
    }
}
