using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDGETCONTROLLER : MonoBehaviour {

    public AudioClip GASGASGAS;
    public AudioSource aSource;
    public Rigidbody fidgetRB;
    public float angularVelocityMod;
    public RewardController rewardController;

    private Vector2 startPos;
    private Vector2 endPos;
    private float startTime;
    private float spinCounter;
    private int spinRewards;

	void Start () {
        aSource.clip = GASGASGAS;
        aSource.volume = 0;
        fidgetRB.maxAngularVelocity = 20;
        Screen.orientation = ScreenOrientation.Portrait;
	}
	
	void Update ()
    {
        spinCounter += fidgetRB.angularVelocity.magnitude*Time.deltaTime;
        if (spinCounter*fidgetRB.angularVelocity.magnitude>4.9f)
        {
            spinCounter = 0;
            rewardController.ShowNewReward(fidgetRB.angularVelocity.magnitude);
        }
        GetPlayerInput();
        aSource.pitch = aSource.volume*aSource.volume + 0.1f;
        aSource.volume = fidgetRB.angularVelocity.magnitude/15;
	}

    void Spin(float velocity)
    {
        if (!aSource.isPlaying)
            aSource.Play();
        fidgetRB.AddRelativeTorque(-(new Vector3(0, 0, velocity)), ForceMode.VelocityChange);
        
    }

    void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos =Input.mousePosition;
            startTime = Time.realtimeSinceStartup;
        }
        if (Input.GetMouseButtonUp(0))
        {
            bool rotateRight = false;
            endPos = Input.mousePosition;
            Vector2 middle = new Vector2(Screen.width / 2, Screen.height / 2);
            if (Vector2.SignedAngle(middle - startPos, middle - endPos) > 0) rotateRight = true;
            Spin((rotateRight ? 1 : -1) *Vector2.Distance(startPos,endPos) / (Time.realtimeSinceStartup - startTime)/300);
        }
    }
}
