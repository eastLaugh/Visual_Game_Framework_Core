using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AutumnFramework;

[Bean]
public class CombatSystem:MonoBehaviour{
    
    // int i=0;
    public CombatSystem(){

    }
    public void Run(Enemy enemy,Action<string> callback){
        Debug.Log("Here you are!!");
    }

    private void Start() {
    }

    private void Awake() {
    }

    void Autumn(){
        
    }
    IEnumerator SwitchToCompatScene(){
        yield return SceneManager.LoadSceneAsync("Combat",LoadSceneMode.Additive);

    }


}