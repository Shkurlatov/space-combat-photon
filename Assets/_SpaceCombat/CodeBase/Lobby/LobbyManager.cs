using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using SpaceCombat.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceCombat.Lobby
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        private const string GAME = "Game";

        public string PlayerName;

        [Header("Create Room Panel")]
        public GameObject CreateRoomPanel;

        public InputField CreateRoomNameInputField;
        public InputField JoinRoomNameInputField;

        [Header("Room List Panel")]
        public GameObject RoomListPanel;

        public GameObject RoomListContent;
        public GameObject RoomListEntityPrefab;

        [Header("Inside Room Panel")]
        public GameObject InsideRoomPanel;

        public Button StartGameButton;
        public GameObject PlayerListEntityPrefab;

        private Dictionary<string, RoomInfo> cachedRoomList;
        private Dictionary<string, GameObject> roomList;
        private Dictionary<int, GameObject> playerListEntities;

        public void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            cachedRoomList = new Dictionary<string, RoomInfo>();
            roomList = new Dictionary<string, GameObject>();
            
            PlayerName = "Player " + Random.Range(1000, 10000);
        }

        private void Start()
        {
            LaunchConnection();
        }

        #region PUN CALLBACKS

        public override void OnConnectedToMaster()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }

            CreateRoomPanel.SetActive(true);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            ClearRoomListView();

            UpdateCachedRoomList(roomList);
            UpdateRoomListView();
        }

        public override void OnJoinedLobby()
        {
            RoomListPanel.SetActive(true);
            CreateRoomPanel.SetActive(true);
            InsideRoomPanel.SetActive(false);

            cachedRoomList.Clear();
            ClearRoomListView();
        }

        public override void OnLeftLobby()
        {
            RoomListPanel.SetActive(true);
            cachedRoomList.Clear();
            ClearRoomListView();

        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {

        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {

        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {

        }

        public override void OnJoinedRoom()
        {
            cachedRoomList.Clear();

            if (playerListEntities == null)
            {
                playerListEntities = new Dictionary<int, GameObject>();
            }

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                GameObject entity = Instantiate(PlayerListEntityPrefab);
                entity.transform.SetParent(InsideRoomPanel.transform);
                entity.transform.localScale = Vector3.one;
                entity.GetComponent<PlayerListEntity>().Initialize(p.ActorNumber, p.NickName);

                object isPlayerReady;
                if (p.CustomProperties.TryGetValue(GameConstants.PLAYER_READY, out isPlayerReady))
                {
                    entity.GetComponent<PlayerListEntity>().SetPlayerReady((bool) isPlayerReady);
                }

                playerListEntities.Add(p.ActorNumber, entity);
            }

            StartGameButton.gameObject.SetActive(CheckPlayersReady());

            Hashtable props = new Hashtable
            {
                {GameConstants.PLAYER_LOADED_LEVEL, false}
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

            InsideRoomPanel.SetActive(true);
        }

        public override void OnLeftRoom()
        {
            foreach (GameObject entity in playerListEntities.Values)
            {
                Destroy(entity.gameObject);
            }

            playerListEntities.Clear();
            playerListEntities = null;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GameObject entity = Instantiate(PlayerListEntityPrefab);
            entity.transform.SetParent(InsideRoomPanel.transform);
            entity.transform.localScale = Vector3.one;
            entity.GetComponent<PlayerListEntity>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

            playerListEntities.Add(newPlayer.ActorNumber, entity);

            StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Destroy(playerListEntities[otherPlayer.ActorNumber].gameObject);
            playerListEntities.Remove(otherPlayer.ActorNumber);

            StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
            {
                StartGameButton.gameObject.SetActive(CheckPlayersReady());
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (playerListEntities == null)
            {
                playerListEntities = new Dictionary<int, GameObject>();
            }

            GameObject entity;
            if (playerListEntities.TryGetValue(targetPlayer.ActorNumber, out entity))
            {
                object isPlayerReady;
                if (changedProps.TryGetValue(GameConstants.PLAYER_READY, out isPlayerReady))
                {
                    entity.GetComponent<PlayerListEntity>().SetPlayerReady((bool) isPlayerReady);
                }
            }

            StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        #endregion

        #region UI CALLBACKS

        public void OnCreateRoomButtonClicked()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();

                return;
            }

            if (!CreateRoomNameInputField.text.Equals(""))
            {
                CreateRoom();
            }
        }

        public void OnJoinRoomButtonClicked()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();

                return;
            }

            if (!JoinRoomNameInputField.text.Equals(""))
            {
                PhotonNetwork.JoinRoom(JoinRoomNameInputField.text);
            }
        }

        public void OnBackButtonClicked()
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }
        }

        public void CreateRoom()
        {
            string roomName = CreateRoomNameInputField.text;

            byte maxPlayers = GameConstants.MAX_PLAYERS_AMOUNT;
            maxPlayers = (byte) Mathf.Clamp(maxPlayers, 2, 8);

            RoomOptions options = new RoomOptions {MaxPlayers = maxPlayers, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);

            CreateRoomPanel.SetActive(false);
        }

        public void OnJoinRandomRoomButtonClicked()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void OnLeaveGameButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void LaunchConnection()
        {
            string playerName = PlayerName;

            if (!playerName.Equals(""))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                Debug.LogError("Player Name is invalid.");
            }
        }

        public void OnRoomListButtonClicked()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public void OnStartGameButtonClicked()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            PhotonNetwork.LoadLevel(GAME);
        }

        #endregion

        private bool CheckPlayersReady()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return false;
            }

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                object isPlayerReady;
                if (p.CustomProperties.TryGetValue(GameConstants.PLAYER_READY, out isPlayerReady))
                {
                    if (!(bool) isPlayerReady)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        
        private void ClearRoomListView()
        {
            foreach (GameObject entry in roomList.Values)
            {
                Destroy(entry.gameObject);
            }

            roomList.Clear();
        }

        public void LocalPlayerPropertiesUpdated()
        {
            StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }

        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
                {
                    if (cachedRoomList.ContainsKey(info.Name))
                    {
                        cachedRoomList.Remove(info.Name);
                    }

                    continue;
                }

                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList[info.Name] = info;
                }
                else
                {
                    cachedRoomList.Add(info.Name, info);
                }
            }
        }

        private void UpdateRoomListView()
        {
            foreach (RoomInfo info in cachedRoomList.Values)
            {
                GameObject entity = Instantiate(RoomListEntityPrefab);
                entity.transform.SetParent(RoomListContent.transform);
                entity.transform.localScale = Vector3.one;
                entity.GetComponent<RoomListEntity>().Initialize(info.Name, (byte)info.PlayerCount, (byte)info.MaxPlayers);

                roomList.Add(info.Name, entity);
            }
        }
    }
}