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
    public bool swordHitEnemy;
    public AudioClip swordSoundClip;

    bool canSwing = true;
    bool magicSwordActive = false;
    GameObject activeSword;
    Animator animScript;

    private void Awake()
    {
        animScript = GetComponent<Animator>();
        directionFacingNESW = startingDirectionNESW;
        swordCollider.enabled = false;
        swordCollider.transform.localPosition = new Vector3(.025f, -0.5f, 0);
        swordCollider.transform.localScale = new Vector3(.25f, .69f, 1);
        swordHitEnemy = false;
    }

    // Update is called once per frame
    void Update () {

        if (GameObject.Find("Sword(Clone)") == null)
        {
            magicSwordActive = false;
        }
        else
        {
            magicSwordActive = true;
        }

        //Which direction is Link facing
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            directionFacingNESW = 0;
            swordCollider.transform.localPosition = new Vector3(-.097f, 0.919f, 0);
            swordCollider.transform.localScale = new Vector3(.25f, .69f, 1);
        } else if (Input.GetAxisRaw("Vertical") == -1)
        {
            directionFacingNESW = 2;
            swordCollider.transform.localPosition = new Vector3(.032f, -0.89f, 0);
            swordCollider.transform.localScale = new Vector3(.25f, .69f, 1);
        } else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            directionFacingNESW = 1;
            swordCollider.transform.localPosition = new Vector3(.85f, -0.05f, 0);
            swordCollider.transform.localScale = new Vector3(.75f, .2f, 1);
        } else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            directionFacingNESW = 3;
            swordCollider.transform.localPosition = new Vector3(-.871f, -0.058f, 0);
            swordCollider.transform.localScale = new Vector3(.75f, .2f, 1);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animScript.speed = 1.0f;
            //add sword animation
            if (canSwing && !this.GetComponent<Bow>().isShooting)
            {
                AudioSource.PlayClipAtPoint(swordSoundClip, Camera.main.transform.position);
                StartCoroutine(HitTime(hitboxDuration));
            }
        }
	}

    IEnumerator HitTime(float duration)
    {
        isSwingingSword = true;
        canSwing = false;
        swordCollider.enabled = true;
        GetComponent<ArrowKeyMovement>().isDoingAction = true;
        yield return new WaitForSeconds(duration);
        isSwingingSword = false;
        canSwing = true;
        swordCollider.enabled = false;
        GetComponent<ArrowKeyMovement>().isDoingAction = false;

        //after
        if (GetComponent<Player>().currentHealth == GetComponent<Player>().maxHealth)
        {
            if (!magicSwordActive && !swordHitEnemy)
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
        swordHitEnemy = false;
    }
}
