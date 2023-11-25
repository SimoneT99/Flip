using System.Collections;
using UnityEngine;


public class LevelManagerBasic : MonoBehaviour
{

    /*Singletone pattern:
    * -accesso globale
    * -controllo not null
    * -elimina duplicati
    */
    private static LevelManagerBasic _levelManagerBasic;

    public static LevelManagerBasic levelManagerBasic { get { return _levelManagerBasic; } }


    private void Awake()
    {
        if (_levelManagerBasic != null && _levelManagerBasic != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _levelManagerBasic = this;
        }
    }

    ///////////////////// codice effettivo del manager /////////////////////

    public LevelState actualLevelState;

    //abbiamo bisogno di un player manager
    public GameObject player;
    public string playerID = "player";

    public CameraController cameraController;
    public UIController uIController;

    public string Thislevel;

    public bool cameraAnimationStarted = false;
    public bool cameraAnimationEnded = false;

    private bool cursorActive = true;

    [SerializeField] Timer timer;
    private void Start()
    {
        Time.timeScale = 1;
        //facciamo il subscribe agli eventi del livello
        LevelEventSystem.levelEventSystem.OnEndLineTriggerEnter += EndLineEntered;
        //settiamo lo stato attuale del livello in starting
        actualLevelState = LevelState.settingUP;
    }

    public void LateUpdate()
    {
        switch (actualLevelState)
        {
            case LevelState.settingUP:
                unPauseGame();
                Cursor.visible = false;
                actualLevelState = LevelState.starting;
                break;
            case LevelState.starting:
                //prepartita
                //TODO enable player input
                if (!cameraAnimationStarted) {
                    cameraAnimationStarted = true;
                    CameraManager.cameraManager.StartCameraAnimation();
                }
                if (cameraAnimationEnded)
                {
                    Debug.Log("spawn player");
                    PlayerManager.playerManager.SpawnPlayer();
                    this.actualLevelState = LevelState.inGame;
                    Timer.timer.startCounting(Thislevel);
                }
                break;
            case LevelState.inGame:
                //se ci fosse qualche evento in game qui si potrebbe gestire
                break;
            case LevelState.ending:
                Timer.timer.stopCounting();
                Timer.timer.saveTime();
                uIController.OpenEndingScreen();
                actualLevelState = LevelState.ended;
                break;
            case LevelState.ended:
                break;
        }
    }

    //Pause and unpause methods
    public void PauseGame()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void unPauseGame()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    //evento per la fine del livello
    private void EndLineEntered()
    {       
        actualLevelState = LevelState.ending;    
    }

    public void NotifyPlayerIsDead()
    {
        //se ci fossero condizioni del livello potremmo metterle qui, oppure delegarne la scelta ad altre classi, per il momento lasciamo qui
        PlayerManager.playerManager.SpawnPlayer();
        Timer.timer.restartTime();
        /**
         * timer.restartTimer();
         */
    }

    public enum LevelState{
        settingUP, starting, inGame, ending, ended
    }

    public void NotifyCameraAnimationEnded(){

        Debug.Log("l'animazione è finita");
        cameraAnimationEnded = true;
    }
}
