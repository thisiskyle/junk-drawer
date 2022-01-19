using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : Component
{
    private static T _ins = null;
    public static T ins
    {

        get
        {
            if (_ins == null)
            {
                _ins = FindObjectOfType<T>();

                if (_ins == null)
                {
                    _ins = (new GameObject((typeof(T)).ToString())).AddComponent<T>();
                }
            }
            return _ins;
        }
    }
    public virtual void Init()
    {

    }
    
    public void Hide()
    {
        CleanUp();
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void CleanUp()
    {
        
    }
}
