using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : ECSComponent
{
    public int health = 100;

    public TestComponent() {
        signature = ComponentSignatures.TestComponent;
    }
}
