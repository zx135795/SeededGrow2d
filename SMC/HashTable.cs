﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SMC
{
    public class HashTable_Double2dArray<T> 
    {

        struct IndexAndValue<T1>
        {
            public int Index;
            public T1 Value;
            public IndexAndValue(int index, T1 value)
            {
                Index = index;
                Value = value;
            }
        }
        List<IndexAndValue<T>>[,] mapHashXY;
        List<IndexAndValue<T>>[,] mapHashXZ;
        int width;
        int height;
        int depth;
        int Count;
        public HashTable_Double2dArray(int width, int height, int depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
            mapHashXY = new List<IndexAndValue<T>>[this.width, this.height];
            mapHashXZ = new List<IndexAndValue<T>>[this.width, this.depth];
        }
        static int FindK(List<IndexAndValue<T>> list, int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Index == index)
                    return i;
            }
            return -1;
        }
        public void SetHashValue(int x, int y, int z, T value)
        {
            if (mapHashXY[x, y] == null)
            {
                mapHashXY[x, y] = new List<IndexAndValue<T>>();
                mapHashXY[x, y].Add(new IndexAndValue<T>(z, value));
                Count++;
            }
            else
            {
                if (mapHashXZ[x, z] == null)
                {
                    mapHashXZ[x, z] = new List<IndexAndValue<T>>();
                    mapHashXZ[x, z].Add(new IndexAndValue<T>(y, value));
                    Count++;
                }
                else
                {
                    if (mapHashXY[x, y].Count > mapHashXZ[x, z].Count)
                    {
                        mapHashXZ[x, z].Add(new IndexAndValue<T>(y, value));
                        Count++;
                    }
                    else
                    {
                        mapHashXY[x, y].Add(new IndexAndValue<T>(z, value));
                        Count++;
                    }
                }
            }
        }
        public bool GetHashValue(int x, int y, int z, ref T value)
        {
            if (mapHashXY[x, y] != null)
            {
                int index = FindK(mapHashXY[x, y], z);
                if (index == -1)
                {
                    if (mapHashXZ[x, z] != null)
                    {
                        int index2 = FindK(mapHashXZ[x, z], y);
                        if (index2 == -1)
                            return false;
                        else
                        {
                            value = mapHashXZ[x, z][index2].Value;
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    value = mapHashXY[x, y][index].Value;
                    return true;
                }
            }
            else
                return false;
        }
        public void SetDefaultValue(T value)
        {
            return;
        }


        public void Clear()
        {
            return;
        }
    }
}
