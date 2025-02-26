using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class UIManager : MonoBehaviour, IStartable, IDisposable
{
    // no output for external use, no UI subscription garbage in Controllers
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _newGameButton;
    
    // IClickHandler pattern makes it possible to scale control over all UI user input in one place 
    // If there are to many Handlers this might be split intro MainUIManager + SubUIManager pattern
    // InputController is an example of a SubUIManager
    IRestartClickHandler _restartClickHandler;
    IStartStopLifeClickHandler _startStopLifeClickHandler;
    ISaveLoadClickHandler _saveLoadClickHandler;
    
    [Inject]    
    public void Construct(IRestartClickHandler restartClickHandler, IStartStopLifeClickHandler startStopClickHandler, ISaveLoadClickHandler saveLoadClickHandler)
    {
        _restartClickHandler = restartClickHandler;
        _startStopLifeClickHandler = startStopClickHandler;
        _saveLoadClickHandler = saveLoadClickHandler;
    }

    public void Start()
    {
        _startButton.onClick.AddListener(() => _startStopLifeClickHandler.OnStartLifeClicked());
        _stopButton.onClick.AddListener(() => _startStopLifeClickHandler.OnStopLifeClicked());
        _saveButton.onClick.AddListener(() => _saveLoadClickHandler.OnSaveClicked());
        _loadButton.onClick.AddListener(() => _saveLoadClickHandler.OnLoadClicked());
        _newGameButton.onClick.AddListener(() => _restartClickHandler.OnRestartClicked());
    }

    public void Dispose()
    {
        _startButton.onClick.RemoveAllListeners();
        _stopButton.onClick.RemoveAllListeners();
        _saveButton.onClick.RemoveAllListeners();
        _loadButton.onClick.RemoveAllListeners();
        _newGameButton.onClick.RemoveAllListeners();
    }
}

public interface IRestartClickHandler
{
    void OnRestartClicked();
}

public interface IStartStopLifeClickHandler
{
    void OnStartLifeClicked();
    void OnStopLifeClicked();
}

public interface ISaveLoadClickHandler
{
    void OnSaveClicked();
    void OnLoadClicked();
}
