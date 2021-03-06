﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    abstract class GameObject
    {
        protected Vector2D position;
        public Image sprite;
        protected float scaleFactor;
        private string imagePath;

        //Creates a collisionbox for all gameobjects.
        public RectangleF CollisionBox
        {
            get
            {
                return new RectangleF
                    (
                    position.X,
                    position.Y,
                    sprite.Width * scaleFactor,
                    sprite.Height * scaleFactor
                );
            }
        }

        //Gets and sets the position of the object.
        public Vector2D Position
        {
            get
            {
                return position;
            }
            set { position = value; }
        }

        //The base constructor.
        public GameObject(string imagePath, Vector2D startPos, float scaleFactor)
        {
            position = startPos;
            this.scaleFactor = scaleFactor;
            this.imagePath = imagePath;
            this.sprite = Image.FromFile(imagePath);
        }

        public virtual void Draw(Graphics dc)
        {
            dc.DrawImage(sprite, position.X, position.Y, sprite.Width * scaleFactor, sprite.Height * scaleFactor);
        }

        public virtual void Update(float fps)
        {
            CheckCollision();
        }

        protected void CheckCollision()
        {
            for (int i = 0; i < GameWorld.objectList.Count; i++)
            {
                GameObject obj = GameWorld.objectList[i];

                if (obj != this)
                {
                    if (IsCollidingWith(obj))
                    {
                        //Only running on collision
                        OnCollision(obj);
                    }
                }
            }
        }

        public bool IsCollidingWith(GameObject other)
        {
            return CollisionBox.IntersectsWith(other.CollisionBox);
        }

        //This is empty, because the individual objects have their own way of reacting to collision.
        public virtual void OnCollision(GameObject other)
        {

        }

    }
}
