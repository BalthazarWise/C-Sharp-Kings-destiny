using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMenu
{
    class AdventureMapGenerator
    {
        const int mapCoordsX = 38;
        const int mapCoordsY = 18;
        static char[,] mapTiels;
        static bool[,] mapTielsReserved;
        int playerTurn = 0;
        static Character cPlayer;

        public AdventureMapGenerator()
        {
            mapTiels = new char[mapCoordsX, mapCoordsY];
            mapTielsReserved = new bool[mapCoordsX, mapCoordsY];
            cPlayer = Character.LoadCharacter();

            AdventureMapGeneratorTerra();
            AdventureMapNextLocationGenerator();
            AdventureMapObstaclesGenerator();            
        }
        public char[,] GetMapTiels()
        {
            return mapTiels;
        }
        public bool[,] GetTielsReserved()
        {
            return mapTielsReserved;
        }
        public static void AdventureMapGeneratorTerra()
        {
            // Заполнить проходимой територией
            for (int y = 0; y < mapCoordsY; y++)
            {
                for (int x = 0; x < mapCoordsX; x++)
                {
                    mapTiels[x, y] = '.';
                    mapTielsReserved[x, y] = false;
                }
            }
            // Создать квадрат непроходимой територии
            for (int x = 0; x < mapCoordsX; x++)
            {
                mapTiels[x, 0] = '#';
                mapTielsReserved[x, 0] = true;
            }
            for (int x = 0; x < mapCoordsX; x++)
            {
                mapTiels[x, mapCoordsY - 1] = '#';
                mapTielsReserved[x, mapCoordsY - 1] = true;
            }
            for (int y = 0; y < mapCoordsY; y++)
            {
                mapTiels[0, y] = '#';
                mapTielsReserved[0, y] = true;
            }
            for (int y = 0; y < mapCoordsY; y++)
            {
                mapTiels[mapCoordsX-1, y] = '#';
                mapTielsReserved[mapCoordsX - 1, y] = true;
            }
        }
        public static void AdventureMapNextLocationGenerator()
        {
            mapTiels[36, 9] = 'L';
            mapTielsReserved[36, 9] = true;
            mapTiels[37, 9] = 'L';
            mapTielsReserved[37, 9] = true;
        }
        private static void AdventureMapObstaclesGenerator()
        {
            int obstaclesCount = 0;
            for (int y = 0; y < mapCoordsY; y++)
            {
                for (int x = 0; x < mapCoordsX; x++)
                {
                    if (!mapTielsReserved[x, y])
                    {
                        obstaclesCount++;
                    }
                }
            }
            Random rnd = new Random();
            obstaclesCount = obstaclesCount / (rnd.Next(4) + 12);
            for (int i = 0; i < obstaclesCount; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(mapCoordsX-2) + 1;
                    y = rnd.Next(mapCoordsY-2) + 1;
                } while (mapTielsReserved[x, y]);
                mapTiels[x, y] = '&';
                mapTielsReserved[x, y] = true;
            }
        }
    }
}
