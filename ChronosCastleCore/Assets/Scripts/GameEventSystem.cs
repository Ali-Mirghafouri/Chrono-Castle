using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem current;

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
        //DontDestroyOnLoad(gameObject); // Persist across scene changes
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (current != this) Destroy(gameObject);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    //==Placement System Related Actions==//
    public event Action<GameObject, string> StructurePlacedEvent; //gameObject that just got placed gets passed in
    public event Action<GameObject, string> StructureRemovedEvent; //gameObject, just before deletion, gets passed in

    public event Action PlacementModeEnteredEvent;
    public event Action PlacementModeExitedEvent;

    public event Action StructureSelectUIEvent;

    public void OnStructurePlacedEvent(GameObject structureGO, string structureName)
    {
        StructurePlacedEvent?.Invoke(structureGO, structureName);
    }

    public void OnStructureRemovedEvent(GameObject structureGO, string structureName) 
    {
        StructureRemovedEvent?.Invoke(structureGO, structureName);
    }

    public void OnPlacementModeEnteredEvent()
    {
        PlacementModeEnteredEvent?.Invoke();
    }

    public void OnPlacementModeExitedEvent() 
    {
        PlacementModeExitedEvent?.Invoke();
    }

    public void OnStructureSelectUIEvent()
    {
        StructureSelectUIEvent?.Invoke();
    }

    //==Sound Events==//
    public event Action<Transform, string> UnitSoundEvent;
    public event Action<Transform, string> UISoundEvent;
    public event Action<Transform, string> EnvironmentSoundEvent;

    public void UnitSoundEventPlay(Transform transform, string searchKey) 
    {
        UnitSoundEvent?.Invoke(transform, searchKey);
    }

    public void UISoundEventPlay(Transform transform, string searchKey) 
    {
        UISoundEvent?.Invoke(transform, searchKey);
    }

    public void EnvironmentSoundEventPlay(Transform transform, string searchKey) 
    {
        EnvironmentSoundEvent?.Invoke(transform, searchKey);
    }
}
