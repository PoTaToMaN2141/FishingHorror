using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish Library", menuName = "ScriptableObjects/FishLibrary", order = 2)]
public class FishLibrary : ScriptableObject
{
    public List<GameObject> stageOneFish;
    public List<GameObject> stageTwoFish;
    public List<GameObject> stageThreeFish;
    public List<GameObject> stageFourFish;
}
