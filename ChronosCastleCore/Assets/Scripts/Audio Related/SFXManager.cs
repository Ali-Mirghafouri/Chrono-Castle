using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //this SFXManager has all the logic related to playing the SoundFX, this can be in the SFXEventManager but it's omitted and placed here for the sake of brevity in between classes.

    public static SFXManager current;

    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if(soundFXObject == null) 
        {
            Debug.Log("SoundFXObject is missing, please assign it in the inspector, thanks");
            return;
        }
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

}
