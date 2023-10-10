using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private GridCell _gridCellPrefab;
        [SerializeField] private int _gridSize;

        [SerializeField] private GridCell[] _gridsCell;

        [ContextMenu(nameof(CreateGrid))]
        private void CreateGrid()
        {
            if (_gridsCell != null)
            {
                foreach (var cell in _gridsCell)
                {
                    if (cell != null)
                        cell.DestoyInEditor();
                }
            }

            _gridsCell = new GridCell[_gridSize * _gridSize];
            var index =0;
            for (int height = 0; height < _gridSize; height++)
            {
                for (int width = 0; width < _gridSize; width++)
                {
                    var position = new Vector3(width, 0, height);
                    var gridCell = Instantiate(_gridCellPrefab, transform);
                    gridCell.name = $"GridCell{index}";
                    gridCell.transform.position = position;
                    _gridsCell[index] = gridCell;
                    gridCell.Construct();
                    index++;
                }
            }

            EditorUtility.SetDirty(gameObject);
        }
    }
}

