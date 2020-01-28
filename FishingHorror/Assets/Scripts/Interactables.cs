using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable Objects", menuName = "ScriptableObjects/Interactables", order =1)]
public class Interactables : ScriptableObject
{
    public List<GameObject> interactableList;
}
