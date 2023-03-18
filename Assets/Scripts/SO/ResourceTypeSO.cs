using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string resourceName;

    public Sprite resourceSprite;
}
