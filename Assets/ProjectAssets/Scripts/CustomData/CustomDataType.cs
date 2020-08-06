using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
public class CustomDataType : MonoBehaviourPunCallbacks
{
    [SerializeField] MyCustomSerialization _customSerialization = new MyCustomSerialization();
    [SerializeField] bool _sendAsTyped = true;
    private void Start()
    {
        PhotonPeer.RegisterType(typeof(MyCustomSerialization), (byte)'M', MyCustomSerialization.Serialize, MyCustomSerialization.Deserialize);
    }

    private void Update()
    {
        if (_customSerialization.MyNumber != -1)
        {
            Debug.Log("Sending custom serialization");
            SendCustomSerialization(_customSerialization, _sendAsTyped);
            _customSerialization.MyNumber = -1;
            _customSerialization.MyString = string.Empty;
        }
    }
    private void SendCustomSerialization(MyCustomSerialization data,bool typed)
    {
        if (!typed)
            base.photonView.RPC("RPC_ReceiveMyCustomSerialization", RpcTarget.AllViaServer, MyCustomSerialization.Serialize(_customSerialization));
        else
            base.photonView.RPC("RPC_TypedReceiveMyCustomSerialization", RpcTarget.AllViaServer, _customSerialization);
    }
    [PunRPC]
    private void RPC_ReceiveMyCustomSerialization(byte[] datas)
    {
        MyCustomSerialization result = (MyCustomSerialization)MyCustomSerialization.Deserialize(datas);
        Debug.Log("Received byte array: " + result.MyNumber + ", " + result.MyString);
    }
    [PunRPC]
    private void RPC_TypedReceiveMyCustomSerialization(MyCustomSerialization datas)
    {
        Debug.Log("Received typed: " + datas.MyNumber + ", " + datas.MyString);
    }
}
