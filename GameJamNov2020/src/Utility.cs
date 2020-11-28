using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    static class Utility
    {
        public static bool IsLevelComplete = false;

        public static void MakeLevel1(List<Texture2D> textures, World world)
        {
            List<LevelRow> levelMap = new List<LevelRow>() 
            {   
                // interior walls
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(170,  170) },
                
                // doors
                new LevelRow() { TextureName = TextureNames.Door, Position = new Vector2(170,  100) },
                new LevelRow() { TextureName = TextureNames.Door, Position = new Vector2(1240, 600) },

                // player
                new LevelRow() { TextureName = TextureNames.Player, Position = new Vector2(350,  580) },

                // duplication powerups
                new LevelRow() { TextureName = TextureNames.DuplicatePowerUp, Position = new Vector2(1200,  100) },

                // enemy
                new LevelRow() { TextureName = TextureNames.Enemy, Position = new Vector2(900,  100) },
            };

            MakeLevelFromLevelMap(levelMap, textures, world);
        }

        public static void MakeLevel2(List<Texture2D> textures, World world)
        {
            List<LevelRow> levelMap = new List<LevelRow>()
            {   
                // interior walls
                // left barrier
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(10,  240) },
                // corner
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(170,  170) },
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(240,  170) },
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(240,  240) },
                // middle blocks
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(600,  600) },
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(670,  600) },
                // right blocks
                
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(1200, 560) },
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(1200, 630) },
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(1200, 700) },
                new LevelRow() { TextureName = TextureNames.Wall, Position = new Vector2(1500, 560) },
                
                // doors
                new LevelRow() { TextureName = TextureNames.Door, Position = new Vector2(170,  700) },
                new LevelRow() { TextureName = TextureNames.Door, Position = new Vector2(700, 700) },
                new LevelRow() { TextureName = TextureNames.Door, Position = new Vector2(1300, 700) },

                // player
                new LevelRow() { TextureName = TextureNames.Player, Position = new Vector2(50,  50) },

                // duplication powerups
                new LevelRow() { TextureName = TextureNames.DuplicatePowerUp, Position = new Vector2(700,  300) },
                new LevelRow() { TextureName = TextureNames.DuplicatePowerUp, Position = new Vector2(1200, 200) },

                // enemy
                new LevelRow() { TextureName = TextureNames.Enemy, Position = new Vector2(400,  100) },
                new LevelRow() { TextureName = TextureNames.Enemy, Position = new Vector2(900,  100) },
            };

            MakeLevelFromLevelMap(levelMap, textures, world);
        }

        private static void MakeLevelFromLevelMap(List<LevelRow> levelMap, List<Texture2D> textures, World world)
        {
            foreach (LevelRow levelRow in levelMap)
            {
                Entity entity = world.CreateEntity();
                entity.Attach(new Sprite(textures[(int)levelRow.TextureName]));
                entity.Attach(new Transform2(levelRow.Position));
                switch (levelRow.TextureName)
                {
                    case TextureNames.Wall:
                        AttachWallComponents(entity);
                        break;
                    case TextureNames.Door:
                        AttachDoorComponents(entity);
                        break;
                    case TextureNames.DuplicatePowerUp:
                        AttachDuplicatePowerComponents(entity);
                        break;
                    case TextureNames.Player:
                        AttachPlayerComponents(entity);
                        break;
                    case TextureNames.Enemy:
                        AttachEnemyComponents(entity);
                        break;
                }
            }
        }

        private static void AttachWallComponents(Entity wallEntity)
        {
            wallEntity.Attach(new StaticObject());
            wallEntity.Attach(new Collider());
        }

        private static void AttachDoorComponents(Entity doorEntity)
        {
            doorEntity.Attach(new Collider());
            doorEntity.Attach(new DoorFlag());
            doorEntity.Attach(new Collisions());
        }

        private static void AttachDuplicatePowerComponents(Entity duplicatePowerEntity)
        {
            duplicatePowerEntity.Attach(new DuplicationPowerFlag());
            duplicatePowerEntity.Attach(new Collider());
        }

        private static void AttachPlayerComponents(Entity playerEntity)
        {
            playerEntity.Attach(new PlayerFlag());
            playerEntity.Attach(new MovementDirection());
            playerEntity.Attach(new Collider());
            playerEntity.Attach(new Collisions());
            playerEntity.Attach(new DynamicObject());
        }

        private static void AttachEnemyComponents(Entity enemyEntity)
        {
            enemyEntity.Attach(new AIPattern());
            enemyEntity.Attach(new MovementDirection());
            enemyEntity.Attach(new Collider());
            enemyEntity.Attach(new EnemyFlag());
        }
    }

    public struct LevelRow
    { 
        public TextureNames TextureName { get; set; }
        public Vector2 Position { get; set; }
    }

    public enum TextureNames
    {
        Wall = 0,
        Door = 1,
        DuplicatePowerUp = 2,
        Player = 3,
        Enemy = 4
    }

    public enum GameState
    {
        Simulating,
        Pause,
        LevelComplete,
        Reset
    }
}
