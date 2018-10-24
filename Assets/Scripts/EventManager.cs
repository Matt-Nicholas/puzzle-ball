using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/*
Event messaging system for Unity with flexible parameter passing
Code is 99% from the untiy tutorial here: https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system
the other 1% is from this forum post: https://forum.unity.com/threads/messaging-system-passing-parameters-with-the-event.331284/
*/



// Replacement of the basic Unityevent with a unity event that has an object parameter <T> did not work
[System.Serializable]
public class ThisEvent:UnityEvent<string> { }


// This class holds the messages, you need to attatch it to one gameobject in the scene
public class EventManager:MonoBehaviour
{
    private Dictionary<string, ThisEvent> eventDictionary;

    private static EventManager eventManger;

    // grabs the instance of the event manager
    public static EventManager Instance
    {
        get
        {
            if(!eventManger)
            {
                eventManger = FindObjectOfType(typeof(EventManager)) as EventManager;

                if(!eventManger)
                {
                    Debug.Log("There needs to be one active EventManager script on a gameobject in your scene.");
                }
                else
                {
                    eventManger.Init();

                }
            }
            return eventManger;
        }
    }

    // creates dictionary for the events
    void Init()
    {
        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, ThisEvent>();
        }
    }

    // function called to insert an event in the dictionary
    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        ThisEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ThisEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    // removes an event from the dictionary
    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if(eventManger == null) return;
        ThisEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    // event trigger with an object passed as a parameter.
    public static void TriggerEvent(string eventName, string variant)
    {
        ThisEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(variant);
        }
    }
}