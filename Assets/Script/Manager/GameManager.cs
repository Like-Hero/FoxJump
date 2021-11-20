using System;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player { get; set; }
    public Transform deadPoint { get; set; } //死亡点
    public Transform BornPoint;
    public CurrentPassData currentPassData { get; set; }
    protected override void Awake()
    {
        base.Awake();
        currentPassData = new CurrentPassData();
    }
    private void Start()
    {
        if(Player == null)
        {
            Player = Instantiate(SceneController.Ins.PlayerPrefab, BornPoint.position, Quaternion.identity);
        }
        StartNextSceneInitial();
    }
    private void Update()
    {
        if (JudgePlayerIsDead())
            return;
    }
    public bool JudgePlayerIsDead()
    {
        if (Player.transform.position.y < deadPoint.position.y)
        {
            PlayerDead();
            return true;
        }
        return false;
    }
    private void PlayerDead()
    {
        SceneController.Ins.TransitionSameSceneEnter();
    }
    public void StartNextSceneInitial()
    {
        if (Player.horizontalJoystick == null)
        {
            Player.horizontalJoystick = GameObject.FindGameObjectWithTag("horizontalJoystick").GetComponent<Joystick>();
        }
        if (Player.verticalJoystick == null)
        {
            Player.verticalJoystick = GameObject.FindGameObjectWithTag("verticalJoystick").GetComponent<Joystick>();
        }
        deadPoint = GameObject.FindGameObjectWithTag("deadPoint").transform;
        GameObject.FindGameObjectWithTag("VCamera").GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }
    
}
