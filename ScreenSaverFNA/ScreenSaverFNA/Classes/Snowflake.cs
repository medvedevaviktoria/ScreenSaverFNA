namespace ScreenSaverFNA.Classes
{
    public class Snowflake(int x, int y, int size, int speed)
    {
        /// <summary>
        /// Местоположение снежинки по X
        /// </summary>
        public int X = x;

        /// <summary>
        /// Местоположение снежинки по Y
        /// </summary>
        public int Y = y;

        /// <summary>
        /// Размер снежинки
        /// </summary>
        public int Size = size;

        /// <summary>
        /// Скорость движения снежинки
        /// </summary>
        public int Speed = speed;
    }
}
