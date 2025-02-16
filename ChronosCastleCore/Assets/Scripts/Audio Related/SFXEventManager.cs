using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;


public class SFXEventManager : MonoBehaviour
{
    //this class will hold all the audio files for the playing of sound effects (like a library), as well as what to do when said events are invoked.
    public static SFXEventManager current;

    [SerializeField] private List<AudioClip> audioClips;

    private Dictionary<string, AudioClip> audioDictionary;
    
    void Awake()
    {
        if (!current) current = this;
    }

    void Start()
    {
        audioDictionary = new Dictionary<string, AudioClip>();
        DictSetup();
    }

    //==
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (GameEventSystem.current == null)
        {
            Debug.Log("Game Event System is missing from scene, cannot link events for sound playing :(");
            return;
        }
        GameEventSystem.current.StructurePlacedEvent += StructurePlacedSoundPlay;

    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (GameEventSystem.current == null)
        {
            Debug.Log("Game Event System is missing from scene, cannot unlink events for sound playing");
            return;
        }
        GameEventSystem.current.StructurePlacedEvent -= StructurePlacedSoundPlay;
    }

    void PlayUISound(Transform transform, string searchKey) 
    {
        if (GameEventSystemValid()) 
        {

        }
    }

    void StructurePlacedSoundPlay(GameObject placedGameobject, string structureName) 
    {
        if (GameEventSystemValid()) 
        {
            if (SFXManagerValid()) 
            {
                Transform soundTransform = placedGameobject.transform;
                TryPlaySound("StructurePlaced", soundTransform, 1.0f);
            }                      
        }
    }

    bool GameEventSystemValid() 
    {
        if (GameEventSystem.current == null) 
        { 
            return false; 
        }
        else 
        {
            return true;
        }
    }

    bool SFXManagerValid() 
    {
        if(SFXManager.current == null) { return false; }
        else { return true; }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (current != this) Destroy(gameObject);
    }

    void DictSetup()
    {
        foreach (AudioClip clip in audioClips)
        {
            audioDictionary.Add(clip.name, clip);
        }
    }

    void TryPlaySound(string key, Transform position, float volume) 
    {
        AudioClip clip;
        if (audioDictionary.TryGetValue(key, out clip))
        {
            SFXManager.current.PlaySoundFXClip(clip, position, volume);
        }
        else 
        {
            Debug.Log("No sound with key " + key + " present in AudioDictionary");
        }
    }
}
