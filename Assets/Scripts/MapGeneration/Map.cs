using UnityEngine;

namespace MapGen
{
    class Map
    {
        private GameObject[][] _tiles;

        #region Indexer Overload
        public GameObject[] this[int index]
        {
            get
            {
                return _tiles[index];
            }
        }
        #endregion

        #region Constructor
        public Map(){}
        #endregion

        #region Main Methods

        private void InitMap(int width, int height)
        {
            _tiles = new GameObject[height][];
            for (int arr = 0; arr < height; arr++)
            {
                _tiles[arr] = new GameObject[width];
            }
        }

        public void CreateMap(int width, int height)
        {
            ClearLevel();
            InitMap(width, height);
        }

        public void SetTile(int x, int y, GameObject obj){
            _tiles[x][y] = obj;
        }

        private void ClearLevel()
        {
            if (_tiles != null)
            {
                for (int goArray = 0; goArray < _tiles.Length; goArray++)
                {
                    for (int go = 0; go < _tiles[goArray].Length; go++)
                    {
                        GameObject.DestroyImmediate(_tiles[goArray][go]);
                        _tiles[goArray][go] = null;
                    }
                }
                _tiles = null;
                System.GC.Collect();
            }
        }
        #endregion
    }
}