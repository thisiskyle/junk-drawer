using System.Collections;
using System.Collections.Generic;

public class Entity 
{
    public int id { get; private set; }
    public uint signature; 


    public Entity(int id) {
        this.id = id;
        signature = 0;
    }
}
