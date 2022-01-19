using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{
    private List<Entity> entities;

    // TODO: This could be a list of lists....
    // TODO: or this could be a dictionary of dictionaries....
    private List<TestComponent> testComponents;



    //
    public EntityManager() {
        entities = new List<Entity>();
        testComponents = new List<TestComponent>();

    }

    //
    public Entity GetEntity(int entityId)
    {
        return entities.Find(e => e.id == entityId);
    }

    //
    public List<Entity> GetEntitiesBySignature(uint sig)
    {
        return entities.FindAll(e => e.signature == sig);
    }

    //
    public Entity CreateEntity()
    {
        int newId = 101;
        Entity e = new Entity(newId);
        entities.Add(e);
        return e;
    }

    //
    public void AddComponent<T>(List<int> entityIds) where T : ECSComponent, new()
    {
        foreach(int i in entityIds)
        {
            Entity e = GetEntity(i);
            T component = new T();

            e.signature |= component.signature;
        }
    }

    //
    public void AddTestComponent(List<int> entityIds) {
        foreach(int i in entityIds)
        {
            Entity e = GetEntity(i);
            TestComponent component = new TestComponent();

            e.signature |= component.signature;
            component.parentId = e.id;
            testComponents.Add(component);
        }
    }
    //
    public Dictionary<int, TestComponent> GetTestComponentsById(List<int> id)
    {
        Dictionary<int, TestComponent> components = new Dictionary<int, TestComponent>();

        foreach(TestComponent c in testComponents)
        {
            if(id.Contains(c.parentId))
            {
                Debug.Log(c.parentId);
                components.Add(c.parentId, c);
            }
        }

        return components;
    }
}
