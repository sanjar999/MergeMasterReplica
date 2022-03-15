using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    [SerializeField] Unit _unit;
    [SerializeField] Button _spawnButton;
    [SerializeField] Board _board;

    private void Awake()
    {
        _spawnButton.onClick.AddListener(SpawnUnit);
    }

    private void SpawnUnit()
    {
        for (int x = 0; x < (int)_board.MovingArea.width; x++)
        {
            for (int y = 0; y < (int)_board.MovingArea.height; y++)
            {
                var unit = _board.GetObjectFromBoard(x, y);
                if (unit == null)
                {
                    var instance = Instantiate(_unit);
                    _board.SetObjectToBoard(x, y, instance);

                    //saving index off spawned unit
                    instance.x = x;
                    instance.y = y;

                    var yHalfScale = instance.transform.localScale.y * 0.5f;
                    //index to position
                    instance.transform.position = new Vector3(x + _board.MovingArea.x, yHalfScale, y + _board.MovingArea.y);
                    return;
                }
            }
        }
    }
}
