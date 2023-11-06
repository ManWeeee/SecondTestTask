using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [Header("Board Properties")] 
    [SerializeField] private float _spacing = 10;
    [SerializeField] private int _fieldSize = 4;
    [SerializeField] private int maxStartCellAmount;
    [SerializeField] private int minStartCellAmount;
    
    private float _cellSize = 75f;
    
    private RectTransform _rectTransform;
    private CellFactory _cellFactory;
    
    private Cell _cellPrefab;
    private Cell[,] _cells = null;

    private bool anyCellMoved;

    [Inject]
    public void Construct(Cell cellPrefab, CellFactory cellFactory)
    {
        _cellFactory = cellFactory;
        _cellPrefab = cellPrefab;
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
            MoveOnInput(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.A))
            MoveOnInput(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.S))
            MoveOnInput(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.D))
            MoveOnInput(Vector2.right);
        #endif
    }

    private void MoveOnInput(Vector2 direction)
    {
        anyCellMoved = false;
        ResetAllCellFlags();
        
        Move(direction);
        
        if (anyCellMoved)
        {
            GenerateRandomCell(1);
            //TODO method that will check if game is finished for some reason
        }
    }

    private void Move(Vector2 direction)
    {
        int startXYIndex = direction.x > 0 || direction.y < 0 ? _fieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int y = 0; y < _fieldSize; y++)
        {

            for (int x = startXYIndex; x >= 0 && x < _fieldSize; x -= dir)
            {

                var cell = direction.x != 0 ? _cells[y, x] : _cells[x, y];
                
                if(cell.IsEmpty)
                    continue;

                var cellToMerge = FindCellToMerge(cell, direction);
                
                if (cellToMerge)
                {
                    cell.MergeWithCell(cellToMerge);
                    anyCellMoved = true;
                    
                    continue;
                }

                var emptyCell = FindCellToMove(cell, direction);
                if(emptyCell)
                {
                    cell.MoveToCell(emptyCell);
                    anyCellMoved = true;
                }
            }
        }
    }
    
    private Cell FindCellToMerge(Cell cell, Vector2 direction)
    {
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for (int x = startX, y = startY; x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize; y -= (int)direction.y, x += (int)direction.x)
        {
            if (_cells[y, x].IsEmpty)
                continue;

            if (_cells[y, x].Value == cell.Value && _cells[y, x].HasMerged == false)
                return _cells[y, x];

            break;
        }

        return null;
    }
    
    private Cell FindCellToMove(Cell cell, Vector2 direction)
    {
        Cell emptyCell = null;
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for (int x = startX, y = startY; x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize; y -= (int)direction.y, x += (int)direction.x)
        {
            if (_cells[y, x].IsEmpty)
                emptyCell = _cells[y, x];
            else
                break;
        }

        return emptyCell;
    }
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        GenerateBoard();
    }

    private void CreateBoard()
    {
        _cells = new Cell[_fieldSize, _fieldSize];
        
        _cellSize = _cellPrefab.GetSize;
        
        float boardSize = _fieldSize * (_cellSize + _spacing) + _spacing;

        _rectTransform.sizeDelta = new Vector2(boardSize, boardSize);

        float startPositionX = -(boardSize / 2) + (_cellSize / 2) + _spacing;
        float startPositionY = (boardSize / 2) - (_cellSize / 2) - _spacing;

        for (int y = 0; y < _fieldSize; y++)
        {
            for (int x = 0; x < _fieldSize; x++)
            {
                var cell = _cellFactory.Create();
                cell.transform.SetParent(this.transform);
                cell.transform.localPosition = new Vector2(startPositionX + x * (_cellSize + _spacing) , startPositionY - y * (_cellSize + _spacing));
                cell.transform.localScale = new Vector3(1, 1, 1);
                _cells[y, x] = cell;
                cell.SetTile(x, y, 0);
            }
        }
    }

    private void GenerateBoard()
    {
        if(_cells == null)
            CreateBoard();

        for (int y = 0; y < _fieldSize; y++)
            for (int x = 0; x < _fieldSize; x++)
                _cells[y,x].SetTile(0);
        
        GenerateRandomCell(Random.Range(minStartCellAmount, maxStartCellAmount + 1)); // Random.Range for int doesn't take max value it takes max - 1
    }

    private void GenerateRandomCell(int amountOfCells)
    {
        for (int i = 0; i < amountOfCells; i++)
        {
            int horizontalIndex = Random.Range(0, _fieldSize);
            int verticalIndex = Random.Range(0, _fieldSize);
            while (!_cells[verticalIndex, horizontalIndex].IsEmpty)
            {
                horizontalIndex = Random.Range(0, _fieldSize);
                verticalIndex = Random.Range(0, _fieldSize);
            }

            _cells[verticalIndex,horizontalIndex].SetTile(1);
        }
    }

    private void ResetAllCellFlags()
    {
        for(int y = 0; y < _fieldSize; y++)
            for(int x = 0; x < _fieldSize; x++)
                _cells[y,x].ResetFlag();
    }
}