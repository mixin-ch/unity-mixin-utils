using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchmarkTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Benchmarker.Instance.Benchmarks.Add(MyBenchmarkFunction);
        Benchmarker.Instance.RunBenchmark();
    }

    private void MyBenchmarkFunction()
    {
        int i = 10;
        i /= 10;
    }
}
