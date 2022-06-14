using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }
}
