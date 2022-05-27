using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Filter/Layer Filter")]

public class LayerFilter : ContextFilter
{
    public LayerMask mask;
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)//are these on the path layer
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            //if this layer is the path layer it is layer 6 and therefore move the number 1 move to left 6 times
            //shift based on layer to the left //current layer is six

            //if(mask == (mask | (1 << item.gameObject.layer)))
            if (0 != (mask & (1 << item.gameObject.layer))) //comparison to between mask and layer //if its not zero, it contains the layer
            {
                filtered.Add(item); //add to the list 
            }
        }
        return filtered;
    }
}
