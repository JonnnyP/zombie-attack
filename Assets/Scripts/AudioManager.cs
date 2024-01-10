// TODO
// Change xp pickup sound, zombie death sound and gun sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public AudioClip projectileSound;
    public AudioClip zombieDeathSound;
    public AudioClip xpPickUpSound;

    public void PlayProjectileSound() {

        AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, 0.5f);
    } 

    public void PlayZombieDeathSound() {
        AudioSource.PlayClipAtPoint(zombieDeathSound, Camera.main.transform.position, 0.35f);
    }

    public void PlayXpPickUpSound() {
        AudioSource.PlayClipAtPoint(xpPickUpSound, Camera.main.transform.position, 0.45f);
    }
}
