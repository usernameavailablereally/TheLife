using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;

    public Button StartButton => _startButton;
    public Button StopButton => _stopButton;
}
