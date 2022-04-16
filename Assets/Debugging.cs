using UnityEngine;
using TMPro;

public class Debugging : MonoBehaviour
{

    [SerializeField] ComputeShader CS;
    ComputeBuffer CBUnrolled, CBNotUnrolled;
    readonly int N = 4;

    [SerializeField] TextMeshProUGUI myDisplay, yourDisplay;

    private void Start()
    {

        CBUnrolled = new ComputeBuffer(N, sizeof(float));
        CBNotUnrolled = new ComputeBuffer(N, sizeof(float));

        CS.SetBuffer(0, "_CBUnrolled", CBUnrolled);
        CS.SetBuffer(0, "_CBNotUnrolled", CBNotUnrolled);

        CS.Dispatch(0, (int)((N + (64 - 1)) / 64), 1, 1);

        float[] yourAnsUnrolled = new float[N];
        float[] yourAnsNotUnrolled = new float[N];

        CBUnrolled.GetData(yourAnsUnrolled);
        CBNotUnrolled.GetData(yourAnsNotUnrolled);

        yourDisplay.text = "Not unrolled loop\n";

        for (int i = 0; i < N; i++)
        {
            Debug.Log("Not Unrolled ans = " + yourAnsNotUnrolled[i] + "   ---   " + "Unrolled ans = " + yourAnsUnrolled[i]);
            yourDisplay.text += i.ToString() + ": " + yourAnsNotUnrolled[i].ToString() + "\n";
        }

        yourDisplay.text += "\nUnrolled loop\n";
        for (int i = 0; i < N; i++)
        {
            yourDisplay.text += i.ToString() + ": " + yourAnsUnrolled[i].ToString() + "\n";
        }

        CBUnrolled.Release();
        CBNotUnrolled.Release();


        // These are the result I (the author) get when running the above script:
        float[] myAnsNotUnrolled = new float[]{ -0.1610027f, -0.1510025f, 0.1998477f, 0.1984635f };
        float[] myAnsUnrolled = new float[]{ -0.1609996f, -0.1510009f, 0.1998638f, 0.1984647f };

        myDisplay.text = "Not unrolled loop\n";
        for (int i = 0; i < N; i++)
        {
            myDisplay.text += i.ToString() + ": " + myAnsNotUnrolled[i].ToString() + "\n";
        }
        myDisplay.text += "\nUnrolled loop\n";
        for (int i = 0; i < N; i++)
        {
            myDisplay.text += i.ToString() + ": " + myAnsUnrolled[i].ToString() + "\n";
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
