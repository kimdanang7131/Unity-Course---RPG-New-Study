using UnityEngine;

public enum SkillUpgradeType
{
    None,

    // Dash Tree
    Dash, // Dash to Avoid damage
    Dash_CloneOnStart, // 대쉬할때 클론생성
    Dash_CloneOnStartAndArrival, // 대쉬 시작 끝 클론생성
    Dash_ShardOnStart, // shard 시작시 생성
    Dash_ShardOnStartAndArrival, // shard 시작 끝 생성

    // Shard Tree
    Shard, // 적에게 닿으면 터짐 , 시간 지나도 터짐
    Shard_MoveToEnemy, // 적 근처로 이동
    Shard_MultiCast, // N 차지가 있다면 바로 캐스트가능
    Shard_Teleport, // 마지막 샤드 배치한곳과 위치바꾸기
    Shard_TeleportHPRewind, // 

    // Sword Tree
    SwordThrow,
    SwordThrow_Spin,
    SwordThrow_Pierce,
    SwordThrow_bounce,

    // Time Echo Tree
    TimeEcho,
    TimeEcho_SingleAttack,
    TimeEcho_MultiAttack,
    TimeEcho_ChanceToDuplicate,

    TimeEcho_HealWisp,

    TimeEcho_CleanseWisp,
    TimeEcho_CooldownWisp,

    // Domain Expansion Tree
    Domain_SlowingDown,
    Domain_EchoSpam,
    Domain_ShardSpam
}
