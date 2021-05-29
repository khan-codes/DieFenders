using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    int hitPoints = 20;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem enemyDeathPrefab;
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip deathSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        
        if (hitPoints <= 0)
        { 
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        if (hitPoints > 0)
        {
            hitPoints = hitPoints - 1;
        }
        hitParticlePrefab.Play();

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private void KillEnemy()
    {
        AudioSource oneShotAudioSource = PlayClipAt(deathSound, transform.position);
        //AudioSource.PlayClipAtPoint(deathSound, transform.position);        // you have to play sound right before destroying the gameobject (and hence destroying the audio clip midway)
        // PlayClipAtPoint is the ideal solution to this problem. It creates a sound clip at a position and automatically destroys it as the clip ends.
        // this is also the reason why we did not use death particles vfx in the same way as damage vfx. Instead we instantiated the particles as an independent object at a particular location
        // (usually transform.position, because it has to be on the enemy) and then when we destroy the gameobject the particles don't just vanish         
        PlayDeathFX();
        
        // the particles are not getting destryed through script is beacuse when you the destroy the enemy gameobject (the line below) eveything associated with it including scripts are also 
        // destroyed
        Destroy(gameObject);
    }

    private void PlayDeathFX()
    {
        var fx = Instantiate(enemyDeathPrefab, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        // changing parents doesn't work here
        //fx.name = "EnemyDeathFX";
        //fx.gameObject.transform.parent = this.transform;
        fx.Play();

        float destroyAfterDelay = fx.main.duration;
        Destroy(fx.gameObject, destroyAfterDelay);      // the reason the particles were not being destryed was that we were trying to destroy the particles system, what we need to do is destroy the 
        // gameobject.
    }

    AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.spatialBlend = 0;
        aSource.clip = clip; // define the clip
                             // set other aSource properties here, if desired
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length + 1f); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }
}
