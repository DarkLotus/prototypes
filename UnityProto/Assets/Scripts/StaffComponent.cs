using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class StaffComponent : MonoBehaviourEx {
    public int Wage = 0;
    public int Programming, Art, Managment;
    public HumanState State = HumanState.Idle;

    Animator Anim;
    private bool bShowTrainMenu;

    private PlayerManager _pm;
	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
        if (LevelSerializer.IsDeserializing)
            return;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StaffController>().addStaff(this);
        _pm = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
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

    void OnMouseDown() {
        if(State != HumanState.Typing)
        bShowTrainMenu = true;
    }

    void OnGUI() {
        if (bShowTrainMenu)
            bShowTrainMenu = GUIHelpers.DrawTrainStaffMenu(this,_pm);
    }

    public enum HumanState
    {
        Idle,
        IdleSitting,
        Walking,
        Typing
    }

    internal void BeginTraining(Training t) {
        var r = RadicalRoutine.Create(Train(t));
        r.Run();
    }

    private IEnumerator Train(Training t) {
        this.State = HumanState.Typing;
        yield return new RadicalWaitForSeconds(t.Strength * 2);
        if(t.Skill.ID == 1)
        this.Programming += t.Strength;
        if (t.Skill.ID == 2)
            this.Art += t.Strength;
        this.State = HumanState.Idle;

    }
}
