using UnityEngine;
using UnityEngine.Pool;

namespace GrayCube.Containers
{
    public class FillerSource : MonoBehaviour
    {
        [SerializeField] private GameObject _fillerPrefab;

        private IObjectPool<IFiller> _pool;
        private IFiller _currentFiller;

        private void Awake()
        {
            InitObjectPool();

            ReleaseFiller();
        }

        private void InitObjectPool()
        {
            _pool = new ObjectPool<IFiller>(
                CreateFiller,
                (filler) => ((MonoBehaviour)filler).gameObject.SetActive(true),
                (filler) => ((MonoBehaviour)filler).gameObject.SetActive(false)
            );
        }

        private IFiller CreateFiller()
        {
            var filler = Instantiate(_fillerPrefab);
            filler.transform.position = transform.position;
            return filler.GetComponent<IFiller>();
        }

        private void ReleaseFiller()
        {
            var filler = _pool.Get();
            filler.Filled += OnFillerFilled;

            _currentFiller = filler;
        }

        private void OnFillerFilled()
        {
            _currentFiller.Filled -= OnFillerFilled;
            ReleaseFiller();
        }
    }
}
