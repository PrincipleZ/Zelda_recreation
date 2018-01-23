using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

    public AudioClip rupee_collection_sound_clip;
    public AudioClip heartR_collection_sound_clip;
    public AudioClip countingAudio;

    Inventory inventory;
    Player player;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        player = GetComponent<Player>();
        if(inventory == null)
        {
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject object_collided_with = other.gameObject;

        if(object_collided_with.tag == "rupee")
        {
            if (inventory != null)
                inventory.AddRupees(1);
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(countingAudio, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "rupeeB")
        {
            if (inventory != null)
                inventory.AddRupees(5);
            Destroy(object_collided_with);

            StartCoroutine(GetComponent<playerSounds>().MultipleCollected(5));
            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "heartR")
        {
            if (inventory != null)
                player.heal(2);
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(heartR_collection_sound_clip, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(countingAudio, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "key")
        {
            if (inventory != null)
                inventory.AddKeys(1);
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(heartR_collection_sound_clip, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(countingAudio, Camera.main.transform.position);
        }
    }
}
