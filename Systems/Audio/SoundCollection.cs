using UnityEngine;
using System.Collections.Generic;
using AutumnFramework;

[CreateAssetMenu(fileName = "SoundCollection", menuName = "Visual Game Framework/SoundCollection", order = 0)]
[Config]
public class SoundCollection : ScriptableObject {
    public List<SoundDetails> soundDetails;

    public SoundDetails FindSoundDetailsByName(string name){
        return soundDetails.Find(i=>i.SoundName==name);
    }


    

}