using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    

    

    private void Start()
    {
        
    }
   

    public void CoinCollected(GameObject obj)
    {
        obj.SetActive(false);
    }

   
}
