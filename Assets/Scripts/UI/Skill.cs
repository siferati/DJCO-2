using UnityEngine;

public class Skill {

    /* --- Attrs --- */

    // game object of this skill
    private GameObject gameObj;

    // render of this skill
    private CanvasRenderer cRend;

    // audio to play when skill is pressed
    private AudioSource sound;

    // default alpha value
    private readonly float DEFAULT_ALPHA;

    // default volume value
    private readonly float DEFAULT_VOLUME;


    /* --- Methods --- */

    // constructor
    public Skill(Transform parent, string name) {
        gameObj = parent.Find(name).gameObject;
        cRend = gameObj.GetComponent<CanvasRenderer>();
        DEFAULT_ALPHA = cRend.GetAlpha();
        sound = gameObj.GetComponent<AudioSource>();
        DEFAULT_VOLUME = sound.volume;
    }

    // activates this skill
    public bool Activate(float alpha) {

        if (cRend.GetAlpha() == DEFAULT_ALPHA)
        {
            cRend.SetAlpha(alpha);
            PlaySound();
            return true;
        }

        return false;
    }

    // deactivates this skill
    public void Deactivate() {
        cRend.SetAlpha(DEFAULT_ALPHA);
    }

    // setAlpha
    public void SetAlpha(float alpha)
    {
        cRend.SetAlpha(alpha);
    }

    // plays sound
    private void PlaySound() {
        if (sound != null) {
            sound.Play();
        }        
    }

    public void Silence() {
        sound.volume = (float) (DEFAULT_VOLUME / 5.0);
    }

    public void RestoreVolume() {
        sound.volume = DEFAULT_VOLUME;
    }
}