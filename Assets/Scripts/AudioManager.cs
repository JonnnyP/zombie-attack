// TODO
// Change xp pickup sound, zombie death sound and gun sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public AudioClip projectileSound;
    public AudioClip zombieDeathSound;
    public AudioClip xpPickUpSound;

    public float audioLevel;

    public void PlayProjectileSound() {

        AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, audioLevel);
    } 

    public void PlayZombieDeathSound() {
        AudioSource.PlayClipAtPoint(zombieDeathSound, Camera.main.transform.position, audioLevel);
    }

    public void PlayXpPickUpSound() {
        AudioSource.PlayClipAtPoint(xpPickUpSound, Camera.main.transform.position, audioLevel);
    }
}
