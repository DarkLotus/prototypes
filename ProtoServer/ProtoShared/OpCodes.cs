namespace ProtoShared
{
public static class OpCodes
{
public const short S_ChatMessage = 100;
public const short C_CreateCharacter = 101;
public const short S_DeleteObject = 102;
public const short C_DissconnectRequest = 103;
public const short C_DropItem = 104;
public const short S_EnterWorld = 105;
public const short C_LoginRequest = 106;
public const short S_LoginResponse = 107;
public const short C_MoveRequest = 108;
public const short C_PickupItem = 109;
public const short S_Ping = 110;
public const short S_PlayAnimation = 111;
public const short C_SelectCharacter = 112;
public const short S_ShowObject = 113;
public const short S_ShowOtherToon = 114;
public const short S_SyncObjectLocation = 115;
public const short C_UseObject = 116;
public const short C_UseSkill = 117;
}
}
