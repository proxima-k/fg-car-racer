using System;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSettings : MonoBehaviour {
    public static MapSettings Instance { get; private set; }

    public enum FacingDirection {
        Vertical,
        Horizontal
    }

    [SerializeField] private FacingDirection _facingDirection = FacingDirection.Vertical;

    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start() {
        GetStartingPositions();
    }

    public Vector3[] GetStartingPositions() {
        Vector3[] carStartingPositions = new Vector3[2];

        Tilemap raceTrackTileMap = GetComponentInParent<Tilemap>();

        Vector3Int cellCoords = raceTrackTileMap.WorldToCell(transform.position);

        Vector3 cellCenterWorldPosition = raceTrackTileMap.GetCellCenterWorld(cellCoords);

        Vector2 raceTrackSize = raceTrackTileMap.cellSize;
        if (_facingDirection == FacingDirection.Horizontal) {
            carStartingPositions[0] = cellCenterWorldPosition + Vector3.up * (raceTrackSize.y / 4);
            carStartingPositions[1] = cellCenterWorldPosition + Vector3.down * (raceTrackSize.y / 4);
        }
        else if (_facingDirection == FacingDirection.Vertical) {
            carStartingPositions[0] = cellCenterWorldPosition + Vector3.left * (raceTrackSize.x / 4);
            carStartingPositions[1] = cellCenterWorldPosition + Vector3.right * (raceTrackSize.x / 4);
        }
        
        Debug.Log(carStartingPositions[0]);
        Debug.Log(carStartingPositions[1]);
        return carStartingPositions;
    }
}
