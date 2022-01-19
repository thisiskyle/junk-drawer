using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : ECSSystem
{

    public TestSystem() { }

    public void Run(EntityManager em) 
    {
        List<Entity> entities = em.GetEntitiesBySignature(1 << 0);
        List<int> ids = new List<int>();

        foreach(Entity e in entities) 
        {
            ids.Add(e.id);
        }

        Dictionary<int, TestComponent> testComponents = em.GetTestComponentsById(ids);

        foreach(Entity e in entities) 
        {
            Debug.Log("ID: " + e.id);
            Debug.Log("Sig: " + e.signature);
            Debug.Log("Health: " + testComponents[e.id].health);
        }

    }
}
