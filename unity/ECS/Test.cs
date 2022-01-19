using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    EntityManager em;
    TestSystem testSystem;



    void Start()
    {
        em = new EntityManager();
        testSystem = new TestSystem();

        Entity newEntity = em.CreateEntity();

        em.AddTestComponent(new List<int>() {newEntity.id});

        RunTest();
    }


    public void RunTest() 
    {
        testSystem.Run(em);

    }
}
