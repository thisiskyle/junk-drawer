using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScriptableFloat", menuName = "ScriptableObjects/ScriptableFloat")]
public class ScriptableFloat : ScriptableObject
{
    [SerializeField]
    private float maxValue = 0;
    [SerializeField]
    private float minValue = 0;
    [SerializeField]
    private float value;
    [SerializeField]
    private bool clamped = false;


    public float Value 
    {
        get { return this.value; }
        private set {}
    }


    private UnityEvent eventAdd = new UnityEvent();
    private UnityEvent eventSubtract = new UnityEvent();


    public ScriptableFloat(float v)
    {
        value = v;
    }

    public void Add(float amount)
    {
        eventAdd.Invoke();
        if(clamped)
        {
            value = Mathf.Clamp(value + amount, minValue, maxValue);
        }
        else 
        {
            value += amount;
        }
    }

    public void Subtract(float amount)
    {
        eventSubtract.Invoke();
        if(clamped)
        {
            value = Mathf.Clamp(value - amount, minValue, maxValue);
        }
        else 
        {
            value -= amount;
        }
    }

    public void Set(float amount)
    {
        if(clamped)
        {
            value = Mathf.Clamp(amount, minValue, maxValue);
        }
        else 
        {
            value = amount;
        }
    }

    public void SubscribeToAdditionEvent(UnityAction callback)
    {
        eventAdd.AddListener(callback);
    }

    public void UnsubscribeFromAdditionEvent(UnityAction callback)
    {
        eventAdd.RemoveListener(callback);
    }

    public void SubscribeToSubtractionEvent(UnityAction callback)
    {
        eventSubtract.AddListener(callback);
    }

    public void UnsubscribeFromSubtractionEvent(UnityAction callback)
    {
        eventSubtract.RemoveListener(callback);
    }
}
