using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCreature : MonoBehaviour
{
    public void DestroyMe() { 
        Destroy(this.transform.parent.gameObject); 
    }
}
