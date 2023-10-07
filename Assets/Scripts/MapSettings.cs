using System;
using Unity.Mathematics;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSettings : MonoBehaviour {
    public static MapSettings Instance { get; private set; }

    public enum FacingDirection {
        Up,
        Left,
        Down,
        Right
    }

    [SerializeField] private FacingDirection _facingDirection = FacingDirection.Up;
    
    Tilemap _raceTrackTileMap;
    private Vector3 _startCellCenterWorldPos;
    private Vector3Int _startCellCoords;
    private float _finishLineRotation;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;

        _raceTrackTileMap = GetComponentInParent<Tilemap>();
        _startCellCoords = _raceTrackTileMap.WorldToCell(transform.position);
        _startCellCenterWorldPos = _raceTrackTileMap.GetCellCenterWorld(_startCellCoords);
        
        CreateFinishLine();
    }


    // create a finishing line in front
    private void CreateFinishLine() {

        Vector3Int coordFromStart = Vector3Int.zero;
        _finishLineRotation = 0;

        switch (_facingDirection) {
            case FacingDirection.Up:
                coordFromStart = Vector3Int.up;
                break;
            case FacingDirection.Down:
                coordFromStart = Vector3Int.down;
                _finishLineRotation = 180;
                break;
            case FacingDirection.Left:
                coordFromStart = Vector3Int.left;
                _finishLineRotation = 90;
                break;
            case FacingDirection.Right:
                coordFromStart = Vector3Int.right;
                _finishLineRotation = -90;
                break;
        }
            
        
        Vector3 finishLineWorldPos = _raceTrackTileMap.GetCellCenterWorld(_startCellCoords + coordFromStart);

        FinishLine finishLine = Resources.Load<FinishLine>("Prefabs/FinishLine");
        finishLine = Instantiate(finishLine, finishLineWorldPos, Quaternion.Euler(0, 0, _finishLineRotation));
        finishLine.transform.SetParent(_raceTrackTileMap.transform, true);
    }
    
    public Vector3[] GetStartingPositions() {
        Vector3[] carStartingPositions = new Vector3[2];
        
        Vector2 raceTrackSize = _raceTrackTileMap.cellSize;
        if (_facingDirection == FacingDirection.Left || _facingDirection == FacingDirection.Right) {
            carStartingPositions[0] = _startCellCenterWorldPos + Vector3.up * (raceTrackSize.y / 4);
            carStartingPositions[1] = _startCellCenterWorldPos + Vector3.down * (raceTrackSize.y / 4);
        }
        else if (_facingDirection == FacingDirection.Up || _facingDirection == FacingDirection.Down) {
            carStartingPositions[0] = _startCellCenterWorldPos + Vector3.left * (raceTrackSize.x / 4);
            carStartingPositions[1] = _startCellCenterWorldPos + Vector3.right * (raceTrackSize.x / 4);
        }
        
        Debug.Log(carStartingPositions[0]);
        Debug.Log(carStartingPositions[1]);
        return carStartingPositions;
    }

    public Quaternion[] GetStartingRotations() {
        Quaternion[] carStartingRotations = new Quaternion[2];

        Quaternion finishLineQuaternion = Quaternion.Euler(0, 0, _finishLineRotation);
        
        if (_facingDirection == FacingDirection.Left) {
            carStartingRotations[0] = finishLineQuaternion;
            carStartingRotations[1] = finishLineQuaternion;
            return carStartingRotations;
        }

        carStartingRotations[0] = finishLineQuaternion;
        carStartingRotations[1] = finishLineQuaternion;
        return carStartingRotations;
    }
}
