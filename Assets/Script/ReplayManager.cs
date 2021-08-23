using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    [Header("Properties")]
    private bool _isInReplayMode;
    private int _currentReplayIndex = 0;

    [Header("Reference")]
    public List<ReplayRecord> ReplayRecords = new List<ReplayRecord>();
    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    
    private void FixedUpdate()
    {
        //It is checked if it is in replay mode.
        if (!_isInReplayMode && CarGameManager.Instance.CanPlay)
        {
            Record(ReplayRecords);
        }
        else if(_isInReplayMode && CarGameManager.Instance.CanPlay)
        {
            Replay(ReplayRecords);
        }
        
    }
    //The movements of the object are recorded. It is added to the list.
    public void Record(List<ReplayRecord> list)
    {
        list.Add(new ReplayRecord { position = transform.position, rotation = transform.rotation });
    }
    //Moves added to the list are played one by one.
    public void Replay(List<ReplayRecord> list)
    {
        int nextIndex = _currentReplayIndex + 1;

        if (nextIndex < list.Count)
        {
            SetTransform(nextIndex);
        }
        _boxCollider.isTrigger = true;
    }
    //The values saved in the list are assigned to the object.
    public void SetTransform(int index)
    {
        _currentReplayIndex = index;
        ReplayRecord actionReplayRecord = ReplayRecords[index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
        
    }
    public bool IsInReplayMode
    {
        get
        {
            return _isInReplayMode;
        }
            
        set
        {
            _isInReplayMode = value;
        }
    }
    
}
