using System;
using System.IO;

namespace Level
{
    public class RoomStats
    {
        public int size;
        public bool b_up, b_down, b_left, b_right, up, down, left, right;
        public float x, y, z;

        public static byte[] Serialize(RoomStats roomStats)
        { 
            MemoryStream ms = new MemoryStream(24);
            
            ms.Write(BitConverter.GetBytes(roomStats.size), 0, 4);
            ms.Write(BitConverter.GetBytes(roomStats.b_up), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.b_down), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.b_left), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.b_right), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.up), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.down), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.left), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.right), 0, 1);
            ms.Write(BitConverter.GetBytes(roomStats.x), 0, 4);
            ms.Write(BitConverter.GetBytes(roomStats.y), 0, 4);
            ms.Write(BitConverter.GetBytes(roomStats.z), 0, 4);

            return ms.ToArray();
        }

        public static RoomStats Deserialize(byte[] bytes)
        {
            RoomStats roomStats = new RoomStats
            {
                size = BitConverter.ToInt32(bytes, 0),
                b_up = BitConverter.ToBoolean(bytes, 4),
                b_down = BitConverter.ToBoolean(bytes, 5),
                b_left = BitConverter.ToBoolean(bytes, 6),
                b_right = BitConverter.ToBoolean(bytes, 7),
                up = BitConverter.ToBoolean(bytes, 8),
                down = BitConverter.ToBoolean(bytes, 9),
                left = BitConverter.ToBoolean(bytes, 10),
                right = BitConverter.ToBoolean(bytes, 11),
                x = BitConverter.ToSingle(bytes, 12),
                y = BitConverter.ToSingle(bytes, 16),
                z = BitConverter.ToSingle(bytes, 20)
            };
            
            return roomStats;
        }
    }
}