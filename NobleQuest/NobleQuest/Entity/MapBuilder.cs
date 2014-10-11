﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace NobleQuest.Entity
{
    public class MapBuilder
    {
        public NobleQuestGame Game;
        public EntityFactory EntityFactory;

        private const int LANE_LENGTH = 5;

        public NodeEntity[] TopLane;
        public NodeEntity[] MiddleLane;
        public NodeEntity[] BottomLane;

        public MapBuilder()
        {
            EntityFactory = new EntityFactory();
            TopLane = new NodeEntity[LANE_LENGTH];
            MiddleLane = new NodeEntity[LANE_LENGTH];
            BottomLane = new NodeEntity[LANE_LENGTH];
        }

        public void BuildMap(NobleQuestGame game)
        {
            this.Game = game;

            GameEntity ThisGameEntity = null;

            float hudYOffset = game.Graphics.PreferredBackBufferHeight - 225.0f;

            this.Game.Player.Resources = EntityFactory.GetResourceEntity(game, new Vector2(0f, hudYOffset), true);
            this.Game.Enemy.Resources = EntityFactory.GetResourceEntity(game, new Vector2(0f, hudYOffset), false);

            ThisGameEntity = EntityFactory.GetPlayerTown(this.Game, new Vector2(100f, 200f));
            this.Game.Player.Town = (TownNode)ThisGameEntity;
            this.Game.NodeEntityList.Add((NodeEntity)this.Game.Player.Town);

            ThisGameEntity = EntityFactory.GetEnemyTown(this.Game, new Vector2(700f, 200f));
            this.Game.Enemy.Town = (TownNode)ThisGameEntity;
            this.Game.NodeEntityList.Add((NodeEntity)this.Game.Enemy.Town);

            // Build Top Lane
            this.BuildLane(TopLane, 100.0f, 100.0f, game.Player.Town, game.Enemy.Town);

            // Build Middle Lane
            this.BuildLane(MiddleLane, 100.0f, 200.0f, game.Player.Town, game.Enemy.Town);

            // Build Bottom Lane
            this.BuildLane(BottomLane, 100.0f, 300.0f, game.Player.Town, game.Enemy.Town);

            // Randomly Choose Crossing Paths
            int topIndex = this.Game.Random.Next(LANE_LENGTH - 1);
            int bottomIndex = LANE_LENGTH - topIndex;

            PathEntity crossOverPath = 
                EntityFactory.GetPathEntity(this.Game, TopLane[topIndex], MiddleLane[topIndex + 1]);
            TopLane[topIndex].RightPaths.Add(crossOverPath);
            MiddleLane[topIndex + 1].LeftPaths.Add(crossOverPath);

            this.Game.PathEntityList.Add(crossOverPath);

            crossOverPath =
                EntityFactory.GetPathEntity(this.Game, MiddleLane[bottomIndex - 2], BottomLane[bottomIndex - 1]);
            MiddleLane[bottomIndex - 2].RightPaths.Add(crossOverPath);
            BottomLane[bottomIndex - 1].LeftPaths.Add(crossOverPath);
            this.Game.PathEntityList.Add(crossOverPath);
        }

        public void BuildLane(NodeEntity[] LaneArray, float xOffset, float yPosition, 
            NodeEntity startNode, NodeEntity endNode)
        {
            GameEntity ThisGameEntity = null;
            GameEntity LastGameEntity = null;

            LastGameEntity = startNode;

            for (int i = 0; i < LaneArray.Length; i++)
            {
                ThisGameEntity = EntityFactory.GetNode(
                    this.Game, 
                    new Vector2(LastGameEntity.Position.X + xOffset, yPosition));
                LaneArray[i] = (NodeEntity)ThisGameEntity;
                this.Game.NodeEntityList.Add((NodeEntity)ThisGameEntity);
                this.Game.PathEntityList.Add(EntityFactory.GetPathEntity(this.Game, (NodeEntity)LastGameEntity, (NodeEntity)ThisGameEntity));
                LastGameEntity = ThisGameEntity;
            }

            this.Game.PathEntityList.Add(EntityFactory.GetPathEntity(this.Game, (NodeEntity)LastGameEntity, endNode));
        }
    }
}
