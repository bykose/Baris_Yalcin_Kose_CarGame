using UnityEngine;

public class CarController : MonoSingleton<CarController>
{

    [Header("Properties")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _turnSpeed = 200f;
    private float _horizontalInput;

    [Header("Reference")]
    private GameObject _activeCarGO;


    private void Update()
    {
        //Values are taken from the keyboard so that they can be tested in the computer environment.
        _horizontalInput = Input.GetAxis("Horizontal");

        if (CarGameManager.Instance.CanPlay)
        {
            _activeCarGO.transform.Translate(Vector3.up * _speed * Time.deltaTime);
            _activeCarGO.transform.Rotate(Vector3.back, _turnSpeed * _horizontalInput * Time.deltaTime);
        }
    }
    public GameObject ActiveCarGO
    {
        get
        {
            return _activeCarGO;
        }
        set
        {
            _activeCarGO = value;
        }
    }
    public void SetTurnDirection(float direction)
    {
        _horizontalInput = direction;
    }
}
