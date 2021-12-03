using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Inspector Variables
    public AudioSource BGM;
    [SerializeField] private AudioClip levelTheme;
    [SerializeField] private AudioClip bossTheme;

    //Instance Variables
    private bool bossActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameObject.FindObjectOfType<BossMasterClass>() != null) || (GameObject.FindObjectOfType<BossTwo>() != null)){
            bossActive = true;
        }else{
            bossActive = false;
        }
        ChangeBGM();
    }

    public void ChangeBGM(){
        if((BGM.clip.name == levelTheme.name) && bossActive){
            BGM.Stop();
            BGM.clip = bossTheme;
            BGM.Play();
        }
        if((BGM.clip.name == bossTheme.name) && !bossActive){
            BGM.Stop();
            BGM.clip = levelTheme;
            BGM.Play();
        }
        
    }
}
