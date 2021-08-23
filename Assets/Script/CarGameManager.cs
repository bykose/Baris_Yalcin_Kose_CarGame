using UnityEngine;


public class CarGameManager : MonoSingleton<CarGameManager>
{
    [Header("Properties")]
    [SerializeField] private bool _canPlay;
    private Car _activeCar;
    [SerializeField] private int _activeCarIndex = 0; //Designer can change index on inspector for debug active car
    private int _replayIndex ;


    [Header("Reference")]
    [SerializeField] private GameObject[] _carGO;
    [SerializeField] private GameObject[] _carPoints;
    

    public GameObject[] CarGo
    {
        get
        {
            return _carGO;
        }
    }
    public bool CanPlay
    {
        get
        {
            return _canPlay;
        }
        set
        {
            _canPlay = value;
        }
    }
    private void Awake()
    {
        SetCarActive(_activeCarIndex);
        SetPointsActive(_activeCarIndex);
        _replayIndex = _activeCarIndex;
    }
    private void Update()
    { 
        //
        if (!_canPlay && Input.GetMouseButtonDown(0))
        {
            SetGameStatus();
        }
    }

    public void RestartStage()
    {
        SetGameStatus();
        SetActiveCarPos();
        SetReplayZero();
    }

    public void NextStage()
    {
        SetGameStatus();

        if (_activeCarIndex >= _carGO.Length)
        {
            return;
        }
        _carPoints[_activeCarIndex].SetActive(false);
        _activeCarIndex++;

        SetCarActive(_activeCarIndex);

        CarController.Instance.ActiveCarGO = _activeCar.gameObject;

        SetPointsActive(_activeCarIndex);
        SetReplayZero();
    }
    //Car Replays are also played from the beginning when the RestartStage function is run.
    private void SetReplayZero()
    {
        for (int i = _replayIndex ; i <_activeCarIndex; i++)
        {
            _carGO[i].GetComponent<ReplayManager>().SetTransform(0);
        }
    }
    //Sets the Car object to be played.
    private void SetCarActive(int index)
    {
        
        if (index<_carGO.Length)
        {
            _carGO[index].SetActive(true);
            _activeCar = _carGO[index].GetComponent<Car>();
        }
        if (CarController.Instance.ActiveCarGO != null)
        {
            return;
        }
                
        CarController.Instance.ActiveCarGO = _activeCar.gameObject;
    }
    //Sets the entrance and exit points of the played Car object.
    private void SetPointsActive(int index)
    {
        if (index < _carGO.Length)
        {
            _carPoints[index].SetActive(true);
        }
    }

    private void SetGameStatus()
    {
        _canPlay = !_canPlay;
    }

    private void SetActiveCarPos()
    {
        _activeCar.gameObject.transform.position = _activeCar.InitialPos;
        _activeCar.gameObject.transform.rotation = _activeCar.InitialRotation;
    }
}
