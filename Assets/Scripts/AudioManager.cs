using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Souce --------")]
[SerializeField] AudioSource musicSource;
[SerializeField] AudioSource SFXSource;

    [Header("-------- Audio clip --------")]

public AudioClip forest;
public AudioClip menu;
public AudioClip spiderBoss;
public AudioClip town;
public AudioClip arrow;
public AudioClip heal;
public AudioClip run;
public AudioClip jump;
public AudioClip hit;
public AudioClip miss;
public AudioClip coin;
public AudioClip hurt;
public AudioClip hiss;
public AudioClip click;
public AudioClip begin;
public AudioClip magic;
public AudioClip fireball;
public AudioClip explosion;

private GameObject Boss = null;
private bool bossStatus;

Spawner spawner;


private void Start()
{
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
}

public void MenuMusic() {
    musicSource.Stop();
    musicSource.clip = menu;
    musicSource.Play();
}

public void TransitionMusic() {
    if (bossStatus == true && spawner.Level == 1) {
        SpiderMusic();
    }
    if (bossStatus == false && spawner.Level == 1) {
        ForestMusic();
    }
    if (bossStatus == false && spawner.Level == 2) {
        TownMusic();
    }
}

public void ForestMusic() {
    musicSource.Stop();
    musicSource.clip = forest;
    musicSource.Play();
}
public void SpiderMusic() {
    musicSource.Stop();
    musicSource.clip = spiderBoss;
    musicSource.Play();
}

public void TownMusic() {
    musicSource.Stop();
    musicSource.clip = town;
    musicSource.Play();
}

public void PlaySFX(AudioClip clip) {
    SFXSource.PlayOneShot(clip);
}

public void PlayCutSFX(AudioClip clip) {
    SFXSource.Stop();
    PlaySFX(clip);
}

public void PlayMusic(AudioClip clip) {
    musicSource.PlayOneShot(clip);
}

public void Update() {
    Boss = GameObject.FindGameObjectWithTag("Boss");
    if (Boss != null) {
        bossStatus = true;
    }
    else {
        bossStatus = false;
    }
}
}
