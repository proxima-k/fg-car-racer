using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [SerializeField] private GameObject Player1UI;
    [SerializeField] private GameObject Player2UI;
    
    [SerializeField] private Image Player1FuelBar;
    [SerializeField] private Image Player2FuelBar;

    
    public void Initialize(List<CarController> participants) {

        switch (participants.Count) {
            case 0:
                Logger.LogError("There are no participants.");
                return;
            case 1:
                participants[0].OnFuelChanged += Player1_OnFuelChanged;
                Player1UI.SetActive(true);
                Player2UI.SetActive(false);
                
                break;
            case 2:
                participants[0].OnFuelChanged += Player1_OnFuelChanged;
                participants[1].OnFuelChanged += Player2_OnFuelChanged;
                
                Player1UI.SetActive(true);
                Player2UI.SetActive(true);
                
                break;
            default:
                Logger.LogWarning("There are extra participants in the game.");
                break;
        }

    }

    private void Player1_OnFuelChanged(object sender, CarController.OnFuelChangedEventArgs e) {
        Player1FuelBar.fillAmount = e.fuelNormalized;
    }
    private void Player2_OnFuelChanged(object sender, CarController.OnFuelChangedEventArgs e) {
        Player2FuelBar.fillAmount = e.fuelNormalized;
    }
}
