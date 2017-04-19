using UnityEngine;
using System.Collections;

public class SceneIntro : MonoBehaviour {

    public enum Scene {Intro, Opening, Main, Win};
    public Scene scene;

	// Use this for initialization
	void Start () {
        if (this.scene == Scene.Intro)
        {
            IntroScene();
        }

        if (this.scene == Scene.Main)
        {
            MainScene();
        }

        if (this.scene == Scene.Opening)
        {
            OpeningScene();
        }
        if(this.scene == Scene.Win)
        {
            WinScene();
        }
	}

    void IntroScene()
    {
        string firstMessage = "MISSION: Explore the planet for resources";
        string secondMessage = "MISSION STATUS: Crash landed";
        string thirdMessage = "NEW MISSION: Reach the yellow rescue beacon";
        MessageDisplay.S.QueueMessage(firstMessage);
        MessageDisplay.S.QueueMessage(secondMessage);
        MessageDisplay.S.QueueMessage(thirdMessage);
    }

    void OpeningScene()
    {
        return; // do nothing
    }

    void WinScene()
    {
        string winMessage = "MISSION STATUS: Complete.  Pilot safely returned to ship.";
        MessageDisplay.S.QueueMessage(winMessage);
    }

    void MainScene()
    {
        string suitError = "ERROR:\n\tSuit malfunction detected\n\tUser control impossible\n\tControl transferred to remote operator";
        
        // freeze player for duration of introduction messages
        float totalMessageLength = MessageDisplay.S.MessageLength(suitError);
        totalMessageLength += MessageDisplay.S.messageInterval * 0; // specifies the number of intervals for the intro messages and adds to the time needed to freeze
        Player1Controller.Instance.FreezeMovement(totalMessageLength);

        MessageDisplay.S.QueueMessage(suitError);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
