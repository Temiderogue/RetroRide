using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool _isConeSpawned = false;
    [SerializeField] private GameObject[] _block;
    [SerializeField] private GameObject _cone, _boost, _boostPoint, _empty;
    [SerializeField] private Vector3 _poolPosition;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private int _blocksAmount = 15, _coneAmount = 45;
    [SerializeField] private int _currentBlock = 0, _currentCone = 0;
    [SerializeField] private bool _isFirstTime;
    private float _randZ;
    private GameObject[] _blocks, _obstacles;

    private float[] _lines = new float[3]{-0.5f, 0, 0.5f};
    private int _randNum;
    private int _randPos;

    private void Start()
    {
        _blocks = new GameObject[_blocksAmount];
        _obstacles = new GameObject[_coneAmount];
        CreateBlocks();
        Time.timeScale = 1;
        CreateCones();
    }

    private void CreateBlocks()
    {
        for (int i = 0; i < _blocksAmount; i++)
        {
            _randNum = Random.Range(0, 1);
            _poolPosition.z += 4.8f;
            _blocks[i] = Instantiate(_block[_randNum], _poolPosition, Quaternion.identity);
        }
    }

    public void CreateCones()
    {
        for (int i = 0; i < _coneAmount; i++)
        {
            _randNum = Random.Range(0, 100);
            _randPos = Random.Range(0, 3);
            _spawnPosition.x = _lines[_randPos];
            _spawnPosition.z += 3f;
            if (_randNum<85)
            {
                _obstacles[i] = Instantiate(_cone, _spawnPosition, Quaternion.identity);
            }
            else if (_randNum >= 85 && _randNum <95)
            {
                _obstacles[i] = Instantiate(_boost, _spawnPosition, Quaternion.identity);
            }
            else
            {
                _obstacles[i] = Instantiate(_empty, _spawnPosition, Quaternion.identity);
            }

            
        }
    }

    public void ChangeBlockPosition()
    {
        _poolPosition.z += 4.8f;
        _blocks[_currentBlock].transform.position = _poolPosition;
        _currentBlock++;
        if (_currentBlock >= _blocksAmount)
        {
            _currentBlock = 0;
        }
    }

    public void ChangeConePosition()
    {
        _spawnPosition.z += 3f;
        _randNum = Random.Range(0, 3);
        _spawnPosition.x = _lines[_randNum];
        _obstacles[_currentCone].transform.position = _spawnPosition;
        _currentCone++;
        if (_currentCone >= _coneAmount)
        {
            _currentCone = 0;
        }
    }
}
