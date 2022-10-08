using GrayCube.GameState;
using GrayCube.Slots;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GrayCube.Save
{
    public class SaveSystem : MonoBehaviour, ISaver
    {
        [SerializeField] private SlotItemData _itemData;
        [SerializeField] private float _savePeriod;

        private GameSave _currentSave;
        private IGameState _gameState;
        private string _filePath;
        private Coroutine _saveCoroutine;

        public float Volume
        {
            get => _currentSave != null ? _currentSave.Volume : 1f;
            set
            {
                _currentSave.Volume = value;
            }
        }

        private void Awake()
        {
            _filePath = $"{Application.persistentDataPath}/GameData" + (Application.isEditor ? "_DEV" : string.Empty) + ".bytes";
            Load();
            _saveCoroutine = StartCoroutine(SaveCoroutine());
        }

        private void OnDestroy()
        {
            Save();
        }

        public GameSave GetGameSave() => _currentSave;
        public GameObject[,] GetGridItems() => _currentSave.GetGridItems(_itemData);
        public void SetGridItems(ISlotItem[,] items) => _currentSave.SetGridItems(items);
        public void SetPocketItem(ISlotItem item) => _currentSave.SetPocketItem(item);
        public GameObject GetPocketItem() => _currentSave.GetPocketItem(_itemData);

        public void Load()
        {
            if (!File.Exists(_filePath))
            {
                Debug.Log($"Save file does not exsist: {_filePath}");
                _currentSave = new GameSave();
                return;
            }

            GameSave data = null;
            FileStream fs = new FileStream(_filePath, FileMode.Open);
            fs.Position = 0;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = formatter.Deserialize(fs) as GameSave;
            }
            catch (SerializationException ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                fs.Close();
            }

            _currentSave = data ?? new GameSave();
        }

        public void Save()
        {
            FileStream fs = new FileStream(_filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, _currentSave);
            }
            catch (SerializationException ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                fs.Close();
            }
        }

        public void ClearPockets()
        {
            SetPocketItem(null);
            SetGridItems(null);
        }

        private IEnumerator SaveCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_savePeriod);
                Save();
            }
        }
    }
}