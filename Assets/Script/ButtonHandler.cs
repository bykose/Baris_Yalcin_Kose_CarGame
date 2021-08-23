using UnityEngine;
public class ButtonHandler : MonoSingleton<ButtonHandler>
{

    [Header("Properties")]
    private float _direction;

    #region Set Button's horizontal axis
    public void TurnCarDirectionLeft()
    {
        _direction = -1f;
        CarController.Instance.SetTurnDirection(_direction);
    }
    public void TurnCarDirectionRight()
    {
        _direction = 1f;
        CarController.Instance.SetTurnDirection(_direction);
    }
    public void SetDirectionZero()
    {
        _direction = 0f;
        CarController.Instance.SetTurnDirection(_direction);
    }
    #endregion

}
