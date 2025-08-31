using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class InputManager
    {
        private KeyboardState _previous;
        private KeyboardState _current;

        // Call once per frame to advance internal state
        public void Update()
        {
            _previous = _current;
            _current = Keyboard.GetState();
        }

        // Safe to call multiple times per frame after Update()
        public bool IsKeyPressedOnce(Keys key)
        {
            return _current.IsKeyDown(key) && !_previous.IsKeyDown(key);
        }
    }
}
