using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FuelSO", fileName = "NewFuelType")]
public class FuelSO : ScriptableObject {
    public Color SpriteColor;
    public int FillAmount = 25;
    public float ReappearTime = 5f;
}
