namespace SpaceCombat.Gameplay
{
    public struct ScreenSize
    {
        public ScreenSize(float halfWidth, float halfHeight)
        {
            HalfWidth = halfWidth;
            HalfHeight = halfHeight;
        }

        public float HalfWidth;
        public float HalfHeight;
    }
}