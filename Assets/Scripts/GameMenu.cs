using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Transform _mainMenu;
    [SerializeField] private Transform _settings;
    [SerializeField] private Transform _changeCar;
    [SerializeField] private Transform _car;
    [SerializeField] private Transform _cylinder;
    [SerializeField] private List<CarInMenu> _cars = new List<CarInMenu>();
    [SerializeField] private float _speed;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _blackScreen;
    [SerializeField] private Animator _animator;
    [SerializeField] private Button[] _buttons;

    private int _currentCar = 0;
    private Vector3 _carPool = new Vector3(0, 20, 0);
    private Vector3 _carStand = new Vector3(0, 0, 2.5f);

    private int garbageCreationRate = 1024;
    private static int[] _garbage;
    void Update()
    {
        _garbage = new int[garbageCreationRate];
    }

    private void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        Data.ChosenCar = _cars[_currentCar];
        _blackScreen.enabled = false;
    }

    public void Play()
    {
        Data.isGameStarted = true;

        for (int i = 0; i < _cars.Count; i++)
        {
            if (_cars[i].State == "Selected")
            {
                Data.ChosenCar = _cars[i];
                break;
            }
        }

        StartCoroutine(LoadGame());
    }

    public void ChangeCarScreen()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].enabled = false;
        }
        StartCoroutine(EnableButtons());
        ChangeScreen(_mainMenu, _changeCar);

        for (int i = 0; i < _cars.Count; i++)
        {
            _cars[i].transform.position = _carPool;
        }
        Data.ChosenCar.transform.position = new Vector3(0.4f,0,3);
        Data.ChosenCar.transform.DOMove(new Vector3(0, 0, 2.5f),_speed,false);
        _cylinder.DOMove(new Vector3(0, -0.254f, 2.5f), _speed, false);

        switch (_cars[_currentCar].State)
        {
            case "OnSale":
                _text.text = "Buy";
                Data.CurrentState = "OnSale";
                break;
            case "Bought":
                _text.text = "Select";
                Data.CurrentState = "Bought";
                break;
            case "Selected":
                _text.text = "Selected";
                Data.CurrentState = "Selected";
                break;
            default:
                break;
        }
    }

    public void Setting()
    {
        ChangeScreen(_mainMenu, _settings);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        ChangeScreenBack(_mainMenu, _settings);
    }

    public void BackFromChange()
    {
        ChangeScreenBack(_mainMenu, _changeCar);
        _cars[_currentCar].transform.position = _carPool;

        for (int i = 0; i < _cars.Count; i++)
        {
            if (Data.ChosenCar.Name == _cars[i].Name)
            {
                _currentCar = i;
                break;
            }
        }

        Data.ChosenCar.transform.position = _carStand;
        Data.ChosenCar.transform.DOMove(new Vector3(0.4f, 0, 3f), _speed, false);
        _cylinder.DOMove(new Vector3(0.4f, -0.254f, 3f), _speed, false);
        
    }

    private void ChangeScreen(Transform first, Transform second)
    {
        first.DOLocalMoveX(-3000f, _speed, false);
        second.DOLocalMoveX(0f, _speed, false);
    }

    private void ChangeScreenBack(Transform first, Transform second)
    {
        first.DOLocalMoveX(0f, _speed, false);
        second.DOLocalMoveX(-3000f, _speed, false);
    }

    public void ChangeCar(int arrow) 
    {
        _cars[_currentCar].transform.position = _carPool;
        _currentCar += arrow;
        if (_currentCar<=-1)
        {
            _currentCar = _cars.Count-1;
        }
        else if (_currentCar >= _cars.Count)
        {
            _currentCar = 0;
        }

        _cars[_currentCar].transform.position = _carStand;

        switch (_cars[_currentCar].State)
        {
            case "OnSale":
                _text.text = "Buy";
                Data.CurrentState = "OnSale";
                break;
            case "Bought":
                _text.text = "Select";
                Data.CurrentState = "Bought";
                break;
            case "Selected":
                _text.text = "Selected";
                Data.CurrentState = "Selected";
                break;
            default:
                break;
        }
    }

    public void ButtonClick()
    {
        switch (Data.CurrentState)
        {
            case "OnSale":
                /*
                if (true){}   проверка есть ли достаточно валюты
                */
                _cars[_currentCar].State = "Bought";
                Data.CurrentState = "Bought";
                _text.text = "Select";

                break;
            case "Bought":
                _cars[_currentCar].State = "Selected";
                Data.CurrentState = "Selected";
                _text.text = "Selected";
                Data.ChosenCar.State = "Bought";
                Data.ChosenCar = _cars[_currentCar];
                break;
            case "Selected":
                
                break;
            default:
                break;
        }
    }

    private IEnumerator LoadGame()
    {
        
        _blackScreen.enabled = true;
        _animator.SetBool("FadeIn",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    private IEnumerator EnableButtons()
    {
        yield return new WaitForSeconds(_speed);
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].enabled = true;
        }
    }
}
