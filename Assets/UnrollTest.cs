using UnityEngine;
using TMPro;

public class UnrollTest : MonoBehaviour
{

    [SerializeField] ComputeShader CS;
    ComputeBuffer CBUnrolled, CBNotUnrolled;
    readonly int N = 4;

    [SerializeField] TextMeshProUGUI myDisplay, yourDisplay;

    private void Start()
    {

        CBUnrolled = new ComputeBuffer(N, sizeof(double));
        CBNotUnrolled = new ComputeBuffer(N, sizeof(double));

        CS.SetBuffer(0, "_CBUnrolled", CBUnrolled);
        CS.SetBuffer(0, "_CBNotUnrolled", CBNotUnrolled);

        CS.Dispatch(0, (int)((N + (64 - 1)) / 64), 1, 1);

        double[] yourAnsUnrolled = new double[N];
        double[] yourAnsNotUnrolled = new double[N];

        CBUnrolled.GetData(yourAnsUnrolled);
        CBNotUnrolled.GetData(yourAnsNotUnrolled);

        yourDisplay.text = "Not unrolled loop\n";

        for (int i = 0; i < N; i++)
        {
            Debug.Log("Not Unrolled ans = " + yourAnsNotUnrolled[i] + "   ---   " + "Unrolled ans = " + yourAnsUnrolled[i]);
            yourDisplay.text += yourAnsNotUnrolled[i].ToString() + "\n";
        }

        yourDisplay.text += "\nUnrolled loop\n";
        for (int i = 0; i < N; i++)
        {
            yourDisplay.text += yourAnsUnrolled[i].ToString() + "\n";
        }

        CBUnrolled.Release();
        CBNotUnrolled.Release();


        // These are the result I (the author) gets when running the above script:
        double[] myAnsNotUnrolled = new double[]{ -0.160999998450279, -0.150999998673797, 0.199847648978894, 0.198463366517202 };
        double[] myAnsUnrolled = new double[]{ 0.197165968601361, 0.195968076362597, 0.194871433045889, 0.193876681728184 };

        myDisplay.text = "Not unrolled loop\n";
        for (int i = 0; i < N; i++)
        {
            myDisplay.text += myAnsNotUnrolled[i].ToString() + "\n";
        }
        myDisplay.text += "\nUnrolled loop\n";
        for (int i = 0; i < N; i++)
        {
            myDisplay.text += myAnsUnrolled[i].ToString() + "\n";
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.Quit();
        }
    }
}
