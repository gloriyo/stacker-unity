using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabSpawner : MonoBehaviour
{

    [SerializeField]
    private SlidingSlab slabPrefab;

    public void SpawnSlab()
    {
        var slab = Instantiate(slabPrefab);

        if (SlidingSlab.PreviousSlab != null && SlidingSlab.PreviousSlab.IsStarterSlab() == false)
        {
            var newHeight = SlidingSlab.PreviousSlab.transform.position.y + slabPrefab.transform.localScale.y;
            slab.transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);

        }
        else
        {
            slab.transform.position = transform.position;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, slabPrefab.transform.localScale);
    }
}
