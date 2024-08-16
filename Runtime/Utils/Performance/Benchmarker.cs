using Mixin.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benchmarker : Singleton<Benchmarker>
{
    public delegate void BenchmarkFunction();

    public List<BenchmarkFunction> Benchmarks = new List<BenchmarkFunction>();
    public int Iterations = 1000;

    public void RunBenchmark()
    {
        if (Benchmarks.Count == 0)
        {
            Debug.LogError("No benchmark functions added.");
            return;
        }

        foreach (BenchmarkFunction benchmark in Benchmarks)
        {
            float totalTime = 0f;
            for (int i = 0; i < Iterations; i++)
            {
                float startTime = Time.realtimeSinceStartup;
                benchmark();
                totalTime += Time.realtimeSinceStartup - startTime;
            }

            float averageTime = totalTime / Iterations;
            float averageTimeMs = averageTime * 1000f;
            $"Average execution time for {benchmark.Method.Name}: {averageTimeMs} ms".Log(Color.cyan);
        }
    }
}
