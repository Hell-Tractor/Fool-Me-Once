using System;
using System.Collections.Generic;
using UnityEngine;

public class Utility {
    public static int[] GetRandomArray(int minInclusive, int maxInclusive, int total) {
        total = Math.Min(total, maxInclusive - minInclusive + 1);
        List<int> buffer = new List<int>();
        for (int i = minInclusive; i <= maxInclusive; ++i)
            buffer.Add(i);
        int[] result = new int[total];
        for (int i = 0; i < total; ++i) {
            result[i] = buffer[UnityEngine.Random.Range(0, buffer.Count)];
            buffer.Remove(result[i]);
        }
        return result;
    }
}