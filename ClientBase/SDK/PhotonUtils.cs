using ExitGames.Client.Photon;
using Photon.Pun;

namespace ClientBase.SDK
{
    internal class PhotonUtils
    {
        public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object @object = SerializationUtils.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
            PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, @object, RaiseEventOptions, sendOptions);
        }
    }
}
