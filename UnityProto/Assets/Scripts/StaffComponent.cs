using UnityEngine;
using System.Collections;

public class StaffComponent : MonoBehaviour {
    public int Wage = 0;
    public int Programming, Art, Managment;
    public HumanState State = HumanState.Idle;

    Animator Anim;
	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
        if (LevelSerializer.IsDeserializing)
            return;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StaffController>().addStaff(this);
        
	}
	

	// Update is called once per frame
	void Update () {
        AnimatorStateInfo stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        switch (State) {
            case HumanState.Idle:
                Anim.SetInteger("State", 0);
                break;
            case HumanState.IdleSitting:
                Anim.SetInteger("State", 1);
                break;
            case HumanState.Typing:
                Anim.SetInteger("State", 3);
                break;
            default:
                break;
        }

	}


    public enum HumanState
    {
        Idle,
        IdleSitting,
        Walking,
        Typing
    }
}
