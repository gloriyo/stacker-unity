using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingSlab : MonoBehaviour
{
    public static SlidingSlab CurrentSlab { get; private set; }
    public static SlidingSlab PreviousSlab { get; private set; }

    [SerializeField] 
    private float startingSlab = 0; // is not starting slab

    [SerializeField] 
    private float moveSpeed = 1f;

    internal bool IsStarterSlab()
    {
        bool isStarterSlab = startingSlab == 0 ? false : true;
        Debug.Log(isStarterSlab);
        return isStarterSlab;
        // return false;
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float overHang = transform.position.x - PreviousSlab.transform.position.x;

        Debug.Log(overHang);
        
        float direction = overHang > 0 ? 1f : -1f;
        SlicedSlab(overHang, direction);
    }

    void OnEnable()
    {
        if (PreviousSlab == null)
        {
            PreviousSlab = this;
        }
        CurrentSlab = this;
        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(PreviousSlab.transform.localScale.x, transform.localScale.x, PreviousSlab.transform.localScale.x);
    }

    private Color GetRandomColor()
    {
        Color newColor = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
        return newColor;
    }

    void SlicedSlab(float overHang, float direction)
    {
        float remainingSize = PreviousSlab.transform.localScale.x - Mathf.Abs(overHang);
        float hangingSize = PreviousSlab.transform.localScale.x - remainingSize;

        float remainingPosition = PreviousSlab.transform.position.x + (overHang / 2f);
        transform.localScale = new Vector3(remainingSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(remainingPosition, transform.position.y, transform.position.z);

        float hangingPosition = transform.position.x + (remainingSize / 2f * direction) + (hangingSize / 2f * direction);

        SpawnDroppingSlab(hangingPosition, hangingSize);
    }

    void SpawnDroppingSlab(float fallingSlabPosition, float fallingSlabSize)
    {
        var slab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        slab.transform.localScale = new Vector3(fallingSlabSize, transform.localScale.y, transform.localScale.z);
        slab.transform.position = new Vector3(fallingSlabPosition, transform.position.y, transform.position.z);

        slab.AddComponent<Rigidbody>();
        slab.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(slab.gameObject, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
