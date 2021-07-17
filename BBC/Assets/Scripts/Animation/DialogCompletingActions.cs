using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCompletingActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameData gameData;

    public void ActivateTask_PostDialog(int taskNumber)
    {
        gameData.CurrentTaskNumber = taskNumber;
        Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask(false);
    }

    public void LeaveNPC() => gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.gameObject.SetActive(false);

    public void ChangeDialogStartNode()
    {
        var npc = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger;
        switch (npc.alias)
        {
            case "Дровосек":
                if (npc.overrideStartNode == 5)
                    npc.overrideStartNode = 0;
                break;
        }
    } 

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        sceneIndex = gameData.SceneIndex;
    }
}
