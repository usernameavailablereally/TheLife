using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _newGameButton;

    public Button StartButton => _startButton;
    public Button StopButton => _stopButton;
    public Button SaveButton => _saveButton;
    public Button LoadButton => _loadButton;
    public Button NewGameButton => _newGameButton;
}
