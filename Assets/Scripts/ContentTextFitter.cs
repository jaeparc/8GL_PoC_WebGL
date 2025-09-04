using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTextFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
    }
}
