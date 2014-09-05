using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace AnimatedSpriteAssignment
{
    class Stickman
    {
        Texture2D spritesheet;  // The *entire* spritesheet
        Rectangle srcRect;      // Where in the *spritesheet* we're drawing
        Rectangle destRect;     // Where on the *screen* we're drawing
        Vector2 position;       // Position of the player
        Vector2 origin;         // We're not using this, but have to have it for drawing
        AnimatedSpriteGame parent;            // Game1
        int state;              // The state that the player is in IDLE/WALKING/JUMPING/RUNNING...
        int facing;             // Either facing LEFT or RIGHT.
        const int IDLE = 0;
        const int WALKING = 1;
        const int JUMPING = 2;
        const int RUNNING = 3;
        const int LEFT = -1;
        const int RIGHT = 1;
        const float WALK_SPEED = 0.8f;
        const float RUN_SPEED = 4.0f;

        int frameCounter;       // Which frame of the animation we're in (a value between 0 and 23)
        float frameRate;        // This should always be 1/24 (or 0.04167 seconds)
        float timeCounter;      // How much time has elapsed since the last time we incremented the frame counter

        public Stickman(Texture2D texture, Vector2 position, AnimatedSpriteGame game)
        {
            this.spritesheet = texture;
            this.position = position;
            this.parent = game;
            state = IDLE;
            frameCounter = 0;
            frameRate = 1.0f / 24.0f;            
            
            origin = new Vector2(0, 0);  
            srcRect = new Rectangle(0, 0, spritesheet.Width / 24, spritesheet.Height / 4);
            destRect = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public void Update(GameTime gameTime)
        {
            // Get what keys are pressed
            bool shiftIsPressed = Keyboard.GetState().IsKeyDown(Keys.LeftShift);
            bool aIsPressed = Keyboard.GetState().IsKeyDown(Keys.A);
            bool dIsPressed = Keyboard.GetState().IsKeyDown(Keys.D);
            bool spaceIsPressed = Keyboard.GetState().IsKeyDown(Keys.Space);

            // Update Time Counter
            timeCounter += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            // Update Time and Frame Counters
            if (timeCounter > frameRate)
            {
                timeCounter -= frameRate;
                frameCounter++;
                if (frameCounter >= 24)
                {
                    frameCounter -= 24;
                }
            }

            // Update Facing and State dependent upon keys pressed
            if (aIsPressed)
            {
                facing = LEFT;
                if (shiftIsPressed)
                {
                    state = RUNNING;
                }
                else
                {
                    state = WALKING;
                }
            }
            else if (dIsPressed)
            {
                facing = RIGHT;
                if (shiftIsPressed)
                {
                    state = RUNNING;
                }
                else
                {
                    state = WALKING;
                }
            }
            else
            {
                // Completely arbitrary default facing for when we load
                if (facing == 0)
                {
                    facing = LEFT;
                }
                state = IDLE;
            }

            // Update Source Rectangle 
            srcRect.X = frameCounter * srcRect.Width;
            srcRect.Y = state * srcRect.Height;

            // Update Position dependent upon State
            switch (state)
            {
                case IDLE:
                    position.X += 0 * facing;
                    break;
                case WALKING:
                    position.X += WALK_SPEED * facing;
                    break;
                case JUMPING:
                    break;
                case RUNNING:
                    position.X += RUN_SPEED * facing;
                    break;
            }

            // Update the destination rectangle based on our position.
            destRect.X = (int)position.X;
            destRect.Y = (int)position.Y;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(parent.spritefont, "Time Counter: " + timeCounter + "\nFrame: " + frameCounter, parent.textPosition, Color.White);
            // Using Draw method 5 of 7
            if (facing == LEFT)
                sb.Draw(spritesheet, destRect, srcRect, Color.White, 0.0f, origin, SpriteEffects.FlipHorizontally, 1.0f);
            else
                sb.Draw(spritesheet, destRect, srcRect, Color.White, 0.0f, origin, SpriteEffects.None, 1.0f);
        }
    }
}
