using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //singleton class
    public static MusicPlayer instance;
    private void Awake()
    {
        if (instance!=null)
            Destroy(gameObject);
        else 
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
            
    }
}
