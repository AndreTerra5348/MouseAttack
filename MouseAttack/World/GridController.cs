using Godot;
using static Godot.GD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Misc;
using MouseAttack.Extensions;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Monster;
using MouseAttack.Constants;
using MouseAttack.World.Monster;

namespace MouseAttack.World
{
    public class GridController : TileMap, IInitializable, ISharable
    {
        public event EventHandler Initialized;
        public event EventHandler RoundFinished;

        const int GrassTile = 0;
        Vector2 UsedRectSize => GetUsedRect().Size;
        Vector2 UsedRectPosition => GetUsedRect().Position;
        Vector2[] _directions = new Vector2[8] 
        {
            Vector2.Up,
            Vector2.Down,
            Vector2.Left,
            Vector2.Right,
            Vector2.Up + Vector2.Left,
            Vector2.Up + Vector2.Right,
            Vector2.Down + Vector2.Left,
            Vector2.Down + Vector2.Right,
        };

        AStar2D _aStar = new AStar2D();

        public const float TurnDelay = 0.3f;
        public int RoundCount { get; private set; } = 1;
        public int MonsterCount { get; private set; } = 2;
        public bool IsTurnElapsing { get; private set; } = false;

        public GridController() => 
            TreeSharer.RegistryNode(this);

        public override void _Ready()
        {
            base._Ready();
            _aStar.ReserveSpace((int)(UsedRectSize.x * UsedRectSize.y));

            int startX = (int)UsedRectPosition.x;
            int startY = (int)UsedRectPosition.y;

            for (int x = startX; x < UsedRectSize.x; x++)
            {
                for (int y = startY; y < UsedRectSize.y+1; y++)
                {
                    Vector2 mapPosition = new Vector2(x, y);
                    _aStar.AddPoint(GetIdFromMapPosition(mapPosition), MapToWorld(mapPosition));
                }
            }

            for (int x = startX; x < UsedRectSize.x; x++)
            {
                for (int y = startY; y < UsedRectSize.y+1; y++)
                {
                    Vector2 mapPosition = new Vector2(x, y);
                    int id = GetIdFromMapPosition(mapPosition);
                    foreach (Vector2 direction in _directions)
                    {
                        Vector2 neighbourMapPosition = mapPosition + direction;
                        if (neighbourMapPosition.x < 0)
                            continue;
                        if (neighbourMapPosition.y <= 0)
                            continue;
                        if (neighbourMapPosition.x >= UsedRectSize.x)
                            continue;
                        if (neighbourMapPosition.y >= UsedRectSize.y+1)
                            continue;

                        int neighbourId = GetIdFromMapPosition(neighbourMapPosition);
                        if (!_aStar.HasPoint(neighbourId))
                            continue;

                        _aStar.ConnectPoints(id, neighbourId);
                    }
                    if (GetCell(x, y) != GrassTile && GetCell(x, y) != MonsterGenerator.MonsterSpawnTile)
                        _aStar.SetPointDisabled(id, true);
                }
            }

            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public void SetCellDisabled(Vector2 worldPosition, bool isDisabled = true)
        {
            int id = GetIdFromWorldPosition(worldPosition);
            _aStar.SetPointDisabled(id, isDisabled);
        }

        public void SetCellAsTaken(Vector2 worldPosition) =>
            SetCellDisabled(worldPosition, true);

        public void SetCellAsEmpty(Vector2 worldPosition) =>
            SetCellDisabled(worldPosition, false);

        public bool IsCellAvailable(Vector2 worldPosition)
        {
            int id = GetIdFromWorldPosition(worldPosition);
            return _aStar.HasPoint(id) && !_aStar.IsPointDisabled(id);
        }

        /// <summary>
        /// Get the path to the target,
        /// First Element is Caller's Position,
        /// Last Element is Target's Position
        /// </summary>
        /// <param name="worldPosition">Caller Position</param>
        /// <param name="targetWorldPosition">Target Position</param>
        /// <returns>Vector2 array representing the path to the target</returns>
        public Vector2[] GetAvailablePath(Vector2 worldPosition, Vector2 targetWorldPosition)
        {
            int id = GetIdFromWorldPosition(worldPosition);
            int targetId = GetIdFromWorldPosition(targetWorldPosition);            
            return _aStar.GetPointPath(id, targetId);
        }

        private int GetIdFromMapPosition(Vector2 mapPosition) =>
            ((int)(mapPosition.y + (mapPosition.x * UsedRectSize.y)));

        private int GetIdFromWorldPosition(Vector2 worldPosition) =>
            GetIdFromMapPosition(WorldToMap(worldPosition));

        async public void ElapseTurn()
        {
            IsTurnElapsing = true;

            await this.CreateTimer(TurnDelay);
            foreach (Node node in GetChildren())
            {
                var monster = node as MonsterEntity;
                if (monster == null)
                    continue;

                monster.Act();
            }
            RoundFinished?.Invoke(this, EventArgs.Empty);
            RoundCount++;
            MonsterCount = GetChildren().OfType<MonsterEntity>().Count();

            IsTurnElapsing = false;
        }
    }
}
