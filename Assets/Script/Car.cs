using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [Header("Reference")]
    private Vector3 _initialPos;
    private Quaternion _initialRotation;
    public Material Material;
    private ReplayManager _replay;
    private Renderer _rend;
    

    #region Properties Get, Set
    public Vector3 InitialPos
    {
        get
        {
            return _initialPos;
        }
        set
        {
            _initialPos = value;
        }
    }
    public Quaternion InitialRotation
    {
        get
        {
            return _initialRotation;
        }
        set
        {
            _initialRotation = value;
        }
    }
    #endregion
    private void Start()
    {
        _replay = GetComponent<ReplayManager>();
        _rend = GetComponent<Renderer>();
        
        SetInitial();
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the replays collide, it restarts the Level.
        if (other.gameObject.CompareTag("Replay") && this.gameObject.CompareTag("Replay"))
        {
            SceneChangeEditor.Instance.RestartLevel();
            return;
        }
        //If the car object hits other cars or an obstacle,
        //the car returns to the starting point and the positions saved for replay are deleted.
        if (other.gameObject.CompareTag("Replay") || other.gameObject.CompareTag("Obstacle"))
        {
            CarGameManager.Instance.RestartStage();
            _replay.ReplayRecords.Clear();
        }
        //If the car object comes to the exit point, it is passed to the next stage.
        //If it is the last car, it will be passed to the next level.
        else if (other.gameObject.CompareTag("Exit") && this.gameObject.tag == "Car")
        {
            
            if (CarController.Instance.ActiveCarGO.name == CarGameManager.Instance.CarGo[CarGameManager.Instance.CarGo.Length-1].name)
            {
                CarGameManager.Instance.CanPlay = false;
                if (SceneManager.GetActiveScene().name == "Level-2")
                    return;
                SceneChangeEditor.Instance.NextLevel("Level-2");
            }
            else
            {
                CarGameManager.Instance.NextStage();
                
                _replay.IsInReplayMode = true;
                this.gameObject.tag = "Replay";

                ChangeMaterial();
            }
        }
        
    }
    //The initial position and rotation of the Car object are set.
    private void SetInitial()
    {
        _initialPos = this.transform.position;
        _initialRotation = this.transform.rotation;
    }
    private void ChangeMaterial()
    {
        _rend.enabled = true;
        _rend.sharedMaterial = Material;
    }
}
