using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{
    public GameObject GM;
    public void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GH");
        GM.GetComponent<GameGenHandler>().addhits();
    }


}
