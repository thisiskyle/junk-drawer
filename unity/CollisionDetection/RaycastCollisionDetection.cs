using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionDetection : MonoBehaviour
{
    public int numOfRays = 4;
    public float skin = 0.05f;
    public bool hitLeft { get; private set;}
    public bool hitRight { get; private set;}
    public bool hitUp { get; private set;}
    public bool hitDown { get; private set;}

    public RaycastHit2D hitV;
    public RaycastHit2D hitH;

    private float halfY;
    private float halfX;
    private LayerMask collidableMask;



    void Start()
    {
        collidableMask = LayerMask.GetMask("collidable");
        halfY = (transform.lossyScale.y / 2) - skin;
        halfX = (transform.lossyScale.x / 2) - skin;
    }

    public void CheckForVerticalCollision(Vector3 frameVelocity)
    {
        hitUp = hitDown = false;
        int directionY = frameVelocity.y > 0 ? 1 : -1;
        int directionX = frameVelocity.x > 0 ? 1 : -1;
        Vector3 origin = transform.position;
        origin.y += halfY * directionY; 
        origin.x += halfX * directionX; 

        float spacing = (transform.lossyScale.x - (skin * 2)) / (numOfRays - 1);
        float rayLength = Mathf.Abs(frameVelocity.y) + (skin);

        for(int i = 0; i < numOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.up * directionY, rayLength, collidableMask);
            Debug.DrawRay(origin, Vector3.up * directionY * rayLength, Color.red);
            origin.x += spacing * -directionX;

            if(hit.collider != null)
            {
                rayLength = hit.distance;
                hitDown = directionY == -1; 
                hitUp = directionY == 1; 

                hitV = hit;
            }
        }
    }

    public void CheckForHorizontalCollision(Vector3 frameVelocity)
    {
        hitRight = hitLeft = false;
        int directionY = frameVelocity.y > 0 ? 1 : -1;
        int directionX = frameVelocity.x > 0 ? 1 : -1;
        Vector3 origin = transform.position;
        origin.y += halfY; 
        origin.x += halfX * directionX; 

        float spacing = (transform.lossyScale.y - (skin * 2)) / (numOfRays - 1);
        float rayLength = Mathf.Abs(frameVelocity.x) + (skin);

        for(int i = 0; i < numOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.right * directionX, rayLength, collidableMask);
            Debug.DrawRay(origin, Vector3.right * directionX * rayLength, Color.red);
            origin.y -= spacing;

            if(hit.collider != null)
            {
                rayLength = hit.distance;
                hitRight = directionX == 1; 
                hitLeft = directionX == -1; 
                hitH = hit;
            }
        }
    }
}
